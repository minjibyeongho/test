using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
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


        public void logPrint()
        {
            Console.WriteLine(
                   "Index : {0}, FileName : {1}, ILS_Type : {2}, TotalCount : {3}, Success : {4}, Fail : {5}, Descrption : {6}",
                   Index, FileName, ILS_Type, TotalCount, Success, Fail, Description
            );
        }

    }
}
