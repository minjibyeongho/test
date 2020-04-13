using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Ntreev.Library.Psd;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public partial class FrmValidatePsdArrowCode : Form
    {
        private PsdReader _psdReader;
        private int m_rowIdx = 0;
        private int m_psdCnt = 0;

        public FrmValidatePsdArrowCode()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var g = grid;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            LoadConfig();
        }

        #region ConfigEdit
        private void LoadConfig()
        {
            try
            {
                var configFile = "config.txt";
                var pathTag = "PATH=";
                if (File.Exists(configFile))
                {
                    var lines = File.ReadAllLines(configFile);
                    foreach (var x in lines)
                    {
                        if (x.StartsWith(pathTag))
                        {
                            txtFolderPath.Text = x.Substring(pathTag.Length);
                            break;
                        }
                    }
                }
            }
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
                configList.Add(string.Format("{0}{1}", pathTag, txtFolderPath.Text));
                File.WriteAllLines(configFile, configList);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "(*.xlsx)|*.xlsx|(*.xls)|*.xls";
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            txtFilePath.Text = dlg.FileName;
            var filePath = txtFilePath.Text;
            var bw = new BackgroundWorker();
            bw.DoWork += (ss, ee) =>
                {
                    var resultTable = ExcelRead(filePath);
                    ee.Result = resultTable;
                };
            bw.RunWorkerCompleted += (ss, ee) =>
                {
                    var resultTable = ee.Result as DataTable;
                    this.Cursor = Cursors.Arrow;
                    btnSelectFile.Enabled = true;
                    if (resultTable != null)
                        gridExcel.DataSource = resultTable;
                    var folderPath = txtFolderPath.Text;
                    if (Directory.Exists(folderPath))
                    {
                        LoadFIle(folderPath);
                    }
                };
            bw.RunWorkerAsync();
            var tempArr = new string[1] { "데이터 로딩중...." };
            gridExcel.DataSource = tempArr.Select(x => new { Data = x }).ToArray();
            this.Cursor = Cursors.WaitCursor;
            btnSelectFile.Enabled = false;
        }

        private DataTable ExcelRead(string filePath)
        {
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();
            //Create an Excel workbook instance and open it from the predefined location
            var excelWorkbook = excelApp.Workbooks.Open(filePath);

            //Add a new worksheet to workbook with the Datatable name
            var sheetList = excelWorkbook.Sheets.Cast<Excel.Worksheet>().ToArray();

            var dlg = new FrmSheetList();
            dlg.SetGridItem(sheetList);

            if (dlg.ShowDialog() != DialogResult.OK)
                return null;
            var sheetName = dlg.GetSheetName;

            var sheet = sheetList.Where(x => x.Name == sheetName).FirstOrDefault();
            if (sheet == null)
                return null;

            // 첫 번째 시트의 데이타를 읽어서 datagridview 에 보이게 함.
            var resultTable = new DataTable();
            var range = sheet.UsedRange;
            var isColumns = true;

            foreach (Excel.Range row in range.Rows)
            {
                var rowValues = row.Value;
                var colCnt = row.Columns.Count;
                if (isColumns)
                {
                    for (int i = 1; i <= colCnt; i++)
                    {
                        var header = rowValues[1, i];
                        if (header != null)
                        {
                            resultTable.Columns.Add(header);
                        }
                    }
                    isColumns = false;
                    continue;
                }

                DataRow dtRow = resultTable.NewRow();
                for (int i = 1; i <= resultTable.Columns.Count; i++)
                {
                    dtRow[i - 1] = rowValues[1, i];
                }
                resultTable.Rows.Add(dtRow);
            }
            excelWorkbook.Close();

            return resultTable;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            txtFolderPath.Text = dlg.SelectedPath;
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            var pathTextBox = sender as TextBox;
            var filePath = pathTextBox.Text;

            if (Directory.Exists(filePath) == false)
                return;

            SaveConfig();
            LoadFIle(filePath);
        }

        private void LoadFIle(string path)
        {
            grid.DataSource = null;
            var files = Directory.GetFiles(path, "*.psd", SearchOption.AllDirectories);
            IList<VaildateArrowCodeVM> psdFileList = new List<VaildateArrowCodeVM>();
            psdFileList.Clear();
            var idx = 0;
            foreach (var file in files)
            {
                var item = new VaildateArrowCodeVM();
                item.Index = ++idx;
                item.FileName = file;
                var patternId = file.Substring(file.LastIndexOf("\\") + 1);
                if (patternId.Contains(".psd"))
                {
                    item.Pattern_Id = patternId.Replace(".psd", string.Empty);
                }
                else if (patternId.Contains(".PSD"))
                {
                    item.Pattern_Id = patternId.Replace(".PSD", string.Empty);
                }
                item.ILS_Type = GetILSType(file);
                if (item.ILS_Type == null)
                {
                    item.ResultMessage = "파일명 오류";
                }
                psdFileList.Add(item);
            }
            var copyTable = ConvertListToDataTable(psdFileList);
            grid.DataSource = copyTable;
        }

        private string GetILSType(string file)
        {
            var fi = new FileInfo(file);
            var fileName = fi.Name;

            if (fileName.StartsWith(ILSType.FilePrefix1_NC, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code1_NC;
            }
            else if (fileName.StartsWith(ILSType.FilePrefix2_JC, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code2_JC;
            }
            else if (fileName.StartsWith(ILSType.FilePrefix3_CE, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code3_CE;
            }
            else if (fileName.StartsWith(ILSType.FilePrefix4_ET, StringComparison.CurrentCultureIgnoreCase))
            {
                return ILSType.Code4_ET;
            }
            else if (fileName.StartsWith("8"))
            {
                var document = PsdDocument.Create(fileName);

                var totalLayerList = document.Childs.Reverse();
                var fisrtLayer = totalLayerList.FirstOrDefault();
                var firstChild = fisrtLayer.Childs.FirstOrDefault();
                if (firstChild.Name.StartsWith("Arrow_"))
                    return ILSType.Code5_MimeticDiagram;
                else if (firstChild.Name.EndsWith("_AI"))
                    return ILSType.Code6_CrossRoadPoint3D;
            }
            else
            {
                var document = PsdDocument.Create(fileName);
                var totalLayerList = document.Childs.Reverse();
                var fisrtLayer = totalLayerList.FirstOrDefault();
                if (fisrtLayer.Name.Equals("Title"))
                    return ILSType.Code7_RestAreaSummaryMap_Mapy;
                else if (fisrtLayer.Name.Equals("Title_set"))
                    return ILSType.Code8_RestAreaSummaryMap_Gini;
            }
            return null;
        }

        private void btnValidateCode_Click(object sender, EventArgs e)
        {
            _psdReader = new PsdReader();

            if (grid.Rows.Count < 2 || gridExcel.Rows.Count < 2)
                return;
            m_psdCnt = grid.Rows.Count;
            m_rowIdx = 0;

            if (grid.Rows.Count > m_rowIdx)
                ValidateArrowCode(m_rowIdx);
        }

        private void ValidateArrowCode(int idx)
        {
            var rows = grid.Rows.Cast<DataGridViewRow>().ToList();
            var row = rows[idx].DataBoundItem as DataRowView;
            var copyVm = new VaildateArrowCodeVM();
            CopyProperties(row, copyVm);
            if (copyVm == null)
                return;
            if (copyVm.ILS_Type == null)
                return;
            ReadPsd(copyVm);
        }

        private void FrmValidatePsdArrowCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grid.SelectedRows.Count != 1)
                return;

            var selectRow = grid.SelectedRows[0].DataBoundItem as DataRowView;
            var copyVm = new ValidatePsdFileVM();
            CopyProperties(selectRow, copyVm);

            var dlg = new FrmValidatePsdFile();
            dlg.Setup(copyVm, false);
            dlg.Show(this);
        }

        private void ReadPsd(VaildateArrowCodeVM vm)
        {
            var psdFullPath = vm.FileName;
            if (File.Exists(psdFullPath) == false)
                return;

            var fileInfo = new FileInfo(psdFullPath);
            var fileName = fileInfo.Name;

            var pos = fileInfo.Name.LastIndexOf(".");
            if (pos < 0)
                return;

            var bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                try
                {
                    _psdReader.ReadFile(psdFullPath);
                }
                catch (Exception ex)
                {
                    e.Result = ex.ToString();
                }

            };

            bw.RunWorkerCompleted += (s, e) =>
            {
                vm.ResultMessage = e.Result as string;

                var list = _psdReader.GetList();
                if (vm != null)
                {
                    IEnumerable<PsdLayerVM> arrowCodeList;
                    if (vm.ILS_Type != "CE")
                        arrowCodeList = list.Where(x => x.Name.Contains("Arrow_"));
                    else
                        arrowCodeList = list.Where(x => x.Name.Contains("d0"));

                    var arrowListTable = grid.DataSource as DataTable;

                    var width = _psdReader.FileSectionWidth;
                    var height = _psdReader.FileSectionHeight;

                    vm.Arrow_Id = string.Format("Width:{0} X Height:{1}", width, height);
                    var patternId = arrowListTable.Rows.Cast<DataRow>().Where(x => x["pattern_id"].ToString() == vm.Pattern_Id).FirstOrDefault();
                    if (patternId == null)
                        return;

                    patternId["ResultMessage"] = vm.Arrow_Id;

                    foreach (var arrowCode in arrowCodeList)
                    {
                        var arrowCodeName = arrowCode.Name;
                        if (vm.ILS_Type != "CE")
                            arrowCodeName = arrowCodeName.Replace("Arrow_", string.Empty);
                        else
                            arrowCodeName = arrowCodeName.Replace("_AI", string.Empty);

                        var item = arrowListTable.NewRow();
                        foreach (var property in vm.GetType().GetProperties())
                        {
                            if (property.Name != "Arrow_Id")
                                item[property.Name] = property.GetValue(vm, null);
                            else
                            {
                                item[property.Name] = arrowCodeName;
                                item["ResultMessage"] = "";
                            }
                        }
                        arrowListTable.Rows.Add(item);
                    }

                    var table = gridExcel.DataSource as DataTable;
                    var patternIdList = table.Rows.Cast<DataRow>().Where(x => vm.Pattern_Id.Equals(x["pattern_id"].ToString(), StringComparison.CurrentCultureIgnoreCase));
                    if (patternIdList.Count() > 0)
                    {
                        foreach (var serverItem in patternIdList)
                        {
                            var isExist = arrowListTable.Rows.Cast<DataRow>().Where(x => x["arrow_id"].ToString().Equals(serverItem["arrow_id"].ToString(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                            if (isExist != null)
                            {
                                isExist["Result"] = true;
                                patternId["Result"] = true;
                            }
                            else
                            {
                                patternId["Result"] = false;
                            }

                        }
                    }

                    if (m_psdCnt - 1 > m_rowIdx)
                    {
                        m_rowIdx++;
                        ValidateArrowCode(m_rowIdx);
                        return;
                    }
                    else
                    {
                        arrowListTable.DefaultView.Sort = "[Index]";
                        arrowListTable.AcceptChanges();
                        btnValidateCode.Enabled = true;
                    }
                }
            };
            bw.RunWorkerAsync();
            btnValidateCode.Enabled = false;
        }

        private DataTable ConvertListToDataTable<T>(IList<T> list)
        {
            // New table.
            DataTable table = null;
            // Get max columns.
            var sample = list.FirstOrDefault();
            if (sample == null)
                return table;

            table = new DataTable();
            var properties = sample.GetType().GetProperties();
            int columnCnt = properties.Length;

            foreach (var propertie in properties)
            {
                var dataCol = new DataColumn();
                dataCol.ColumnName = propertie.Name;
                dataCol.DataType = propertie.PropertyType;
                dataCol.AllowDBNull = true;

                table.Columns.Add(dataCol);
            }

            // Add rows.
            foreach (var data in list)
            {
                var dataproperties = data.GetType().GetProperties();
                var dataRow = table.NewRow();
                foreach (var propertie in dataproperties)
                {
                    dataRow[propertie.Name] = propertie.GetValue(data, null);
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        public void CopyProperties<T>(DataRowView source, T target)
        {
            var properties = target.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (source.Row.Table.Columns.Contains(p.Name) == false) { continue; }
                var isExist = source.Row[p.Name];
                if (isExist == null || string.IsNullOrWhiteSpace(isExist.ToString()))
                    continue;

                var targetProperty = target.GetType().GetProperty(p.Name);
                targetProperty.SetValue(target, isExist, null);
            }
        }

        private void ExportExcel(bool captions = true)
        {
            var filePath = txtFilePath.Text;
            filePath = filePath.Substring(0, filePath.LastIndexOf("."));
            int idx = 1;
            var savePath = string.Format("{0}_내보내기{1}", filePath, idx);
            while (File.Exists(savePath))
            {
                idx++;
                savePath = string.Format("{0}_내보내기{1}", filePath, idx);
            }
            //string.Format("{0}{1}");

            int num = 0;
            object missingType = Type.Missing;

            Excel.Application objApp;
            Excel._Workbook objBook;
            Excel.Workbooks objBooks;
            Excel.Sheets objSheets;
            Excel._Worksheet objSheet;
            Excel.Range range;

            string[] headers = new string[grid.ColumnCount];
            string[] columns = new string[grid.ColumnCount];

            for (int c = 0; c < grid.ColumnCount; c++)
            {
                headers[c] = grid.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                num = c + 65;
                columns[c] = Convert.ToString((char)num);
            }

            //try
            //{
            objApp = new Excel.Application();
            objBooks = objApp.Workbooks;
            objBook = objBooks.Add(Missing.Value);
            objSheets = objBook.Worksheets;
            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

            if (captions)
            {
                for (int c = 0; c < grid.ColumnCount; c++)
                {
                    range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                    range.set_Value(Missing.Value, headers[c]);
                }
            }

            for (int i = 0; i < grid.RowCount; i++)
            {
                for (int j = 0; j < grid.ColumnCount; j++)
                {
                    if (grid.Rows[i].Cells[j].Value == null)
                        grid.Rows[i].Cells[j].Value = string.Empty;

                    range = objSheet.get_Range(columns[j] + Convert.ToString(i + 2), Missing.Value);
                    range.set_Value(Missing.Value, grid.Rows[i].Cells[j].Value.ToString());
                }
            }

            objApp.Visible = false;
            objApp.UserControl = false;

            objBook.SaveAs(savePath,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                        missingType, missingType, missingType, missingType,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        missingType, missingType, missingType, missingType, missingType);

            objBook.Close(false, missingType, missingType);

            Cursor.Current = Cursors.Default;

            MessageBox.Show("내보내기 완료");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }
    }
}
