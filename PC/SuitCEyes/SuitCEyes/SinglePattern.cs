using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class SinglePattern : UserControl
    {
        [Browsable(true)]
        public event EventHandler<StrengthEventArgs> StrengthUpdated;

        public bool Selected { get; set; }
        public int Strength { get; set; }

        public SinglePattern()
        {
            InitializeComponent();
            Selected = true;
            Strength = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);

            // Declare and instantiate a new pen that will be disposed of at the end of the method.
            var myPen = new Pen(Color.Aqua);
            var selectPen = new Pen(Color.Black);
            selectPen.Width = 4;
            var myBrush = new SolidBrush((Strength > 0) ? Color.Red : Color.DarkRed);
            var fontBrush = new SolidBrush(Color.White);
            var drawFont = new Font("Arial", 16);
            var stringformat = new StringFormat();
            stringformat.LineAlignment = StringAlignment.Center;
            stringformat.Alignment = StringAlignment.Center;

            int grid_width = this.Size.Width;
            int grid_height = this.Size.Height;
            int dia = Math.Min(grid_width, grid_height) - 4;
            if (dia < 1)
                dia = 1;

            float cx = grid_width / 2;
            float cy = grid_height / 2;
            var r = new Rectangle((int)cx - dia / 2, (int)cy - dia / 2, dia, dia);
            e.Graphics.FillEllipse(myBrush, r);

            if (Strength > 0)
            {
                e.Graphics.DrawString(String.Format("{0}", Strength), drawFont, fontBrush, cx, cy, stringformat);
            }

            if (Selected)
            {
                e.Graphics.DrawEllipse(selectPen, r);
            }

        }

        private void selectLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Strength = Convert.ToInt32(item.Tag);
            RaiseStrengthChanged();
            Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EnterTextMessageBox f = new EnterTextMessageBox("Value");
            f.Value = Strength.ToString();
            if (f.ShowDialog() == DialogResult.OK) {
                int res;
                if (Int32.TryParse(f.Value, out res))
                {
                    if (res >= 0 && res <= 255)
                    {
                        Strength = res;
                        RaiseStrengthChanged();
                        Invalidate();
                    }
                    else
                        MessageBox.Show("Only range 0-255 is allowed");
                }
                else
                    MessageBox.Show("Invalid number");
            }
        }

        private void RaiseStrengthChanged()
        {
            StrengthUpdated?.Invoke(this, new StrengthEventArgs(Strength));
        }

        public class StrengthEventArgs : EventArgs {
            public int Strength { get; set; }

            public StrengthEventArgs(int str) : base() {
                Strength = str;
            }
        }
    }    
}
