using System.Windows.Forms;

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
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTreesVisibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPages = new MetroFramework.Controls.MetroTabControl();
            this.tabSchematic = new MetroFramework.Controls.MetroTabPage();
            this.schematicControl = new SatSolver.SchematicsDrawer.ShematicControl();
            this.tabSolution = new MetroFramework.Controls.MetroTabPage();
            this.tbDebug = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.netControl1 = new SatSolver.UserInterface.CustomControls.SatTreeControl();
            this.miterControl = new SatSolver.UserInterface.CustomControls.SatMiterControl();
            this.netControl2 = new SatSolver.UserInterface.CustomControls.SatTreeControl();
            this.menuStrip1.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.tabSchematic.SuspendLayout();
            this.tabSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
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
            this.solveToolStripMenuItem,
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
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.solveToolStripMenuItem.Text = "&Solve";
            this.solveToolStripMenuItem.Click += new System.EventHandler(this.solveToolStripMenuItem_Click);
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleTreesVisibilityToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toggleTreesVisibilityToolStripMenuItem
            // 
            this.toggleTreesVisibilityToolStripMenuItem.Name = "toggleTreesVisibilityToolStripMenuItem";
            this.toggleTreesVisibilityToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.toggleTreesVisibilityToolStripMenuItem.Text = "&Toggle Trees Visibility";
            this.toggleTreesVisibilityToolStripMenuItem.Click += new System.EventHandler(this.toggleTreesVisibilityToolStripMenuItem_Click);
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.tabSchematic);
            this.tabPages.Controls.Add(this.tabSolution);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 0);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(521, 664);
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
            this.tabSchematic.Margin = new System.Windows.Forms.Padding(10);
            this.tabSchematic.Name = "tabSchematic";
            this.tabSchematic.Size = new System.Drawing.Size(513, 622);
            this.tabSchematic.TabIndex = 0;
            this.tabSchematic.Text = "Schematics";
            this.tabSchematic.VerticalScrollbarBarColor = true;
            this.tabSchematic.VerticalScrollbarHighlightOnWheel = false;
            this.tabSchematic.VerticalScrollbarSize = 10;
            // 
            // schematicControl
            // 
            this.schematicControl.AutoScroll = true;
            this.schematicControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schematicControl.Location = new System.Drawing.Point(0, 0);
            this.schematicControl.Name = "schematicControl";
            this.schematicControl.Padding = new System.Windows.Forms.Padding(10);
            this.schematicControl.Size = new System.Drawing.Size(513, 622);
            this.schematicControl.TabIndex = 2;
            this.schematicControl.UseSelectable = true;
            // 
            // tabSolution
            // 
            this.tabSolution.Controls.Add(this.tbDebug);
            this.tabSolution.HorizontalScrollbarBarColor = true;
            this.tabSolution.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSolution.HorizontalScrollbarSize = 10;
            this.tabSolution.Location = new System.Drawing.Point(4, 38);
            this.tabSolution.Name = "tabSolution";
            this.tabSolution.Size = new System.Drawing.Size(554, 622);
            this.tabSolution.TabIndex = 1;
            this.tabSolution.Text = "Solution";
            this.tabSolution.VerticalScrollbarBarColor = true;
            this.tabSolution.VerticalScrollbarHighlightOnWheel = false;
            this.tabSolution.VerticalScrollbarSize = 10;
            // 
            // tbDebug
            // 
            this.tbDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDebug.Location = new System.Drawing.Point(0, 0);
            this.tbDebug.Multiline = true;
            this.tbDebug.Name = "tbDebug";
            this.tbDebug.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDebug.Size = new System.Drawing.Size(554, 622);
            this.tbDebug.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(20, 84);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabPages);
            this.splitContainer.Size = new System.Drawing.Size(984, 664);
            this.splitContainer.SplitterDistance = 459;
            this.splitContainer.TabIndex = 4;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel.Controls.Add(this.netControl1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.miterControl, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.netControl2, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(459, 664);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // netControl1
            // 
            this.netControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.netControl1.Location = new System.Drawing.Point(3, 3);
            this.netControl1.Name = "netControl1";
            this.netControl1.Size = new System.Drawing.Size(145, 658);
            this.netControl1.TabIndex = 1;
            this.netControl1.UseSelectable = true;
            // 
            // miterControl
            // 
            this.miterControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.miterControl.Location = new System.Drawing.Point(305, 3);
            this.miterControl.Name = "miterControl";
            this.miterControl.Size = new System.Drawing.Size(151, 658);
            this.miterControl.TabIndex = 3;
            this.miterControl.UseSelectable = true;
            // 
            // netControl2
            // 
            this.netControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.netControl2.Location = new System.Drawing.Point(154, 3);
            this.netControl2.Name = "netControl2";
            this.netControl2.Size = new System.Drawing.Size(145, 658);
            this.netControl2.TabIndex = 2;
            this.netControl2.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SAT Solver";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPages.ResumeLayout(false);
            this.tabSchematic.ResumeLayout(false);
            this.tabSolution.ResumeLayout(false);
            this.tabSolution.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;
        private CustomControls.SatMiterControl miterControl;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem toggleTreesVisibilityToolStripMenuItem;
        private TextBox tbDebug;
    }
}
