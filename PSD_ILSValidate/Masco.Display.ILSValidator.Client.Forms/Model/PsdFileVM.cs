using Masco.Display.ILSValidator.Client.Common;
using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class PsdFileVM : ModelBase<PsdFileVM>
    {
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
