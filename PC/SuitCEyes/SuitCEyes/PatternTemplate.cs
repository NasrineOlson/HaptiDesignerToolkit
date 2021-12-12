using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuitCEyes
{
    public class PatternTemplate
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Dictionary<int, Color> CellBackgroundColorDict;
        public Dictionary<int, int> RemapChannelsDict;

        private EditPatternUserControl editUserControl = null;
        private PatternUserContorl simulateUserControl = null;

        public int NumberOfChannels { get { return Width* Height; } }
        
        public PatternTemplate(string name, int w, int h)
        {
            Name = name;
            Width = w;
            Height = h;
        }

        public override string ToString()
        {
            return Name;
        }

        public void SetRemapChannel(int index, int channel)
        {
            if (RemapChannelsDict == null)
                RemapChannelsDict = new Dictionary<int, int>();

            RemapChannelsDict[index] = channel;
        }

        public void SetCellBackgroundColor(int index, Color color)
        {
            if(CellBackgroundColorDict == null)           
                CellBackgroundColorDict = new Dictionary<int, Color>();
            
            CellBackgroundColorDict[index] = color;
        }

        public Color GetCellBackgroundColor(int index)
        {
            Color color = SystemColors.Control;

            if (CellBackgroundColorDict != null && CellBackgroundColorDict.ContainsKey(index))
            {
                color = CellBackgroundColorDict[index];
            }

            return color;
        }

        internal EditPatternUserControl GetUserControl()
        {
            if(editUserControl == null)
            {
                editUserControl = new EditPatternUserControl();

                editUserControl.SetGridSize(Width, Height);

                // Setup all background colors
                if (CellBackgroundColorDict != null) {
                    foreach (KeyValuePair<int, Color> entry in CellBackgroundColorDict)
                    {
                        editUserControl.SetCellBackgroundColor(entry.Key, entry.Value);
                    }
                }
            }

            editUserControl.ClearEventHandler();           

            return editUserControl;
        }

        internal PatternUserContorl GetSimulateUserControl()
        {
            if (simulateUserControl == null)
            {
                simulateUserControl = new PatternUserContorl
                {
                    ReadOnly = true
                };
                simulateUserControl.SetGridSize(Width, Height);

                // Setup all background colors
                if (CellBackgroundColorDict != null)
                {
                    foreach (KeyValuePair<int, Color> entry in CellBackgroundColorDict)
                    {
                        simulateUserControl.Cells[entry.Key].BackgroundColor = entry.Value;                        
                    }
                }
            }

            return simulateUserControl;
        }

        internal PatternUserContorl GetUserControlPattern()
        {
            EditPatternUserControl us = GetUserControl();
            return us.GetPatterUsercontrol();
        }

        internal int RemapChannel(int i)
        {
            if (RemapChannelsDict != null && RemapChannelsDict.ContainsKey(i))
            {
                return RemapChannelsDict[i];
            }

            return i;
        }
    }
}
