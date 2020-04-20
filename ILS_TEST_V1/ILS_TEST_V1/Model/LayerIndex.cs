using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ILS_TEST_V1.Model
{
    public class LayerIndex
    {
        public static string GetName<T>(int index)
        {
            var type = typeof(T);
            var members = type.GetMembers();
            var result = string.Empty;
            foreach (var m in members.Where(x => x.MemberType == System.Reflection.MemberTypes.Field))
            {
                var obj = m.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                if (obj == null) continue;

                var desc = ((DescriptionAttribute)obj).Description;
                var value = (int)type.GetField(m.Name).GetValue(m);
                if (value != index)
                    continue;

                result = desc;
                break;
            }
            return result;
        }

        public class LayerInfoAttribute : Attribute
        {
            public int Index { get; set; }
            public int ParentIndex { get; set; }
            public int Depth { get; set; }
            public int Seq { get; set; }


            public LayerInfoAttribute(int index, int parentIndex, int depth, int seq)
            {
                Index = index;
                ParentIndex = parentIndex;
                Depth = depth;
                Seq = seq;
            }
        }

        public class CM
        {
            [LayerInfo(1, 2, 2, 1)]
            [Description("Night Sky")]
            public const int Night_Sky = 1;

            //[LayerInfo(1, 2, 2, 1)]
            [Description("Night_on_Sky")]
            public const int Night_on_Sky = 2;

            [Description("Day Sky")]
            public const int DaySky = 3;

            [Description("Day_on_Sky")]
            public const int Day_on_Sky = 4;

            [Description("Road_A")]
            public const int Road_A = 5;

            [Description("Road_B")]
            public const int Road_B = 6;

            [Description("Main")]
            public const int Main = 7;

            [Description("Night_Filter_A")]
            public const int Night_Filter_A = 8;

            [Description("Night_Filter_B")]
            public const int Night_Filter_B = 9;

            [Description("Night_on")]
            public const int Night_on = 10;

            [Description("Direction")]
            public const int Direction = 11;

            [Description("Direction_Sub")]
            public const int Direction_Sub = 12;

            [Description("Transparency_Color")]
            public const int Transparency_Color = 13;

            [Description("Road Background_Color")]
            public const int Road_Background_Color = 14;

            ////[Description("Arrow")]
            ////public const int Arrow_KRCA_Arrow = 15;

            //[Description("Transparency")]
            //public const int Arrow_KRCA_Transparency = 16;

            //[Description("Direction")]
            //public const int Arrow_KRCA_Direction = 17;

            ////[Description("Arrow_KRCA")]
            ////public const int Arrow_KRCA = 18;

            //[Description("Arrow_")]
            //public const int Arrow_ = 18;

            //[Description("Arrow")]
            //public const int Arrow = 19;


            public static string GetName(int index)
            {
                return LayerIndex.GetName<LayerIndex.NC>(index);
            }
        }

        public class NC : CM
        {
            [Description("Arrow")]
            public const int Arrow_KRCA_Arrow = 15;

            [Description("Transparency")]
            public const int Arrow_KRCA_Transparency = 16;

            [Description("Direction")]
            public const int Arrow_KRCA_Direction = 17;

            [Description("Arrow_KRCA")]
            public const int Arrow_KRCA = 18;

            [Description("Arrow")]
            public const int Arrow = 19;

            public const int FixedLayerIndex = Road_Background_Color;

            public static string GetName(int index)
            {
                if (index < LayerIndex.NC.FixedLayerIndex)
                {
                    return LayerIndex.GetName<LayerIndex.CM>(index);
                }
                else
                    return LayerIndex.GetName<LayerIndex.NC>(index);
            }
        }

        public class JC : CM
        {

            [Description("Arrow")]
            public const int Arrow_KRJA_Arrow = 15;

            [Description("Transparency")]
            public const int Arrow_KRJA_Transparency = 16;

            [Description("Direction")]
            public const int Arrow_KRJA_Direction = 17;

            [Description("Arrow_KRJA")]
            public const int Arrow_KRJA = 18;

            [Description("Arrow")]
            public const int Arrow = 19;


            public const int FixedLayerIndex = Road_Background_Color;

            public static string GetName(int index)
            {
                if (index < LayerIndex.JC.FixedLayerIndex)
                {
                    return LayerIndex.GetName<LayerIndex.CM>(index);
                }
                else
                    return LayerIndex.GetName<LayerIndex.JC>(index);
            }
        }

        public class CE
        {

            [LayerInfo(1, 2, 2, 1)]
            [Description("야간하늘_M")]
            public const int L01_Night_Sky = 1;

            //[LayerInfo(1, 2, 2, 1)]
            [Description("야간_on_하늘")]
            public const int L02_Night_on_Sky = 2;

            [Description("주간하늘_M")]
            public const int L03_DaySky = 3;

            [Description("주간_on_하늘")]
            public const int L04_Day_on_Sky = 4;

            [Description("도로면패턴")]
            public const int L05_Road_A = 5;

            [Description("도로면검정")]
            public const int L06_Road_B = 6;

            [Description("메인")]
            public const int L07_Main = 7;

            [Description("야간필터")]
            public const int L08_Night_Filter_A = 8;

            [Description("야간_on")]
            public const int L09_Night_on = 9;

            [Description("Direction")]
            public const int L10_Direction = 10;

            [Description("Direction_Sub")]
            public const int L11_Direction_Sub = 11;

            [Description("투명색(255/0/255)")]
            public const int L12_Transparency_Color = 12;

            [Description("도로색배경")]
            public const int L13_Road_Background_Color = 13;

            [Description("D0")]
            public const int L14_D0 = 14;

            [Description("Transparency")]
            public const int L15_Transperency = 15;

            [Description("Direction")]
            public const int L16_Direction = 16;

            [Description("화살표")]
            public const int L17_Arrow = 17;


            public static int GetFixedLayerIndex()
            {
                return L09_Night_on;
            }

            public static bool L14_DO_StringStartWith(string sourceName, string name)
            {
                return sourceName.StartsWith(name, StringComparison.CurrentCultureIgnoreCase);
            }
        }


        public class ET
        {
            [Description("Night Sky")]
            public const int L01_Night_Sky = 1;

            //[LayerInfo(1, 2, 2, 1)]
            [Description("Night_on_Sky")]
            public const int L02_Night_on_Sky = 2;

            [Description("Day Sky")]
            public const int L03_DaySky = 3;

            [Description("Day_on_Sky")]
            public const int L04_Day_on_Sky = 4;

            [Description("Road_A")]
            public const int L05_Road_A = 5;

            [Description("Road_B")]
            public const int L06_Road_B = 6;

            [Description("Main")]
            public const int L07_Main = 7;

            [Description("Night_Filter_A")]
            public const int L08_Night_Filter_A = 8;

            [Description("Night_Filter_B")]
            public const int L09_Night_Filter_B = 9;

            [Description("Night_on")]
            public const int L10_Night_on = 10;

            [Description("Hipass Red Road")]
            public const int L11_Hipass_Red_Road = 11;

            [Description("Hipass Arrow")]
            public const int L12_Hipass_Arrow = 12;

            [Description("Direction_Main")]
            public const int L13_Direction_Main = 13;

            [Description("Direction_Road")]
            public const int L14_Direction_Road = 14;

            [Description("Column")]
            public const int L15_Column = 15;

            [Description("Hipass↓")]
            public const int L16_Hipass1 = 16;

            [Description("Hipass")]
            public const int L17_Hipass = 17;

            [Description("Sign post")]
            public const int L18_Sign_post = 18;

            [Description("ETC_")]
            public const int L19_ETC_ = 19;

            [Description("ETC")]
            public const int L20_ETC = 20;

            public static int GetFixedLayerIndex()
            {
                return L10_Night_on;
            }
            public static bool L19_ETC_StringStartWith(string sourceName, string name)
            {
                return sourceName.StartsWith(name, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public class MimeticDiagram
        {
            [Description("Night Sky")]
            public const int L01_Night_Sky = 1;

            [Description("Night_on_Sky")]
            public const int L02_Night_on_Sky = 2;

            [Description("Day Sky")]
            public const int L03_DaySky = 3;

            [Description("Day_on_Sky")]
            public const int L04_Day_on_Sky = 4;

            [Description("Road_A")]
            public const int L05_Road_A = 5;
            ////////////////////////////////////////////
            [Description("Post")]
            public const int L06_Post = 6;

            [Description("Post")]
            public const int L07_Post = 7;

            [Description("Main")]
            public const int L08_Main = 8;

            [Description("Night")]
            public const int L09_Night = 9;

            [Description("Post")]
            public const int L10_Post = 10;

            [Description("Post")]
            public const int L11_Post = 11;

            [Description("Moon_")]
            public const int L12_Moon = 12;

            [Description("Moon_")]
            public const int L13_Moon = 13;

            [Description("Night_on")]
            public const int L14_Night_on = 14;

            [Description("Direction")]
            public const int L15_Direction = 15;

            [Description("Direction_Sub")]
            public const int L16_Direction_Sub = 16;

            [Description("Transparency_Color")]
            public const int L17_Transparency_Color = 17;

            [Description("Arrow")]
            public const int L18_Arrow_0_Arrow = 18;

            [Description("Direction")]
            public const int L19_Arrow_0_Direction = 19;

            [Description("Arrow_")]
            public const int Arrow_ = 20;

            [Description("Arrow")]
            public const int L21_Arrow = 21;

            public static int GetFixedLayerIndex()
            {
                return L17_Transparency_Color;
            }

            public static bool Arrow_StringStartWith(string sourceName, string name)
            {
                return sourceName.StartsWith(name, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public class CrossRoadPoint3D
        {
            [Description("Night Sky")]
            public const int L01_Night_Sky = 1;

            [Description("Night Main")]
            public const int L02_Night_Main = 2;

            [Description("Night on")]
            public const int L03_Night_on = 03;

            [Description("Day Sky")]
            public const int L04_DaySky = 4;

            [Description("Day Main")]
            public const int L05_Day_on_Sky = 5;

            [Description("Day on")]
            public const int L06_Day_on = 6;

            [Description("Transparency Color")]
            public const int L07_Transparency_Color = 07;

            [Description("Road Background Color")]
            public const int L08_Road_Background_Color = 08;

            [Description("_AI")]
            public const int _Arrow_AI = 09;

            [Description("Arrow")]
            public const int L11_Arrow = 11;

            public static int GetFixedLayerIndex()
            {
                return L08_Road_Background_Color;
            }
            public static bool L10_00000000_AI_StringStartWith(string sourceName, string name)
            {
                return sourceName.StartsWith(name, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public class RestAreaSummaryMap_Gini
        {
            [Description("Title_set")]
            public const int L01_Title_set = 1;

            [Description("In_out")]
            public const int L02_In_out = 2;

            [Description("Icon")]
            public const int L03_Icon = 3;

            [Description("Tree")]
            public const int L04_Tree = 4;

            [Description("Building_set")]
            public const int L05_Building_set = 5;

            [Description("Ground")]
            public const int L06_Ground = 6;

            [Description("B_ground")]
            public const int L07_B_ground = 7;

            public static int GetFixedLayerIndex()
            {
                return L07_B_ground;
            }
        }

        public class RestAreaSummaryMap_Mapy
        {
            [Description("Title")]
            public const int L01_Title = 1;

            [Description("In_out")]
            public const int L02_In_out = 2;

            [Description("Logo")]
            public const int L03_Logo = 3;

            [Description("Icon")]
            public const int L04_Icon = 4;

            [Description("Arrow")]
            public const int L05_Arrow = 5;

            [Description("Tree")]
            public const int L06_Tree = 6;

            [Description("Building_set")]
            public const int L07_Building_set = 7;

            [Description("Ground")]
            public const int L08_Ground = 8;

            [Description("Mappy_ground")]
            public const int L09_Mappy_ground = 9;

            public static int GetFixedLayerIndex()
            {
                return L09_Mappy_ground;
            }
        }



    }
}
