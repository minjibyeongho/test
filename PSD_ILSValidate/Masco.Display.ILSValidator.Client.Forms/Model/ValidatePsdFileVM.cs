using Masco.Display.ILSValidator.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class ValidatePsdFileVM : ModelBase<ValidatePsdFileVM>
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public string ILS_Type { get; set; }
        public int TotalCount { get; set; }
        public int Success { get; set; }
        public int Fail { get; set; }
        public string Description { get; set; }


        
    }
}
