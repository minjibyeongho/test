using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
{
    public class Validate1
    {

        IList<ValidateVM> _vmList = new List<ValidateVM>();
        //int Count = 0;

        //public const string MC_001 = 

        public Validate1()
        {
            InitCode();

            

        }

        private void InitCode()
        {
            var enums = Masco.Core.Helper.Refrection.GetEnums<ValidationCodeType>();
            var index = 1;
            var type = typeof(ValidationCodeType);
            foreach (var kv in enums)
            {
                ValidateVM item = new ValidateVM();
                item.INDEX = index++;
                item.CODE = kv.Key;
                item.CHECK = false;
                item.TITLE = kv.Value;


                var enumString = kv.Key.ToString();

                if (string.IsNullOrEmpty(enumString) == false)
                {
                    var arr = enumString.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 1)
                        item.ILSType = arr[1];
                }

                _vmList.Add(item);
            }
        }

        public IList<ValidateVM> GetList()
        {
            return _vmList;
        }

    }
}
