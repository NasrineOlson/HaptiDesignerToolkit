namespace SuitCEyes
{
    partial class EditPatternUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPatternUserControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSimulate = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonRemoveFrame = new System.Windows.Forms.Button();
            this.buttonAddFrame = new System.Windows.Forms.Button();
            this.labelMaxFrames = new System.Windows.Forms.Label();
            this.numericUpDownIndex = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.max255ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.middle128ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.low100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxDuration = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxPen = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFill = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonClear = new System.Windows.Forms.ToolStripButton();
            this.patternUserContorl1 = new SuitCEyes.PatternUserContorl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndex)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSimulate);
            this.panel1.Controls.Add(this.buttonRun);
            this.panel1.Controls.Add(this.buttonRemoveFrame);
            this.panel1.Controls.Add(this.buttonAddFrame);
            this.panel1.Controls.Add(this.labelMaxFrames);
            this.panel1.Controls.Add(this.numericUpDownIndex);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 44);
            this.panel1.TabIndex = 3;
            // 
            // buttonSimulate
            // 
            this.buttonSimulate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSimulate.Location = new System.Drawing.Point(381, 13);
            this.buttonSimulate.Name = "buttonSimulate";
            this.buttonSimulate.Size = new System.Drawing.Size(56, 23);
            this.buttonSimulate.TabIndex = 6;
            this.buttonSimulate.Text = "Simulate";
            this.buttonSimulate.UseVisualStyleBackColor = true;
            this.buttonSimulate.Click += new System.EventHandler(this.buttonSimulate_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Location = new System.Drawing.Point(443, 13);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(56, 23);
            this.buttonRun.TabIndex = 5;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonRemoveFrame
            // 
            this.buttonRemoveFrame.Location = new System.Drawing.Point(209, 13);
            this.buttonRemoveFrame.Name = "buttonRemoveFrame";
            this.buttonRemoveFrame.Size = new System.Drawing.Size(86, 23);
            this.buttonRemoveFrame.TabIndex = 4;
            this.buttonRemoveFrame.Text = "Remove frame";
            this.buttonRemoveFrame.UseVisualStyleBackColor = true;
            this.buttonRemoveFrame.Click += new System.EventHandler(this.buttonRemoveFrame_Click);
            // 
            // buttonAddFrame
            // 
            this.buttonAddFrame.Location = new System.Drawing.Point(135, 13);
            this.buttonAddFrame.Name = "buttonAddFrame";
            this.buttonAddFrame.Size = new System.Drawing.Size(68, 23);
            this.buttonAddFrame.TabIndex = 3;
            this.buttonAddFrame.Text = "Add frame";
            this.buttonAddFrame.UseVisualStyleBackColor = true;
            this.buttonAddFrame.Click += new System.EventHandler(this.buttonAddFrame_Click);
            // 
            // labelMaxFrames
            // 
            this.labelMaxFrames.AutoSize = true;
            this.labelMaxFrames.Location = new System.Drawing.Point(97, 18);
            this.labelMaxFrames.Name = "labelMaxFrames";
            this.labelMaxFrames.Size = new System.Drawing.Size(24, 13);
            this.labelMaxFrames.TabIndex = 2;
            this.labelMaxFrames.Text = "/10";
            // 
            // numericUpDownIndex
            // 
            this.numericUpDownIndex.Location = new System.Drawing.Point(45, 16);
            this.numericUpDownIndex.Name = "numericUpDownIndex";
            this.numericUpDownIndex.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownIndex.TabIndex = 1;
            this.numericUpDownIndex.ValueChanged += new System.EventHandler(this.numericUpDownIndex_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frame";
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 120);
            // 
            // max255ToolStripMenuItem
            // 
            this.max255ToolStripMenuItem.Name = "max255ToolStripMenuItem";
            this.max255ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.max255ToolStripMenuItem.Tag = "255";
            this.max255ToolStripMenuItem.Text = "Max (255)";
            this.max255ToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // middle128ToolStripMenuItem
            // 
            this.middle128ToolStripMenuItem.Name = "middle128ToolStripMenuItem";
            this.middle128ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.middle128ToolStripMenuItem.Tag = "128";
            this.middle128ToolStripMenuItem.Text = "Middle (128)";
            this.middle128ToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // low100ToolStripMenuItem
            // 
            this.low100ToolStripMenuItem.Name = "low100ToolStripMenuItem";
            this.low100ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.low100ToolStripMenuItem.Tag = "100";
            this.low100ToolStripMenuItem.Text = "Low (100)";
            this.low100ToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.offToolStripMenuItem.Tag = "0";
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem1.Text = "Set";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.patternUserContorl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(517, 539);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "Duration (ms)";
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(100, 25);
            this.textBoxDuration.Text = "250";
            this.textBoxDuration.ToolTipText = "Set the duration of the frame in millseconds";
            this.textBoxDuration.TextChanged += new System.EventHandler(this.textBoxDuration_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel2.Text = "Pen";
            // 
            // textBoxPen
            // 
            this.textBoxPen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxPen.Name = "textBoxPen";
            this.textBoxPen.Size = new System.Drawing.Size(100, 25);
            this.textBoxPen.Text = "255";
            this.textBoxPen.ToolTipText = "Set the motor power (0 - motor off, 255 - max speed)";
            this.textBoxPen.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonFill
            // 
            this.buttonFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonFill.Image = ((System.Drawing.Image)(resources.GetObject("buttonFill.Image")));
            this.buttonFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(41, 22);
            this.buttonFill.Text = "Fill all";
            this.buttonFill.ToolTipText = "Fill all channels with current pen value";
            this.buttonFill.Click += new System.EventHandler(this.buttonFill_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.textBoxDuration,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.textBoxPen,
            this.toolStripSeparator3,
            this.buttonFill,
            this.buttonClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(517, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonClear
            // 
            this.buttonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(43, 22);
            this.buttonClear.Text = "All off";
            this.buttonClear.ToolTipText = "Turn off all motors";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // patternUserContorl1
            // 
            this.patternUserContorl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternUserContorl1.GridHeight = 0;
            this.patternUserContorl1.GridWidth = 0;
            this.patternUserContorl1.Location = new System.Drawing.Point(3, 28);
            this.patternUserContorl1.MyContextMenu = null;
            this.patternUserContorl1.Name = "patternUserContorl1";
            this.patternUserContorl1.PenValue = 0;
            this.patternUserContorl1.ReadOnly = false;
            this.patternUserContorl1.Size = new System.Drawing.Size(511, 458);
            this.patternUserContorl1.TabIndex = 2;
            // 
            // EditPatternUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditPatternUserControl";
            this.Size = new System.Drawing.Size(517, 539);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndex)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PatternUserContorl patternUserContorl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSimulate;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonRemoveFrame;
        private System.Windows.Forms.Button buttonAddFrame;
        private System.Windows.Forms.Label labelMaxFrames;
        private System.Windows.Forms.NumericUpDown numericUpDownIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem max255ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem middle128ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem low100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox textBoxDuration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox textBoxPen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton buttonFill;
        private System.Windows.Forms.ToolStripButton buttonClear;
    }
}
