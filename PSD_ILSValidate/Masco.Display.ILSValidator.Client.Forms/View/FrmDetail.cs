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
    public partial class FrmSheetList : Form
    {
        public string GetSheetName
        {
            get;
            private set;
        }
    
        public FrmSheetList()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            gridSheetList.AllowUserToAddRows = false;
            gridSheetList.AllowUserToDeleteRows = false;
            gridSheetList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridSheetList.MultiSelect = false;
        }

        internal void SetGridItem(Microsoft.Office.Interop.Excel.Worksheet[] sheetList)
        {
            gridSheetList.DataSource = sheetList.Select(x => new { SheetName = x.Name }).ToArray();
        }

        private void btnSelectSheet_Click(object sender, EventArgs e)
        {
            SelectGridItem();
        }

        private void gridSheetList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectGridItem();
        }

        private void SelectGridItem()
        {
            var selectRow = gridSheetList.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var selectItem = selectRow.DataBoundItem;
            if (selectItem == null)
                return;
            var strSheetName = selectItem.ToString().Replace("{", string.Empty).Replace("}", string.Empty).Trim();
            var valuesArr = strSheetName.Split('=');
            GetSheetName = valuesArr[1].Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
