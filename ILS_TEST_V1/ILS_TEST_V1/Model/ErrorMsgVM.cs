using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
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
