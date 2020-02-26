using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1
{
    class ILSType
    {
        // 멤버변수
        public const string Text0 = "";
        public const string Text1_NC = "일반교차로";
        public const string Text2_JC = "JC";
        public const string Text3_CE = "도시고속";
        public const string Text4_ET = "ETC";
        public const string Text5_MimeticDiagram = "모식도()";
        public const string Text6_CrossRoadPoint3D = "3D 교차로";
        public const string Text7_RestAreaSummaryMap_Mapy = "휴게소 요약맵(맵피)";
        public const string Text8_RestAreaSummaryMap_Gini = "휴게소 요약맵(지니)";

        public const string Code1_NC = "NC";
        public const string Code2_JC = "JC";
        public const string Code3_CE = "CE";
        public const string Code4_ET = "ET";
        public const string Code5_MimeticDiagram = "MimeticDiagram";
        public const string Code6_CrossRoadPoint3D = "CrossRoadPoint3D";
        public const string Code7_RestAreaSummaryMap_Mapy = "RestAreaSummaryMap_Mapy";
        public const string Code8_RestAreaSummaryMap_Gini = "RestAreaSummaryMap_Gini";
        
        public const string FilePrefix1_NC = "KRCM";
        public const string FilePrefix2_JC = "KRJM";
        public const string FilePrefix3_CE = "90";
        public const string FilePrefix4_ET = "KREI";

        IList<KeyValuePair<int, string>> _dic = new List<KeyValuePair<int, string>>(); // 각 멤버변수를 담을 그릇


        public ILSType()    // 생성자
        {
            _dic.Add(new KeyValuePair<int, string>(0, Text0));
            _dic.Add(new KeyValuePair<int, string>(1, Text1_NC));
            _dic.Add(new KeyValuePair<int, string>(2, Text2_JC));
            _dic.Add(new KeyValuePair<int, string>(3, Text3_CE));
            _dic.Add(new KeyValuePair<int, string>(4, Text4_ET));
            _dic.Add(new KeyValuePair<int, string>(5, Text5_MimeticDiagram));
            _dic.Add(new KeyValuePair<int, string>(6, Text6_CrossRoadPoint3D));
            _dic.Add(new KeyValuePair<int, string>(7, Text7_RestAreaSummaryMap_Mapy));
            _dic.Add(new KeyValuePair<int, string>(8, Text8_RestAreaSummaryMap_Gini));
        }

    }
}
