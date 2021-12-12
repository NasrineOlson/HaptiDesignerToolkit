using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class Simulate : Form
    {
        private Pattern _pattern;

        public Simulate()
        {
            InitializeComponent();          
        }
        private void Simulate_Load(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        public void SetPattern(Pattern pattern)
        {
            _pattern = pattern;
            panel1.Controls.Clear();
            PatternUserContorl uc = _pattern.Template.GetSimulateUserControl();
            uc.SetFrame(_pattern.Frames[0]);
            SetFrameInfo(1, _pattern.Frames.Count, _pattern.Frames[0].Duration);
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

            backgroundWorker1.RunWorkerAsync(_pattern);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            Pattern pattern = e.Argument as Pattern;

            int length = pattern.Frames.Count;

            for(int i = 0; i < length; i++)
            {              
                int duration = pattern.Frames[i].Duration;
                backgroundWorker1.ReportProgress(i, pattern.Frames[i]);
                Thread.Sleep(duration);

                if (bw.CancellationPending)
                    break;
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
      
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Frame currentFrame = e.UserState as Frame;

            SetFrameInfo((int)e.ProgressPercentage + 1, _pattern.Frames.Count, currentFrame.Duration);

            PatternUserContorl uc = panel1.Controls[0] as PatternUserContorl;
            uc.SetFrame(currentFrame);
        }

        private void SetFrameInfo(int currentFrame, int maxFrame, int duration)
        {
            textBoxFrame.Text = String.Format("{0}/{1}", currentFrame, maxFrame);
            textBoxDuration.Text = String.Format("{0}", duration);

        }

    }
}
