using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects;
using SatSolver.UserInterface.CustomControls;
using SatSolver.Utilities;

namespace SatSolver.UserInterface.ApplicationForm
{
    public partial class MainForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="which">1 for netTree1 and 2 for netTree2</param>
        private void ShowBrowseNetListFileDialog(int which)
        {
            Circuit circuit = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a NetList file for Circuit " + which;
            ofd.Filter = "NetList Files|*.net";

            //TODO only do this if is DEBUG BUILD!
            ofd.InitialDirectory = System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                NetListReader reader = new NetListReader(ofd.FileName);
                try
                {
                    circuit = reader.GenerateCircuit();
                }
                catch (InvalidNetListFileException inlfException)
                {
                    MessageBox.Show(inlfException.ToString(), "Invalid NetList File", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Critical Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            if (circuit != null)
            {   if(which == 1)
                    netTree1.AddCircuit(circuit);
                else
                    netTree2.AddCircuit(circuit);
            }
        }
    }
}
