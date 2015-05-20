using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SatSolver.Objects;
using SatSolver.Objects.Gates;

namespace SatSolver.Utilities
{
    /// <summary>
    ///     This class reads NetList files and generates an objects from them
    /// </summary>
    public class NetListReader
    {

        private string _file;
        /// <summary>
        ///     Constructor for NetListReader
        /// </summary>
        /// <param name="netListFile">Path to the file to read</param>
        public NetListReader(string pathToFile)
        {
            _file = pathToFile;
        }

        public Circuit GenerateCircuit()
        {
            var circuit = new Circuit(_file);
            int numNets;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            Dictionary<string, int> inputs = new Dictionary<string, int>();
            Dictionary<string, int> outputs = new Dictionary<string, int>();


            var counter = 0;
            string line;

            //Read all lines and add each line to a list
            List<string> lines = new List<string>();
            var fileContent = new StreamReader(_file);
            while ((line = fileContent.ReadLine()) != null)
            {
                lines.Add(line);
                counter++;
            }

            //If file is empty, throw exception...
            if (!(lines.Count > 0))
            {
                throw new InvalidNetListFileException("File Is empty!", _file, 0);
            }

            //Check for line 1 to get toal number of nets in the circuit
            if (Helpers.IsDigitsOnly(lines[0]))
            {
                numNets = Convert.ToInt32(lines[0]);
            }
            else //if first line is not integer, throw exception
            {
                throw new InvalidNetListFileException("Expected the first line of the file to be integer", _file, 1);
            }

            //check line 2 to get name of inputs 
            string[] inputNames = lines[1].Split(delimiterChars);
            
            //check line 3 to get name of outputs
            string[] outputNames = lines[2].Split(delimiterChars);
            
            /*
             *  We know for example if we have 2 inputs and 1 output, there should exist only 3 lines to assign a net to a name
             *  so we add the count of elements in inputNames and outputNames to see how many lines we need to read to assign
             *  a net to a name!
             */
            int numLinesToCheck = inputNames.Count() + outputNames.Count();

            //if (numNets != numLinesToCheck)
            //{
            //    throw new Exception("Error: Number of total netlists does not match number of assigned nets to netlist");
            //}

            int emptyLinePosition = 3 + numLinesToCheck;
            for (int i = 3; i < numLinesToCheck + 3; i++)
            {      
                int net;
                string name;   

                string[] data = lines[i].Split(delimiterChars);
                name = data[1];
                net = Convert.ToInt32(data[0]);

                //if the net name exist in inputs, then we add it to dictionary of inputs
                if (inputNames.Contains(data[1]))
                    inputs.Add(name, net);          

                if (outputNames.Contains(data[1]))
                    outputs.Add(name, net );                      
            }
            
            /*
             * Now, the next line should be an empty line and immideatly after it the gates should be 
             * presented. First we check if it is the case, if not, throwing exception, if yes, continue
             */

            if (!string.IsNullOrWhiteSpace(lines[emptyLinePosition]))
                throw new InvalidNetListFileException("Expected an empty line after " +
                                    "the last netlist assignement to start reading gates."
                                    , _file, emptyLinePosition);                

            //Itterate over lines which supposed to contain gate definitions
            for (int i = 3 + numLinesToCheck + 1; i < lines.Count; i++)
            {
                Gate gate;
                Net net;
                int id;
                string name;
                
                string[] data = lines[i].Split(delimiterChars);
                
                //check if the line starts with letter (name of a gate e.g. "a" for AND gate
                //TODO change this to implicitly check agains a,i,o,z,x or better the whole gate name
                if (!Char.IsLetter(data[0][0])) 
                   throw new InvalidNetListFileException("The line to generate gate from does not " +
                                                         "start with a valid character", _file, i); 
                
                GateType gateType = data[0].ParseEnum<GateType>();

                switch (data[0])
                {
                    //Cases for dual input gates
                    case "and":
                    case "or":
                    case "xor":

                        /*
                         * Check for correctness of the line:
                         * 
                         * And gate needs 2 inputs and 1 output, including the gate name itslef the
                         * total count of items after spliting the line should only be 4.
                         * 
                         * Also the last 3 elements should be digits only.
                         */
                        if (data.Length != 4)
                            throw new InvalidNetListFileException("There are insufficent nets for" +
                                     gateType.GetEnumDescription() + " gate", _file, i);
                        if (!Helpers.IsDigitsOnly(data.SubArray(1, 3)))
                            throw new InvalidNetListFileException("Nets are not nummeric", _file, i);

                        //By now, we should have a valid line, so construction the gate object
                        switch (gateType)
                        {
                            case GateType.Or:
                                gate = new GateOr(GateType.Or);
                                break;
                            case GateType.And:
                                gate = new GateAnd(GateType.And);
                                break;
                            case GateType.Xor:
                                gate = new GateXor(GateType.Xor);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }


                        //get 2 inputs
                        for (int j = 1; j <= gate.GetCountOfInputsRequired(); j++)
                        {
                            //get inputs
                            id = Convert.ToInt32(data[j]);
                            name = inputs.Where(x => x.Value == id).FirstOrDefault().Key;
                            net = !string.IsNullOrWhiteSpace(name) ? new Net(name, id) : new Net(id);

                            gate.AddInputNet(net);
                        }

                        //get 1 output
                        id = Convert.ToInt32(data[3]);
                        if (id == 49)
                        {
                            var shit = "cool!";
                        }
                        name = outputs.Where(x => x.Value == id).FirstOrDefault().Key;
                        net = !string.IsNullOrWhiteSpace(name) ? new Net(name, id) : new Net(id);
                        gate.SetOutputNet(net);

                        circuit.AddGate(gate);
                        break;

                    //cases for single input gates    
                    case "inv":
                    case "one":
                    case "zero":

                        //similar checks as in the dual input case
                        if (data.Length != 3)
                            throw new InvalidNetListFileException("There are insufficent nets for" +
                                                                 gateType.GetEnumDescription() + " gate", _file, i);
                        if (!Helpers.IsDigitsOnly(data.SubArray(1, 2)))
                            throw new InvalidNetListFileException("Nets are not nummeric", _file, i);

                        //By now, we should have a valid line, so construction the gate object
                        switch (gateType)
                        {
                            case GateType.Inv:
                                gate = new GateInv(GateType.Inv);
                                break;
                            case GateType.One:
                                gate = new GateOne(GateType.One);
                                break;
                            case GateType.Zero:
                                gate = new GateZero(GateType.Zero);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        //get input net 1
                        id = Convert.ToInt32(data[1]);
                        name = inputs.FirstOrDefault(c => c.Value == id).Key;
                        net = !string.IsNullOrWhiteSpace(name) ? new Net(name, id) : new Net(id);

                        gate.AddInputNet(net);
                        

                        //get 1 output
                        id = Convert.ToInt32(data[2]);
                        name = outputs.Where(x => x.Value == id).FirstOrDefault().Key;
                        net = !string.IsNullOrWhiteSpace(name) ? new Net(name, id) : new Net(id);
                        gate.SetOutputNet(net);

                        circuit.AddGate(gate);

                    break;

                    //Default case if we face an unknown name, throw exception and warning about line
                    default:
                        throw new InvalidNetListFileException("Can not identify gate name", _file, i);

                }
            } 

            return circuit;
        }



    }
}