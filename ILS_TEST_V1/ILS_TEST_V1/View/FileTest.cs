using Ntreev.Library.Psd;
using ILS_TEST_V1.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILS_TEST_V1
{
    public partial class FileTest : Form
    {
        public FileTest()
        {
            InitializeComponent();

            //2020.03.17 gridVerify 마지막행 제거 //제거 안할 시 빈 부분 더블클릭하면 error발생하므로 (최정웅)
            gridVerify.AllowUserToAddRows = false;

            //2020.02.24 gridVerify 더블클릭시 이벤트 생성 (박찬규)
            gridVerify.CellMouseDoubleClick += gridVerify_CellMouseDoubleClick;
        }


        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            //2020.02.21 메서드화 작업 코드수정 (박찬규)
            FileSearch();
        }

        //2020.02.24 gridVerify 더블클릭시 PsdFileTest 다이얼로그 발생 (박찬규)
        void gridVerify_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            int rowIndex = gridVerify.CurrentRow.Index;
            Console.WriteLine(rowIndex);
            var filePath = gridVerify.Rows[rowIndex].Cells[1].Value.ToString();
            Console.WriteLine(filePath);
            PsdFileTest dlg = new PsdFileTest(filePath);
            dlg.Show();
        }

        private void gridVerify_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void FileSearch()
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }
            
            // 선택한 폴더명 (최정웅)
            txtFolderPath.Text = fbd.SelectedPath;

            // 그리드 행, 열 초기화 (최정웅)
            gridVerify.Columns.Clear();
            gridVerify.Rows.Clear();

            var selectPath = txtFolderPath.Text;
            var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);

            //DataGridView 열에 데이터 넣기(최정웅 박찬규)
            #region PSD FILE LIST
            gridVerify.ColumnCount = 6;
            
            gridVerify.Columns[0].Name = "Index";
            gridVerify.Columns[1].Name = "FileName";
            gridVerify.Columns[2].Name = "ILS_Type";
            gridVerify.Columns[3].Name = "TotalCount";
            gridVerify.Columns[4].Name = "Success";
            gridVerify.Columns[5].Name = "Fail";

            //gridVerify.Columns.Add("FileName", "FileName"); 
            #endregion
           

            //DataGridView 행에 값 넣는 곳 (최정웅 , 박찬규)
            //순번 표기를 위한 Index 변수 추가(박찬규)
            #region PSD FILE Data
            int Index = 0;

            foreach (var file in files)
            {
                ++Index;
                var doc = PsdDocument.Create(file);
                var ILStype = GetILSType(file);
                gridVerify.Rows.Add(Index, file,ILStype);
            }
            #endregion

            //각 열의 데이터에 맞게 자동으로 사이즈 조절 기능 추가 (박찬규)
            gridVerify.AutoResizeColumns();
        }


        // 2020.02.26 민병호 ILSType 비교 로직 작성
        /*
         ****** 참고자료 ******
            ################### ILSType 정리 ####################
            일반교차로             NC                        KRCM
            JC                     JC                        KRJM
            도시고속               CE                        90
            ETC                    ET                        KREI
            모식도()               MimeticDiagram            8
            3D 교차로              CrossRoadPoint3D          8
            휴게소 요약맵(맵피)    RestAreaSummaryMap_Mapy   ?
            휴게소 요약맵(지니)    RestAreaSummaryMap_Gini   ?
            ######################################################
         */
        private string GetILSType(string file)
        {
            // file은 현재 filepath를 의미
            // C: \Users\User\Desktop\LTS_Confirm\ILS샘플\사인보드\사인보드 + 유도선\KRCM12B30266E0BE4C0802.PSD

            var file_info = new FileInfo(file);
            var fileName = file_info.Name;
            // fileName
            // KRCM12B30266E0BE4C0802.PSD
            
            //비교 로직
            // startwith(String, StringComparison) : 비교할 문자열, 비교하는 방법 열거형(enum)
            // StringComparison : https://docs.microsoft.com/ko-kr/dotnet/api/system.stringcomparison?view=netframework-4.8
            // 이 로직은 결국 filename을 가져와서 파일이름을 비교( 파일명 앞부분이 코드 ) 하여 ILSType을 지정하게끔 한다
            if (fileName.StartsWith(ILSType.FilePrefix1_NC, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code1_NC;
            }
            if (fileName.StartsWith(ILSType.FilePrefix2_JC, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code2_JC;
            }
            if (fileName.StartsWith(ILSType.FilePrefix3_CE, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code3_CE;
            }
            if (fileName.StartsWith(ILSType.FilePrefix4_ET, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code4_ET;
            }
            else if (fileName.StartsWith("8"))  //모식도, 3D 교차점
            {
                // 모식도, 3D 교차점은 파일명으로 비교 기준을 정할 수 없어서 안에 파고들어서 첫번째 layer의 이름으로 비교한다.
                var document = PsdDocument.Create(file);

                var totalLayerList = document.Childs.Reverse();
                var fisrtLayer = totalLayerList.FirstOrDefault();
                var firstChild = fisrtLayer.Childs.Reverse().FirstOrDefault();
                if (firstChild.Name.StartsWith("Arrow_"))
                    return ILSType.Code5_MimeticDiagram;
                else if (firstChild.Name.EndsWith("_AI"))
                    return ILSType.Code6_CrossRoadPoint3D;
            }
            else
            {
                // 휴게소요약 mapy와 gini의 경우도 마찬가지로 파일명으로 비교 불가
                // 파고들어서 첫번째 Layer의 mapy(Title), gini(Title_set)으로 비교 하여 구분
                var document = PsdDocument.Create(file);
                var totalLayerList = document.Childs.Reverse();
                var fisrtLayer = totalLayerList.FirstOrDefault();
                if (fisrtLayer.Name.Equals("Title"))
                    return ILSType.Code7_RestAreaSummaryMap_Mapy;
                else if (fisrtLayer.Name.Equals("Title_set"))
                    return ILSType.Code8_RestAreaSummaryMap_Gini;
            }
            return null;
        }
       
    }
}
