using System;
using System.IO; // 2020.02.27 파일 이름 가져오는 거 (박찬규)
using Ntreev.Library.Psd;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ILS_TEST_V1.Model;
using Ntreev.Library.Psd.UserModel;
using ILS_TEST_V1.Helper;

//2020.04.10 pcg 
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ILS_TEST_V1.View
{
    public partial class PsdFileTest : Form
    {
        BindingList<IPsdLayer> layerAdd = new BindingList<IPsdLayer>(); //총 레이어를 담기위한 BindingList (2020.03.17 최정웅)
        BindingList<layerModel> gridlist = new BindingList<layerModel>();   // 레이어의 정보를 담은 BindingList ( 2020.03.17 민병호 )

        // 원본 코드
        private IList<ValidateVM> _fullValidCodeList; // 오류검증 list( 2020.04.20 민병호 )
        private ValidatePsdFileVM _validatePsdFileVM;
        private PsdFileVM _psdFile;
        private PsdFileSectionVM _psdFileSection;
        private ValidationMethods _validationMethods;
        private bool _isAutoClose;
        private string _errorStringReadFile;

        // private string filepath;
        private int _index = 0;

        // 파라미터 없는 생성자( 2020.02.24 민병호 )
        public PsdFileTest()
        {
            InitializeComponent();
            InitControls();

            dataGridView1.AllowUserToAddRows = false;
            gridValidCode.AllowUserToAddRows = false;
            gridErrorMsg.AllowUserToAddRows = false;
        }

        private void InitControls()
        {
            _psdFile = new PsdFileVM();
            _psdFileSection = new PsdFileSectionVM();

            _validationMethods = new ValidationMethods();
            _validationMethods.Reset();

            InitGrid(dataGridView1);
            InitGrid(gridValidCode);
            InitGrid(gridErrorMsg);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridValidCode.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridErrorMsg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView1.DataSource = _validationMethods.DSPsdLayerVMList;
            gridValidCode.DataSource = _validationMethods.DsValidCodeList;
            gridErrorMsg.DataSource = _validationMethods.DsErrorMsgList;

            var columnName = string.Empty;
            {
                columnName = Masco.Core.Helper.Refrection.GetPropName<ValidateVM>(x => x.INDEX);
                gridValidCode.Columns[columnName].Width = 60;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ValidateVM>(x => x.CODE);
                gridValidCode.Columns[columnName].Width = 60;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ValidateVM>(x => x.CHECK);
                gridValidCode.Columns[columnName].Width = 60;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ValidateVM>(x => x.TITLE);
                gridValidCode.Columns[columnName].Width = 200;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ValidateVM>(x => x.ResultState);
                gridValidCode.Columns[columnName].Width = 80;
            }

            // ILS 검증부분 이벤트 추가 및 새로 생성
            _fullValidCodeList = new List<ValidateVM>();
            var v = new Validate1();
            _fullValidCodeList = v.GetList();

            // radio btn 이벤트 추가
            rbtn1.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn2.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn3.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn4.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn5.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn6.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn7.CheckedChanged += rbtnLSType_CheckedChanged;
            rbtn8.CheckedChanged += rbtnLSType_CheckedChanged;
            // rdoAll.CheckedChanged += rdoILSTypeAll_CheckedChanged;
        }
        private void InitGrid(DataGridView g)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //private void ShowLog(string msg)
        //{
        //tsslLog.Text = string.Format("{0:yyyy/MM/dd hh:mm:ss} {1}", DateTime.Now, msg);
        //}


        private void btnVerify_Click(object sender, EventArgs e)
        {
            //ShowLog("Begin Validation");

            _validationMethods.Reset();
            gridValidCode.Refresh();
            //try
            //{
            HashSet<string> set;
            var ilsType = GetSelectedILSType(out set);
            _validationMethods.Run(ilsType, _psdFile, _psdFileSection);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}


            gridValidCode.Refresh();
            //ShowLog("End Validation");


            if (_validatePsdFileVM != null)
            {
                var totalCount = _validationMethods.DsValidCodeList.Count;
                var sCount = 0;
                foreach (var x in _validationMethods.DsValidCodeList)
                    if (x.ResultState == ResultType.Success)
                        sCount++;

                if (string.IsNullOrWhiteSpace(_errorStringReadFile))
                {
                    _validatePsdFileVM.Success = sCount;
                    _validatePsdFileVM.TotalCount = totalCount;
                    _validatePsdFileVM.Fail = totalCount - sCount;
                }
                else
                    _validatePsdFileVM.Description = _errorStringReadFile;

                if (_isAutoClose)
                    this.Close();
            }
        }

        void rbtnLSType_CheckedChanged(object sender, EventArgs e)
        {
            RefreshValidCodeList();
        }

        void RefreshValidCodeList()
        {
            _validationMethods.DsValidCodeList.Clear();

            HashSet<string> ilsTypeSet;
            GetSelectedILSType(out ilsTypeSet);

            foreach (var item in ilsTypeSet)
            {
                Console.WriteLine("ilsTypeSet : {0}", item);
            }

            foreach (var x in _fullValidCodeList)
            {

                Console.WriteLine("x.ILSType : {0}, x.CHECK : {1}, x.CODE : {2}", x.ILSType, x.CHECK, x.CODE);

                if (ilsTypeSet.Contains(x.ILSType))
                    _validationMethods.DsValidCodeList.Add(x);
            }
        }

        // 선택된 radio btn을 가져오는 것
        string GetSelectedILSType(out HashSet<string> ilsType)
        {
            var result = string.Empty;
            ilsType = new HashSet<string>();
            if (rbtn1.Checked)
            {
                ilsType.Add("CM");
                ilsType.Add("NC");
                result = "NC";
            }
            else if (rbtn2.Checked)
            {
                ilsType.Add("CM");
                ilsType.Add("JC");
                result = "JC";
            }
            else if (rbtn3.Checked)
            {
                ilsType.Add("CE");
                result = "CE";
            }
            else if (rbtn4.Checked)
            {
                ilsType.Add("ET");
                result = "ET";
            }
            else if (rbtn5.Checked)
            {
                ilsType.Add("MD");
                result = "MD";
            }
            else if (rbtn6.Checked)
            {
                ilsType.Add("CR3D");
                result = "CR3D";
            }
            else if (rbtn7.Checked)
            {
                ilsType.Add("RASMM");
                result = "RASMM";
            }
            else if (rbtn8.Checked)
            {
                ilsType.Add("RASMG");
                result = "RASMG";
            }
            return result;
        }

        internal void Setup(ValidatePsdFileVM vm, bool isAutoClose = true)
        {
            this._isAutoClose = isAutoClose;
            if (vm.ILS_Type == ILSType.Code1_NC)
            {
                rbtn1.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code2_JC)
            {
                rbtn2.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code3_CE)
            {
                rbtn3.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code4_ET)
            {
                rbtn4.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code5_MimeticDiagram)
            {
                rbtn5.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code6_CrossRoadPoint3D)
            {
                rbtn6.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code7_RestAreaSummaryMap_Mapy)
            {
                rbtn7.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code8_RestAreaSummaryMap_Gini)
            {
                rbtn8.Checked = true;
            }
            filePath.Text = vm.FileName;
            _validatePsdFileVM = vm;
        }
        /*
        public void Setup(ValidatePsdFileVM vm, string folderPath, bool isFilterByArrow, bool isAutoClose = true, bool isExportLayerList = true)
        {
            this._isAutoClose = isAutoClose;
            if (isExportLayerList)
            {
                this._isExportLayerList = isExportLayerList;
                this._exportFilePath = folderPath;
                this._isFilterByArrow = isFilterByArrow;
            }

            if (vm.ILS_Type == ILSType.Code1_NC)
            {
                rdoILSType1.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code2_JC)
            {
                rdoILSType2.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code3_CE)
            {
                rdoILSType3.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code4_ET)
            {
                rdoILSType4.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code5_MimeticDiagram)
            {
                rdoILSType5.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code6_CrossRoadPoint3D)
            {
                rdoILSType6.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code7_RestAreaSummaryMap_Mapy)
            {
                rdoILSType7.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code8_RestAreaSummaryMap_Gini)
            {
                rdoILSType8.Checked = true;
            }

            txtPsdFullPath.Text = vm.FileName;
            _validatePsdFileVM = vm;
        }
        */

        /*
        // 파라미터 있는 생성자 생성 ( 2020.02.24 민병호 )
        public PsdFileTest(string filepath)
        {
            InitializeComponent();
            InitControls();
            filePath.Text = filepath;

            //그리드 마지막행 제거
            dataGridView1.AllowUserToAddRows = false;
            gridValidCode.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
        }
        */

        // dataGridView1 데이터그리드뷰 이름

        //child 레이어 list add, 재귀함수 ( 2020.03.17 최정웅)
        private void GetLayer(IPsdLayer layer, int layerDepth, int layerSeq)
        {
            Console.WriteLine(layer);
            Console.WriteLine("index: {0}, layerDepth: {1}, layerSeq: {2}", _index, layerDepth, layerSeq);
            Console.WriteLine();

            // 새로운 VM 생성
            layerModel tmpVm = new layerModel();

            // 상위, 순번, 단계
            tmpVm.Index = ++_index;
            tmpVm.LayerDepth = layerDepth;
            tmpVm.LayerSeq = layerSeq;


            // 각 레이어의 속성 정보를 넣어줌
            tmpVm.Name = layer.Name;
            tmpVm.BlendMode = layer.BlendMode;

            //int
            tmpVm.Depth = layer.Depth;
            tmpVm.Top = layer.Top;
            tmpVm.Bottom = layer.Bottom;
            tmpVm.Left = layer.Left;
            tmpVm.Right = layer.Right;
            tmpVm.Width = layer.Width;
            tmpVm.Height = layer.Height;

            // bool
            tmpVm.HasImage = layer.HasImage;
            tmpVm.HasMask = layer.HasMask;  //bool
            tmpVm.IsClippinig = layer.IsClipping;


            // Channel
            var sb1 = new List<string>();
            var sb2 = new List<string>();
            var sb3 = new List<string>();

            foreach (var c in layer.Channels)
            {
                sb1.Add(string.Format("{0}", c.Type));
                sb2.Add(string.Format("{0}", c.Data.Length));
                sb3.Add(string.Format("{0}", c.Data.Length > 0 ? c.Data[0].ToString() : "-"));
            }

            tmpVm.ChannelTypes = string.Join("/", sb1);
            tmpVm.ChannelSize = string.Join("/", sb2);
            tmpVm.ChannelARGB = string.Join("/", sb3);

            tmpVm.ChildCount = layer.Childs.Count();

            // Float
            tmpVm.Opacity = layer.Opacity;  //불투명도
            var descriptionList = layer.GetDescription();
            tmpVm.IsVisible = GetDictionaryValue(descriptionList, DescriptionMode.Records_Flags_IsVisible) != "True";
            tmpVm.IsLock = GetDictionaryValue(descriptionList, DescriptionMode.Records_Flags_IsTransparency) == "True";
            tmpVm.SectionType = GetDictionaryValue(descriptionList, DescriptionMode.Records_SectionType);
            var flagNumber = GetDictionaryValue(descriptionList, DescriptionMode.Records_Flags_Number);
            tmpVm.RecordsFlags = ToBin(int.Parse(flagNumber), 8);
            tmpVm.HeightUnit = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_HeightUnit);
            tmpVm.HorizontalRes = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_HorizontalRes);
            tmpVm.HorizontalResUnit = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_HorizontalResUnit);
            tmpVm.VerticalRes = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_VerticalRes);
            tmpVm.VerticalResUnit = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_VerticalResUnit);
            tmpVm.WidthUnit = GetDictionaryValue(descriptionList, DescriptionMode.Docuemnt_WidthUnit);

            /*
            layer.BlendMode;    //BlendMode
            layer.Channels.; // [], iChannel
            layer.LinkedLayer;  //????
            layer.Parent;
            layer.Resources;
             */

            gridlist.Add(tmpVm);
            // bindingList에 담는다.
            //layerAdd.Add(layer);

            var childSeq = 1;
            foreach (var y in layer.Childs.Reverse()) //IPsdLayer는 첫번째 child가 최상위 폴더가 아닌 그하위 폴더 즉 ETC1 이런거가 나옴 그래서 최상위 폴더가 안나오는 것임.
            {
                GetLayer(y, layerDepth + 1, childSeq++);
            }
        }


        // layer 카운트, parent layer카운트 및 레이어별 속성 list add 수정중임 (2020.03.17 최정웅)
        public void ReadFile(string filename)
        {
            gridlist.Clear();

            var doc = PsdDocument.Create(filePath.Text);

            // 처음 layer 읽기 시작하는 부분
            // 상위, 순번 등의 레이어 단계를 나타내기 위해서 순서 정리가 필요
            // 기존의 것은 보기 힘들게 되어 있으므로 이해해서 알아보기 쉽게 만드는게 중요( 사용하려면은.... )

            var layerSeq = 1;
            foreach (var x in doc.Childs.Reverse())
            {
                GetLayer(x, 1, layerSeq++);
            }


            var idx = gridlist.Count();

            // 상위 인덱스 찾는 로직
            foreach (var x in gridlist)
            {
                x.Index = idx--;
                if (x.LayerDepth == 1)
                {
                    x.ParentIndex = 0;
                }
                else
                {
                    x.ParentIndex = -1;
                }
            }
            int layerDepth = 1;

            while (true)
            {
                ++layerDepth;
                var subLayerList = gridlist.Where(x => x.LayerDepth == layerDepth && x.ParentIndex < 0);
                if (subLayerList.Any() == false)
                    break;

                foreach (var z in subLayerList)
                {
                    //var parentIndex = gridlist.Where(x => x.Index > z.Index).OrderByDescending(x => x.Index).Min(x => x.Index);
                    var parentIndex = gridlist.Where(x => x.Index > z.Index && x.LayerDepth == z.LayerDepth - 1).Min(x => x.Index);
                    z.ParentIndex = parentIndex;
                }
            }
            #region DSPsdLayerVMList Add부분(에러검증할 때 사용할 LIST에 추가)
            //var list = gridlist;
            //foreach (var x in list)
            //{
            //_validationMethods.DSPsdLayerVMList.Add(x);
            //}
            #endregion

            doc.Dispose();
        }


        // FileTest에서 더블클릭시 파일 경로 읽어오면 Text변경 이벤트 발생 -> PSD파일 읽을 수 있도록 구현( 2020.02.24 민병호 ) 
        private void filePath_TextChanged(object sender, EventArgs e)
        {
            var doc = PsdDocument.Create(filePath.Text);
            //Console.WriteLine(filePath.Text);

            // 2020.02.24 민병호
            // 읽어오는 psd libaray 구현
            // 각자 구현해보고( 공통 util로 만드는 작업 )
            // PSD Library 활용

            //2020.02.27 파일 정보 텍스트 박스 입력 메서드 생성 (박찬규)
            //FileInformaion(filePath.Text);

            // layer 정보를 읽어온다
            ReadFile(filePath.Text);

            // bindingList가 모두 add된 상태
            dataGridView1.DataSource = gridlist;
        }


        //2020.02.27 파일 정보 텍스트 박스 입력 메서드 생성 (박찬규)
        private void FileInformaion(string filePath)
        {
            var doc = PsdDocument.Create(filePath);
            var imageSource = doc.ImageResources;
            var properties = imageSource["Resolution"] as IEnumerable<KeyValuePair<string, object>>;
            // 속성을 뽑아오는 것까지는 성공! ( IEnumerable<KeyValuePair<string, object>> 되어 있는 데이터를 어떻게 뽑아서 가져올지가 필요 )
            //var tlist = properties.ToList();

            // 2020/03/18 pixel 정보 뽑아 오는 것 테스트 중( 민병호 )
            // 현재 상황 : properties에 pixel정보 들어가 있음
            // 2020/04/08 Linq로 select 가능한지 확인( 코드 수를 줄일 수 있는 방안이 될 것.. )
            StringBuilder min_sb1 = new StringBuilder();

            foreach (var item in properties)
            {
                // Console.WriteLine("#######속성뽑기");
                var key = item.Key;
                // Console.WriteLine("key : {0}, value : {1}", key, item.Value);
                if ("HorizontalRes".Equals(item.Key))
                {
                    min_sb1.Append(item.Value);
                    min_sb1.Append(" * ");
                }

                if ("VerticalRes".Equals(item.Key))
                {
                    min_sb1.Append(item.Value);
                }
            }

            // psdFileVM 요소 등록( 2020/04/23 민병호 )
            FileInfo fileInfo = new FileInfo(filePath);
            _psdFile.Name = fileInfo.Name.Split('.')[0];
            _psdFile.Extension = fileInfo.Extension;

            var imageSource11 = doc as IImageSource;
            var iproperties = imageSource11 as IProperties;

            List<string> tmp = new List<string>();
            foreach (var channel in imageSource11.Channels)
            {
                tmp.Add(channel.Type.ToString());
            }
            string tmpChannelType = string.Join(" / ", tmp);

            BeginInvoke((MethodInvoker)(() => { FileChannelType.Text = tmpChannelType; }));
            //FileChannelType.Text = tmpChannelType;

            // PsdFileSectionVM 요소 등록 ( 2020/04/23 민병호 )
            _psdFileSection.NumberOfChannels = doc.FileHeaderSection.NumberOfChannels;
            _psdFileSection.Width = doc.Width;
            _psdFileSection.Height = doc.Height;
            _psdFileSection.Depth = doc.FileHeaderSection.Depth;
            _psdFileSection.ColorMode = doc.FileHeaderSection.ColorMode.ToString();
            _psdFileSection.ChannelTypes = tmpChannelType;
            _psdFileSection.Pixel = min_sb1.ToString();

            BeginInvoke((MethodInvoker)(() => { FileDepth.Text = doc.Depth.ToString(); }));
            //FileDepth.Text = doc.Depth.ToString();


            // TextBox에 요소 넣는 메소드( 2020/04/23 민병호 )
            setTxtBox(filePath, doc, min_sb1);

            /*
                요소 들어갔나 확인
            _psdFile.ComponentPrint();
            _psdFileSection.Componentprint();
            */

            /*
            //2020.02.27 채널 개수에 따른 채널 타입 추출 (박찬규)
            for (int i = 0; i < doc.FileHeaderSection.NumberOfChannels; i++)
            {
                if (TypeTemp != null)
                {
                    TypeTemp += "/";
                }
                TypeTemp1 = imageSource.Channels[i].Type.ToString();
                TypeTemp += TypeTemp1;
            }
            FileChannelType.Text = TypeTemp;
            */

            doc.Dispose();
        }

        public void setTxtBox(string path, PsdDocument doc, StringBuilder sb)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                //2020.02.27  파일 정보 텍스트 박스 값 입력
                FileNameBox.Text = Path.GetFileNameWithoutExtension(path);  //파일명 텍스트박스  2020.02.27 파일의 확장자를 제외한 파일명을 가져온다. (박찬규)            
                FileExtensionName.Text = Path.GetExtension(path);  //파일 확장명 텍스트 박스 2020.02.27 파일의 확장자만 가져온다. (박찬규)

                FileChannelCount.Text = doc.FileHeaderSection.NumberOfChannels.ToString();
                FileWidth.Text = doc.Width.ToString();
                FileHeight.Text = doc.Height.ToString();
                FileDepth.Text = doc.FileHeaderSection.Depth.ToString();
                FileColorMode.Text = doc.FileHeaderSection.ColorMode.ToString();

                // 72 * 72 이런식으로 들어가 있을 것
                FilePixel.Text = sb.ToString();
            }));
        }


        // 원본 소스에서의 필요 메소드( 분석은 아직 못함 2020/04/08 민병호 )
        public static string ToBin(int value, int len)
        {
            return (len > 1 ? ToBin(value >> 1, len - 1) : null) + "01"[value & 1];
        }

        private string GetDictionaryValue(Dictionary<DescriptionMode, object> descList, DescriptionMode mode)
        {
            return (descList.ContainsKey(mode) == false) ? string.Empty : string.Format("{0}", descList[mode]);
        }

        private string GetChannelData(IChannel[] channels, int chIndex)
        {
            if (channels.Length <= chIndex)
                return "-:-";

            var ch = channels[chIndex];
            var result = string.Format("{0}:{1}", ch.Type, ch.Data.Length > 0 ? ch.Data[0].ToString() : "-");
            return result;
        }

        // datagridview 변경 시 이벤트 발생( 2020/04/08 민병호 - propertygridView와 매칭 )
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            propertyGrid1.SelectedObject = null;
            // 표로 속성을 표시하는 개체를 가져오거나 설정합니다.
            // 한개 선택이 아닐 때는 반환( 한개만 선택하도록 설정 )
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            // datagridview에서 선택된 행에 바인딩된 객체를 가져오는 것
            var item = dataGridView1.SelectedRows[0].DataBoundItem as layerModel;
            // DataGridViewRow.DataBoundItem : 행을 채운 데이터 바인딩된 개체를 가져옵니다.
            // propertyGrid.SelectedObject : 표로 속성을 표시하는 개체를 가져오거나 설정합니다.
            // 즉, propertyGrid에 datagridview에서 선택된 객체를 바인딩 시켜주는 것
            // ( layerModel에서 category, description에 설정된 값들을 읽어서 propertygrid에 표출해준다 )
            propertyGrid1.SelectedObject = item;
        }

        //2020.04.10 pcg  엑셀출력 이벤트 관련 내용
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;

        private void ExcellPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  // 바탕화면 경로(우선 바탕화면에 경로 잡아놈!!)
                string path = Path.Combine(desktopPath, FileNameBox.Text + "_" + System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss ") + ".xlsx"); // 엑셀 파일 저장 경로(우선 파일명으로 작업함)

                excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
                workBook = excelApp.Workbooks.Add();                            // 워크북 추가
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기

                //여기서부터 작업 하면되는데 행에다가는 순번 적으면 될꺼 같고 입력값에는 레이어네임 입력하면 될꺼같다.
                //workSheet.Cells[1, 1] = "레이어";
                // 헤더 출력
                for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                {
                    workSheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                //내용 출력
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
                    {
                        workSheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                    }
                }

                workSheet.Columns.AutoFit();                                    // 열 너비 자동 맞춤
                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);    // 엑셀 파일 저장
                workBook.Close(true);
                excelApp.Quit();
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }

        }
        /// <summary>
        /// 액셀 객체 해제 메소드
        /// </summary>
        /// <param name="obj"></param>
        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);  // 액셀 객체 해제
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();   // 가비지 수집
            }
        }

        private void PsdFileTest_Load(object sender, EventArgs e)
        {
            //기존ILS의 OpenPsd() 참고
            if (this._isAutoClose)
            {
                this.Visible = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Width = 0;
                this.Height = 0;
                //this.Text = "";
                this.ControlBox = false;
                //this.ShowInTaskbar = false;
            }

            _validationMethods.DSPsdLayerVMList.Clear();

            this.Enabled = false;
            var bw = new BackgroundWorker();
            bw.DoWork += (a, b) =>
            {
                try
                {
                    FileInformaion(filePath.Text);
                }
                catch (Exception ex)
                {
                    _errorStringReadFile = ex.ToString();
                }
            };
            bw.RunWorkerCompleted += (a, b) =>
            {
                //this.Enabled = true;
                //var errorMsg = "이미지 컬러 모드가 RGB가 아닙니다";
                //if (_isAutoClose)
                //{
                    //_validatePsdFileVM.Description = errorMsg;
                    //this.Close();
                //}
                //else
                //{
                    //MessageBox.Show(errorMsg);
                    //return;
                //}

                var list = gridlist;
                foreach (var x in list)
                {
                    _validationMethods.DSPsdLayerVMList.Add(x);
                }

                if (this._validatePsdFileVM != null)
                {
                    btnVerify.PerformClick();
                }
            };
            bw.RunWorkerAsync();
        }



    }
}

