using Masco.Display.ILSValidator.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class ErrorMsgVM
    {
        public int Index { get; set; }
        public ErrorMsgLevel Level { get; set; }
        public ValidationCodeType Code { get; set; }
        public int LayerIDX { get; set; }
        public string ErrorMsg { get; set; }
    }
}
