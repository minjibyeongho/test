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
    public partial class FrmRequest : Form
    {
        private RequestVM _vm;
        public Action<RequestVM> OnUpdateQuery { get; set; }
        public Action<RequestVM> OnInsertQuery { get; set; }
        public Action<int> OnDeleteQuery { get; set; }

        public FrmRequest()
        {
            InitializeComponent();
            InitControls();
        }

        private void DataBinding(Control control, string ctrPropertyName, string vmPropertyName, object defaultValue = null)
        {
            var binding = control.DataBindings.Add(ctrPropertyName, _vm, vmPropertyName, true, DataSourceUpdateMode.OnPropertyChanged, defaultValue);
            var bn = new BindingNotifier(binding);
            _vm.PropertyChanged += bn.Binding_WakeUpCall;
        }

        private void InitControls()
        {
            _vm = new RequestVM();

            DataBinding(txtCheckListPK, "Text", Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.CHECKLIST_PK));
            DataBinding(txtCreateDt, "Text", Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.CREATE_DT));
            DataBinding(txtDescription, "Text", Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.DESCRIPTION));
            DataBinding(txtIdx, "Text", Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.IDX));
            DataBinding(txtReqKind, "Text", Masco.Core.Helper.Refrection.GetPropName<RequestVM>(x => x.REQ_KIND));
        }

        internal void Setup(RequestVM vm)
        {
            if (vm.IDX > 0)
                btnInsert.Enabled = false;
            else
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            Common.Helper.CopyProperties(vm, _vm);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var idx = Proxy.Instance.InsertRequest(_vm);
            if (idx <= 0)
                return;
            _vm.IDX = idx;
            var item = Proxy.Instance.GetRequestBy(_vm.CHECKLIST_PK, _vm.IDX);
            if (item == null)
                return;

            _vm.CREATE_DT = item.CREATE_DT;
            if (OnInsertQuery != null)
                OnInsertQuery.Invoke(_vm);
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var idx = Proxy.Instance.UpdateRequest(_vm);
            if (idx <= 0)
                return;

            if (OnUpdateQuery != null)
                OnUpdateQuery.Invoke(_vm);

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "요청", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;

            var idx = Proxy.Instance.DeleteRequestBy(_vm.CHECKLIST_PK, _vm.IDX);
            if (idx <= 0)
                return;

            if (OnDeleteQuery != null)
                OnDeleteQuery.Invoke(idx);

            this.Close();
        }


    }
}
