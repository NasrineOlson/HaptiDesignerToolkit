using System.Collections.Generic;
using System.Windows.Forms;

namespace SuitCEyes
{
    public partial class NewPatternForm : Form
    {
        public PatternTemplate Template { get { return comboBoxTemplate.SelectedItem as PatternTemplate; } }
        public string HaptogramName { get { return textBoxName.Text; } }

        public NewPatternForm()
        {
            InitializeComponent();
        }

        internal void AddTemplate(List<PatternTemplate> templatePatterns)
        {
            comboBoxTemplate.Items.Clear();
            comboBoxTemplate.Items.AddRange(templatePatterns.ToArray());

            if(comboBoxTemplate.Items.Count > 0)
                comboBoxTemplate.SelectedIndex = 0;
        }

        private void NewPatternForm_Shown(object sender, System.EventArgs e)
        {
            textBoxName.Focus();
        }
    }
}
