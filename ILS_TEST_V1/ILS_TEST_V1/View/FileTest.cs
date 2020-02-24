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

                gridVerify.Rows.Add(Index, file);
            }
            #endregion

            //각 열의 데이터에 맞게 자동으로 사이즈 조절 기능 추가 (박찬규)
            gridVerify.AutoResizeColumns();
        }

       
    }
}
