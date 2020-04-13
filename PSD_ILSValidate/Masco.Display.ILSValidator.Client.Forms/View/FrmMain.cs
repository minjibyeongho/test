using Masco.Display.ILSValidator.Client.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public partial class FrmMain : Form
    {
        BindingList<ILSValidatorVM> _datasource = null;
        public FrmMain()
        {
            InitializeComponent();

            if (DateTime.Now >= new DateTime(2020, 12, 31))
            {
                MessageBox.Show("프로그램 사용만기일이 지났습니다.");
                this.Close();
            }

            InitControls();

            //this.Shown += (s, e) => { tsmValidatePSDFile.PerformClick();  };
        }

        private void InitControls()
        {
            _datasource = new BindingList<ILSValidatorVM>();

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            grid.DataSource = _datasource;

            grid.SelectionChanged += grid_SelectionChanged;
            grid.CellMouseDoubleClick += grid_CellMouseDoubleClick;

            #region column
            var columnName = string.Empty;
            {
                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_AM);
                grid.Columns[columnName].Width = 110;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OBN);
                grid.Columns[columnName].Width = 110;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OEM);
                grid.Columns[columnName].Width = 110;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.BOTTOM_NAME);
                grid.Columns[columnName].Width = 100;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.CHECKLIST_PK);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.CREATE_DT);
                grid.Columns[columnName].Width = 160;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.ILS_TYPE);
                grid.Columns[columnName].Width = 120;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.IS_NAME);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MAIN_CODE);
                grid.Columns[columnName].Width = 180;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MAP_ID);
                grid.Columns[columnName].Width = 100;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MMS_LONLAT);
                grid.Columns[columnName].Width = 200;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.NAME);
                grid.Columns[columnName].Width = 100;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.NODE_ID);
                grid.Columns[columnName].Width = 100;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.REQ_TYPE);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.REQ_UNAME);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.UPDATE_DT);
                grid.Columns[columnName].Width = 160;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.UPDATE_UNAME);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.WORK_STATE);
                grid.Columns[columnName].Width = 80;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.ImagePk_BaseMap);
                grid.Columns[columnName].Visible = false;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.ImagePk_Reference);
                grid.Columns[columnName].Visible = false;

                columnName = Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.Remarks);
                grid.Columns[columnName].Visible = false;
            } 
            #endregion
        }

        void grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowDetail();
        }

        private void ShowDetail()
        {
            if (grid.SelectedRows.Count < 1)
            {
                propertyGrid.SelectedObject = null;
                return;
            }
            var row = grid.SelectedRows[0];
            var item = row.DataBoundItem as ILSValidatorVM;
            if (item == null)
            {
                MessageBox.Show("데이터 타입을 확인하세요.");
                return;
            }

            var dlg = new FrmCheckListDetail();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.Setup(item);
            dlg.InsertQuery = OnInsertQuery;
            dlg.UpdateQuery = OnUpdateQuery;
            dlg.Show(this);
        }

        void OnInsertQuery(ILSValidatorVM vm)
        {
            if (vm == null)
            {
                MessageBox.Show("추가 실패");
                return;
            }
            _datasource.Add(vm);
            MessageBox.Show("추가 완료");
        }

        void OnUpdateQuery(ILSValidatorVM vm)
        {
            if (vm == null)
            {
                MessageBox.Show("수정 실패");
                return;
            }

            var item = _datasource.FirstOrDefault(x => x.CHECKLIST_PK == vm.CHECKLIST_PK);
            if (item != null)
            {
                Common.Helper.CopyProperties(vm, item);
            }
            grid.Refresh();
            MessageBox.Show("수정 완료");
        }

        void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count < 1)
            {
                propertyGrid.SelectedObject = null;
                return;
            }
            var row = grid.SelectedRows[0];
            var item = row.DataBoundItem;
            propertyGrid.SelectedObject = item;

        }

        #region GetFilterDate
        private string GetFilterDate()
        {
            var arr = txtCreateDT.Text.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime d1, d2;
            string a1 = string.Empty;
            string a2 = string.Empty;
            if (arr.Length == 1)
            {
                a1 = arr[0];
                a2 = arr[0];
            }
            else if (arr.Length == 2)
            {
                a1 = arr[0];
                a2 = arr[1];
            }
            else
            {
                return null;
            }

            if (DateTime.TryParse(a1, out d1) == false || DateTime.TryParse(a2, out d2) == false)
                return null;

            if (d1 > d2)
            {
                var temp = d2;
                d2 = d1;
                d1 = temp;
            }
            return string.Format("{0:yyyy/MM/dd}~{1:yyyy/MM/dd}", d1.Date, d2.Date);
        } 
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filter = new Dictionary<string, string>();
            var value = string.Empty;

            value = GetFilterDate();
            if (value == null)
            {
                MessageBox.Show("요청날짜를 입력하세요 (yyyy/MM/dd~yyyy/MM/dd)");
                return;
            }

            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.CREATE_DT), string.Format("{0}", value));

            value = txtWorkState.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.WORK_STATE), string.Format("{0}", value));

            value = txtMainCode.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.MAIN_CODE), string.Format("{0}", value));

            value = txtApplyAM.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_AM), string.Format("{0}", value));

            value = txtApplyOBN.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OBN), string.Format("{0}", value));

            value = txtApplyOEM.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.APPLY_OEM), string.Format("{0}", value));

            value = txtReqType.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.REQ_TYPE), string.Format("{0}", value));

            value = txtILSType.Text;
            if (string.IsNullOrWhiteSpace(value) == false)
                filter.Add(Masco.Core.Helper.Refrection.GetPropName<ILSValidatorVM>(x => x.ILS_TYPE), string.Format("{0}", value));

            try
            {

                var result = Proxy.Instance.GetCheckListBy(filter);
                _datasource.Clear();
                foreach (var x in result)
                {
                    _datasource.Add(x);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void tsmInputAdd_Click(object sender, EventArgs e)
        {
            var dlg = new FrmLaneInfo();
            dlg.Setup(null);
            dlg.ShowDialog(this);
        }


        private void tsmValidatePSDFile_Click(object sender, EventArgs e)
        {
            var dlg = new FrmValidatePsdFile();
            dlg.Show(this);
        }

        private void tsmFilterReset_Click(object sender, EventArgs e)
        {
            foreach (var x in grpFilter.Controls.OfType<TextBox>())
            {
                if (x == txtCreateDT)
                    continue;
                x.Clear();
            }
            txtReqType.Clear();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            tsmValidatePSDFile.PerformClick();
        }

        private void tsmValidateMultiPsdFile_Click(object sender, EventArgs e)
        {
            var dlg = new FrmValidateMultiPsdFile();
            dlg.Show();
        }

        private void tsmValidatePsdArrowCode_Click(object sender, EventArgs e)
        {
             var dlg = new FrmValidatePsdArrowCode();
            dlg.Show();
        }

        private void txtCreateDT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}