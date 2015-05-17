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
            this.netControl2 = new SatSolver.UserInterface.CustomControls.NetTreeControl();
            this.netControl1 = new SatSolver.UserInterface.CustomControls.NetTreeControl();
            this.menuStrip1.SuspendLayout();
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
            // netControl2
            // 
            this.netControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.netControl2.Location = new System.Drawing.Point(280, 88);
            this.netControl2.Name = "netControl2";
            this.netControl2.Size = new System.Drawing.Size(250, 657);
            this.netControl2.TabIndex = 2;
            this.netControl2.UseSelectable = true;
            // 
            // netControl1
            // 
            this.netControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.netControl1.Location = new System.Drawing.Point(24, 88);
            this.netControl1.Name = "netControl1";
            this.netControl1.Size = new System.Drawing.Size(250, 657);
            this.netControl1.TabIndex = 1;
            this.netControl1.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.netControl2);
            this.Controls.Add(this.netControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SAT Solver";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private CustomControls.NetTreeControl netControl1;
        private CustomControls.NetTreeControl netControl2;

    }
}

