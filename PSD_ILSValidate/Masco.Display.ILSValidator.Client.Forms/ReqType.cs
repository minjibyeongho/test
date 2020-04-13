using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class ReqType
    {
        public const string Text0 = "";
        public const string Text1 = "신규";
        public const string Text2 = "수정";
        public const string Text3 = "삭제";

        IList<KeyValuePair<int, string>> _dic = new List<KeyValuePair<int, string>>();

        public ReqType()
        {
            _dic.Add(new KeyValuePair<int, string>(0, Text0));
            _dic.Add(new KeyValuePair<int, string>(1, Text1));
            _dic.Add(new KeyValuePair<int, string>(2, Text2));
            _dic.Add(new KeyValuePair<int, string>(3, Text3));
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
