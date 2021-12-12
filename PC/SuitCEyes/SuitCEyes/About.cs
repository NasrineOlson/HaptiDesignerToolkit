using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            labelVersion.Text = "Version: " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }
    }
}
