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
    public partial class EditPatternUserControl : UserControl
    {
        public event EventHandler<EventArgs> HaptogramModified;

        private Pattern _pattern;
         
        public EditPatternUserControl()
        {
            InitializeComponent();

            patternUserContorl1.ValuesUpdated += PatternUserContorl1_ValuesUpdated;
            patternUserContorl1.AcrossFramesUpdated += PatternUserContorl1_AcrossFramesUpdated;
            patternUserContorl1.MyContextMenu = contextMenuStrip1;
            UpdatePenValue();
            UpdateDurationValue();
        }

        public void SetGridSize(int width, int height)
        {
            patternUserContorl1.SetGridSize(width, height);
        }

        public void SetPattern(Pattern pattern)
        {
            _pattern = pattern;
            UpdatePattern(1);
        }

        public void UpdatePattern(int index)
        {
            if (_pattern == null)
                throw new Exception("Pattern cannot be null");

            UpdateFrameCounter();
            numericUpDownIndex.Value = index;
            SetFrame(index);
        }

        public void UpdateFrameCounter()
        {
            int maxFrames = _pattern.Frames.Count;
            labelMaxFrames.Text = String.Format("/{0}", maxFrames);

            numericUpDownIndex.Maximum = maxFrames;
            numericUpDownIndex.Minimum = 1;
        }

        private void numericUpDownIndex_ValueChanged(object sender, EventArgs e)
        {
            SetFrame((int)numericUpDownIndex.Value);
        }

        internal void SetCellBackgroundColor(int key, Color value)
        {
            patternUserContorl1.Cells[key].BackgroundColor = value;
        }

        internal PatternUserContorl GetPatterUsercontrol()
        {
            return patternUserContorl1;
        }

        internal void ClearEventHandler()
        {
            HaptogramModified = null;
        }

        private void SetFrame(int index)
        {
            patternUserContorl1.SetFrame(_pattern.Frames[index - 1]);
            textBoxDuration.Text = String.Format("{0}", _pattern.Frames[index - 1].Duration);
        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            int duration = _pattern.Frames[(int)numericUpDownIndex.Value - 1].Duration;
            _pattern.AddNewFrame(duration);
            UpdateFrameCounter();
            numericUpDownIndex.Value = _pattern.Frames.Count;

            SetFrame((int)numericUpDownIndex.Value);

            RaiseHaptogramModified();
        }

        private void buttonRemoveFrame_Click(object sender, EventArgs e)
        {
            int index = (int)numericUpDownIndex.Value;
            if (_pattern.Frames.Count > 1)
            {
                _pattern.RemoveFrame(index - 1);
                if (index > 1)
                    index--;
                UpdatePattern(index);
                RaiseHaptogramModified();
            }
            else
                MessageBox.Show("Cannot remove last frame. Please delete the haptogram instead.", "Remove last frame", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            Simulate f = new Simulate();
            f.SetPattern(_pattern);
            f.ShowDialog();
        }

        private void UpdatePenValue()
        {
            if (Int32.TryParse(textBoxPen.Text, out int pen) && pen >= 0 && pen <= 255)
            {
                patternUserContorl1.PenValue = pen;
                textBoxPen.BackColor = System.Drawing.SystemColors.Window;
            }
            else
                textBoxPen.BackColor = System.Drawing.Color.Red;
        }

        private void UpdateDurationValue()
        {
            int frameIndex = (int)numericUpDownIndex.Value - 1;

            if (_pattern == null || frameIndex < 0)
                return;

            if (Int32.TryParse(textBoxDuration.Text, out int res) && res > 0)
            {
                _pattern.Frames[frameIndex].Duration = res;
                textBoxDuration.BackColor = System.Drawing.SystemColors.Window;
            }
            else
                textBoxDuration.BackColor = System.Drawing.Color.Red;
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            int val = Convert.ToInt32(item.Tag);

            foreach (PatternUserContorl.Cell c in patternUserContorl1.Cells)
            {
                if (c.Selected)
                {
                    c.Selected = false;
                    c.Value = val;
                }
            }

            CopyCellValueToFrame();
            patternUserContorl1.Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EnterTextMessageBox f = new EnterTextMessageBox("Set value", "Value: ");
            if (f.ShowDialog() == DialogResult.OK) 
            {
                if (Int32.TryParse(f.Value, out int res))
                {
                    if (res >= 0 && res <= 255)
                    {
                        foreach (PatternUserContorl.Cell c in patternUserContorl1.Cells)
                        {
                            if (c.Selected)
                            {
                                c.Selected = false;
                                c.Value = res;
                            }
                        }
                        CopyCellValueToFrame();
                        patternUserContorl1.Invalidate();
                    }
                    else
                        MessageBox.Show("Only values between 0-255 are allowed", "Illegal pen value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Invalid number");
            }
        }
        private void PatternUserContorl1_ValuesUpdated(object sender, PatternUserContorl.ValuesUpdatedEventArgs e)
        {
            CopyCellValueToFrame();
        }

        private void PatternUserContorl1_AcrossFramesUpdated(object sender, PatternUserContorl.AcrossFramesEventArgs e)
        {
            int frameIndex = (int)numericUpDownIndex.Value - 1;
            bool isStartFrameIndex = true;
            int duration = _pattern.Frames[frameIndex].Duration;

            foreach (int i in e.IndexList)
            {
                if (frameIndex >= _pattern.Frames.Count)
                    _pattern.AddNewFrame(duration);

                // First frame
                if (isStartFrameIndex)
                {
                    for (int t = 1; t < e.IndexList.Count; t++)
                    {
                        int c = e.IndexList[t];

                        _pattern.Frames[frameIndex].Channels[c] = 0;
                    }

                    isStartFrameIndex = false;
                }
                else
                    _pattern.Frames[frameIndex].Channels[i] = patternUserContorl1.Cells[i].Value;


                frameIndex++;
            }

            SetFrame((int)numericUpDownIndex.Value);
            UpdateFrameCounter();
            patternUserContorl1.Invalidate();

            RaiseHaptogramModified();
        }

        private void CopyCellValueToFrame()
        {
            int frameIndex = (int)numericUpDownIndex.Value - 1;

            if (_pattern == null || frameIndex < 0)
                return;

            int max_values = Math.Max(_pattern.Frames[frameIndex].Channels.Length, patternUserContorl1.Cells.Length);

            for (int i = 0; i < max_values; i++)
            {
                _pattern.Frames[frameIndex].Channels[i] = patternUserContorl1.Cells[i].Value;
            }

            RaiseHaptogramModified();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            PlayPattern f = new PlayPattern
            {
                Pattern = _pattern
            };
            f.ShowDialog();
        }

        private void textBoxDuration_TextChanged(object sender, EventArgs e)
        {
            UpdateDurationValue();
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            UpdatePenValue();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {            
            foreach(PatternUserContorl.Cell c in patternUserContorl1.Cells)
            {                
                c.Value = patternUserContorl1.PenValue;
            }

            CopyCellValueToFrame();

            patternUserContorl1.Invalidate();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (PatternUserContorl.Cell c in patternUserContorl1.Cells)
            {
                c.Value = 0;
            }

            CopyCellValueToFrame();

            patternUserContorl1.Invalidate();
        }

        private void RaiseHaptogramModified()
        {
            HaptogramModified?.Invoke(this, new EventArgs());
        }
    }
}
