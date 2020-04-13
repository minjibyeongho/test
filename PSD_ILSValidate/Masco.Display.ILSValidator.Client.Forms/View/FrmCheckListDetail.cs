using Masco.Display.ILSValidator.Client.Common;
using Masco.Display.ILSValidator.Client.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public partial class FrmCheckListDetail : Form
    {
        private ILSValidatorVM _vm = null;
        private BindingList<RequestVM> _requestList = null;
        private BindingList<LaneInfoVM> _laneInfoList = null;

        public Action<ILSValidatorVM> InsertQuery { get; set; }
        public Action<ILSValidatorVM> UpdateQuery { get; set; }

        private bool _isUpdatedDescription;

        public FrmCheckListDetail()
        {
            InitializeComponent();
            InitContols();
        }

        private void ucLaneInfo_Load(object sender, EventArgs e)
        {
        }

        #region InitContols()

        private void DataBinding(Control control, string ctrPropertyName, string vmPropertyName, object defaultValue = null)
        {
            var binding = control.DataBindings.Add(ctrPropertyName, _vm, vmPropertyName, true, DataSourceUpdateMode.OnPropertyChanged, defaultValue);
            var bn = new BindingNotifier(binding);
            _vm.PropertyChanged += bn.Binding_WakeUpCall;
        }

        private void InitGrid<T>(DataGridView g, IList<T> source)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //            g.DataSource = new BindingList<T>(source);
            g.DataSource = source;
        }

        private void InitDialog(Form dlg)
        {
            dlg.Owner = this;
            dlg.StartPosition = FormStartPosition.CenterParent;
        }

        private void InitContols()
        {
            this.StartPosition = FormStartPosition.CenterParent;

            _vm = new ILSValidatorVM();

            DataBinding(txtPk, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.CHECKLIST_PK), 0);
            DataBinding(txtWorkState, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.WORK_STATE), string.Empty);

            DataBinding(txtMainCode, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MAIN_CODE), string.Empty);
            DataBinding(txtName, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.NAME), string.Empty);
            DataBinding(chkIsName, "Checked", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.IS_NAME), false);
            DataBinding(txtMapId, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MAP_ID), string.Empty);
            DataBinding(txtNodeId, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.NODE_ID), string.Empty);
            DataBinding(txtMMSLonLat, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MMS_LONLAT), string.Empty);

            {
                var ucReqType = new UcRadioButton();
                ucReqType.Setup(new ReqType().GetList());
                ucReqType.Dock = DockStyle.Fill;
                panelReqType.Controls.Add(ucReqType);
                DataBinding(ucReqType, "SelectedText", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.REQ_TYPE), string.Empty);
            }
            {
                var ucILSType = new UcRadioButton();
                ucILSType.Setup(new ILSType().GetList());
                ucILSType.Dock = DockStyle.Fill;
                panelILSType.Controls.Add(ucILSType);
                DataBinding(ucILSType, "SelectedText", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.ILS_TYPE), string.Empty);
            }

            DataBinding(txtCreateDT, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.CREATE_DT), new DateTime(9999, 12, 31));
            DataBinding(txtUpdateDT, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.UPDATE_DT), new DateTime(9999, 12, 31));

            DataBinding(txtApplyAM, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_AM), string.Empty);
            DataBinding(txtApplyOBN, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OBN), string.Empty);
            DataBinding(txtApplyOEM, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OEM), string.Empty);

            DataBinding(txtReqUserName, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.REQ_UNAME), string.Empty);
            DataBinding(txtUpdateUserName, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.UPDATE_UNAME), string.Empty);

            DataBinding(txtRemarks, "Text", Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.Remarks), string.Empty);
            txtRemarks.TextChanged += txtDescription_TextChanged;

            string columnName;
            _laneInfoList = new BindingList<LaneInfoVM>();
            InitGrid(gridLaneInfo, _laneInfoList);
            gridLaneInfo.DoubleClick += gridLaneInfo_DoubleClick;
            {
                //columnName = Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.LANEINFO_PK);
                //columnName = Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.CHECKLIST_PK);

                columnName = Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.DIRECTION);
                gridLaneInfo.Columns[columnName].Width = 55;

                columnName = Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.NUMBER);
                gridLaneInfo.Columns[columnName].Width = 55;

                columnName = Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.IMAGE_CODE);
                gridLaneInfo.Columns[columnName].Width = 200;
            }

            _requestList = new BindingList<RequestVM>();
            InitGrid(gridRequest, _requestList);
            gridRequest.DoubleClick += gridRequest_DoubleClick;
            {
                columnName = Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.IDX);
                gridRequest.Columns[columnName].Width = 55;

                columnName = Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.CREATE_DT);
                gridRequest.Columns[columnName].Width = 150;

                columnName = Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.REQ_KIND);
                gridRequest.Columns[columnName].Width = 100;

                columnName = Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.DESCRIPTION);
                gridRequest.Columns[columnName].Width = 400;
            }

            picCaptureImageBasemap.SizeMode = PictureBoxSizeMode.Zoom;
            picCaptureImageRef.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _isUpdatedDescription = true;
        }

        #endregion InitContols()

        internal void Setup(ILSValidatorVM item)
        {
            if (item == null)
            {
                btnUpdate.Enabled = false;
            }
            else
            {
                Common.Helper.CopyProperties(item, _vm);

                var reqList = Proxy.Instance.GetRequestList(item.CHECKLIST_PK, null);
                foreach (var x in reqList.Where(x => x.IS_DELETE == false).OrderByDescending(x => x.IDX))
                    _requestList.Add(x);

                var laneInfoList = Proxy.Instance.GetLaneInfoListBy(item.CHECKLIST_PK);
                foreach (var x in laneInfoList.OrderBy(x => x.NUMBER))
                    _laneInfoList.Add(x);

                var remarks = Proxy.Instance.GetMemoBy(item.CHECKLIST_PK);
                _vm.Remarks = remarks;

                ////var stream = Proxy.Instance.GetCaptureImageBy(item.CI_PK_BASEMAP);
                ////picCaptureImageBasemap.Image = Image.FromStream(stream);
                GetImageAsync(item.CHECKLIST_PK, true, picCaptureImageBasemap);
                GetImageAsync(item.CHECKLIST_PK, false, picCaptureImageRef);

                btnInsert.Enabled = false;
            }
        }

        internal void GetImageAsync(int checkListPk, bool isBasemap, PictureBox pic)
        {
            pic.Image = null;

            var imagePk = Proxy.Instance.GetImagePkBy(checkListPk, isBasemap);
            if (imagePk < 1)
                return;

            if (isBasemap)
                _vm.ImagePk_BaseMap = imagePk;
            else
                _vm.ImagePk_Reference = imagePk;

            Proxy.Instance.DownloadCaptureImageByAsync(imagePk, (bytes) =>
            {
                if (bytes != null && bytes.Length > 0)
                {
                    var ms = new MemoryStream(bytes, 0, bytes.Length);
                    ms.Write(bytes, 0, bytes.Length);
                    var returnImage = Image.FromStream(ms, true);//Exception occurs here
                    pic.Image = returnImage;
                }
            });
        }

        private void ucNameListLane_Load(object sender, EventArgs e)
        {
        }

        private bool Valid(bool isNew)
        {
            //Diction<ErrorProvider> _ep = new List<ErrorProvider>();
            var count = 0;

            //public int CHECKLIST_PK { get; set; }
            if (isNew && _vm.CHECKLIST_PK > 0)
            {
                errorProvider1.SetError(txtPk, "신규레코드는 코드값을 할당할 수 없습니다.");
                count++;
            }

            //public DateTime CreateDT { get;set; }
            //if (_vm.CreateDT < DateTime.MinValue)
            //{
            //    errorProvider1.SetError(txtPk, "날짜를 확인하세요.");
            //}

            //public DateTime UpdateDT { get; set; }
            //if (_vm.UpdateDT < DateTime.MinValue)
            //{
            //    errorProvider1.SetError(txtPk, "날짜를 확인하세요.");
            //}

            //public string ReqUserName { get; set; }
            if (string.IsNullOrWhiteSpace(_vm.REQ_UNAME))
            {
                errorProvider1.SetError(txtReqUserName, "요청자명을 입력하세요.");
                count++;
            }

            //public string UpdateUserName { get; set; }

            //public string ReqType { get; set; }
            //public string ILSType { get; set; }
            //public string MainCode { get; set; }
            //public string Name { get; set; }
            //public bool IsName { get; set; }
            //public int MapId { get; set; }
            //public int NodeId { get; set; }
            //public string MMSLonLat { get; set; }
            //public Int16 LaneCount { get; set; }
            //public string LaneName { get; set; }
            //public Int16 ImageCount { get; set; }
            //public string ImageName { get; set; }
            //public string BottomName { get; set; }
            //public string ImageRequest { get; set; }

            ////public Stream CaptureBaseMap { get; set; }
            ////public Stream CaptureReference { get; set; }
            //public int CI_PK_BASEMAP{ get; set; }
            //public int CI_PK_REFERENCE { get; set; }

            //public string CaptureBasemapImgPath { get; set; }
            //public string CaptureReferenceImgPath { get; set; }

            //public string ApplyAMTeam1 { get; set; }
            //public string ApplyAMTeam2 { get; set; }
            //public string ApplyAMTeam3 { get; set; }

            //public string ApplyOEMTeam1 { get; set; }
            //public string ApplyOEMTeam2 { get; set; }
            //public string ApplyOEMTeam3 { get; set; }

            //public string ApplyOBNTeam1 { get; set; }
            //public string ApplyOBNTeam2 { get; set; }
            //public string ApplyOBNTeam3 { get; set; }

            //public int DESC_PK{ get; set; }
            //public string Description { get; set; }
            return count == 0;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (Valid(true) == false)
                return;

            var pk = Proxy.Instance.InsertCheckList(_vm);
            if (InsertQuery == null)
            {
                if (pk < 1)
                {
                    InsertQuery.Invoke(null);
                }
                else
                {
                    _vm.CHECKLIST_PK = pk;
                    InsertQuery.Invoke(_vm);
                }
            }
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var pk = Proxy.Instance.UpdateItem(_vm);

            if (pk < 1)
            {
                UpdateQuery.Invoke(null);
                return;
            }

            if (_isUpdatedDescription)
            {
                Proxy.Instance.InsertMemo(_vm.CHECKLIST_PK, txtRemarks.Text);
            }

            UpdateQuery.Invoke(_vm);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCaptureImageBasemap_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.ShowDialog(this);
            dlg.Multiselect = false;
            //dlg.AddExtension = "jpg|*.jpg|png|*.png";
            dlg.DefaultExt = "JPG File (*.jpg)|*.jpg|PNG File (*.png) |*.png|BMP File (*.bmp) |*.bmp";
            var fileName = dlg.FileName;

            try
            {
                picCaptureImageBasemap.Image = new Bitmap(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCaptureImageBasemapFromClipboard_Click(object sender, EventArgs e)
        {
            var pictureBox = picCaptureImageBasemap;

            pictureBox.Image = Clipboard.GetImage();
            var clipboardImage = Clipboard.GetImage();
            if (clipboardImage == null)
            {
                MessageBox.Show("클립보드에 이미지가 없습니다.");
                return;
            }

            pictureBox.Image = clipboardImage;
            var stream = new System.IO.MemoryStream();
            clipboardImage.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            var errorMsg = string.Empty;
            var checkListPk = _vm.CHECKLIST_PK;
            var captureImagePk = Proxy.Instance.UploadCaptureFile(checkListPk, true, stream, out errorMsg);
            if (captureImagePk < 0)
            {
                MessageBox.Show(errorMsg);
                return;
            }
            _vm.ImagePk_BaseMap = captureImagePk;
        }

        private void btnCaptureImageBasemapClear_Click(object sender, EventArgs e)
        {
            var resultPk = Proxy.Instance.DeleteImageIsUseBy(_vm.ImagePk_BaseMap);
            if (resultPk <= 0)
            {
                MessageBox.Show("Basemap 이미지 초기화 실패.");
                return;
            }
            picCaptureImageBasemap.Image = null;
        }

        private void btnCaptureImageRefFromClipboard_Click(object sender, EventArgs e)
        {
            var pictureBox = picCaptureImageRef;
            var clipboardImage = Clipboard.GetImage();
            if (clipboardImage == null)
            {
                MessageBox.Show("클립보드에 이미지가 없습니다.");
                return;
            }
            pictureBox.Image = clipboardImage;

            var stream = new System.IO.MemoryStream();
            clipboardImage.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            var errorMsg = string.Empty;
            var checkListPk = _vm.CHECKLIST_PK;
            var captureImagePk = Proxy.Instance.UploadCaptureFile(checkListPk, false, stream, out errorMsg);
            if (captureImagePk < 0)
            {
                MessageBox.Show(errorMsg);
                return;
            }
            _vm.ImagePk_Reference = captureImagePk;
        }

        private void btnCaptureImageRefClear_Click(object sender, EventArgs e)
        {
            var resultPk = Proxy.Instance.DeleteImageIsUseBy(_vm.ImagePk_Reference);
            if (resultPk <= 0)
            {
                MessageBox.Show("참조 이미지 초기화 실패.");
                return;
            }
            picCaptureImageRef.Image = null;
        }

        private void picCaptureImageBasemap_DoubleClick(object sender, EventArgs e)
        {
            var dlg = new FrmImage();
            dlg.Setup(picCaptureImageBasemap.Image, 0);
            dlg.Show(this);
        }

        private void picCaptureImageRef_DoubleClick(object sender, EventArgs e)
        {
            var dlg = new FrmImage();
            dlg.Setup(picCaptureImageRef.Image, 0);
            dlg.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnPropertyObject_Click(object sender, EventArgs e)
        {
            var dlg = new FrmPropertyObject();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.Setup(_vm);
            dlg.ShowDialog(this);
        }

        #region REQUEST

        private void btnRequestAdd_Click(object sender, EventArgs e)
        {
            var dlg = new FrmRequest();
            InitDialog(dlg);

            var item = new RequestVM();
            item.CHECKLIST_PK = _vm.CHECKLIST_PK;
            dlg.Setup(item);
            dlg.OnInsertQuery = FrmRequest_OnInsertQuery;
            dlg.ShowDialog(this);
        }

        private void gridRequest_DoubleClick(object sender, EventArgs e)
        {
            if (gridRequest.SelectedRows.Count != 1)
                return;

            var dlg = new FrmRequest();
            InitDialog(dlg);

            var item = gridRequest.SelectedRows[0].DataBoundItem as RequestVM;

            dlg.Setup(item);
            dlg.OnUpdateQuery = FrmRequest_OnUpdateQuery;
            dlg.OnDeleteQuery = FrmRequest_OnDeleteQuery;

            dlg.ShowDialog(this);
        }

        private void FrmRequest_OnInsertQuery(RequestVM vm)
        {
            _requestList.Insert(0, vm);
        }

        private void FrmRequest_OnUpdateQuery(RequestVM vm)
        {
            var item = _requestList.SingleOrDefault(x => x.CHECKLIST_PK == vm.CHECKLIST_PK && x.IDX == vm.IDX);
            Common.Helper.CopyProperties(vm, item);
            gridLaneInfo.Refresh();
        }

        private void FrmRequest_OnDeleteQuery(int idx)
        {
            var item = _requestList.SingleOrDefault(x => x.CHECKLIST_PK == _vm.CHECKLIST_PK && x.IDX == idx);
            _requestList.Remove(item);
            gridRequest.Refresh();
        }

        #endregion REQUEST

        #region LANEINFO

        private void btnLaneInfoAdd_Click(object sender, EventArgs e)
        {
            var dlg = new FrmLaneInfo();
            InitDialog(dlg);

            var item = new LaneInfoVM();
            item.CHECKLIST_PK = _vm.CHECKLIST_PK;
            dlg.Setup(item);
            dlg.OnInsertQuery = FrmLaneInfo_OnInsertQuery;

            dlg.ShowDialog(this);
        }

        private void gridLaneInfo_DoubleClick(object sender, EventArgs e)
        {
            if (gridLaneInfo.SelectedRows.Count != 1)
                return;

            var dlg = new FrmLaneInfo();
            InitDialog(dlg);

            var item = gridLaneInfo.SelectedRows[0].DataBoundItem as LaneInfoVM;

            dlg.Setup(item);
            dlg.OnUpdateQuery = FrmLaneInfo_OnUpdateQuery;
            dlg.OnDeleteQuery = FrmLaneInfo_OnDeleteQuery;

            dlg.ShowDialog(this);
        }

        private void LaneNumberSort()
        {
            var list = _laneInfoList.OrderBy(x => x.NUMBER).ToList();
            _laneInfoList.Clear();
            foreach (var x in list)
                _laneInfoList.Add(x);
            gridLaneInfo.Refresh();
        }

        private void FrmLaneInfo_OnInsertQuery(LaneInfoVM vm)
        {
            //var pos = 0;
            //if (_laneInfoList.Count == 0)
            //    _laneInfoList.Add(vm);
            //else
            //{
            //    for (int idx = 0; idx < _laneInfoList.Count(); idx++)
            //    {
            //        if (vm.NUMBER >= _laneInfoList[idx].NUMBER)
            //        {
            //            _laneInfoList.Insert(idx, vm);
            //            break;
            //        }
            //    }
            //}
            _laneInfoList.Add(vm);
            LaneNumberSort();
        }

        private void FrmLaneInfo_OnUpdateQuery(LaneInfoVM vm)
        {
            var item = _laneInfoList.SingleOrDefault(x => x.LANEINFO_PK == vm.LANEINFO_PK);
            Common.Helper.CopyProperties(vm, item);
            LaneNumberSort();
        }

        private void FrmLaneInfo_OnDeleteQuery(int laneInfoPk)
        {
            var item = _laneInfoList.SingleOrDefault(x => x.LANEINFO_PK == laneInfoPk);
            _laneInfoList.Remove(item);
            gridLaneInfo.Refresh();
        }

        #endregion LANEINFO
    }

    public class BindingNotifier
    {
        private Binding binding;

        public BindingNotifier(Binding binding)
        {
            this.binding = binding;
        }

        public void Binding_WakeUpCall(object sender, PropertyChangedEventArgs e)
        {
            binding.ReadValue();
        }
    }
}