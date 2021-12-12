using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitCEyes
{
    public class Pattern
    {       
        public string Name { get; set; }
        public PatternTemplate Template { get; set; }
        
        public List<Frame> Frames = new List<Frame>();

        public Pattern(string name, PatternTemplate template)
        {
            Name = name;
            Template = template;       
        }

        public void AddNewFrame(int duration, int[] channels)
        {
            Frame frame = new Frame(Template.NumberOfChannels);

            if( channels.Length > Template.NumberOfChannels)
            {
                throw new Exception("Too many channels");
            }

            frame.Duration = duration;
            Array.Copy(channels, frame.Channels, channels.Length);
            Frames.Add(frame);
        }

        public void AddNewFrame(int duration)
        {
            AddNewFrame(duration, new int[Template.NumberOfChannels]);
        }

        public void RemoveFrame(int index)
        {
            Frames.Remove(Frames[index]);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
