using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class ILSType
    {
        public const string NotSet = "";
        public const string NC = "일반교차로";
        public const string JC = "JC";
        public const string CE = "도시고속";
        public const string ET = "ETC";
        public const string MD = "MimeticDiagram";
        public const string CR3D = "CrossRoadPoint3D";
        public const string RASMG = "RestAreaSummaryMap_Gini";
        public const string RASMM = "RestAreaSummaryMap_Mapy";

        IList<KeyValuePair<int, string>> _dic = new List<KeyValuePair<int, string>>();

        public ILSType()
        {
            _dic.Add(new KeyValuePair<int, string>(0, NotSet));
            _dic.Add(new KeyValuePair<int, string>(1, NC));
            _dic.Add(new KeyValuePair<int, string>(2, JC));
            _dic.Add(new KeyValuePair<int, string>(3, CE));
            _dic.Add(new KeyValuePair<int, string>(4, ET));
            _dic.Add(new KeyValuePair<int, string>(5, MD));
            _dic.Add(new KeyValuePair<int, string>(6, CR3D));
            _dic.Add(new KeyValuePair<int, string>(7, RASMG));
            _dic.Add(new KeyValuePair<int, string>(8, RASMM));
        }

        public IList<KeyValuePair<int, string>> GetList()
        {
            return _dic.Where(x => x.Key > 0).ToList();
        }

        public int GetKey(string typeName)
        {
            foreach (var x in _dic)
            {
                if (x.Value == typeName)
                    return x.Key;
            }
            return 0;
        }

        public string GetValue(int key)
        {
            
            if (_dic.Where(x => x.Key == key).Any() == false)
                return string.Empty;

            var item = _dic.First(x => x.Key == key);
            return item.Value;
        }
    }
}
