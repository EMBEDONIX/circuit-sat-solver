using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SatSolver.UserInterface.CustomControls
{
    public partial class SatMiterControl : SatTreeControl
    {
        public SatMiterControl()
        {
            InitializeComponent();
            browseForNetlist.Visible = false;
            labelControlName.Text = "Miter";
        }
    }
}