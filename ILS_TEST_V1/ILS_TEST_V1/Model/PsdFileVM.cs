using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Ntreev.Library.Psd;

namespace ILS_TEST_V1.Model
{
    public class PsdFileVM : ModelBase<PsdFileVM>
    {
        public string Name { get; set; }
        public string Extension { get; set; }

        // Model 요소 확인용
        public void ComponentPrint()
        {
            Console.WriteLine("Name : {0}, Extension : {1}", Name, Extension);
        }

    }
}
