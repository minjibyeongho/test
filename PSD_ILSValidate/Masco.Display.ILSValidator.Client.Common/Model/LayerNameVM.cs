using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class LayerNameVM
    {
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public string Name { get; set; }
        public int LayerSeq { get; set; }
        public bool IsShow { get; set; }
        public bool IsVariable { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}",Index, ParentIndex, Name );
        }
    }
}
