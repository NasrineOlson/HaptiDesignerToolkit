using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class PlayPattern : Form
    {
        public Pattern Pattern { get; set; }

        private enum ProgressType
        {
            Info,
            Error,
            Warning,
            Command,
            Response
        }

        public PlayPattern()
        {
            InitializeComponent();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            try
            {
                string com_port = e.Argument as string;

                bw.ReportProgress((int)ProgressType.Info, "Opening port " + com_port);

                using (SerialPort port = new SerialPort(com_port, 9600, Parity.None, 8, StopBits.One))
                {
                    port.ReadTimeout = 1000;
                    port.NewLine = "]";
                    port.DtrEnable = true;
                    port.Open();
                    bw.ReportProgress((int)ProgressType.Info, "Initializing communication");

                    HardwareInfo info = new HardwareInfo(sendCommand(bw, port, "V"));
                    bw.ReportProgress((int)ProgressType.Info, "Firmware version: " + info.Version);
                    bw.ReportProgress((int)ProgressType.Info, "Max frames in haptogram: " + info.MaxFrames);
                    bw.ReportProgress((int)ProgressType.Info, "Max channels per frame: " + info.MaxChannels);

                    // Verify compatiblity with current pattern
                    if( Pattern.Frames.Count > info.MaxFrames)                    
                        bw.ReportProgress((int)ProgressType.Warning, "WARNING: Haptogram contains more frames that hardware supports. The animation will be truncated.");
                    if (Pattern.Frames[0].Channels.Length > info.MaxChannels)
                        bw.ReportProgress((int)ProgressType.Warning, "WARNING: Haptogram contains more channels that hardware supports. The channels will be truncated.");
                    if (Pattern.Frames[0].Channels.Length < info.MaxChannels)
                        bw.ReportProgress((int)ProgressType.Warning, "WARNING: Haptogram contains fewer channels that hardware supports. The remaining will be set to zero.");

                    bw.ReportProgress((int)ProgressType.Info, "\nResets all frames");
                    sendCommand(bw, port, "R");

                    bw.ReportProgress((int)ProgressType.Info, "\nLoad all frames for pattern \"" + Pattern.Name + "\"");
                    int no_frames = Math.Min(info.MaxFrames, Pattern.Frames.Count);

                    for(int frame_counter = 0; frame_counter < no_frames; frame_counter++)
                    {
                        string msg = "F," + Pattern.Frames[frame_counter].Duration + ",";                        

                        for (int i = 0; i < info.MaxChannels; i++)
                        {
                            if (i < Pattern.Frames[frame_counter].Channels.Length)
                                msg += Pattern.Frames[frame_counter].Channels[Pattern.Template.RemapChannel(i)];
                            else
                                msg += "0";

                            if(i < info.MaxChannels - 1)
                                msg += ",";
                        }

                        sendCommand(bw, port, msg);
                    }
                    bw.ReportProgress((int)ProgressType.Info, "\nPlay frames");
                    sendCommand(bw, port, "P");

                    // Wait for it to run through all frames
                    port.ReadTimeout = 10000;
                    string reply = port.ReadLine();
                    reply += "]";
                    bw.ReportProgress((int)ProgressType.Info, "\nAll frames played. Stopping.");

                }
                bw.ReportProgress((int)ProgressType.Info, "Closing port");
            }
            catch(Exception ex)
            {
                bw.ReportProgress((int)ProgressType.Error, "ERROR: " + ex.Message);
            }
        }

        private string sendCommand(BackgroundWorker bw, SerialPort port, string cmd)
        {
            string c = "[" + cmd + "]";
            port.Write(c);            
            bw.ReportProgress((int)ProgressType.Command, c);            
            string reply = port.ReadLine();
            reply += "]";
            bw.ReportProgress((int)ProgressType.Response, reply);

            // Extract only the response code
            string code = reply.Substring(reply.LastIndexOf('['));
            if(code[1] == 'E')
            {
                int index = code.IndexOf(",") + 1;
                int length = code.LastIndexOf("]") - index;
                int error_code = Convert.ToInt32(code.Substring(index, length));              
                throw new Exception(ConvertArduinoErrorToHumanable(error_code));
            }

            return reply.Substring(0, reply.LastIndexOf('['));
        }

        private string ConvertArduinoErrorToHumanable(int ret)
        {
            switch(ret) {
                case 1:
                    return "Unknown command";
                case 2:
                    return "Illegal parameter";
                case 3:
                    return "Frames missing";
                case 4:
                    return "Out of memory";
                case 5:
                    return "End of file";
            }

            return "Untranslated error message (" + ret + ")";
        }

   
        private void ScanSerialPorts()
        {
            toolStripSerialports.Items.Clear();
            toolStripSerialports.Items.AddRange(SerialPort.GetPortNames());
            toolStripSerialports.Enabled = toolStripSerialports.Items.Count > 0;
            toolStripSerialports.SelectedIndex = 0;
            buttonStop.Enabled = false;
            buttonPlay.Enabled = toolStripSerialports.Enabled;
        }

        private void PlayPattern_Load(object sender, EventArgs e)
        {
            ScanSerialPorts();            
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            string com_port = toolStripSerialports.SelectedItem as string;

            backgroundWorker1.RunWorkerAsync(com_port);

            buttonStop.Enabled = true;
            buttonPlay.Enabled = false;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {        
            buttonStop.Enabled = false;
            buttonPlay.Enabled = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = e.UserState as string;
            ProgressType type = (ProgressType) e.ProgressPercentage;
            
            switch (type)
            {
                case ProgressType.Info:                    
                    AppendText(msg, new Font("Microsoft Sans Serif", 8, FontStyle.Regular), System.Drawing.Color.Black, true);
                    break;
                case ProgressType.Warning:
                    AppendText(msg, new Font("Microsoft Sans Serif", 8, FontStyle.Regular), System.Drawing.Color.LightSalmon, true);
                    break;
                case ProgressType.Error:                    
                    AppendText(msg, new Font("Microsoft Sans Serif", 8, FontStyle.Regular), System.Drawing.Color.Red, true);
                    break;
                case ProgressType.Command:                    
                    AppendText(msg, new Font("Courier New", 8, FontStyle.Regular), System.Drawing.Color.Green, true);
                    break;
                case ProgressType.Response:                                        
                    AppendText(msg, new Font("Courier New", 8, FontStyle.Regular), System.Drawing.Color.Blue, true);
                    break;                
            }
            
            
        }

        private void toolStripButtonReReadSerial_Click(object sender, EventArgs e)
        {
            ScanSerialPorts();
        }

        public void AppendText(string text, Font font, Color color, bool addNewLine = false)
        {
            richTextBox1.SuspendLayout();
            richTextBox1.SelectionColor = color;
            richTextBox1.SelectionFont = font;
            richTextBox1.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            richTextBox1.ScrollToCaret();
            richTextBox1.ResumeLayout();
        }

        public class HardwareInfo
        {
            public string Version { get; }
            public int MaxFrames { get; }
            public int MaxChannels { get; }

            public HardwareInfo(string info)
            {
                string[] v = info.Split(new char[] { ';' });
                if(v.Length != 3)
                {
                    throw new Exception("Unable to parse hardware information");
                }

                Version = v[0].Trim();
                MaxFrames = Convert.ToInt32(v[1].Trim());
                MaxChannels = Convert.ToInt32(v[2].Trim());
            }
        }
    }
}
