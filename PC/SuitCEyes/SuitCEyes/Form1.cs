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
    public partial class Form1 : Form
    {
        private Document _document;
        
        public Form1()
        {
            InitializeComponent();           
        }

        private void UpdateWindowTitle()
        {
            this.Text = "Haptogram Designer";

            if (_document == null)
            {
                return;
            }

            if (_document.IsModified)
                this.Text += " - *";
            else
                this.Text += " - ";

            if (string.IsNullOrEmpty(_document.Filename))
                this.Text += "Untitled";
            else
                this.Text += _document.Filename;
        }

        private bool ExitApplication()
        {
            if (!_document.IsModified)
                return true;

            DialogResult res = MessageBox.Show("You have unsaved changes. Do you want to save the document before exit?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                return Save();                
            }
            else if (res == DialogResult.No)
                 return true;

            return false;                    
        }

        #region Haptogram ListBox

        private void UpdateListbox()
        {
            listBoxPatterns.BeginUpdate();
            listBoxPatterns.Items.Clear();
            foreach(Pattern p in _document._patterns)
                listBoxPatterns.Items.Add(p);
            listBoxPatterns.EndUpdate();
        }
        private void toolStripButtonAddPattern_Click(object sender, EventArgs e)
        {
            NewPatternForm dia = new NewPatternForm();
            dia.AddTemplate(_document._templatePatterns);

            if(dia.ShowDialog() == DialogResult.OK)
            {
                string name = dia.HaptogramName;
                PatternTemplate template = dia.Template;

                if (_document.IsValidName(name))
                {
                    Pattern p = new Pattern(name, template);
                    p.AddNewFrame(250);
                    _document.AddNewPattern(p);
                    UpdateListbox();
                }
                else
                    MessageBox.Show("The name is already in used or contains illegal characters.", "Illegal name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            UpdateWindowTitle();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                string warn = "Are you sure you want to delete this haptogram?";
                if (MessageBox.Show(warn, "Delete haptogram?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Pattern a = listBoxPatterns.SelectedItem as Pattern;
                    _document.Delete(a);
                    UpdateListbox();
                    splitContainer1.Panel2.Controls.Clear();
                }
            }

            UpdateWindowTitle();
        }

        private void listBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();

            if (listBoxPatterns.SelectedItem != null)
            {
                Pattern selected = listBoxPatterns.SelectedItem as Pattern;
                EditPatternUserControl us = selected.Template.GetUserControl();               
                us.HaptogramModified += HaptogramModified;
                us.SetPattern(selected);

                us.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(us);
            }
        }

        private void HaptogramModified(object sender, EventArgs e)
        {
            if (_document.IsModified == false)
            {
                _document.IsModified = true;
                UpdateWindowTitle();
            }
           
        }

        #endregion

        public void NewDocumentWithTemplates()
        {
            _document = new Document();

            PatternTemplate t = new PatternTemplate("Grid 6x6", 6, 6);
            t.SetCellBackgroundColor(0 + 0, Color.LightSalmon);
            t.SetCellBackgroundColor(0 + 1, Color.LightSalmon);
            t.SetCellBackgroundColor(0 + 2, Color.LightSalmon);
            t.SetCellBackgroundColor(0 + 3, Color.LightSalmon);
            t.SetCellBackgroundColor(0 + 4, Color.LightSalmon);
            t.SetCellBackgroundColor(1 * 6 + 0, Color.LightSalmon);
            t.SetCellBackgroundColor(1 * 6 + 1, Color.LightSalmon);
            t.SetCellBackgroundColor(1 * 6 + 2, Color.LightSalmon);
            t.SetCellBackgroundColor(1 * 6 + 3, Color.LightSalmon);
            t.SetCellBackgroundColor(1 * 6 + 4, Color.LightSalmon);
            t.SetCellBackgroundColor(2 * 6 + 0, Color.LightSalmon);
            t.SetCellBackgroundColor(2 * 6 + 1, Color.LightSalmon);
            t.SetCellBackgroundColor(2 * 6 + 2, Color.LightSalmon);
            t.SetCellBackgroundColor(2 * 6 + 3, Color.LightSalmon);
            t.SetCellBackgroundColor(2 * 6 + 4, Color.LightSalmon);
            t.SetCellBackgroundColor(3 * 6 + 0, Color.LightSalmon);
            t.SetCellBackgroundColor(3 * 6 + 1, Color.LightSalmon);
            t.SetCellBackgroundColor(3 * 6 + 2, Color.LightSalmon);
            t.SetCellBackgroundColor(3 * 6 + 3, Color.LightSalmon);
            t.SetCellBackgroundColor(3 * 6 + 4, Color.LightSalmon);
            t.SetCellBackgroundColor(4 * 6 + 0, Color.LightSalmon);
            t.SetCellBackgroundColor(4 * 6 + 1, Color.LightSalmon);
            t.SetCellBackgroundColor(4 * 6 + 2, Color.LightSalmon);
            t.SetCellBackgroundColor(4 * 6 + 3, Color.LightSalmon);
            t.SetCellBackgroundColor(4 * 6 + 4, Color.LightSalmon);         
            _document._templatePatterns.Add(t);

            _document._templatePatterns.Add(new PatternTemplate("Grid 5x5", 5, 5));
            _document._templatePatterns.Add(new PatternTemplate("Grid 4x4", 4, 4));

            t = new PatternTemplate("Grid 3x3", 3, 3);
            t.SetRemapChannel(0, 2);
            t.SetRemapChannel(1, 1);
            t.SetRemapChannel(2, 0);
            _document._templatePatterns.Add(t);


        }

        private bool Save()
        {
            if (String.IsNullOrEmpty(_document.Filename))
            {
                return SaveAs();
            }
            else
            {
                _document.Save();
                UpdateWindowTitle();
            }
            

            return true;
        }

        private bool SaveAs()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _document.Filename = saveFileDialog1.FileName;
                return Save();
            }

            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check last opened document
            string lastOpened = Properties.Settings.Default.LastOpenDocument;
            if(string.IsNullOrEmpty(lastOpened))
                NewDocumentWithTemplates();
            else
            {
                _document = new Document();
                try
                {
                    _document.Load(lastOpened);
                }
                catch (Exception ex)
                {
                    Properties.Settings.Default.LastOpenDocument = "";
                    Properties.Settings.Default.Save();
                    _document = new Document();

                    MessageBox.Show(ex.Message, "Error while opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            UpdateListbox();
            UpdateWindowTitle();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitApplication())
                e.Cancel = true;
            else
            {
                if(_document != null && !String.IsNullOrEmpty(_document.Filename))
                    Properties.Settings.Default.LastOpenDocument = _document.Filename;
                Properties.Settings.Default.Save();
            }
        }

        private bool UnsavedChanged()
        {            
            if (_document.IsModified)
            {
                DialogResult res = MessageBox.Show("You have unsaved changes. Do you want to save the document before creating a new document?", "New", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (res == DialogResult.Cancel)
                {
                    return false;
                }
                else if (res == DialogResult.Yes)
                {
                    return Save();
                }
            }

            return true;
        }

        #region Menus

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UnsavedChanged())
            {
                NewDocumentWithTemplates();
                listBoxPatterns.Items.Clear();
                splitContainer1.Panel2.Controls.Clear();
                UpdateWindowTitle();
            }  
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About dia = new About();
            dia.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UnsavedChanged())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _document = new Document();
                    try
                    {
                        _document.Load(openFileDialog1.FileName);
                    }
                    catch(Exception ex)
                    {                        
                        _document = new Document();
                        
                        MessageBox.Show(ex.Message, "Error while opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    UpdateListbox();
                    splitContainer1.Panel2.Controls.Clear();
                    UpdateWindowTitle();

                }
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
       
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();            
        }

        #endregion

    }
}


