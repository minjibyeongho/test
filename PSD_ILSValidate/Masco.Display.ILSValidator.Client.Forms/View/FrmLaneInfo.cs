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
    public partial class FrmLaneInfo : Form
    {
        private LaneInfoVM _vm;
        public Action<LaneInfoVM> OnUpdateQuery { get; set; }
        public Action<LaneInfoVM> OnInsertQuery { get; set; }
        public Action<int> OnDeleteQuery { get; set; }

        public FrmLaneInfo()
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
            _vm = new LaneInfoVM();
            
            DataBinding(txtLaneInfoPk, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.LANEINFO_PK));
            DataBinding(txtCheckListPK, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.CHECKLIST_PK));
            DataBinding(txtDirection, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.DIRECTION));
            DataBinding(txtImageCode, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.IMAGE_CODE));
            DataBinding(txtImageName, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.IMAGE_NAME));
            DataBinding(txtLaneName, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.LANE_NAME));
            DataBinding(txtNumber, "Text", Masco.Core.Helper.Refrection.GetPropName<LaneInfoVM>(x => x.NUMBER));
        }

        public void Setup(LaneInfoVM vm)
        {
            if (vm.LANEINFO_PK > 0)
            {
                btnInsert.Enabled = false;
            }
            else
            { 
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }

            Common.Helper.CopyProperties(vm, _vm);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var laneInfoPk = Proxy.Instance.InsertLaneInfo(_vm);
            if (laneInfoPk > 0)
            {
                _vm.LANEINFO_PK = laneInfoPk;
                if (OnInsertQuery != null)
                    OnInsertQuery.Invoke(_vm);
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var laneInfoPk = Proxy.Instance.UpdateLaneInfo(_vm);
            if (laneInfoPk > 0)
            {
                if (OnUpdateQuery != null)
                    OnUpdateQuery.Invoke(_vm);

                this.Close();
            }
        }
      
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("차선정보를 삭제하시겠습니까?", "요청", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;

            var idx = Proxy.Instance.DeleteLaneInfoBy (_vm.LANEINFO_PK);
            if (idx <= 0)
                return;

            if (OnDeleteQuery != null)
                OnDeleteQuery.Invoke(idx);

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
