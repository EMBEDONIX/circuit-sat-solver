﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SatSolver.Objects
{
    
    /// <summary>
    /// An implementation of Davis-Putnam algorithm
    /// </summary>
    public class DavisPutnam
    {

        public Object ThreadLocker = new object();
        public event EventHandler<DavisPutnamEventArgs> Report;

        private Thread _thread; //thread to solve in it
        private bool _continue; //should the execution continue?
        private CNF _cnf;
        private List<int> _uid; //to hold unique value of nets


        private List<int> _unitClauseList = new List<int>();
        private int _varUnitClause = int.MinValue; //The Unit Clause variable
        private int _currentVar = int.MinValue; //to hold current variable which we are setting to 0 or 1

        private CNF _backupCnfA; //to backup original CNF when setting a variable to 0
        private CNF _backupCnfB; //to backup original CNF when setting a variable to 1

        public DavisPutnam(CNF cnf)
        {
            _cnf = cnf;

            GenerateCurrentExistingVariables();
        }

        private void OnReport(string msg, int lvl, DpType type)
        {
            if (Report != null)
            {
                    
                    Report(this, new DavisPutnamEventArgs(msg, lvl, _cnf.Clone(), type));
                    Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Stops the current running algorithm
        /// </summary>
        public void Stop()
        {
            try
            {
                _continue = false;
                _thread.Abort();
            }
            catch
            {
                // ignored
            }
            finally
            {
                _thread = null;
            }
            OnReport("Stopping...", 1, DpType.Stopped);
        }

        /// <summary>
        /// See if algorithm is active and is busy solving
        /// </summary>
        /// <returns></returns>
        public bool IsSolving()
        {
            return (_thread != null && _continue);
        }


        /// <summary>
        /// Start the algorithm
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            OnReport("Starting Davis-Putnam Algorithm with initial CNF:", 1, DpType.Starting);
            _continue = true;

            if (_thread == null)
            {
                _thread = new Thread(() =>
                {
                    var loopCount = 0;

                    //MAIN LOOP
                    while (_continue)
                    {

                        //do unit clause rule
                        PerformUnitClauseRule();

                        //do pure literal rule
                        PerformPureLiteralRule();

                        //Countermeasure to forever going loop! lol!
                        loopCount++;
                        if (loopCount > 6) //go to backtracking mode!
                        {
                            OnReport("Can not find any solution!", 1, DpType.Stopped);
                            _thread = null;
                            _continue = false;
                            _thread = null;
                            loopCount = 0;
                        }

                        Thread.Sleep(50);
                    }
                    
                });
                _thread.Start();
            }
        }

        private void PerformUnitClauseRule()
        {
            OnReport("Performing Unit Clause Rule", 1, DpType.PerformingUnitClause);

            //check all items in cnf and see if it has any list
            //which has only one item inside it, then it should be unit clause!
            for (int i = 0; i < _cnf.Data.Count; i++)
            {
                if (_cnf.Data[i].Count == 1)
                {
                    _unitClauseList.Add(_cnf.Data[i][0]);
                    OnReport($"Found Unit Clause {_cnf.Data[i].DumpList()}", 
                        2, DpType.FoundUnitClause);
                }
            }

            for (int i = 0; i < _unitClauseList.Count; i++)
            {
                if (_unitClauseList[i] > 0)
                {
                    SimplifyOne(_unitClauseList[i]);
                }
                else
                {
                    SimplifyZero(_unitClauseList[i]);
                }

                _unitClauseList.Remove(_unitClauseList[i]);

                //defensive !
                if (i >= _unitClauseList.Count)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Assigns the value 0 to the given variable and simplify based on it
        /// </summary>
        /// <param name="num">Variable to do simplification based on it</param>
        private void SimplifyZero(int num)
        {
            while (VariableExist(num, true))
            {
                for (int i = 0; i < _cnf.Data.Count; i++)
                {

                    if (_cnf.Data[i].Contains(-num))
                    {
                        var toRemove = _cnf.Data[i].DumpList();
                        _cnf.Data.Remove(_cnf.Data[i]); //remove total clause
                        OnReport($"{toRemove} Contains {-num}. Removing the entire clause!",
                            3, DpType.RemovingUintClause);
                    }
                }

                for (int i = 0; i < _cnf.Data.Count; i++)
                {
                    if (_cnf.Data[i].Contains(num))
                    {
                        var toRemove = _cnf.Data[i].DumpList();
                        _cnf.Data[i].Remove(num); //remove var from clause
                        OnReport($"{toRemove} Contains {num}. Removing {-num} from this clause",
                            3, DpType.RemovingUintClause);
                    }
                }
            }
        }

        /// <summary>
        /// Assigns the value 1 to the given variable and simplify based on it
        /// </summary>
        /// <param name="num">Variable to do simplification based on it</param>
        private void SimplifyOne(int num)
        {
            while (VariableExist(num, true))
            {
                for (int i = 0; i < _cnf.Data.Count; i++)
                {

                    if (_cnf.Data[i].Contains(num))
                    {
                        var toRemove = _cnf.Data[i].DumpList();
                        _cnf.Data.Remove(_cnf.Data[i]); //remove total clause
                        OnReport($"{toRemove} Contains {num}. Removing the entire clause!",
                            3, DpType.RemovingUintClause);
                    }
                }

                for (int i = 0; i < _cnf.Data.Count; i++)
                {
                    if (_cnf.Data[i].Contains(-num))
                    {
                        var toRemove = _cnf.Data[i].DumpList();
                        _cnf.Data[i].Remove(-num); //remove var from clause
                        OnReport($"{toRemove} Contains {-num}. Removing {-num} from this clause",
                            3, DpType.RemovingUintClause);
                    }
                }
            }
        }

        /// <summary>
        /// Performs pure literal rule
        /// </summary>
        private void PerformPureLiteralRule()
        {
            var onlyPositives = new List<int>();
            var onlyNegated = new List<int>();

            //first find current existing variables
            GenerateCurrentExistingVariables();

            //search if any variable exists only as positive
            for (int i = 0; i < _uid.Count; i++)
            {
                if (ExistOnlyInPositiveForm(_uid[i]))
                {
                    onlyPositives.Add(_uid[i]);
                    continue;
                }

                if (ExistOnlyInNegatedForm(_uid[i]))
                {
                    onlyNegated.Add(_uid[i]);
                }

                if (onlyPositives.Count > 0)
                {
                    
                }

                if (onlyNegated.Count > 0)
                {
                    
                }
            }
        }

        private bool ExistOnlyInNegatedForm(int id)
        {
            var toCheck = -id;
            for (var i = 0; i < _cnf.Data.Count; i++)
            {
                for (var j = 0; j < _cnf.Data[i].Count; j++)
                {
                    if (_cnf.Data[i][j] == -toCheck)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ExistOnlyInPositiveForm(int id)
        {
            for (var i = 0; i < _cnf.Data.Count; i++)
            {
                for (var j = 0; j < _cnf.Data[i].Count; j++)
                {
                    if (_cnf.Data[i][j] == id)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Check to see if a variable exis in CNF
        /// </summary>
        /// <param name="num">variable to check</param>
        /// <param name="negativeAndPositive">
        /// if true, check for both proitive and negative form, 
        /// otherwide only positive</param>
        /// <returns></returns>
        private bool VariableExist(int num, bool negativeAndPositive)
        {
            foreach (var list in _cnf.Data)
            {
                foreach (var i in list)
                {
                    if (negativeAndPositive)
                    {
                        if (i == num || i == (-num))
                            return true;
                    }
                    else
                    {
                        if (i == num)
                            return true;
                    }
                }
            }

            return false;
        }


        private int GetRightMostInCnf()
        {
            var max = int.MinValue;
            foreach (var list in _cnf.Data)
            {
                foreach (var i in list)
                {
                    max = Math.Max(max, i);
                }
            }
            OnReport($"Right most variable is {max}", 2, DpType.FindingRightMostVariable);
            return max;
        }


        private void GenerateCurrentExistingVariables()
        {
            _uid = new List<int>();
            var all = _cnf.Data.SelectMany(list => list).ToList();
            _uid = all.Distinct().ToList(); //make list unique
            _uid.Sort(); //sort numbers
            _uid.Reverse(); //reverse so highest numbers would evalaute first!
            _uid = _uid.Where(i => i >= 0).ToList(); //remove negative numbers
        }



    }
}