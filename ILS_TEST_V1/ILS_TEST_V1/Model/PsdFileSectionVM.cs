using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
{
    public class PsdFileSectionVM : ModelBase<PsdFileSectionVM>
    {
        public int NumberOfChannels { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        //public Ntreev.Library.Psd.ColorMode ColorMode { get; set; }
        public string ColorMode { get; set; }
        public string ChannelTypes { get; set; }
        public string Pixel { get; set; }

    }
}
