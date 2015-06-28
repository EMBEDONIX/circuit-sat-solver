using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MetroFramework.Forms;
using SatSolver.Objects;
using SatSolver.Objects.Gates;
using SatSolver.UserInterface.CustomControls;

namespace SatSolver.UserInterface.ApplicationForm
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : MetroForm
    {
        private Circuit _circuitA, _circuitB, _circuitM;

        private DavisPutnam _dp;

        /// <summary>
        /// Default Constructor for the MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Focus();
            tabPages.Focus();
            tabPages.Controls[0].Focus();
            netControl1.Focus();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            netControl1.setId(1);
            netControl1.SetParent(this);
            netControl2.setId(2);
            netControl2.SetParent(this);

            netControl1.CircuitLoaded += (o, args) =>
            {
                _circuitA = args.Circuit;
               netControl2.SetNetIdOffset(netControl1.Circuit.GetHighestNetId());
            };

            netControl2.CircuitLoaded += (o, args) =>
            {
                _circuitB = args.Circuit;

                try
                {
                    _circuitM = new MiterCircuit(_circuitA, _circuitB);
                }
                catch (Exception exc)
                {
                    AddLine(exc.Message, 0);
                }
                miterControl.AddCircuit(_circuitM);
                schematicControl.SetCircuit(_circuitM, args.TreeId);

            };

            miterControl.CircuitLoaded += (o, args) =>
            {
                netControl1.ExpandAll();
                netControl2.ExpandAll();
                miterControl.ExpandAll();

            };




#if DEBUG
            netControl1.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\test1.net");
            netControl2.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\test2.net");
#endif
        }

        private void DavisPutnamReport(object o, DavisPutnamEventArgs e)
        {
               tbDebug.BeginInvoke((Action)(() =>
               {

                   AddLine(e.Message, e.Level);

                   switch (e.Type)
                   {
                       case DpType.PerformingUnitClause:
                           break;
                       case DpType.PerformingPureLiteralRule:
                           break;
                       case DpType.Starting:
                       case DpType.RemovingUintClause:
                       case DpType.RemovingPureLiteral:
                       case DpType.RemovingClause:
                           ShowCnf(e.CurrentCnf, 0);
                           break;

                       case DpType.Stopped:
                           break;
                       case DpType.SolutionFound:
                           break;
                       case DpType.OnlyMessage:
                           break;
                        case DpType.FindingRightMostVariable:
                            break;
                        case DpType.PerformingBacktrack:
                           break;
                        case DpType.FoundUnitClause:
                            break;

                       default:
                           throw new ArgumentOutOfRangeException();
                   }
                   
               }));
            
        }

        
        private void ShowCnf(CNF cnf, int level = 0)
        {
            AddLine(string.Empty, 0);
            AddLine(cnf.ToString(), level);
            AddLine(string.Empty, 0);
        }

        private int _lineCounter = 0;
        private void AddLine(string line, int level)
        {
            tbDebug.AppendText($"{++_lineCounter,5}" + " | ");
            var indent = "";
            for (int i = 0; i < level; i++)
            {
                indent += "  ";
            }
            tbDebug.AppendText(indent + line + Environment.NewLine);
        }
    }
}
