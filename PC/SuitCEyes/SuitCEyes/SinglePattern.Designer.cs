namespace SuitCEyes
{
    partial class SinglePattern
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.max255ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.middle128ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.low100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.max255ToolStripMenuItem,
            this.middle128ToolStripMenuItem,
            this.low100ToolStripMenuItem,
            this.offToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 142);
            // 
            // max255ToolStripMenuItem
            // 
            this.max255ToolStripMenuItem.Name = "max255ToolStripMenuItem";
            this.max255ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.max255ToolStripMenuItem.Tag = "255";
            this.max255ToolStripMenuItem.Text = "Max (255)";
            this.max255ToolStripMenuItem.Click += new System.EventHandler(this.selectLevelToolStripMenuItem_Click);
            // 
            // middle128ToolStripMenuItem
            // 
            this.middle128ToolStripMenuItem.Name = "middle128ToolStripMenuItem";
            this.middle128ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.middle128ToolStripMenuItem.Tag = "128";
            this.middle128ToolStripMenuItem.Text = "Middle (128)";
            this.middle128ToolStripMenuItem.Click += new System.EventHandler(this.selectLevelToolStripMenuItem_Click);
            // 
            // low100ToolStripMenuItem
            // 
            this.low100ToolStripMenuItem.Name = "low100ToolStripMenuItem";
            this.low100ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.low100ToolStripMenuItem.Tag = "100";
            this.low100ToolStripMenuItem.Text = "Low (100)";
            this.low100ToolStripMenuItem.Click += new System.EventHandler(this.selectLevelToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.offToolStripMenuItem.Tag = "0";
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.selectLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "Set";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // SinglePattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "SinglePattern";
            this.Size = new System.Drawing.Size(300, 294);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem max255ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem middle128ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem low100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
