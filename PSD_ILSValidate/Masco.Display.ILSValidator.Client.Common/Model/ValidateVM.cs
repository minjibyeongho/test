using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public enum ResultType
    {
        Notset,
        Fail,
        Success
    }

    public class ValidateVM
    {
        public int INDEX { get; set; }
        public ValidationCodeType CODE { get; set; }
        public string ILSType { get; set; }
        public bool CHECK { get; set; }
        public string TITLE { get; set; }

        public ResultType ResultState  { get; set; }
        //public int ErrorCount {get {return _ErrorMsg.Count();}}

        public ValidateVM()
        {
        }
        
    }
}
