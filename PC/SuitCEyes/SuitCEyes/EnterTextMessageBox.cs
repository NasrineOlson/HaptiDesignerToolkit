using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class EnterTextMessageBox : Form
    {
        public string Value { 
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public EnterTextMessageBox(string caption, string label)
        {
            InitializeComponent();

            this.Text = caption;
            label1.Text = label;
            textBox1.Text = Value;
        }
    }
}
