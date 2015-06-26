using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public abstract class Gate
    {
        protected GateType _type;
        protected List<Net> _inNets;
        protected Net _outNet;
        protected CNF _cnf;
        protected int _lastCnfOffset;
        private bool _isFinalMiterOutput;

        public bool IsFinalMiterOutput
        {
            get { return _isFinalMiterOutput; }
            set { _isFinalMiterOutput = value; }
        }


        /// <summary>
        /// Protected constructor
        /// </summary>
        /// <param name="type">Type of the gate from <see cref="GateType"/></param>
        protected Gate(GateType type)
        {
            _type = type;
            _inNets = new List<Net>();
        }

        /// <summary>
        /// Add a <see cref="Net"/> to list of input nets for this gate
        /// </summary>
        /// <param name="net"><see cref="Net"/> to be added</param>
        public void AddInputNet(Net net)
        {
            //First check if this gate is single input and wheter or not it alreday as an input net
            //assigned to it
            if (IsSingleInput() && _inNets.Count > 1)
            {
                throw new Exception("Error: This gate is '" + Helpers.GetEnumDescription(_type) + "' and it has already" +
                                    " have been assigned with an input net. Can not add another net to this type" +
                                    " of gate");
            }

            //Secomnd check if this gate is 2 input and inputs are already set!
            if (_inNets.Count == 2)
            {
                throw new Exception("Error: This gate is '" + Helpers.GetEnumDescription(_type) + "' and it has already" +
                                    " have been assigned with an input net. Can not add another net to this type" +
                                    " of gate");
            }


            _inNets.Add(net);
        }

        /// <summary>
        /// Set the output <see cref="Net"/> of this gate
        /// </summary>
        /// <param name="net"><see cref="Net"/> to be set as output of this gate</param>
        public void SetOutputNet(Net net)
        {
            _outNet = net;
        }

        /// <summary>
        /// Check if this gate has only a single input or dual
        ///     Gates like <see cref="GateType.Inv"/> have only 1 input
        ///     Gates like <see cref="GateType.And"/> have 2 input
        /// </summary>
        /// <returns>True, if single input. Otherwise false</returns>
        public bool IsSingleInput()
        {
            switch (_type)
            {
                case GateType.Inv:
                case GateType.One:
                case GateType.Zero:
                    return true;
                case GateType.Or:
                case GateType.And:
                case GateType.Xor:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get number of inputs that this gate SHOULD have
        ///     Gates like <see cref="GateType.Inv"/> have only 1 input
        ///     Gates like <see cref="GateType.And"/> have 2 input
        /// 
        ///     Note: We are not dealing with gates that take more than 2 input!
        ///     Note: All gates are single output!
        /// </summary>
        /// <returns>1, if single input. Otherwise 2</returns>
        public int GetCountOfInputsRequired()
        {
            switch (_type)
            {
                case GateType.Inv:
                case GateType.One:
                case GateType.Zero:
                    return 1;
                case GateType.Or:
                case GateType.And:
                case GateType.Xor:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException();
            } 
        }

        /// <summary>
        /// Calculate the output of the 
        /// </summary>
        public abstract void CalculateOutput();

        public string GetGateTypeAsString()
        {
            return _type.GetEnumDescription();
        }

        /// <summary>
        /// Get number of input and output nets to and from this gate
        /// </summary>
        /// <returns>Number of nets</returns>
        public int GetNetsCount()
        {
            return _inNets.Count + 1; //remember there is always one output net!
        }

        public GateType GetGateType()
        {
            return _type;
        }

        public IEnumerable<Net> GetAllNets()
        {
            List<Net> nets = new List<Net>();
            nets.AddRange(_inNets);
            nets.Add(_outNet);
            return nets;
        }

        public List<Net> GetInputNets()
        {
            return _inNets;
        }

        public Net GetOutputNet()
        {
            return _outNet;
        } 
        
        /// <summary>
        /// Gets the string which represents the gate symbol
        /// </summary>
        /// <returns></returns>
        public abstract string GetSymbol();

        /// <summary>
        /// Check if all input nets of this gate are named, e.g. a1 a2 b3 etccc
        /// </summary>
        /// <returns></returns>
        public bool IsTopLevelGate()
        {
            return _inNets.Any(net => net.HasName());
        }

        /// <summary>
        /// Check if the output of a gate is named, then its the master output
        /// </summary>
        /// <returns></returns>
        public bool IsLastOutputGate()
        {
            return _outNet.HasName();
        }

        //returns CNF of this gate
        public abstract CNF GetCnf(int offset = 0);
    }
}
