using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SatSolver.Objects
{
    public enum ActiveCnf
    {
        Main,Zero,One
    }

    /// <summary>
    /// An implementation of Davis-Putnam algorithm
    /// </summary>
    public class DavisPutnam
    {
        /* Public Methods */

        /// <summary>
        /// By subscribing to this event, the class which initializes DavisPutnam class can 
        /// listen to the events it generates during solving steps
        /// </summary>
        public event EventHandler<DavisPutnamEventArgs> Report;

        /* Private Methods */

        private Thread _thread; //thread to solve in it
        private bool _continue; //should the execution continue?
        private CNF _cnfMain;
        private CNF _cnfOne; //to backup original CNF when setting a variable to 0
        private CNF _cnfZero; //to backup original CNF when setting a variable to 1
        private CNF _bkpCnf;
        private List<int> _uid; //to hold unique value of nets
        
        private List<int> _unitClauseList = new List<int>();
        private int _varUnitClause = int.MinValue; //The Unit Clause variable
        private int _currentVar = int.MinValue; //to hold current variable which we are setting to 0 or 1

        //Dictionaries to hold assignements to nets
        private List<KeyValuePair<int, bool>> _valsDicMain = new List<KeyValuePair<int, bool>>();
        private List<KeyValuePair<int, bool>> _valsDicZero = new List<KeyValuePair<int, bool>>();
        private List<KeyValuePair<int, bool>> _valsDicOne = new List<KeyValuePair<int, bool>>();

        private ActiveCnf _stateCnf; 

        public DavisPutnam(CNF cnfMain)
        {
            _cnfMain = cnfMain;
            _cnfOne = cnfMain.Clone();
            _cnfZero = _cnfMain.Clone();

            GenerateCurrentExistingVariables();
        }

        private void OnReport(string msg, int lvl, DpType type)
        {
            if (Report != null)
            {
                Report(this, new DavisPutnamEventArgs(msg, lvl, _cnfMain.Clone(), type, _valsDicMain));
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


        private void SetActiveCnf(ActiveCnf a)
        {
            _stateCnf = a;
            _bkpCnf = _cnfMain.Clone(); //backup main cnf!

            switch (a)
            {
                case ActiveCnf.Main:
                    break;

                case ActiveCnf.One:
                    break;

                case ActiveCnf.Zero:
                    break;
            }
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
                        Debug.WriteLine("IN LOOP " + loopCount);

                        //do unit clause rule
                        PerformUnitClauseRule(false);

                        //do pure literal rule
                        PerformPureLiteralRule(false);

                        //Check if solution found
                        if (CheckIfSolutionIsFound())
                        {
                        }

                        //PerformingBacktrack
                        PerformBackTracking();


                        //Countermeasure to forever going loop! lol!
                        if (loopCount > 4096) //go to backtracking mode!
                        {
                            OnReport("Loop Count exceeded "+ loopCount +". Can not find any solution!", 1, DpType.Stopped);
                            _thread = null;
                            _continue = false;
                            _thread = null;
                            loopCount = 0;
                        }

                        loopCount++;

                        Thread.Sleep(20);
                    }
                });
                _thread.Start();
            }
        }

        private void StartDP(List<List<int>> cnf)
        {
            
            bool plr, ucr = false;

            while(plr, ucr)

        }

        private bool CheckIfSolutionIsFound()
        {
            var empty = true;
            foreach (var list in _cnfMain.Data)
            {
                foreach (var i in list)
                {
                    return false;
                }
            }

            if (empty) //CNF is empty
            {
                OnReport("Solution is found!", 1, DpType.SolutionFound);
                Stop();
            }

            return empty;
        }

        private void PerformBackTracking()
        {
            _currentVar = GetRightMostInCnf();
            OnReport("Performing Backtracking with variable " + _currentVar, 1, DpType.PerformingBacktrack);

            //prepare to divide cnfMain in 2 branches
            var cnfBackup = _cnfMain.Clone();
            _cnfOne = _cnfZero = new CNF();
            _cnfOne = _cnfMain.Clone();
            _cnfZero = _cnfMain.Clone();

            //Do simplification with current value set to 1
            OnReport("Setting "+ _currentVar + " to 1", 2, DpType.PerformingBacktrack);
            _cnfMain = _cnfOne;
            SimplifyOne(_currentVar);

            OnReport("Setting " + _currentVar + " to 0", 2, DpType.PerformingBacktrack);
            _cnfMain = _cnfZero;
            SimplifyZero(_currentVar);

            //see wich simplification resulted in shorter CNF
            var originalCount = cnfBackup.Data.SelectMany(list => list).Count();
            var countOne = _cnfOne.Data.SelectMany(list => list).Count();
            var countZero = _cnfOne.Data.SelectMany(list => list).Count();


            if (countOne < originalCount)
            {
                _cnfMain = _cnfOne.Clone();
                _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(_currentVar), true));
                OnReport(_currentVar + " with value 1 resulted in smaller CNF", 2, DpType.PerformingBacktrack);
            }
            else if (countZero < originalCount)
            {
                _cnfMain = _cnfZero.Clone();
                _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(_currentVar), false));
                OnReport(_currentVar + " with value 0 resulted in smaller CNF", 2, DpType.PerformingBacktrack);
            }
            else
            {
                //this must never happen!!!! lol
                OnReport(_currentVar + " with both 0 and 1 resulted in same CNF - NO SOLUTION!", 2,
                    DpType.PerformingBacktrack);
            }


            //reset backups to free memory
            _cnfOne = _cnfZero = new CNF();
        }

        private bool PerformUnitClauseRule(bool checkOnly = false)
        {
            OnReport("Performing Unit Clause Rule", 1, DpType.PerformingUnitClause);
            _unitClauseList = new List<int>();
            //check all items in cnfMain and see if it has any list
            //which has only one item inside it, then it should be unit clause!
            for (int i = 0; i < _cnfMain.Data.Count; i++)
            {
                if (_cnfMain.Data[i].Count == 1)
                {
                    _unitClauseList.Add(_cnfMain.Data[i][0]);
                    OnReport("Found Unit Clause " + _cnfMain.Data[i].DumpList(),
                        2, DpType.FoundUnitClause);
                }
            }

            if (_unitClauseList.Count == 0)
            {
                OnReport("Can not find any Unit Clauses!",
                    2, DpType.PerformingUnitClause);
                return false;
            }
            else
            {
                if (checkOnly)
                    return true;
            }

            _unitClauseList = _unitClauseList.OrderByDescending(i => i).ToList();

            for (int i = 0; i < _unitClauseList.Count; i++)
            {
                if (_unitClauseList[i] > 0)
                {
                    _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(_unitClauseList[i]), true));
                    SimplifyOne(_unitClauseList[i]);
                }
                else
                {
                    _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(_unitClauseList[i]), false));
                    SimplifyZero(_unitClauseList[i]);
                }

                _unitClauseList.Remove(_unitClauseList[i]);

                //defensive !
                if (i > _unitClauseList.Count)
                {
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// Assigns the value 0 to the given variable and simplify based on it
        /// </summary>
        /// <param name="num">Variable to do simplification based on it</param>
        private void SimplifyZero(int num)
        {
            while (VariableExist(num, true))
            {
                for (int i = 0; i < _cnfMain.Data.Count; i++)
                {
                    if (_cnfMain.Data[i].Contains(-num))
                    {
                        var toRemove = _cnfMain.Data[i].DumpList();
                        _cnfMain.Data.Remove(_cnfMain.Data[i]); //remove total clause
                        OnReport(toRemove + " Contains" + (-num) + ". Removing the entire clause!",
                            3, DpType.RemovingUintClause);
                    }
                }

                for (int i = 0; i < _cnfMain.Data.Count; i++)
                {
                    if (_cnfMain.Data[i].Contains(num))
                    {
                        var toRemove = _cnfMain.Data[i].DumpList();
                        _cnfMain.Data[i].Remove(num); //remove var from clause
                        OnReport(toRemove + "  Contains " + num + ". Removing " + (-num)+ " from this clause",
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
                for (int i = 0; i < _cnfMain.Data.Count; i++)
                {
                    if (_cnfMain.Data[i].Contains(num))
                    {
                        var toRemove = _cnfMain.Data[i].DumpList();
                        _cnfMain.Data.Remove(_cnfMain.Data[i]); //remove total clause
                        OnReport(toRemove + " Contains "+ num + ". Removing the entire clause!",
                            3, DpType.RemovingUintClause);
                    }
                }

                for (int i = 0; i < _cnfMain.Data.Count; i++)
                {
                    if (_cnfMain.Data[i].Contains(-num))
                    {
                        var toRemove = _cnfMain.Data[i].DumpList();
                        _cnfMain.Data[i].Remove(-num); //remove var from clause
                        OnReport(toRemove + " Contains " + (-num) + ". Removing " + (-num) + " from this clause",
                            3, DpType.RemovingUintClause);
                    }
                }
            }
        }

        /// <summary>
        /// Performs pure literal rule
        /// </summary>
        private bool PerformPureLiteralRule(bool checkOnly)
        {
            OnReport("Performing Pure Literal Rule", 1, DpType.PerformingPureLiteralRule);

            var onlyPositives = new List<int>();
            var onlyNegateds = new List<int>();

            //first find current existing variables
            GenerateCurrentExistingVariables();

            if (checkOnly)
            {
                return (onlyPositives.Count > 0 || onlyNegateds.Count > 0);
            }

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
                    onlyNegateds.Add(_uid[i]);
                }
            }

            if (onlyPositives.Count == 0 && onlyNegateds.Count == 0)
            {
                OnReport("Can not find any variable which exist only in positive or negated form!"
                    , 2, DpType.PerformingPureLiteralRule);
                return false;
            }

            if (onlyPositives.Count > 0)
            {
                foreach (var p in onlyPositives)
                {
                    _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(p), true));
                    SimplifyOne(p);
                }
            }

            if (onlyNegateds.Count > 0)
            {
                foreach (var n in onlyNegateds)
                {
                    _valsDicMain.Add(new KeyValuePair<int, bool>(Math.Abs(n), false));
                    SimplifyZero(n);
                }
            }

            return true;
        }

        private bool ExistOnlyInNegatedForm(int id)
        {
            var toCheck = -id;
            for (var i = 0; i < _cnfMain.Data.Count; i++)
            {
                for (var j = 0; j < _cnfMain.Data[i].Count; j++)
                {
                    if (_cnfMain.Data[i][j] == -toCheck)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ExistOnlyInPositiveForm(int id)
        {
            for (var i = 0; i < _cnfMain.Data.Count; i++)
            {
                for (var j = 0; j < _cnfMain.Data[i].Count; j++)
                {
                    if (_cnfMain.Data[i][j] == id)
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
            foreach (var list in _cnfMain.Data)
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
            foreach (var list in _cnfMain.Data)
            {
                foreach (var i in list)
                {
                    max = Math.Max(max, i);
                }
            }
            return max;
        }


        private void GenerateCurrentExistingVariables()
        {
            _uid = new List<int>();
            var all = _cnfMain.Data.SelectMany(list => list).ToList();
            _uid = all.Distinct().ToList(); //make list unique
            _uid.Sort(); //sort numbers
            _uid.Reverse(); //reverse so highest numbers would evalaute first!
            _uid = _uid.Where(i => i >= 0).ToList(); //remove negative numbers
        }
    }
}