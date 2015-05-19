namespace SatSolver.UserInterface.ApplicationForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNetList1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNetList2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPages = new MetroFramework.Controls.MetroTabControl();
            this.tabSchematic = new MetroFramework.Controls.MetroTabPage();
            this.tabSolution = new MetroFramework.Controls.MetroTabPage();
            this.schematicControl = new SatSolver.SchematicsDrawer.ShematicControl();
            this.netControl2 = new SatSolver.UserInterface.CustomControls.SatTreeControl();
            this.netControl1 = new SatSolver.UserInterface.CustomControls.SatTreeControl();
            this.menuStrip1.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.tabSchematic.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNetList1ToolStripMenuItem,
            this.openNetList2ToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openNetList1ToolStripMenuItem
            // 
            this.openNetList1ToolStripMenuItem.Name = "openNetList1ToolStripMenuItem";
            this.openNetList1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openNetList1ToolStripMenuItem.Text = "Open NetList &1";
            this.openNetList1ToolStripMenuItem.Click += new System.EventHandler(this.openNetList1ToolStripMenuItem_Click);
            // 
            // openNetList2ToolStripMenuItem
            // 
            this.openNetList2ToolStripMenuItem.Name = "openNetList2ToolStripMenuItem";
            this.openNetList2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openNetList2ToolStripMenuItem.Text = "Open NetList &2";
            this.openNetList2ToolStripMenuItem.Click += new System.EventHandler(this.openNetList2ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabPages
            // 
            this.tabPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPages.Controls.Add(this.tabSchematic);
            this.tabPages.Controls.Add(this.tabSolution);
            this.tabPages.Location = new System.Drawing.Point(537, 88);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(464, 657);
            this.tabPages.TabIndex = 3;
            this.tabPages.UseSelectable = true;
            // 
            // tabSchematic
            // 
            this.tabSchematic.Controls.Add(this.schematicControl);
            this.tabSchematic.HorizontalScrollbarBarColor = true;
            this.tabSchematic.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSchematic.HorizontalScrollbarSize = 10;
            this.tabSchematic.Location = new System.Drawing.Point(4, 38);
            this.tabSchematic.Name = "tabSchematic";
            this.tabSchematic.Size = new System.Drawing.Size(456, 615);
            this.tabSchematic.TabIndex = 0;
            this.tabSchematic.Text = "Schematics";
            this.tabSchematic.VerticalScrollbarBarColor = true;
            this.tabSchematic.VerticalScrollbarHighlightOnWheel = false;
            this.tabSchematic.VerticalScrollbarSize = 10;
            // 
            // tabSolution
            // 
            this.tabSolution.HorizontalScrollbarBarColor = true;
            this.tabSolution.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSolution.HorizontalScrollbarSize = 10;
            this.tabSolution.Location = new System.Drawing.Point(4, 38);
            this.tabSolution.Name = "tabSolution";
            this.tabSolution.Size = new System.Drawing.Size(456, 615);
            this.tabSolution.TabIndex = 1;
            this.tabSolution.Text = "Solution";
            this.tabSolution.VerticalScrollbarBarColor = true;
            this.tabSolution.VerticalScrollbarHighlightOnWheel = false;
            this.tabSolution.VerticalScrollbarSize = 10;
            // 
            // schematicControl
            // 
            this.schematicControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicControl.Location = new System.Drawing.Point(0, 0);
            this.schematicControl.Name = "schematicControl";
            this.schematicControl.Size = new System.Drawing.Size(456, 615);
            this.schematicControl.TabIndex = 2;
            this.schematicControl.UseSelectable = true;
            // 
            // netControl2
            // 
            this.netControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.netControl2.Location = new System.Drawing.Point(280, 88);
            this.netControl2.Name = "netControl2";
            this.netControl2.Size = new System.Drawing.Size(251, 657);
            this.netControl2.TabIndex = 2;
            this.netControl2.UseSelectable = true;
            // 
            // netControl1
            // 
            this.netControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.netControl1.Location = new System.Drawing.Point(23, 88);
            this.netControl1.Name = "netControl1";
            this.netControl1.Size = new System.Drawing.Size(251, 657);
            this.netControl1.TabIndex = 1;
            this.netControl1.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.netControl2);
            this.Controls.Add(this.netControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SAT Solver";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPages.ResumeLayout(false);
            this.tabSchematic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNetList1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNetList2ToolStripMenuItem;
        private CustomControls.SatTreeControl netControl1;
        private CustomControls.SatTreeControl netControl2;
        private MetroFramework.Controls.MetroTabControl tabPages;
        private MetroFramework.Controls.MetroTabPage tabSchematic;
        private MetroFramework.Controls.MetroTabPage tabSolution;
        private SchematicsDrawer.ShematicControl schematicControl;

    }
}

