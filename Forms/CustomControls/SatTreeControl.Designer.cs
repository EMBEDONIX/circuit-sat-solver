using System.Windows.Forms;

namespace SatSolver.UserInterface.CustomControls
{
    partial class SatTreeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.labelControlName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.browseForNetlist = new System.Windows.Forms.ToolStripButton();
            this.expandNodes = new System.Windows.Forms.ToolStripButton();
            this.collapseNodes = new System.Windows.Forms.ToolStripButton();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.treeView = new SatSolver.UserInterface.CustomControls.NetListTreeView();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelControlName,
            this.toolStripSeparator1,
            this.browseForNetlist,
            this.expandNodes,
            this.collapseNodes});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(181, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // labelControlName
            // 
            this.labelControlName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelControlName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelControlName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelControlName.Name = "labelControlName";
            this.labelControlName.Size = new System.Drawing.Size(55, 22);
            this.labelControlName.Text = "Circuit N";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // browseForNetlist
            // 
            this.browseForNetlist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browseForNetlist.Image = global::SatSolver.UserInterface.Properties.Resources.open_folder_32;
            this.browseForNetlist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browseForNetlist.Name = "browseForNetlist";
            this.browseForNetlist.Size = new System.Drawing.Size(23, 22);
            this.browseForNetlist.Text = "Browse for NetList";
            this.browseForNetlist.ToolTipText = "Open NetList File";
            this.browseForNetlist.Click += new System.EventHandler(this.browseForNetlist_Click);
            // 
            // expandNodes
            // 
            this.expandNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.expandNodes.Image = global::SatSolver.UserInterface.Properties.Resources.expand_32;
            this.expandNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.expandNodes.Name = "expandNodes";
            this.expandNodes.Size = new System.Drawing.Size(23, 22);
            this.expandNodes.Text = "Expand Nodes";
            this.expandNodes.Click += new System.EventHandler(this.expandNodes_Click);
            // 
            // collapseNodes
            // 
            this.collapseNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collapseNodes.Image = global::SatSolver.UserInterface.Properties.Resources.collapse_32;
            this.collapseNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.collapseNodes.Name = "collapseNodes";
            this.collapseNodes.Size = new System.Drawing.Size(23, 22);
            this.collapseNodes.Text = "Collapse Nodes";
            this.collapseNodes.Click += new System.EventHandler(this.collapseNodes_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxInfo.Location = new System.Drawing.Point(0, 481);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(181, 120);
            this.textBoxInfo.TabIndex = 3;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(0, 28);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(181, 447);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // SatTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.treeView);
            this.Name = "SatTreeControl";
            this.Size = new System.Drawing.Size(181, 601);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NetListTreeView treeView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton expandNodes;
        private System.Windows.Forms.ToolStripButton collapseNodes;
        private TextBox textBoxInfo;
        protected ToolStripButton browseForNetlist;
        protected ToolStrip toolStrip;
        protected ToolStripLabel labelControlName;
    }
}
