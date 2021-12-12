using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitCEyes
{
    public class Frame
    {
        public int Duration { get; set; }
        public int[] Channels;

        public Frame(int no_channels)
        {
            Channels = new int[no_channels];
        }

        
    }
}
