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
    public partial class PatternUserContorl : UserControl
    {
        public event EventHandler<ValuesUpdatedEventArgs> ValuesUpdated;
        public event EventHandler<AcrossFramesEventArgs> AcrossFramesUpdated;

        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        public bool ReadOnly { get; set; }

        public ContextMenuStrip MyContextMenu { get; set; }

        public int PenValue { get; set; }

        public Cell[] Cells;

        private List<int> orderOfSelection;
        private bool leftMouseDown = false;
        private bool startedMouseMove = false;
        private int currentIndex = 0;
        private PointF currentMousePosition;
        private PointF startPointForMouseMove;

        public PatternUserContorl()
        {

            MyContextMenu = null;
            ReadOnly = false;

            InitializeComponent();
        }

        internal void SetGridSize(int width, int height)
        {
            GridWidth = width;
            GridHeight = height;

            Cells = new Cell[GridWidth * GridHeight];
            for (int i = 0; i < Cells.Length; i++)
                Cells[i] = new Cell();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);

            // Calculate size of individual cells
            float cellWidth = this.Width / GridWidth;
            float cellHeight = this.Height / GridHeight;
            // Make sure the keep the ratio
            float diameter = Math.Min(cellWidth, cellHeight) - 4;
            // Ensure a positive diameter
            if (diameter < 1)
                diameter = 1;

            // Set up graphic style           
            var selectPen = new Pen(Color.Black)
            {
                Width = 4
            };
            var fontBrush = new SolidBrush(Color.White);
            var drawFont = new Font("Arial", 16);
            var stringformat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    int index = x + y * GridWidth;

                    // Calculate the center of the grid
                    float cx = x * cellWidth + cellWidth / 2;
                    float cy = y * cellHeight + cellHeight / 2;

                    // Draw background color
                    RectangleF br = new RectangleF(cx - cellWidth / 2.0f, cy - cellHeight / 2.0f, cellWidth, cellHeight);
                    var bkg = new SolidBrush(Cells[index].BackgroundColor);
                    e.Graphics.FillRectangle(bkg, br);

                    // Draw actual marker
                    var myBrush = new SolidBrush((Cells[index].Value > 0) ? Color.Red : Color.DarkRed);

                    var r = new RectangleF(cx - diameter / 2, cy - diameter / 2, diameter, diameter);
                    e.Graphics.FillEllipse(myBrush, r);

                    if (Cells[index].Value > 0)
                        e.Graphics.DrawString(String.Format("{0}", Cells[index].Value), drawFont, fontBrush, cx, cy, stringformat);

                    if (Cells[index].Selected)
                        e.Graphics.DrawEllipse(selectPen, r);
                }
            }

            // Check if to draw lines
            if (leftMouseDown && orderOfSelection != null && orderOfSelection.Count > 1)
            {
                PointF first = getCenterPointFromIndex(orderOfSelection[0]);

                for (int i = 1; i < orderOfSelection.Count; i++)
                {
                    PointF second = getCenterPointFromIndex(orderOfSelection[i]);

                    e.Graphics.DrawLine(selectPen, first, second);

                    first = second;
                }

                e.Graphics.DrawLine(selectPen, first, currentMousePosition);
            }
            else if (startedMouseMove && leftMouseDown && orderOfSelection != null && orderOfSelection.Count == 1)
                e.Graphics.DrawLine(selectPen, getCenterPointFromIndex(orderOfSelection[0]), currentMousePosition);
        }

        public void SetFrame(Frame frame)
        {
            int max_values = Math.Min(frame.Channels.Length, Cells.Length);

            for (int i = 0; i < max_values; i++)
            {
                Cells[i].Value = frame.Channels[i];
                Cells[i].Selected = false;
            }

            Invalidate(true);
        }

        public void deselectAll()
        {
            for (int i = 0; i < Cells.Length; i++)
                Cells[i].Selected = false;
        }

        public int getIndexFromPointer(Point p)
        {
            // Validate point            
            if (p.X < 0 || p.Y < 0 || p.X > this.Size.Width - 1 || p.Y > this.Size.Height - 1)           
                return -1;
            
            float cellWidth = this.Width / GridWidth;
            float cellHeight = this.Height / GridHeight;
            float diameter = Math.Min(cellWidth, cellHeight) - 4;
            // Ensure a positive diameter
            if (diameter < 1)
                diameter = 1;

            int index = (int)(p.X / cellWidth) + ((int)(p.Y / cellHeight) * GridWidth);
            if (index >= GridWidth * GridHeight)
                return -1;

            PointF center = getCenterPointFromIndex(index);

            PointF distance = new PointF(center.X - p.X, center.Y - p.Y);
            if (Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y) < diameter / 2)
                return index;
            else
                return -1;
        }

        private PointF getCenterPointFromIndex(int index)
        {
            int y = index / GridWidth;
            int x = index - y * GridWidth;

            float cellWidth = this.Width / GridWidth;
            float cellHeight = this.Height / GridHeight;

            return new PointF(x * cellWidth + cellWidth / 2, y * cellHeight + cellHeight / 2);
        }

        #region Mouse
        private void PatternUserContorl_MouseDown(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;

            startedMouseMove = false;
            startPointForMouseMove = e.Location;

            int index = getIndexFromPointer(e.Location);

            if (e.Button == MouseButtons.Left && index >= 0)
            {
                if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
                    deselectAll();
                Cells[index].Value = PenValue;
                Cells[index].Selected = true;
                RaiseValuesUpdated();
                Invalidate();

                currentIndex = index;
                leftMouseDown = true;
                currentMousePosition = e.Location;

                orderOfSelection = new List<int>
                {
                    currentIndex
                };
            }
            else if (e.Button == MouseButtons.Left && index < 0)
            {
                if (Control.ModifierKeys != Keys.Control || Control.ModifierKeys != Keys.Shift)
                    deselectAll();
            }
            else if (e.Button == MouseButtons.Right && index >= 0)
            {
                if (!Cells[index].Selected)
                {
                    if (Control.ModifierKeys != Keys.Control || Control.ModifierKeys != Keys.Shift)
                        deselectAll();
                    Cells[index].Selected = true;
                }
                Invalidate();
                if (MyContextMenu != null)
                    MyContextMenu.Show(PointToScreen(e.Location));
            }
        }


        private void PatternUserContorl_MouseMove(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;

            if (leftMouseDown)
            {
                PointF delta = new PointF(e.Location.X - startPointForMouseMove.X, e.Location.Y - startPointForMouseMove.Y);
                if (Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y) > 20)
                {
                    startedMouseMove = true;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Cross;
                }
                int newIndex = getIndexFromPointer(e.Location);

                if (newIndex >= 0 && newIndex != currentIndex)
                {
                    Cells[newIndex].Selected = true;
                    Cells[newIndex].Value = PenValue;
                    orderOfSelection.Add(newIndex);
                    currentIndex = newIndex;
                    RaiseValuesUpdated();
                    currentMousePosition = getCenterPointFromIndex(newIndex);
                }
                else
                    currentMousePosition = e.Location;


                Invalidate();
            }
        }

        private void PatternUserContorl_MouseUp(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;

            startedMouseMove = false;
            if (leftMouseDown)
            {
                if (orderOfSelection != null && orderOfSelection.Count > 1)
                {
                    if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift)
                    {
                        foreach (int i in orderOfSelection)
                        {
                            Cells[i].Selected = false;
                            Cells[i].Value = PenValue;
                        }
                    }
                    else
                        RaiseAcrossFramesUpdated(orderOfSelection);
                }
                leftMouseDown = false;
                Invalidate();
            }
        }

        #endregion

        private void RaiseValuesUpdated()
        {
            ValuesUpdated?.Invoke(this, new ValuesUpdatedEventArgs());
        }

        private void RaiseAcrossFramesUpdated(List<int> orderOfSelection)
        {            
            AcrossFramesUpdated?.Invoke(this, new AcrossFramesEventArgs() { IndexList = orderOfSelection });
        }

        public class Cell
        {
            public int Value { get; set; }
            public bool Selected { get; set; }

            public Color BackgroundColor { get; set; }

            public Cell()
            {
                Value = 0;
                Selected = false;
                BackgroundColor = SystemColors.Control;
            }
        }

        public class ValuesUpdatedEventArgs : EventArgs
        {
        }

        public class AcrossFramesEventArgs : EventArgs
        {
            public List<int> IndexList { get; set; }
        }

        private void PatternUserContorl_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
