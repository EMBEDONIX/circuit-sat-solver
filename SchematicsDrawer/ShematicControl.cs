using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace SatSolver.SchematicsDrawer
{
    public partial class ShematicControl : MetroUserControl
    {
        public ShematicControl()
        {
            InitializeComponent();

            box.Paint += DrawGrid; //draw grid
        }

        private void DrawGrid(object sender, PaintEventArgs p)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            int h = box.Height;
            int w = box.Width;

            int numVertLines = w/10;
            int numHorLines = h/10;
            int x = 0;
            int y = h;

            for (int i = 1; i < numVertLines; i++)
            {
                p.Graphics.DrawLine(pen, x, 0, x, h  );
                x += 10;
            }

            x = y = 0;
            for (int i = 1; i < numHorLines; i++)
            {
                p.Graphics.DrawLine(pen, 0, y + 10, w, y + 10);
                y += 10;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void ShematicControl_Load(object sender, EventArgs e)
        {
            
        }
    }
}
