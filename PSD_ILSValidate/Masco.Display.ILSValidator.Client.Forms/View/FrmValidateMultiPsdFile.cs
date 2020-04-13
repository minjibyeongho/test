using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public partial class FrmValidateMultiPsdFile : Form
    {
        BindingList<ValidatePsdFileVM> _dataSouce = null;
        public FrmValidateMultiPsdFile()    // 생성자 이 클래스가 불러오면 하위 메소드 실행
        {
            InitializeComponent();
            InitControls();

            // form을 닫는 이벤트
            this.FormClosed += FrmValidateMultiPsdFile_FormClosed;
        }

        void FrmValidateMultiPsdFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
        }

        private void InitControls()
        {
            _dataSouce = new BindingList<ValidatePsdFileVM>();
            // 멀티 검증 griddataview 명칭 : grid
            // grid는 excel 형식으로 만들 경우 사용한다고 함( grid, listview 현재는 두종류가 확인 됨 )
            grid.DataSource = _dataSouce;
            // bindingList의 값을 get, set 메소드 만들어주는 걸로 보면 될듯?
            // bindingList는 데이터를 자동으로 바인딩해주는 List 클래스( List와 큰 차이점은 없다 )

            var g = grid;

            // grid 속성값 정의

            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //grid 이벤트 추가
            g.DoubleClick += g_DoubleClick;

            // txtPath가 바뀌면 실행되는 이벤트
            txtPath.TextChanged += txtPath_TextChanged;
            //txtPath.Text = @"D:\Data_PSD\ILS\NC";
            //LoadFIle(txtPath.Text);

            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                // config.txt 파일에는 PATH=D:\Data_PSD 정보가 들어가 있음
                var configFile = "config.txt";
                var pathTag = "PATH=";
                if (File.Exists(configFile))
                {
                    var lines = File.ReadAllLines(configFile);
                    foreach (var x in lines)
                    {
                        if (x.StartsWith(pathTag))
                        {
                            txtPath.Text = x.Substring(pathTag.Length);
                            break;
                        }
                    }
                }
            }
            // StartWith : 파라미터안의 변수와 인스턴스 시작부분이 일치하는지를 확인
            // File.Exists : 지정된 파일이 있는지 확인
            // File.ReadAllLines : 텍스트 파일의 정보를 string[]로 추출해줌
            // string.Substring : 부분 문자열 검색으로 여기서는 PATH= 까지 읽어 들인다

            catch (Exception)
            {
            }
        }
        private void SaveConfig()
        {
            try
            {
                var configFile = "config.txt";
                var pathTag = "PATH=";
                if (File.Exists(configFile))
                    File.Delete(configFile);

                var configList = new List<string>();
                configList.Add(string.Format("{0}{1}", pathTag, txtPath.Text));
                File.WriteAllLines(configFile, configList);
            }
            catch (Exception)
            {
            }
        }

        void g_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grid.SelectedRows.Count != 1)
                return;

            var x = grid.SelectedRows[0].DataBoundItem as ValidatePsdFileVM;

            var dlg = new FrmValidatePsdFile();
            dlg.Setup(x, false);
            dlg.Show(this);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            var path = txtPath.Text;
            if (Directory.Exists(path))
            {
                dlg.SelectedPath = path;
            }
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            txtPath.Text = path = dlg.SelectedPath;
            SaveConfig();

            LoadFIle(path);
        }
        void txtPath_TextChanged(object sender, EventArgs e)
        {
            var pathTextBox = sender as TextBox;
            var filePath = pathTextBox.Text;

            if (Directory.Exists(filePath) == false)
                return;
            LoadFIle(filePath);
        }

        private void LoadFIle(string path)
        {
            var files = Directory.GetFiles(path, "*.psd", SearchOption.AllDirectories);
            // Directory.GetFiles(path, 검색패턴, 검색옵션(하위 디렉토리까지 or 현재 디렉토리까지 ) : 파일 이름을 반환
            
            _dataSouce.Clear();
            var idx = 0;
            foreach (var file in files)
            {
                var item = new ValidatePsdFileVM();
                item.Index = ++idx;
                item.FileName = file;
                item.ILS_Type = GetILSType(file);
                if (item.ILS_Type == null)
                {
                    item.Description = "파일명 오류";
                }
                _dataSouce.Add(item);
            }
        }

        private string GetILSType(string file)
        {
            var fi = new FileInfo(file);
            var fileName = fi.Name;
            Console.WriteLine("###################");
            Console.WriteLine(fileName);
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

        // 검증 버튼 클릭 이벤트
        private void btnValidate_Click(object sender, EventArgs e)
        {
            // 검증 버튼 클릭 시 row의 데이터를 가져와서 비교하는 로직
            foreach (var row in grid.Rows.OfType<DataGridViewRow>())
            {

                Console.WriteLine("#############");
                Console.WriteLine(row);

                // 개별 파일 검증 class를 생성???
                var dlg = new FrmValidatePsdFile();
                // 2020-02-27 FrmValidatePsdFile 분석 시작
                // row.Selected = true;
                grid.CurrentCell = row.Cells[0];

                var x = row.DataBoundItem as ValidatePsdFileVM;
                if (x.ILS_Type == null)
                    continue;

                dlg.Setup(x);
                dlg.ShowDialog();
            }
            grid.CurrentCell = null;
        }

        private void btnExportLayerName_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grid.Rows.Count < 1)
                return;

            //timer.Interval = 
            var idx = 0;
            GetLayerList(idx);
        }

        private void GetLayerList(int idx)
        {
            var folderPath = txtPath.Text;
            var isFilterByArrow = chboxFilterByArrow.Checked;
            foreach (var row in grid.Rows.OfType<DataGridViewRow>())
            {
                var dlg = new FrmValidatePsdFile();
                grid.CurrentCell = row.Cells[0];

                var item = row.DataBoundItem as ValidatePsdFileVM;
                if (item.ILS_Type == null)
                    continue;

                dlg.Setup(item, folderPath,isFilterByArrow);
                dlg.ShowDialog();
            }
            grid.CurrentCell = null;
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
