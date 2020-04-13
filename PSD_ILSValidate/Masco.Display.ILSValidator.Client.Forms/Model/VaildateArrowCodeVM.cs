using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class VaildateArrowCodeVM
    {
        public VaildateArrowCodeVM()
        {
            Pattern_Id = string.Empty;
            Arrow_Id = string.Empty;
            ILS_Type = string.Empty;
            ResultMessage = string.Empty;
            FileName = string.Empty;
        }
        public int Index { get; set; }
        public string Pattern_Id { get; set; }
        public string Arrow_Id { get; set; }
        public string ILS_Type { get; set; }
        [DefaultValue(false)]
        public bool Result { get; set; }
        public string ResultMessage { get; set; }
        public string FileName { get; set; }
    }
}
