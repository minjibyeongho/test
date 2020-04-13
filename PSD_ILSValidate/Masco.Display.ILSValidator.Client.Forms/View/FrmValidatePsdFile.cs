using Masco.Display.ILSValidator.Client.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    //[Masco.Core.Bootstrapper.EntryPoint]
    public partial class FrmValidatePsdFile : Form
    {
        private PsdReader _psdReader;
        private ValidationMethods _validationMethods;

        private PsdFileVM _psdFile;
        private PsdFileSectionVM _psdFileSection;
        private IList<ValidateVM> _fullValidCodeList;
        private ValidatePsdFileVM _validatePsdFileVM;
        private bool _isAutoClose;
        private bool _isExportLayerList;
        private string _errorStringReadFile;
        private string _exportFilePath;
        private bool _isFilterByArrow;


        public FrmValidatePsdFile() // 생성자( 클래스 실행시 기본 실행 )
        {
            InitializeComponent();  // 디자인이 담긴 곳이라고 보면 됨
            InitControls();         // 맨 처음 동작하는 부분

            // 테스트
            //txtPsdFullPath.Text = @"D:\Data_PSD\KRCM16090693D084F00402.psd";

            //txtPsdFullPath.Text = @"D:\Data_PSD\ILS\CE\9014061f.psd";
            //this.Shown += (s, e) =>
            //{
            //    OpenPSD();
            //};

            this.Shown += FrmValidatePsdFile_Shown;
            this.Load += FrmValidatePsdFile_Load;
        }

        // psd 파일 로더
        void FrmValidatePsdFile_Load(object sender, EventArgs e)
        {
            
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
            OpenPSD();
        }

        void FrmValidatePsdFile_Shown(object sender, EventArgs e)
        {


        }

        private void btnPsdFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                return;

            txtPsdFullPath.Text = dlg.FileName;
            OpenPSD();
        }

        private void btnPsdRead_Click(object sender, EventArgs e)
        {
            OpenPSD();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            //http://www.hoons.net/lecture/view/635

            ShowLog("Begin Validation");

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
            ShowLog("End Validation");


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

        private void gridPSD_SelectionChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = null;
            if (gridPsdLayer.SelectedRows.Count != 1)
                return;
            var item = gridPsdLayer.SelectedRows[0].DataBoundItem as PsdLayerVM;
            propertyGrid1.SelectedObject = item;
        }

        #region InitControls
        private void DataBinding<T>(Control control, string ctrPropertyName, ModelBase<T> dataSouece, string vmPropertyName, object defaultValue = null)
        {
            var binding = control.DataBindings.Add(ctrPropertyName, dataSouece, vmPropertyName, true, DataSourceUpdateMode.OnPropertyChanged, defaultValue);
            var bn = new BindingNotifier(binding);
            dataSouece.PropertyChanged += bn.Binding_WakeUpCall;
        }
        private void SetTextBox()
        {
            txtFileName.Text = _psdFile.Name;
            txtFileExtension.Text = _psdFile.Extension;
            txtFileSectionColorMode.Text = _psdFileSection.ColorMode;
            txtFileSectionWidth.Text = _psdFileSection.Width.ToString();
            txtFileSectionHeight.Text = _psdFileSection.Height.ToString();
            txtFileSectionDepth.Text = _psdFileSection.Depth.ToString();
            txtNumberOfChannels.Text = _psdFileSection.NumberOfChannels.ToString();
            txtFileSectionPixel.Text = _psdFileSection.Pixel;
            var pixel = _psdFileSection.Pixel;
            if (pixel != "72*72")
            {
                if (pixel == "146*146")
                {
                    txtFileSectionPixel.Text = pixel;
                    if (_isAutoClose)
                    {
                        _validatePsdFileVM.Description = "모바일 Pixel";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("모바일 Pixel");
                    }
                }
                else
                {
                    var errorMsg = "픽셀 값 오류";
                    txtFileSectionPixel.Text = pixel;
                    errorProvider1.SetError(txtFileSectionPixel, errorMsg);
                    if (_isAutoClose)
                    {
                        _validatePsdFileVM.Description = errorMsg;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMsg);
                        return;
                    }
                }
            }
            txtChannelsType.Text = _psdFileSection.ChannelTypes;
        }
        private void InitControls()
        {

            _psdReader = new PsdReader();

            _psdFile = new PsdFileVM();
            _psdFileSection = new PsdFileSectionVM();

            _validationMethods = new ValidationMethods();
            _validationMethods.Reset();

            InitGrid(gridValidCode);
            InitGrid(gridPsdLayer);
            InitGrid(gridErrorMsg);

            gridErrorMsg.ReadOnly = false;

            gridValidCode.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridPsdLayer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridErrorMsg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            gridPsdLayer.DataSource = _validationMethods.DSPsdLayerVMList;
            gridValidCode.DataSource = _validationMethods.DsValidCodeList;
            gridErrorMsg.DataSource = _validationMethods.DsErrorMsgList;

            //DataBinding(txtFileName, "Text", _psdFile, Masco.Core.Helper.Refrection.GetPropName<PsdFileVM>(x => x.Name));
            //DataBinding(txtFileExtension, "Text", _psdFile, Masco.Core.Helper.Refrection.GetPropName<PsdFileVM>(x => x.Extension));
            //DataBinding(txtNumberOfChannels, "Text", _psdFileSection, Masco.Core.Helper.Refrection.GetPropName<PsdFileSectionVM>(x => x.NumberOfChannels));
            //DataBinding(txtFileSectionColorMode, "Text", _psdFileSection, Masco.Core.Helper.Refrection.GetPropName<PsdFileSectionVM>(x => x.ColorMode));
            //DataBinding(txtFileSectionWidth, "Text", _psdFileSection, Masco.Core.Helper.Refrection.GetPropName<PsdFileSectionVM>(x => x.Width));
            //DataBinding(txtFileSectionHeight, "Text", _psdFileSection, Masco.Core.Helper.Refrection.GetPropName<PsdFileSectionVM>(x => x.Height));
            //DataBinding(txtFileSectionDepth, "Text", _psdFileSection, Masco.Core.Helper.Refrection.GetPropName<PsdFileSectionVM>(x => x.Depth));

            gridPsdLayer.SelectionChanged += gridPSD_SelectionChanged;

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

            {
                columnName = Masco.Core.Helper.Refrection.GetPropName<PsdLayerVM>(x => x.Index);
                gridPsdLayer.Columns[columnName].Width = 40;

                columnName = Masco.Core.Helper.Refrection.GetPropName<PsdLayerVM>(x => x.ParentIndex);
                gridPsdLayer.Columns[columnName].Width = 40;

                columnName = Masco.Core.Helper.Refrection.GetPropName<PsdLayerVM>(x => x.LayerDepth);
                gridPsdLayer.Columns[columnName].Width = 40;

                columnName = Masco.Core.Helper.Refrection.GetPropName<PsdLayerVM>(x => x.LayerSeq);
                gridPsdLayer.Columns[columnName].Width = 40;

                columnName = Masco.Core.Helper.Refrection.GetPropName<PsdLayerVM>(x => x.Name);
                gridPsdLayer.Columns[columnName].Frozen = true;
            }

            _fullValidCodeList = new List<ValidateVM>();
            var v = new Validate1();
            _fullValidCodeList = v.GetList();

            rdoILSType1.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType2.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType3.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType4.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType5.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType6.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType7.CheckedChanged += rdoILSType_CheckedChanged;
            rdoILSType8.CheckedChanged += rdoILSType_CheckedChanged;
            rdoAll.CheckedChanged += rdoILSTypeAll_CheckedChanged;
        }

        // 선택된 radio btn을 가져오는 것
        string GetSelectedILSType(out HashSet<string> ilsType)
        {
            var result = string.Empty;
            ilsType = new HashSet<string>();
            if (rdoILSType1.Checked)
            {
                ilsType.Add("CM");
                ilsType.Add("NC");
                result = "NC";
            }
            else if (
                rdoILSType2.Checked)
            {
                ilsType.Add("CM");
                ilsType.Add("JC");
                result = "JC";
            }
            else if (rdoILSType3.Checked)
            {
                ilsType.Add("CE");
                result = "CE";
            }
            else if (rdoILSType4.Checked)
            {
                ilsType.Add("ET");
                result = "ET";
            }
            else if (rdoILSType5.Checked)
            {
                ilsType.Add("MD");
                result = "MD";
            }
            else if (rdoILSType6.Checked)
            {
                ilsType.Add("CR3D");
                result = "CR3D";
            }
            else if (rdoILSType7.Checked)
            {
                ilsType.Add("RASMM");
                result = "RASMM";
            }
            else if (rdoILSType8.Checked)
            {
                ilsType.Add("RASMG");
                result = "RASMG";
            }
            return result;
        }

        void RefreshValidCodeList()
        {
            _validationMethods.DsValidCodeList.Clear();

            HashSet<string> ilsTypeSet;
            GetSelectedILSType(out ilsTypeSet);

            foreach (var x in _fullValidCodeList)
            {
                if (ilsTypeSet.Contains(x.ILSType))
                    _validationMethods.DsValidCodeList.Add(x);
            }
        }

        void rdoILSTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            _validationMethods.DsValidCodeList.Clear();
            foreach (var x in _fullValidCodeList)
            {
                _validationMethods.DsValidCodeList.Add(x);
            }
        }

        void rdoILSType_CheckedChanged(object sender, EventArgs e)
        {
            RefreshValidCodeList();
        }

        private void InitGrid(DataGridView g)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        #endregion InitControls

        #region OpenPSD
        private void OpenPSD()
        {
            // PSD 파일 경로
            var psdFullPath = txtPsdFullPath.Text;

            Console.WriteLine("########## psdFullPath");
            Console.WriteLine(psdFullPath);
            Console.WriteLine("####################");

            // file Exists 활용( 파일이 있는지 없는지 여부만 확인 )
            if (File.Exists(psdFullPath) == false)
            {
                return;
            }

            /*
             파일 복사, 이동, 이름 바꾸기, 만들기, 열기, 삭제 및 추가와 같은 일반적인 작업에 FileInfo 클래스를 사용  
            */

            var fileInfo = new FileInfo(psdFullPath);
            var fileName = fileInfo.Name;
            
            var pos = fileInfo.Name.LastIndexOf(".");
            if (pos < 0)
            {
                errorProvider1.SetError(txtFileExtension, "확장자명을 찾을 수 없습니다.");
                return;
            }

            _psdFile.Name = fileInfo.Name.Substring(0, pos);
            _psdFile.Extension = fileInfo.Name.Substring(pos + 1);

            if (_psdFile.Extension.Equals("PSD", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                errorProvider1.SetError(txtFileExtension, "확장자명이 PSD가 아닙니다.");
                MessageBox.Show("확장자 명이 PSD가 아닙니다.");
                return;
            }

            _validationMethods.DSPsdLayerVMList.Clear();

            this.Enabled = false;
            var bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                try
                {
                    _psdReader.ReadFile(psdFullPath);
                }
                catch (Exception ex)
                {
                    _errorStringReadFile = ex.ToString();
                }

            };

            bw.RunWorkerCompleted += (s, e) =>
            {
                // Common.Helper.CopyProperties(_psdReader, this._psdFileSection);
                this.Enabled = true;
                errorProvider1.Clear();
                this._psdFileSection.ColorMode = _psdReader.FileSectionColorMode;
                if (_psdReader.FileSectionColorMode != "RGB")
                {
                    var errorMsg = "이미지 컬러 모드가 RGB가 아닙니다";
                    txtFileSectionColorMode.Text = _psdReader.FileSectionColorMode;
                    errorProvider1.SetError(txtFileSectionColorMode, errorMsg);
                    if (_isAutoClose)
                    {
                        _validatePsdFileVM.Description = errorMsg;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMsg);
                        return;
                    }
                }

                this._psdFileSection.Width = _psdReader.FileSectionWidth;
                this._psdFileSection.Height = _psdReader.FileSectionHeight;
                this._psdFileSection.Depth = _psdReader.FileSectionDepth;
                this._psdFileSection.NumberOfChannels = _psdReader.NumberOfChannels;
                this._psdFileSection.ChannelTypes = _psdReader.FileChannelTypes;
                this._psdFileSection.Pixel = _psdReader.FileSectionPixel; ;

                //var pixel = _psdReader.FileSectionPixel;

                //if (pixel != "72*72")
                //{
                //    var errorMsg = "픽셀 값 오류";
                //    txtFileSectionPixel.Text = pixel;
                //    errorProvider1.SetError(txtFileSectionPixel, errorMsg);
                //    if (_isAutoClose)
                //    {
                //        _validatePsdFileVM.Description = errorMsg;
                //        this.Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show(errorMsg);
                //        //초기화?
                //        _psdReader.FileSectionPixel = string.Empty;
                //        return;
                //    }
                //}
                //this._psdFileSection.Pixel = pixel;
                SetTextBox();

                // psdReader Class 이해 필요( GetList() 메소드를 활용하여 모든 layer의 구조를 가져오게 구현되어 있음 )
                var list = _psdReader.GetList();
                foreach (var x in list)
                {
                    _validationMethods.DSPsdLayerVMList.Add(x);
                }


                if (this._validatePsdFileVM != null)
                {
                    btnValidate.PerformClick();
                    chboxFilterByArrow.Checked = _isFilterByArrow;                   
                    ExportLayerList();
                }

            };
            bw.RunWorkerAsync();
        }

        #endregion OpenPSD

        #region ContextMenu
        private void tsmCopySelectedLayer_Click(object sender, EventArgs e)
        {
            if (gridPsdLayer.SelectedRows.Count != 1)
                return;

            var item = gridPsdLayer.SelectedRows[0].DataBoundItem as PsdLayerVM;
            if (item == null)
                return;

            var vm = new PsdLayerVM();
            Common.Helper.CopyProperties(item, vm);
            var index = _validationMethods.DSPsdLayerVMList.IndexOf(item);

            _validationMethods.DSPsdLayerVMList.Insert(index + 1, vm);
        }

        private void tsmEditMode_Click(object sender, EventArgs e)
        {
            gridPsdLayer.ReadOnly = false;
            tsmEditMode.Checked = !tsmEditMode.Checked;
            //gridPSD.ReadOnly = !tsmEditMode.Checked;

            if (tsmEditMode.Checked == false)
            {
                gridPsdLayer.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                gridPsdLayer.EditMode = DataGridViewEditMode.EditOnF2;
            }
        }

        private void tsmRemoveSelectedLayer_Click(object sender, EventArgs e)
        {
            if (gridPsdLayer.SelectedRows.Count != 1)
                return;

            var item = gridPsdLayer.SelectedRows[0].DataBoundItem as PsdLayerVM;
            if (item == null)
                return;

            _validationMethods.DSPsdLayerVMList.Remove(item);
            //gridPSD.Refresh();
        }
        #endregion

        private void ShowLog(string msg)
        {
            tsslLog.Text = string.Format("{0:yyyy/MM/dd hh:mm:ss} {1}", DateTime.Now, msg);
        }

        //internal void Setup(string ilsType, string fileName)
        internal void Setup(ValidatePsdFileVM vm, bool isAutoClose = true)
        {
            // internal이란 접근제한자 이해 필요, 멀티검증에서 사용하고 있는 메소드
            this._isAutoClose = isAutoClose;
            if (vm.ILS_Type == ILSType.Code1_NC)
            {
                rdoILSType1.Checked = true;
            }
            if (vm.ILS_Type == ILSType.Code2_JC)
            {
                rdoILSType2.Checked = true;
            } if (vm.ILS_Type == ILSType.Code3_CE)
            {
                rdoILSType3.Checked = true;
            } if (vm.ILS_Type == ILSType.Code4_ET)
            {
                rdoILSType4.Checked = true;
            }
            if(vm.ILS_Type == ILSType.Code5_MimeticDiagram)
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
            } if (vm.ILS_Type == ILSType.Code3_CE)
            {
                rdoILSType3.Checked = true;
            } if (vm.ILS_Type == ILSType.Code4_ET)
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

        private void tsmSelectCell_Click(object sender, EventArgs e)
        {
            gridPsdLayer.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void tsmSelectRow_Click(object sender, EventArgs e)
        {       gridPsdLayer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnListExport_Click(object sender, EventArgs e)
        {
            ExportLayerList();
        }

        public void ExportLayerList()
        {
            //var date = DateTime.Now.ToString("yyyyMMdd");
            //var path = string.Format("{0}\\PasLayerList_{1}.csv", _exportFilePath, date);
            //var psdFilePath = txtPsdFullPath.Text;
            //psdFilePath = psdFilePath.Substring(psdFilePath.LastIndexOf("\\")+1);
            //var width = txtFileSectionWidth.Text;
            //var height = txtFileSectionHeight.Text;

            //using (var fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("euc-kr")))
            //{
            //    const string CSV_SPLITER = ",";
            //    {
            //        fs.WriteLine(string.Format("{0}{1}", psdFilePath, CSV_SPLITER));
            //        fs.WriteLine(string.Format("Width : {0}{1}", width, CSV_SPLITER));
            //        fs.WriteLine(string.Format("Height : {0}{1}", height, CSV_SPLITER));
            //    }
            //    {
            //        var psdLayerList = _validationMethods.DSPsdLayerVMList;
            //        foreach (var psdlayer in psdLayerList)
            //        {
            //            var sb = new StringBuilder();

            //            if (chboxFilterByArrow.Checked)
            //            {
            //                if (psdlayer.Name.StartsWith("Arrow_"))
            //                {
            //                    var psdlayerName = string.Format("\"{0}\"", psdlayer.Name);
            //                    sb.Append(psdlayerName); sb.Append(CSV_SPLITER);
            //                    var stringLine = sb.ToString();
            //                    fs.WriteLine(stringLine.Replace("\r", string.Empty).Replace("\n", string.Empty));
            //                }
            //            }
            //            else
            //            {
            //                var psdlayerName = string.Format("\"{0}\"", psdlayer.Name);
            //                sb.Append(psdlayerName); sb.Append(CSV_SPLITER);
            //                var stringLine = sb.ToString();
            //                fs.WriteLine(stringLine.Replace("\r", string.Empty).Replace("\n", string.Empty));
            //            }
            //        }
            //    }
            //}
            var date = DateTime.Now.ToString("yyyyMMdd");
            var path = string.Format("{0}\\PasLayerList_{1}.csv", _exportFilePath, date);
            var psdFilePath = txtPsdFullPath.Text;
            psdFilePath = psdFilePath.Substring(psdFilePath.LastIndexOf("\\") + 1);
            var width = txtFileSectionWidth.Text;
            var height = txtFileSectionHeight.Text;

            using (var fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("euc-kr")))
            {
                const string CSV_SPLITER = ",";
                {
                    fs.WriteLine(string.Format("FileName"));
                    var fileInfo = string.Format("{0}{1}Width:{2} X Height:{3}{4}", psdFilePath, CSV_SPLITER, width, height, CSV_SPLITER);
                    fs.WriteLine(fileInfo);
                }
                {
                    var psdLayerList = _validationMethods.DSPsdLayerVMList;
                    foreach (var psdlayer in psdLayerList)
                    {
                        var sb = new StringBuilder();

                        if (chboxFilterByArrow.Checked)
                        {
                            if (psdlayer.Name.StartsWith("Arrow_"))
                            {
                                var psdlayerName = string.Format("\"{0}\"", psdlayer.Name);
                                sb.Append(psdFilePath); sb.Append(CSV_SPLITER);
                                sb.Append(psdlayerName); sb.Append(CSV_SPLITER);
                                var stringLine = sb.ToString();
                                fs.WriteLine(stringLine.Replace("\r", string.Empty).Replace("\n", string.Empty));
                            }
                        }
                        else
                        {
                            var psdlayerName = string.Format("\"{0}\"", psdlayer.Name);
                            sb.Append(psdFilePath); sb.Append(CSV_SPLITER);
                            sb.Append(psdlayerName); sb.Append(CSV_SPLITER);
                            var stringLine = sb.ToString();
                            fs.WriteLine(stringLine.Replace("\r", string.Empty).Replace("\n", string.Empty));
                        }
                    }
                }
            }
        }

    }
}