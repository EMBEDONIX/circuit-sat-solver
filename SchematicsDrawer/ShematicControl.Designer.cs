namespace SatSolver.SchematicsDrawer
{
    partial class ShematicControl
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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.Panel();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.box = new SatSolver.SchematicsDrawer.SchematicBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.box);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(628, 531);
            this.panel.TabIndex = 0;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // tooltip
            // 
            this.tooltip.AutoPopDelay = 5000;
            this.tooltip.InitialDelay = 300;
            this.tooltip.ReshowDelay = 100;
            // 
            // box
            // 
            this.box.Location = new System.Drawing.Point(3, 3);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(5000, 5000);
            this.box.TabIndex = 1;
            this.box.TabStop = false;
            // 
            // ShematicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel);
            this.Name = "ShematicControl";
            this.Size = new System.Drawing.Size(628, 531);
            this.Load += new System.EventHandler(this.ShematicControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShematicControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ShematicControl_KeyUp);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private SchematicBox box;
        private System.Windows.Forms.ToolTip tooltip;
    }
}
