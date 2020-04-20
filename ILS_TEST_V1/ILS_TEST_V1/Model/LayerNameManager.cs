using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILS_TEST_V1.Model
{
    public class LayerNameManager
    {
        private static LayerNameManager _instance = null;
        public static LayerNameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LayerNameManager();
                return _instance;
            }
        }

        public IList<LayerNameVM> CMList = new List<LayerNameVM>();
        public IList<LayerNameVM> NCList = new List<LayerNameVM>();
        public IList<LayerNameVM> JCList = new List<LayerNameVM>();
        public IList<LayerNameVM> CEList = new List<LayerNameVM>();
        public IList<LayerNameVM> ETList = new List<LayerNameVM>();
        public IList<LayerNameVM> MDList = new List<LayerNameVM>();
        public IList<LayerNameVM> CR3DList = new List<LayerNameVM>();
        public IList<LayerNameVM> RASMGList = new List<LayerNameVM>();
        public IList<LayerNameVM> RASMMList = new List<LayerNameVM>();


        public int NormalCross_MainCode_Length { get; private set; }
        public int NormalCross_ArrowCode_Length { get; private set; }

        public int JC_MainCode_Length { get; private set; }
        public int JC_ArrowCode_Length { get; private set; }

        public int CE_MainCode_Length { get; private set; }
        public int CE_ArrowCode_Length { get; private set; }

        public int ET_MainCode_Length { get; private set; }
        public int ET_ArrowCode_Length { get; private set; }

        public int MD_MainCode_Length { get; private set; }
        public int MD_ArrowCode_Length { get; private set; }

        public int CR3D_MainCode_Length { get; private set; }
        public int CR3D_ArrowCode_Length { get; private set; }

        public int RASMM_MainCode_Length { get; private set; }
        public int RASMM_ArrowCode_Length { get; private set; }

        public int RASMG_MainCode_Length { get; private set; }
        public int RASMG_ArrowCode_Length { get; private set; }

        private LayerNameManager()
        {
            #region CMList
            CMList.Add(new LayerNameVM() { Index = 1, ParentIndex = 2, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 2, ParentIndex = 0, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 3, ParentIndex = 4, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 4, ParentIndex = 0, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 5, ParentIndex = 7, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 6, ParentIndex = 7, IsShow = false });
            CMList.Add(new LayerNameVM() { Index = 7, ParentIndex = 0, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 8, ParentIndex = 10, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 9, ParentIndex = 10, IsShow = false });
            CMList.Add(new LayerNameVM() { Index = 10, ParentIndex = 0, IsShow = false });
            CMList.Add(new LayerNameVM() { Index = 11, ParentIndex = 12, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 12, ParentIndex = 0, IsShow = true });
            CMList.Add(new LayerNameVM() { Index = 13, ParentIndex = 19, IsShow = false });
            CMList.Add(new LayerNameVM() { Index = 14, ParentIndex = 19, IsShow = false });

            foreach (var x in CMList)
                x.Name = LayerIndex.GetName<LayerIndex.CM>(x.Index);

            #endregion

            #region NCList
            foreach (var x in CMList)
            {
                NCList.Add(x);
            }

            NCList.Add(new LayerNameVM() { Index = 15, ParentIndex = 18, IsShow = true, LayerSeq = 3, IsVariable = true });
            NCList.Add(new LayerNameVM() { Index = 16, ParentIndex = 18, IsShow = true, LayerSeq = 2, IsVariable = true });
            NCList.Add(new LayerNameVM() { Index = 17, ParentIndex = 18, IsShow = true, LayerSeq = 1, IsVariable = true });
            NCList.Add(new LayerNameVM() { Index = 18, ParentIndex = 19, IsShow = true, IsVariable = true });

            NCList.Add(new LayerNameVM() { Index = 19, ParentIndex = 0, IsVariable = true });

            #endregion

            #region JCList
            foreach (var x in CMList)
            {
                JCList.Add(x);
            }

            JCList.Add(new LayerNameVM() { Index = 15, ParentIndex = 18, IsShow = true, LayerSeq = 3, IsVariable = true });
            JCList.Add(new LayerNameVM() { Index = 16, ParentIndex = 18, IsShow = true, LayerSeq = 2, IsVariable = true });
            JCList.Add(new LayerNameVM() { Index = 17, ParentIndex = 18, IsShow = true, LayerSeq = 1, IsVariable = true });
            JCList.Add(new LayerNameVM() { Index = 18, ParentIndex = 19, IsShow = true, IsVariable = true });

            JCList.Add(new LayerNameVM() { Index = 19, ParentIndex = 0, IsVariable = true });
            #endregion

            #region CEList
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L01_Night_Sky, ParentIndex = LayerIndex.CE.L02_Night_on_Sky, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L02_Night_on_Sky, ParentIndex = 0, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L03_DaySky, ParentIndex = LayerIndex.CE.L04_Day_on_Sky, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L04_Day_on_Sky, ParentIndex = 0, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L05_Road_A, ParentIndex = LayerIndex.CE.L07_Main, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L06_Road_B, ParentIndex = LayerIndex.CE.L07_Main, IsShow = false });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L07_Main, ParentIndex = 0, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L08_Night_Filter_A, ParentIndex = LayerIndex.CE.L09_Night_on, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L09_Night_on, ParentIndex = 0, IsShow = false });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L10_Direction, ParentIndex = LayerIndex.CE.L11_Direction_Sub, IsShow = true, IsVariable = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L11_Direction_Sub, ParentIndex = 0, IsShow = true, IsVariable = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L12_Transparency_Color, ParentIndex = LayerIndex.CE.L17_Arrow, IsShow = false });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L13_Road_Background_Color, ParentIndex = LayerIndex.CE.L17_Arrow, IsShow = false });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L14_D0, ParentIndex = LayerIndex.CE.L17_Arrow, IsShow = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L16_Direction, ParentIndex = LayerIndex.CE.L17_Arrow, IsShow = true, IsVariable = true });
            CEList.Add(new LayerNameVM() { Index = LayerIndex.CE.L17_Arrow, ParentIndex = 0, IsShow = true });
            #endregion

            ////////////////////////////////////////////////////////////////////////////////수정해야됨
            #region ETList
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L02_Night_on_Sky, ParentIndex = 0, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L03_DaySky, ParentIndex = LayerIndex.ET.L04_Day_on_Sky, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L01_Night_Sky, ParentIndex = LayerIndex.ET.L02_Night_on_Sky, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L04_Day_on_Sky, ParentIndex = 0, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L05_Road_A, ParentIndex = LayerIndex.ET.L07_Main, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L06_Road_B, ParentIndex = LayerIndex.ET.L07_Main, IsShow = false });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L07_Main, ParentIndex = 0, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L08_Night_Filter_A, ParentIndex = LayerIndex.ET.L10_Night_on, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L09_Night_Filter_B, ParentIndex = LayerIndex.ET.L10_Night_on, IsShow = false });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L10_Night_on, ParentIndex = 0, IsShow = false });

            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L11_Hipass_Red_Road, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true, IsVariable = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L12_Hipass_Arrow, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true, IsVariable = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L13_Direction_Main, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L14_Direction_Road, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L15_Column, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L16_Hipass1, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true, IsVariable = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L17_Hipass, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true, IsVariable = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L18_Sign_post, ParentIndex = LayerIndex.ET.L19_ETC_, IsShow = true, IsVariable = true });

            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L19_ETC_, ParentIndex = LayerIndex.ET.L20_ETC, IsShow = true, IsVariable = true });
            ETList.Add(new LayerNameVM() { Index = LayerIndex.ET.L20_ETC, ParentIndex = 0 });
            #endregion
            ////////////////////////////////////////////////////////////////////////////////수정해야됨

            #region MDList
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L01_Night_Sky, ParentIndex = LayerIndex.MimeticDiagram.L02_Night_on_Sky, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L02_Night_on_Sky, ParentIndex = 0, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L03_DaySky, ParentIndex = LayerIndex.MimeticDiagram.L04_Day_on_Sky, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L04_Day_on_Sky, ParentIndex = 0, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L05_Road_A, ParentIndex = LayerIndex.MimeticDiagram.L08_Main, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L06_Post, ParentIndex = LayerIndex.MimeticDiagram.L07_Post, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L07_Post, ParentIndex = LayerIndex.MimeticDiagram.L08_Main, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L08_Main, ParentIndex = 0, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L09_Night, ParentIndex = LayerIndex.MimeticDiagram.L14_Night_on, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L10_Post, ParentIndex = LayerIndex.MimeticDiagram.L11_Post, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L11_Post, ParentIndex = LayerIndex.MimeticDiagram.L14_Night_on, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L12_Moon, ParentIndex = LayerIndex.MimeticDiagram.L13_Moon, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L13_Moon, ParentIndex = LayerIndex.MimeticDiagram.L14_Night_on, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L14_Night_on, ParentIndex = 0, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L15_Direction, ParentIndex = LayerIndex.MimeticDiagram.L16_Direction_Sub, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L16_Direction_Sub, ParentIndex = 0, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L17_Transparency_Color, ParentIndex = LayerIndex.MimeticDiagram.L21_Arrow, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L18_Arrow_0_Arrow, ParentIndex = LayerIndex.MimeticDiagram.Arrow_, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L19_Arrow_0_Direction, ParentIndex = LayerIndex.MimeticDiagram.Arrow_, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.Arrow_, ParentIndex = LayerIndex.MimeticDiagram.L21_Arrow, IsShow = true });
            MDList.Add(new LayerNameVM() { Index = LayerIndex.MimeticDiagram.L21_Arrow, ParentIndex = 0, IsShow = true });
            #endregion

            #region CR3DList
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L01_Night_Sky, ParentIndex = LayerIndex.CrossRoadPoint3D.L03_Night_on, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L02_Night_Main, ParentIndex = LayerIndex.CrossRoadPoint3D.L03_Night_on, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L03_Night_on, ParentIndex = 0, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L04_DaySky, ParentIndex = LayerIndex.CrossRoadPoint3D.L06_Day_on, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L05_Day_on_Sky, ParentIndex = LayerIndex.CrossRoadPoint3D.L06_Day_on, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L06_Day_on, ParentIndex = 0, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L07_Transparency_Color, ParentIndex = LayerIndex.CrossRoadPoint3D.L11_Arrow, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L08_Road_Background_Color, ParentIndex = LayerIndex.CrossRoadPoint3D.L11_Arrow, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D._Arrow_AI, ParentIndex = LayerIndex.CrossRoadPoint3D.L11_Arrow, IsShow = true });
            CR3DList.Add(new LayerNameVM() { Index = LayerIndex.CrossRoadPoint3D.L11_Arrow, ParentIndex = 0, IsShow = true });
            #endregion

            #region RASMMList
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L09_Mappy_ground, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L08_Ground, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L07_Building_set, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L06_Tree, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L05_Arrow, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L04_Icon, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L03_Logo, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L02_In_out, ParentIndex = 0, IsShow = true });
            RASMMList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Mapy.L01_Title, ParentIndex = 0, IsShow = true });
            #endregion

            #region RASMGList
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L07_B_ground, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L06_Ground, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L05_Building_set, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L04_Tree, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L03_Icon, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L02_In_out, ParentIndex = 0, IsShow = true });
            RASMGList.Add(new LayerNameVM() { Index = LayerIndex.RestAreaSummaryMap_Gini.L01_Title_set, ParentIndex = 0, IsShow = true });
            #endregion

            foreach (var x in NCList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.NC>(x.Index);

            foreach (var x in JCList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.JC>(x.Index);

            foreach (var x in CEList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.CE>(x.Index);

            foreach (var x in ETList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.ET>(x.Index);

            foreach (var x in MDList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.MimeticDiagram>(x.Index);

            foreach (var x in CR3DList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.CrossRoadPoint3D>(x.Index);

            foreach (var x in RASMGList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.RestAreaSummaryMap_Gini>(x.Index);

            foreach (var x in RASMMList.Where(x => x.Name == null))
                x.Name = LayerIndex.GetName<LayerIndex.RestAreaSummaryMap_Mapy>(x.Index);

            NormalCross_MainCode_Length = "KRCM16090693D084F00402".Length;
            NormalCross_ArrowCode_Length = "Arrow_KRCA16090693D084F00101".Length;

            JC_MainCode_Length = "KRJM165F01843011690503".Length;
            JC_ArrowCode_Length = "Arrow_KRJA160901734084EF0602".Length;

            CE_MainCode_Length = "9014061f".Length;
            CE_ArrowCode_Length = "d014061f_AI".Length;

            ET_MainCode_Length = "KREI19990000407039800".Length;
            ET_ArrowCode_Length = "ETC_1".Length;

            MD_MainCode_Length = "80224205".Length;
            MD_ArrowCode_Length = "Arrow_50225006".Length;

            CR3D_MainCode_Length = "80224205".Length;
            CR3D_ArrowCode_Length = "00000000_AI".Length;

            //RASMM_MainCode_Length = "80224205".Length;
            //RASMM_ArrowCode_Length = "Arrow_50225006".Length;

            //RASMG_MainCode_Length = "80224205".Length;
            //RASMG_ArrowCode_Length = "Arrow_50225006".Length;

            //JC_MainCode_Length = "KRJM160901734084EF0402.psd".Length;
            //JC_ArrowCode_Length = "Arrow_KRJA160901734084EF0602".Length;

            //JC_MainCode_Length = "KRJM160901734084EF0402.psd".Length;
            //JC_ArrowCode_Length = "Arrow_KRJA160901734084EF0602".Length;
        }

        public bool GetNameBySeq(IList<LayerNameVM> list, int layerSeq, out string name)
        {
            var item = list.SingleOrDefault(x => x.LayerSeq == layerSeq && x.IsVariable);
            name = string.Empty;

            if (item == null)
                return false;

            name = item.Name;
            return true;
        }


    }
}
