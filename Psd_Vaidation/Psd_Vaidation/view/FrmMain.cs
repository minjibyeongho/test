using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Ntreev.Library.Psd;
using Psd_Vaidation.view;

namespace Psd_Vaidation
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            Init();
        }

        ImageIconForm iconForm;
        public FrmMain(ImageIconForm _iconForm)
        {
            InitializeComponent();
            iconForm = _iconForm;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        //bool _isStart = false;

        private void Init()
        {
            //txtFolderPath.Text = @"D:\PSD 자체검즘 샘플\OEM";
            //txtExcel.Text = @"C:\Users\User\Desktop\엑셀 샘플\08112_검증예제로사용 - 복사본";
            txtFolderPath.Text = "";
            txtExcel.Text = "";

            gridExcelInfo.AllowUserToAddRows = false;
            gridExcelInfo.AllowUserToDeleteRows = false;
            gridExcelInfo.AllowUserToOrderColumns = false;

            rbtnOEM.CheckedChanged += rbtn_CheckedChanged;
            rbtnOEM_AM.CheckedChanged += rbtn_CheckedChanged;
            rbtnOEM_G3_ALL.CheckedChanged += rbtn_CheckedChanged;
            rbtnOEM_G3_UpperLevel.CheckedChanged += rbtn_CheckedChanged;

            //rbtnOEM.Enabled = false;
            //rbtnOEM_AM.Enabled = false;
            //rbtnOEM_G3_ALL.Enabled = false;
            //rbtnOEM_G3_UpperLevel.Enabled = false;
        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            var rbtn = sender as RadioButton;
            var typeName = rbtn.Text;

            CheckType(typeName);
            CheckType1(typeName);
        }

        private void CheckType(string typeName)
        {
            gridValidate.Rows.Clear();
            gridValidate.Columns.Clear();

            try
            {
                var selectPath = txtFolderPath.Text;
                var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);
                switch (typeName)
                {
                    case "OEM":
                        SetOEM(files); break;
                    case "OEM_G3_ALL":
                        SetOEM_G3_ALL(files); break;
                    case "OEM_G3_UpperLevel":
                        SetOEM_G3_UpperLevel(files); break;
                    case "OEM_AM":
                        SetOEM_AM(files); break;
                    default:
                        break;
                }
            }
            catch
            {
                //파일과, 폴더를 선택하지 않고 OEM 레벨 변경시 오류발생하므로 예외처리
            }
        }

        private void CheckType1(string typeName)
        {
            gridResult.Rows.Clear();
            gridResult.Columns.Clear();

            try
            {
                var selectPath = txtFolderPath.Text;
                var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);

                switch (typeName)
                {
                    case "OEM":
                        VerifyOEM(files); break;
                    case "OEM_G3_ALL":
                        VerifyOEM_G3_ALL(files); break;
                    case "OEM_G3_UpperLevel":
                        VerifyOEM_G3_UpperLevel(files); break;
                    case "OEM_AM":
                        VerifyOEM_AM(files); break;
                    default:
                        break;
                }
            }
            catch
            {
                //파일과, 폴더를 선택하지 않고 OEM 레벨 변경시 오류발생하므로 예외처리
            }
        }


        private void btnStart_Click(object sender, MouseEventArgs e)
        {

            var selectPath2 = txtExcel.Text;

            gridLayerInfo.Columns.Clear();
            gridLayerInfo.Rows.Clear();
            gridExcelInfo.Columns.Clear();
            gridExcelInfo.Rows.Clear();

            //#region PSD FILE LIST
            //gridLayerInfo.Columns.Add("FileName", "FileName");
            //gridLayerInfo.Columns.Add("결과", "결과");
            //#endregion
            //foreach (var file in files)
            //{
            //var doc = PsdDocument.Create(file);
            //gridLayerInfo.Rows.Add(file);
            //}

            #region excelinfo
            gridExcelInfo.Columns.Add("구간ID", "구간ID");
            gridExcelInfo.Columns.Add("시점", "시점");
            gridExcelInfo.Columns.Add("종점", "종점");
            gridExcelInfo.Columns.Add("결과", "결과");

            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(selectPath2);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                var rowCount = xlRange.Rows.Count;
                var colCount = xlRange.Columns.Count;

                foreach (Worksheet sheet in xlApp.Sheets)
                {
                    for (int i = 1; i <= rowCount; i++)
                    {
                        var excel1 = xlRange.Rows.Cells[i, 1].Value2;
                        var excel2 = xlRange.Rows.Cells[i, 2].Value2;
                        var excel3 = xlRange.Rows.Cells[i, 3].Value2;
                        string excelVerify;


                        var excel11 = "";
                        for (int j = i + 1; j < rowCount; j++)
                        {
                            excel11 = xlRange.Rows.Cells[j, 1].Value2;
                            break;
                        }

                        if (excel1 == null && excel2 == null && excel3 == null)
                        {
                            excelVerify = "";

                        }
                        else if (excel1 == null || excel1 == excel11)
                        {
                            excelVerify = "NG";
                        }
                        else
                            excelVerify = "OK";

                        gridExcelInfo.Rows.Add(excel1, excel2, excel3, excelVerify);
                    }
                    #endregion
                }
                xlApp.Workbooks.Close();
            }
            catch
            {
                MessageBox.Show("엑셀파일을 선택해주세요.");
            }

            rbtnOEM.Enabled = true;
            rbtnOEM_AM.Enabled = true;
            rbtnOEM_G3_ALL.Enabled = true;
            rbtnOEM_G3_UpperLevel.Enabled = true;





            try
            {
                var selectPath = txtFolderPath.Text;
                var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);

                //SetOEM(files);
                //SetOEM_G3_ALL(files);
                //SetOEM_G3_UpperLevel(files);
                //SetOEM_AM(files);

                #region PSD FILE LIST
                gridLayerInfo.Columns.Add("FileName", "FileName");
                #endregion
                foreach (var file in files)
                {
                    var doc = PsdDocument.Create(file);

                    //var LayerInfoVerify = "";
                    //if (gridValidate.CurrentRow.Cells[2].Value.ToString() == "OK")
                    //{
                    //LayerInfoVerify = "OK";
                    //}
                    //else if (gridValidate.CurrentRow.Cells[2].Value.ToString() == "NG")
                    //{
                    //LayerInfoVerify = "NG";
                    //break;
                    //}
                    gridLayerInfo.Rows.Add(file);
                }

                #region gridResult selectionchanged Direct output
                VerifyOEM(files);
                VerifyOEM_G3_ALL(files);
                VerifyOEM_G3_UpperLevel(files);
                VerifyOEM_AM(files);
                #endregion
            }
            catch
            {
                MessageBox.Show("PSD폴더를 선택해주세요.");
            }

            gridExcelInfo.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            gridLayerInfo.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        }

        private string ImageIconForm_value;
        public string PassValue
        {
            get { return this.ImageIconForm_value; }
            set { this.ImageIconForm_value = value; }
        }

        private void SetOEM(string[] files)
        {
            //if (_isStart == false)
            //return;
            //#region OEM
            if (rbtnOEM.Checked == true)
            {
                try
                {
                    #region gridValidate 
                    gridValidate.Columns.Add("항목", "항목");
                    gridValidate.Columns.Add("내용", "내용");
                    gridValidate.Columns.Add("결과", "결과");
                    #endregion

                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        this.gridValidate.Columns["항목"].SortMode = DataGridViewColumnSortMode.Automatic;

                        //1번 레이어 구조
                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStruct1 = "";
                            var layerStrucVerify = "";
                            var cnt = 0;
                            var NGcnt = 0;

                            for (int i = 0; i < LayerSturct.Count; i++)
                            {
                                cnt++;
                                layerStruct1 = LayerSturct[i];
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                if (layerStruct1.ToString() == "객체" && cnt == 1)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //이미지id와 파일명 일치한지확인
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "고속도로icon" && cnt == 6)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 7)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 8)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "배경" && cnt == 9)
                                    layerStrucVerify = "OK";
                                else
                                {
                                    layerStrucVerify = "NG";
                                    NGcnt++;
                                }
                            }
                            if (NGcnt == 0)
                            {
                                layerStrucVerify = "OK";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                layerStrucVerify = "NG";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }

                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            gridResult.Columns.Clear();
                            gridResult.Rows.Clear();

                            var cnt = 0;
                            var NGcnt = 0;
                            var SequenceVerify = "";
                            var layerNumberList1 = "";

                            for (int i = 0; i < LayerNumberList.Count; i++)
                            {
                                cnt++;
                                layerNumberList1 = LayerNumberList[i];
                                if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "고속도로icon" && cnt == 3)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 4)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 5)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "배경" && cnt == 6)
                                    SequenceVerify = "OK";
                                else
                                {
                                    SequenceVerify = "NG";
                                    NGcnt++;
                                }
                            }

                            if (NGcnt == 0)
                            {
                                SequenceVerify = "OK";
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                SequenceVerify = "NG";
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }

                            if (SequenceVerify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {

                            var Show = "";
                            var Visible = true;
                            for (int i = 0; i < LayerList.Count; i++)
                            {

                                Visible = LayerVisible[i];

                                if (Visible == true)
                                {
                                    Show = "OK";
                                }
                                else
                                {
                                    Show = "NG";
                                }
                            }

                            gridValidate.Rows.Add("레이어 ON", "레이어가 OFF인 경우 NG", Show);

                            if (Show == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStrucVerify = "";
                            var Code = doc.Childs[1].Childs[0].Name;
                            int TrueCount = 0;
                            int FalseCount = 0;
                            var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                            int j = 0;


                            //char[] CodeTrim = { '+' };
                            //string[] CodeTrim2 = " + ";
                            //var CodeSplit = Code;
                            //var tmp = "";
                            //string[] array = CodeSplit.Split('+');
                            //for (int arr1 = 0; arr1 < array.Length; arr1++)
                            //{
                            //tmp = array[arr1].Trim();
                            //array[arr1] = tmp;
                            //}

                            for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리
                                }

                                if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                {
                                    layerStrucVerify = "OK";
                                    j++;
                                }
                                else if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            TrueCount--;
                                            layerStrucVerify = "NG";
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    FalseCount++;
                                }
                            }

                            int TrueCount2 = 0;
                            int k = 0;
                            for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리1
                                }

                                if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount2++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount2--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            layerStrucVerify = "NG";
                                            TrueCount2--;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    break;
                                }
                            }
                            gridValidate.Rows.Add("고유코드", "객체별 고유ID의 정보가 상이한 경우 NG", layerStrucVerify);

                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var okCnt = 0;
                            var ngCnt = 0;

                            for (int i = doc.Childs.Length - 1; i >= 0; i--)
                            {
                                var Object1 = doc.Childs[i].Name;

                                var aWidth = 0;
                                var aHeight = 0;
                                int nIndex1 = 0;

                                for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                {
                                    for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                    {
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                        nIndex1++;
                                    }
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                }

                                byte ColorR1 = 0;
                                byte ColorG1 = 0;
                                byte ColorB1 = 0;
                                if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                {
                                    ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                    ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                    ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                }

                                var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                string value1 = RGBToHexadecimal(data1);

                                for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                {
                                    int nIndex2 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                            nIndex2++;
                                        }
                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                    }

                                    var Object2 = doc.Childs[i].Childs[j].Name;

                                    byte ColorR2 = 0;
                                    byte ColorG2 = 0;
                                    byte ColorB2 = 0;

                                    if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                    {
                                        ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                        ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                        ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                    }

                                    var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                    var value2 = RGBToHexadecimal(data2);

                                    var ColorVerify1 = "";
                                    if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "고속도로icon")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "배경")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else
                                    {
                                        ColorVerify1 = "NG";
                                        ngCnt++;
                                    }

                                    for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                    {
                                        var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;
                                        for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                        {
                                            int nIndex4 = 0;
                                            for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                            {
                                                for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                {
                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                    nIndex4++;
                                                }

                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                            }

                                            var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                            byte ColorR4 = 0;
                                            byte ColorG4 = 0;
                                            byte ColorB4 = 0;
                                            if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                            {
                                                ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                            }
                                            var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                            string value4 = RGBToHexadecimal(data4);


                                            // 도로 rgb 
                                            for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                            {
                                                int nIndex5 = 0;
                                                for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                    {
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                        nIndex5++;
                                                    }
                                                    if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                }
                                                var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                var roadObj = "";

                                                if ("도로" == doc.Childs[i].Childs[j].Name)
                                                {
                                                    roadObj = doc.Childs[i].Childs[j].Childs[0].Childs[1].Childs[0].Name;
                                                }


                                                var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                string value5 = RGBToHexadecimal(data5);
                                                var ColorVerify2 = "";

                                                if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else
                                                {
                                                    ColorVerify2 = "NG";
                                                    ngCnt++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            var verify = "";
                            if (ngCnt == 0)
                            {
                                verify = "OK";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            else if (ngCnt != 0)
                            {
                                verify = "NG";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }

                            if (verify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var ImageIconVerify = "";
                            var IconName = "";
                            bool IconVisible;


                            for (int i = doc.Childs[0].Childs.Length - 1; i >= 0; i--)
                            {
                                if ("고속도로icon" == doc.Childs[0].Childs[i].Name)
                                {
                                    IconName = doc.Childs[0].Childs[i].Name;
                                }
                            }

                            try
                            {
                                if (IconName == "고속도로icon")
                                {
                                    ImageIconVerify = "OK";
                                }
                                else if (IconName != "고속도로icon")
                                {
                                    ImageIconVerify = "NG";
                                }
                            }
                            catch (Exception)
                            {
                                //예외처리 안해주면 폴더선택후 스타트할시 인덱스 오류
                            }
                            gridValidate.Rows.Add("아이콘유무", "아이콘유무에 따라 OK or NG발생! OK일 경우 이미지출력", ImageIconVerify);
                            if (ImageIconVerify == "NG")
                            {
                                break;
                            }
                        }
                        gridValidate.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                    //#endregion
                }
                catch
                {
                    //OEM_G3_상위레벨 객체ID 부재로인한 예외처리
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다");
                }
            }
        }
        private void VerifyOEM(string[] files)
        {
            if (rbtnOEM.Checked == true)
            {
                try
                {
                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 구조" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString() /*&& "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString()*/)
                            {
                                gridResult.Columns.Add("레이어구조", "레이어구조");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStruct1 = "";
                                var layerStrucVerify = "";
                                var cnt = 0;

                                for (int i = 0; i < LayerSturct.Count; i++)
                                {
                                    cnt++;
                                    layerStruct1 = LayerSturct[i];
                                    var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                    if (layerStruct1.ToString() == "객체" && cnt == 1)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //파일명과 이미지ID가 일치한지 확인
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "고속도로icon" && cnt == 6)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 7)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 8)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "배경" && cnt == 9)
                                        layerStrucVerify = "OK";
                                    else
                                    {
                                        layerStrucVerify = "NG";
                                    }
                                    gridResult.Rows.Add(layerStruct1, layerStrucVerify);
                                }
                                //foreach (DataGridViewRow item in this.gridResult.Rows)
                                //{
                                //if (item.Cells["레이어구조"].Value.ToString() == "고속도로icon")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //}

                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 순서" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("No.", "No.");
                                gridResult.Columns.Add("레이어순서", "레이어순서");
                                gridResult.Columns.Add("결과", "결과");

                                var cnt = 0;
                                var SequenceVerify = "";
                                var layerNumberList1 = "";

                                for (int i = 0; i < LayerNumberList.Count; i++)
                                {
                                    cnt++;
                                    layerNumberList1 = LayerNumberList[i];
                                    if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "고속도로icon" && cnt == 3)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 4)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 5)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "배경" && cnt == 6)
                                        SequenceVerify = "OK";
                                    else
                                    {
                                        SequenceVerify = "NG";
                                    }
                                    gridResult.Rows.Add(cnt, layerNumberList1, SequenceVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 ON" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어 ON", "레이어 ON");
                                gridResult.Columns.Add("결과", "결과");

                                var Show = "";
                                var Visible = true;
                                var layerName = "";

                                for (int i = 0; i < LayerVisible.Count; i++)
                                {
                                    layerName = LayerList[i];
                                    Visible = LayerVisible[i];

                                    if (Visible == true)
                                    {
                                        Show = "OK";
                                    }
                                    else
                                        Show = "NG";
                                    gridResult.Rows.Add(layerName, Show);
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("고유코드" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString() /*&& "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString()*/)
                            {
                                //gridResult.Columns.Add("고유코드", "고유코드");
                                gridResult.Columns.Add("객체별 고유ID", "객체별 고유ID");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStrucVerify = "";
                                var Code = doc.Childs[1].Childs[0].Name;
                                int TrueCount = 0;
                                int FalseCount = 0;
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                                int j = 0;
                                int k = 0;

                                //char[] CodeTrim = { '+' };
                                //string[] CodeTrim2 = " + ";
                                //var CodeSplit = Code;
                                //var tmp = "";
                                //string[] array = CodeSplit.Split('+');
                                //for (int arr1 = 0; arr1 < array.Length; arr1++)
                                //{
                                //tmp = array[arr1].Trim();
                                //array[arr1] = tmp;
                                //}

                                for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리
                                    }

                                    if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    {
                                        layerStrucVerify = "OK";
                                        j++;
                                    }
                                    else if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                               && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                TrueCount--;
                                                layerStrucVerify = "NG";
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                        FalseCount++;
                                    }
                                }

                                int TrueCount2 = 0;

                                for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리1
                                    }


                                    //if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    //{
                                    //layerStrucVerify = "OK";
                                    //k++;
                                    //}
                                    if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount2++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount2--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                     && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount2--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                layerStrucVerify = "NG";
                                                TrueCount2--;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                    }
                                    gridResult.Rows.Add(IntervalID, layerStrucVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("색상정보" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("색상정보", "색상정보");
                                gridResult.Columns.Add("색상", "색상");
                                gridResult.Columns.Add("코드", "코드");
                                gridResult.Columns.Add("결과", "결과");

                                for (int i = doc.Childs.Length - 1; i >= 0; i--)
                                {
                                    var Object1 = doc.Childs[i].Name;

                                    var aWidth = 0;
                                    var aHeight = 0;
                                    int nIndex1 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                            nIndex1++;
                                        }
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                    }

                                    byte ColorR1 = 0;
                                    byte ColorG1 = 0;
                                    byte ColorB1 = 0;
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                    {
                                        ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                        ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                        ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                    }

                                    var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                    string value1 = RGBToHexadecimal(data1);
                                    //gridResult.Rows.Add(Object1/*, (ColorR1, ColorG1, ColorB1), value1*/);

                                    for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                    {
                                        int nIndex2 = 0;

                                        for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                        {
                                            for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                            {
                                                if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                                nIndex2++;
                                            }
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                        }

                                        var Object2 = doc.Childs[i].Childs[j].Name;

                                        byte ColorR2 = 0;
                                        byte ColorG2 = 0;
                                        byte ColorB2 = 0;

                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                        {
                                            ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                            ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                            ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                        }

                                        var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                        var value2 = RGBToHexadecimal(data2);

                                        try
                                        {
                                            var ColorVerify1 = "";
                                            if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else
                                                ColorVerify1 = "NG";

                                            gridResult.Rows.Add(Object2, (ColorR2, ColorG2, ColorB2), value2, ColorVerify1);
                                        }
                                        catch
                                        { }

                                        foreach (DataGridViewRow item in this.gridResult.Rows)
                                        {
                                            if (item.Cells["색상정보"].Value.ToString() == "고속도로icon")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            else if (item.Cells["색상정보"].Value.ToString() == "배경")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Group>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Image>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                        }

                                        for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                        {
                                            var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;

                                            for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                            {
                                                int nIndex4 = 0;
                                                for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                    {
                                                        if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                        nIndex4++;
                                                    }

                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                }

                                                var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                                byte ColorR4 = 0;
                                                byte ColorG4 = 0;
                                                byte ColorB4 = 0;
                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                                {
                                                    ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                    ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                    ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                                }
                                                var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                                string value4 = RGBToHexadecimal(data4);
                                                //gridResult.Rows.Add(Object4, (ColorR4, ColorG4, ColorB4), value4);


                                                // 도로 rgb 
                                                for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    int nIndex5 = 0;
                                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                    {
                                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                        {
                                                            if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                            nIndex5++;
                                                        }
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                    }
                                                    var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                    //var roadObj = doc.Childs[i].Childs[4].Childs[0].Childs[1].Childs[0].Name;

                                                    var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                    var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                    var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                    var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                    string value5 = RGBToHexadecimal(data5);
                                                    var ColorVerify2 = "";

                                                    if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else
                                                        ColorVerify2 = "NG";

                                                    gridResult.Rows.Add(Object5, (ColorR5, ColorG5, ColorB5), value5, ColorVerify2); //도로명칭 나오는 부분
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString() && "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString())
                        {
                            if ("아이콘유무" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                var fileName = gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString();
                                ImageIconForm iconForm = new ImageIconForm();
                                ImageIconForm.PassValue = txtFolderPath.Text;
                                iconForm.Show();
                            }
                        }
                        gridResult.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                }
                catch
                {
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다");
                }
            }
        }

        private void SetOEM_G3_ALL(string[] files)
        {
            //if (_isStart == false)
            //return;
            #region OEM_G3_ALL
            if (rbtnOEM_G3_ALL.Checked == true)
            {
                try
                {
                    #region gridValidate 
                    gridValidate.Columns.Add("항목", "항목");
                    gridValidate.Columns.Add("내용", "내용");
                    gridValidate.Columns.Add("결과", "결과");
                    #endregion

                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        this.gridValidate.Columns["항목"].SortMode = DataGridViewColumnSortMode.Automatic;

                        //1번 레이어 구조
                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStruct1 = "";
                            var layerStrucVerify = "";
                            var cnt = 0;
                            var NGcnt = 0;

                            for (int i = 0; i < LayerSturct.Count; i++)
                            {
                                cnt++;
                                layerStruct1 = LayerSturct[i];
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                if (layerStruct1.ToString() == "객체" && cnt == 1)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //이미지id와 파일명 일치한지확인
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 6)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 7)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "배경" && cnt == 8)
                                    layerStrucVerify = "OK";
                                else
                                {
                                    layerStrucVerify = "NG";
                                    NGcnt++;
                                }
                            }
                            if (NGcnt == 0)
                            {
                                layerStrucVerify = "OK";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                layerStrucVerify = "NG";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            gridResult.Columns.Clear();
                            gridResult.Rows.Clear();

                            var cnt = 0;
                            var NGcnt = 0;
                            var SequenceVerify = "";
                            var layerNumberList1 = "";

                            for (int i = 0; i < LayerNumberList.Count; i++)
                            {
                                cnt++;
                                layerNumberList1 = LayerNumberList[i];
                                if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 3)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 4)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "배경" && cnt == 5)
                                    SequenceVerify = "OK";
                                else
                                {
                                    SequenceVerify = "NG";
                                    NGcnt++;
                                }
                            }

                            if (NGcnt == 0)
                            {
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                SequenceVerify = "NG";
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            if (SequenceVerify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {

                            var Show = "";
                            var Visible = true;
                            for (int i = 0; i < LayerList.Count; i++)
                            {

                                Visible = LayerVisible[i];

                                if (Visible == true)
                                {
                                    Show = "OK";
                                }
                                else
                                {
                                    Show = "NG";
                                }
                            }

                            gridValidate.Rows.Add("레이어 ON", "레이어가 OFF인 경우 NG", Show);
                            if (Show == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStrucVerify = "";
                            var Code = doc.Childs[1].Childs[0].Name;
                            int TrueCount = 0;
                            int FalseCount = 0;
                            var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                            int j = 0;


                            //char[] CodeTrim = { '+' };
                            //string[] CodeTrim2 = " + ";
                            //var CodeSplit = Code;
                            //var tmp = "";
                            //string[] array = CodeSplit.Split('+');
                            //for (int arr1 = 0; arr1 < array.Length; arr1++)
                            //{
                            //tmp = array[arr1].Trim();
                            //array[arr1] = tmp;
                            //}

                            for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리
                                }

                                if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                {
                                    layerStrucVerify = "OK";
                                    j++;
                                }
                                else if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            TrueCount--;
                                            layerStrucVerify = "NG";
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    FalseCount++;
                                }
                            }

                            int TrueCount2 = 0;
                            int k = 0;
                            for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리1
                                }

                                if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount2++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount2--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            layerStrucVerify = "NG";
                                            TrueCount2--;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    break;
                                }
                            }

                            //var layerStrucVerify2 = "";
                            //if (TrueCount2 == k - (FalseCount + 3)) //gridValidate 고유코드 결과부분
                            //{
                            //layerStrucVerify2 = "OK";
                            //}
                            //else if (TrueCount2 != k - (FalseCount + 3))
                            //{
                            //layerStrucVerify2 = "NG";
                            //}

                            gridValidate.Rows.Add("고유코드", "객체별 고유ID의 정보가 상이한 경우 NG", layerStrucVerify);
                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var okCnt = 0;
                            var ngCnt = 0;

                            for (int i = doc.Childs.Length - 1; i >= 0; i--)
                            {
                                var Object1 = doc.Childs[i].Name;

                                var aWidth = 0;
                                var aHeight = 0;
                                int nIndex1 = 0;

                                for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                {
                                    for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                    {
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                        nIndex1++;
                                    }
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                }

                                byte ColorR1 = 0;
                                byte ColorG1 = 0;
                                byte ColorB1 = 0;
                                if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                {
                                    ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                    ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                    ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                }

                                var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                string value1 = RGBToHexadecimal(data1);

                                for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                {
                                    int nIndex2 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                            nIndex2++;
                                        }
                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                    }

                                    var Object2 = doc.Childs[i].Childs[j].Name;

                                    byte ColorR2 = 0;
                                    byte ColorG2 = 0;
                                    byte ColorB2 = 0;

                                    if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                    {
                                        ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                        ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                        ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                    }

                                    var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                    var value2 = RGBToHexadecimal(data2);

                                    var ColorVerify1 = "";
                                    if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "고속도로icon")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "배경")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else
                                    {
                                        ColorVerify1 = "NG";
                                        ngCnt++;
                                    }

                                    for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                    {
                                        var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;
                                        for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                        {
                                            int nIndex4 = 0;
                                            for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                            {
                                                for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                {
                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                    nIndex4++;
                                                }

                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                            }

                                            var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                            byte ColorR4 = 0;
                                            byte ColorG4 = 0;
                                            byte ColorB4 = 0;
                                            if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                            {
                                                ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                            }
                                            var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                            string value4 = RGBToHexadecimal(data4);


                                            // 도로 rgb 
                                            for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                            {
                                                int nIndex5 = 0;
                                                for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                    {
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                        nIndex5++;
                                                    }
                                                    if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                }
                                                var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                var roadObj = "";

                                                if ("도로" == doc.Childs[i].Childs[j].Name)
                                                {
                                                    roadObj = doc.Childs[i].Childs[j].Childs[0].Childs[1].Childs[0].Name;
                                                }


                                                var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                string value5 = RGBToHexadecimal(data5);
                                                var ColorVerify2 = "";

                                                if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else
                                                {
                                                    ColorVerify2 = "NG";
                                                    ngCnt++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            var verify = "";
                            if (ngCnt == 0)
                            {
                                verify = "OK";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            else if (ngCnt != 0)
                            {
                                verify = "NG";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            if (verify == "NG")
                            {
                                break;
                            }
                        }
                        gridValidate.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                    #endregion
                }
                catch
                {
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다.");
                }
            }
        }
        private void VerifyOEM_G3_ALL(string[] files)
        {
            if (rbtnOEM_G3_ALL.Checked == true)
            {
                try
                {
                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 구조" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어구조", "레이어구조");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStruct1 = "";
                                var layerStrucVerify = "";
                                var cnt = 0;

                                for (int i = 0; i < LayerSturct.Count; i++)
                                {
                                    cnt++;
                                    layerStruct1 = LayerSturct[i];
                                    var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                    if (layerStruct1.ToString() == "객체" && cnt == 1)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //파일명과 이미지ID가 일치한지 확인
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 6)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 7)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "배경" && cnt == 8)
                                        layerStrucVerify = "OK";
                                    else
                                    {
                                        layerStrucVerify = "NG";
                                        //return;
                                    }
                                    gridResult.Rows.Add(layerStruct1, layerStrucVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 순서" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("No.", "No.");
                                gridResult.Columns.Add("레이어순서", "레이어순서");
                                gridResult.Columns.Add("결과", "결과");

                                var cnt = 0;
                                var SequenceVerify = "";
                                var layerNumberList1 = "";

                                for (int i = 0; i < LayerNumberList.Count; i++)
                                {
                                    cnt++;
                                    layerNumberList1 = LayerNumberList[i];
                                    if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 3)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 4)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "배경" && cnt == 5)
                                        SequenceVerify = "OK";
                                    else
                                    {
                                        SequenceVerify = "NG";
                                    }
                                    gridResult.Rows.Add(cnt, layerNumberList1, SequenceVerify);
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 ON" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어 ON", "레이어 ON");
                                gridResult.Columns.Add("결과", "결과");

                                var Show = "";
                                var Visible = true;
                                var layerName = "";

                                for (int i = 0; i < LayerVisible.Count; i++)
                                {
                                    layerName = LayerList[i];
                                    Visible = LayerVisible[i];

                                    if (Visible == true)
                                    {
                                        Show = "OK";
                                    }
                                    else
                                        Show = "NG";
                                    gridResult.Rows.Add(layerName, Show);
                                }

                                //foreach (DataGridViewRow item in this.gridResult.Rows)
                                //{
                                //if (item.Cells["레이어 ON"].Value.ToString() == "고속도로icon")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //else if (item.Cells["레이어 ON"].Value.ToString() == "<Group>")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //else if (item.Cells["레이어 ON"].Value.ToString() == "<Image>")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //}


                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("고유코드" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString() /*&& "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString()*/)
                            {
                                //gridResult.Columns.Add("고유코드", "고유코드");
                                gridResult.Columns.Add("객체별 고유ID", "객체별 고유ID");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStrucVerify = "";
                                var Code = doc.Childs[1].Childs[0].Name;
                                int TrueCount = 0;
                                int FalseCount = 0;
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                                int j = 0;
                                int k = 0;

                                //char[] CodeTrim = { '+' };
                                //string[] CodeTrim2 = " + ";
                                //var CodeSplit = Code;
                                //var tmp = "";
                                //string[] array = CodeSplit.Split('+');
                                //for (int arr1 = 0; arr1 < array.Length; arr1++)
                                //{
                                //tmp = array[arr1].Trim();
                                //array[arr1] = tmp;
                                //}

                                for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리
                                    }

                                    if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    {
                                        layerStrucVerify = "OK";
                                        j++;
                                    }
                                    else if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                               && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                TrueCount--;
                                                layerStrucVerify = "NG";
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                        FalseCount++;
                                    }
                                }

                                int TrueCount2 = 0;

                                for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리1
                                    }


                                    //if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    //{
                                    //layerStrucVerify = "OK";
                                    //k++;
                                    //}
                                    if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount2++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount2--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                     && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount2--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                layerStrucVerify = "NG";
                                                TrueCount2--;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                    }
                                    gridResult.Rows.Add(IntervalID, layerStrucVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("색상정보" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("색상정보", "색상정보");
                                gridResult.Columns.Add("색상", "색상");
                                gridResult.Columns.Add("코드", "코드");
                                gridResult.Columns.Add("결과", "결과");

                                for (int i = doc.Childs.Length - 1; i >= 0; i--)
                                {
                                    var Object1 = doc.Childs[i].Name;

                                    var aWidth = 0;
                                    var aHeight = 0;
                                    int nIndex1 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                            nIndex1++;
                                        }
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                    }

                                    byte ColorR1 = 0;
                                    byte ColorG1 = 0;
                                    byte ColorB1 = 0;
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                    {
                                        ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                        ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                        ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                    }

                                    var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                    string value1 = RGBToHexadecimal(data1);
                                    //gridResult.Rows.Add(Object1/*, (ColorR1, ColorG1, ColorB1), value1*/);

                                    for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                    {
                                        int nIndex2 = 0;

                                        for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                        {
                                            for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                            {
                                                if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                                nIndex2++;
                                            }
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                        }

                                        var Object2 = doc.Childs[i].Childs[j].Name;

                                        byte ColorR2 = 0;
                                        byte ColorG2 = 0;
                                        byte ColorB2 = 0;

                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                        {
                                            ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                            ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                            ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                        }

                                        var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                        var value2 = RGBToHexadecimal(data2);

                                        var ColorVerify1 = "";
                                        if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else
                                            ColorVerify1 = "NG";

                                        gridResult.Rows.Add(Object2, (ColorR2, ColorG2, ColorB2), value2, ColorVerify1);
                                        foreach (DataGridViewRow item in this.gridResult.Rows)
                                        {
                                            if (item.Cells["색상정보"].Value.ToString() == "고속도로icon")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            else if (item.Cells["색상정보"].Value.ToString() == "배경")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Group>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Image>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                        }

                                        for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                        {
                                            var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;

                                            for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                            {
                                                int nIndex4 = 0;
                                                for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                    {
                                                        if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                        nIndex4++;
                                                    }

                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                }

                                                var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                                byte ColorR4 = 0;
                                                byte ColorG4 = 0;
                                                byte ColorB4 = 0;
                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                                {
                                                    ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                    ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                    ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                                }
                                                var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                                string value4 = RGBToHexadecimal(data4);
                                                //gridResult.Rows.Add(Object4, (ColorR4, ColorG4, ColorB4), value4);


                                                // 도로 rgb 
                                                for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    int nIndex5 = 0;
                                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                    {
                                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                        {
                                                            if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                            nIndex5++;
                                                        }
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                    }
                                                    var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                    //var roadObj = doc.Childs[i].Childs[4].Childs[0].Childs[1].Childs[0].Name;

                                                    var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                    var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                    var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                    var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                    string value5 = RGBToHexadecimal(data5);
                                                    var ColorVerify2 = "";

                                                    if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else
                                                        ColorVerify2 = "NG";

                                                    gridResult.Rows.Add(Object5, (ColorR5, ColorG5, ColorB5), value5, ColorVerify2); //도로명칭 나오는 부분
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        gridResult.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                }
                catch
                {
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다.");
                }
            }
        }

        private void SetOEM_G3_UpperLevel(string[] files)
        {
            //if (_isStart == false)
            //return;
            #region OEM_G3_ALL
            if (rbtnOEM_G3_UpperLevel.Checked == true)
            {
                #region gridValidate 
                gridValidate.Columns.Add("항목", "항목");
                gridValidate.Columns.Add("내용", "내용");
                gridValidate.Columns.Add("결과", "결과");
                #endregion

                try
                {
                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        this.gridValidate.Columns["항목"].SortMode = DataGridViewColumnSortMode.Automatic;

                        //1번 레이어 구조
                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            gridResult.Columns.Clear();
                            gridResult.Rows.Clear();

                            var layerStruct1 = "";
                            var layerStrucVerify = "";
                            var cnt = 0;
                            var NGcnt = 0;

                            for (int i = 0; i < LayerSturct.Count; i++)
                            {
                                cnt++;
                                layerStruct1 = LayerSturct[i];
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                if (layerStruct1.ToString() == ImageID[0]) //이미지id와 파일명 일치한지확인
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "동그라미")
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로")
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아")
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "배경")
                                    layerStrucVerify = "OK";
                                else
                                {
                                    NGcnt++;
                                    layerStrucVerify = "NG";
                                }
                            }
                            if (NGcnt == 0)
                            {
                                layerStrucVerify = "OK";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            if (NGcnt != 0)
                            {
                                layerStrucVerify = "NG";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            gridResult.Columns.Clear();
                            gridResult.Rows.Clear();

                            var cnt = 0;
                            var NGcnt = 0;
                            var SequenceVerify = "";
                            var layerNumberList1 = "";

                            for (int i = 0; i < LayerNumberList.Count; i++)
                            {
                                cnt++;
                                layerNumberList1 = LayerNumberList[i];
                                if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 3)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "배경" && cnt == 4)
                                    SequenceVerify = "OK";
                                else
                                {
                                    SequenceVerify = "NG";
                                    NGcnt++;
                                }
                            }

                            if (NGcnt == 0)
                            {
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                SequenceVerify = "NG";
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            if (SequenceVerify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {

                            var Show = "";
                            var Visible = true;
                            for (int i = 0; i < LayerList.Count; i++)
                            {

                                Visible = LayerVisible[i];

                                if (Visible == true)
                                {
                                    Show = "OK";
                                }
                                else
                                {
                                    Show = "NG";
                                }
                            }

                            gridValidate.Rows.Add("레이어 ON", "레이어가 OFF인 경우 NG", Show);
                            if (Show == "NG")
                            {
                                break;
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var okCnt = 0;
                            var ngCnt = 0;

                            for (int i = doc.Childs.Length - 1; i >= 0; i--)
                            {
                                var Object1 = doc.Childs[i].Name;

                                var aWidth = 0;
                                var aHeight = 0;
                                int nIndex1 = 0;

                                for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                {
                                    for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                    {
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                        nIndex1++;
                                    }
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                }

                                byte ColorR1 = 0;
                                byte ColorG1 = 0;
                                byte ColorB1 = 0;
                                if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                {
                                    ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                    ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                    ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                }

                                var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                string value1 = RGBToHexadecimal(data1);

                                for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                {
                                    int nIndex2 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                            nIndex2++;
                                        }
                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                    }

                                    var Object2 = doc.Childs[i].Childs[j].Name;

                                    byte ColorR2 = 0;
                                    byte ColorG2 = 0;
                                    byte ColorB2 = 0;

                                    if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                    {
                                        ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                        ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                        ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                    }

                                    var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                    var value2 = RGBToHexadecimal(data2);

                                    var ColorVerify1 = "";
                                    //if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                    //{
                                    //ColorVerify1 = "OK";
                                    //okCnt++;
                                    //}
                                    if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "고속도로icon")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "배경")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else
                                    {
                                        ColorVerify1 = "NG";
                                        ngCnt++;
                                    }

                                    for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                    {
                                        var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;
                                        for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                        {
                                            int nIndex4 = 0;
                                            for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                            {
                                                for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                {
                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                    nIndex4++;
                                                }

                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                            }

                                            var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                            byte ColorR4 = 0;
                                            byte ColorG4 = 0;
                                            byte ColorB4 = 0;
                                            if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                            {
                                                ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                            }
                                            var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                            string value4 = RGBToHexadecimal(data4);


                                            // 도로 rgb 
                                            for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                            {
                                                int nIndex5 = 0;
                                                for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                    {
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                        nIndex5++;
                                                    }
                                                    if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                }
                                                var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                var roadObj = "";

                                                if ("도로" == doc.Childs[i].Childs[j].Name)
                                                {
                                                    roadObj = doc.Childs[i].Childs[j].Childs[0].Childs[1].Childs[0].Name;
                                                }


                                                var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                string value5 = RGBToHexadecimal(data5);
                                                var ColorVerify2 = "";

                                                if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else
                                                {
                                                    ColorVerify2 = "NG";
                                                    ngCnt++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            var verify = "";
                            if (ngCnt == 0)
                            {
                                verify = "OK";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            else if (ngCnt != 0)
                            {
                                verify = "NG";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            if (verify == "NG")
                            {
                                break;
                            }
                        }
                        gridValidate.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                    #endregion
                }
                catch
                {

                }
            }
        }
        private void VerifyOEM_G3_UpperLevel(string[] files)
        {
            if (rbtnOEM_G3_UpperLevel.Checked == true)
            {
                try
                {
                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 구조" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어구조", "레이어구조");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStruct1 = "";
                                var layerStrucVerify = "";
                                var cnt = 0;

                                for (int i = 0; i < LayerSturct.Count; i++)
                                {
                                    cnt++;
                                    layerStruct1 = LayerSturct[i];
                                    var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                    if (layerStruct1.ToString() == ImageID[0]) //파일명과 이미지ID가 일치한지 확인
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "동그라미")
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로")
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.0 기아")
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "배경")
                                        layerStrucVerify = "OK";
                                    else
                                    {
                                        layerStrucVerify = "NG";
                                    }
                                    gridResult.Rows.Add(layerStruct1, layerStrucVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 순서" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {

                                gridResult.Columns.Clear();
                                gridResult.Rows.Clear();

                                gridResult.Columns.Add("No.", "No.");
                                gridResult.Columns.Add("레이어순서", "레이어순서");
                                gridResult.Columns.Add("결과", "결과");

                                var cnt = 0;
                                var SequenceVerify = "";
                                var layerNumberList1 = "";

                                for (int i = 0; i < LayerNumberList.Count; i++)
                                {
                                    cnt++;
                                    layerNumberList1 = LayerNumberList[i];
                                    if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.0 기아" && cnt == 3)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "배경" && cnt == 4)
                                        SequenceVerify = "OK";
                                    else
                                    {
                                        SequenceVerify = "NG";
                                    }
                                    gridResult.Rows.Add(cnt, layerNumberList1, SequenceVerify);
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 ON" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어 ON", "레이어 ON");
                                gridResult.Columns.Add("결과", "결과");

                                var Show = "";
                                var Visible = true;
                                var layerName = "";

                                for (int i = 0; i < LayerVisible.Count; i++)
                                {
                                    layerName = LayerList[i];
                                    Visible = LayerVisible[i];

                                    if (Visible == true)
                                    {
                                        Show = "OK";
                                    }
                                    else
                                        Show = "NG";
                                    gridResult.Rows.Add(layerName, Show);
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("색상정보" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("색상정보", "색상정보");
                                gridResult.Columns.Add("색상", "색상");
                                gridResult.Columns.Add("코드", "코드");
                                gridResult.Columns.Add("결과", "결과");

                                for (int i = doc.Childs.Length - 1; i >= 0; i--)
                                {
                                    var Object1 = doc.Childs[i].Name;

                                    var aWidth = 0;
                                    var aHeight = 0;
                                    int nIndex1 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                            nIndex1++;
                                        }
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                    }

                                    byte ColorR1 = 0;
                                    byte ColorG1 = 0;
                                    byte ColorB1 = 0;
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                    {
                                        ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                        ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                        ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                    }

                                    var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                    string value1 = RGBToHexadecimal(data1);
                                    //gridResult.Rows.Add(Object1/*, (ColorR1, ColorG1, ColorB1), value1*/);

                                    for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                    {
                                        int nIndex2 = 0;

                                        for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                        {
                                            for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                            {
                                                if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                                nIndex2++;
                                            }
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                        }

                                        var Object2 = doc.Childs[i].Childs[j].Name;

                                        byte ColorR2 = 0;
                                        byte ColorG2 = 0;
                                        byte ColorB2 = 0;

                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                        {
                                            ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                            ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                            ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                        }

                                        var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                        var value2 = RGBToHexadecimal(data2);

                                        var ColorVerify1 = "";
                                        //if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                        //{
                                        //ColorVerify1 = "OK";
                                        //}
                                        if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                        {
                                            ColorVerify1 = "OK";
                                        }
                                        else
                                            ColorVerify1 = "NG";

                                        gridResult.Rows.Add(Object2, (ColorR2, ColorG2, ColorB2), value2, ColorVerify1);
                                        foreach (DataGridViewRow item in this.gridResult.Rows)
                                        {
                                            if (item.Cells["색상정보"].Value.ToString() == "고속도로icon")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            else if (item.Cells["색상정보"].Value.ToString() == "배경")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Group>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Image>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                        }

                                        for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                        {
                                            var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;

                                            for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                            {
                                                int nIndex4 = 0;
                                                for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                    {
                                                        if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                        nIndex4++;
                                                    }

                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                }

                                                var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                                byte ColorR4 = 0;
                                                byte ColorG4 = 0;
                                                byte ColorB4 = 0;
                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                                {
                                                    ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                    ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                    ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                                }
                                                var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                                string value4 = RGBToHexadecimal(data4);
                                                //gridResult.Rows.Add(Object4, (ColorR4, ColorG4, ColorB4), value4);


                                                // 도로 rgb 
                                                for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    int nIndex5 = 0;
                                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                    {
                                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                        {
                                                            if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                            nIndex5++;
                                                        }
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                    }
                                                    var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                    //var roadObj = doc.Childs[i].Childs[4].Childs[0].Childs[1].Childs[0].Name;

                                                    var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                    var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                    var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                    var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                    string value5 = RGBToHexadecimal(data5);
                                                    var ColorVerify2 = "";

                                                    if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else
                                                        ColorVerify2 = "NG";

                                                    gridResult.Rows.Add(Object5, (ColorR5, ColorG5, ColorB5), value5, ColorVerify2); //도로명칭 나오는 부분
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        gridResult.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                }
                catch
                {

                }
            }
        }

        private void SetOEM_AM(string[] files)
        {
            //if (_isStart == false)
            //return;
            #region OEM_AM
            if (rbtnOEM_AM.Checked == true)
            {
                try
                {
                    #region gridValidate 
                    gridValidate.Columns.Add("항목", "항목");
                    gridValidate.Columns.Add("내용", "내용");
                    gridValidate.Columns.Add("결과", "결과");
                    #endregion

                    foreach (var file in files)
                    {

                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        this.gridValidate.Columns["항목"].SortMode = DataGridViewColumnSortMode.Automatic;

                        //1번 레이어 구조
                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStruct1 = "";
                            var layerStrucVerify = "";
                            var cnt = 0;
                            var NGcnt = 0;

                            for (int i = 0; i < LayerSturct.Count; i++)
                            {
                                cnt++;
                                layerStruct1 = LayerSturct[i];
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                if (layerStruct1.ToString() == "객체" && cnt == 1)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //이미지id와 파일명 일치한지확인
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "고속도로icon" && cnt == 6)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 7)
                                    layerStrucVerify = "OK";
                                else if (layerStruct1.ToString() == "배경" && cnt == 8)
                                    layerStrucVerify = "OK";
                                else
                                {
                                    NGcnt++;
                                    layerStrucVerify = "NG";
                                }
                            }
                            if (NGcnt == 0)
                            {
                                layerStrucVerify = "OK";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            if (NGcnt != 0)
                            {
                                layerStrucVerify = "NG";
                                gridValidate.Rows.Add("레이어 구조", "레이어의 폴더 트리 구조가 다른 경우 NG", layerStrucVerify);
                            }
                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            gridResult.Columns.Clear();
                            gridResult.Rows.Clear();

                            var cnt = 0;
                            var NGcnt = 0;
                            var SequenceVerify = "";
                            var layerNumberList1 = "";

                            for (int i = 0; i < LayerNumberList.Count; i++)
                            {
                                cnt++;
                                layerNumberList1 = LayerNumberList[i];
                                if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "고속도로icon" && cnt == 3)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 4)
                                    SequenceVerify = "OK";
                                else if (layerNumberList1.ToString() == "배경" && cnt == 5)
                                    SequenceVerify = "OK";
                                else
                                {
                                    SequenceVerify = "NG";
                                    NGcnt++;
                                }
                            }

                            if (NGcnt == 0)
                            {
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            else if (NGcnt != 0)
                            {
                                SequenceVerify = "NG";
                                gridValidate.Rows.Add("레이어 순서", "정의된 레이어 순서가 다른 경우 NG", SequenceVerify);
                            }
                            if (SequenceVerify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {

                            var Show = "";
                            var Visible = true;
                            for (int i = 0; i < LayerList.Count; i++)
                            {

                                Visible = LayerVisible[i];

                                if (Visible == true)
                                {
                                    Show = "OK";
                                }
                                else
                                {
                                    Show = "NG";
                                }
                            }

                            gridValidate.Rows.Add("레이어 ON", "레이어가 OFF인 경우 NG", Show);
                            if (Show == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var layerStrucVerify = "";
                            var Code = doc.Childs[1].Childs[0].Name;
                            int TrueCount = 0;
                            int FalseCount = 0;
                            var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                            int j = 0;


                            //char[] CodeTrim = { '+' };
                            //string[] CodeTrim2 = " + ";
                            //var CodeSplit = Code;
                            //var tmp = "";
                            //string[] array = CodeSplit.Split('+');
                            //for (int arr1 = 0; arr1 < array.Length; arr1++)
                            //{
                            //tmp = array[arr1].Trim();
                            //array[arr1] = tmp;
                            //}

                            for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리
                                }

                                if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                {
                                    layerStrucVerify = "OK";
                                    j++;
                                }
                                else if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            TrueCount--;
                                            layerStrucVerify = "NG";
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    FalseCount++;
                                }
                            }

                            int TrueCount2 = 0;
                            int k = 0;
                            for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                            {
                                var IntervalID = "";
                                try
                                {
                                    IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    //엑셀 널값으로 인한 예외처리1
                                }

                                if (Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "OK";
                                    TrueCount2++;

                                    try
                                    {
                                        if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                            && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                        {
                                            TrueCount2--;
                                            break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                        }
                                        else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                        {
                                            TrueCount--;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (IntervalID == "")
                                        {
                                            layerStrucVerify = "NG";
                                            TrueCount2--;
                                        }
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (!Code.Contains(IntervalID))
                                {
                                    layerStrucVerify = "NG";
                                    break;
                                }
                            }

                            //var layerStrucVerify2 = "";
                            //if (TrueCount2 == k - (FalseCount + 3)) //gridValidate 고유코드 결과부분
                            //{
                            //layerStrucVerify2 = "OK";
                            //}
                            //else if (TrueCount2 != k - (FalseCount + 3))
                            //{
                            //layerStrucVerify2 = "NG";
                            //}

                            gridValidate.Rows.Add("고유코드", "객체별 고유ID의 정보가 상이한 경우 NG", layerStrucVerify);
                            if (layerStrucVerify == "NG")
                            {
                                break;
                            }
                        }



                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var okCnt = 0;
                            var ngCnt = 0;

                            for (int i = doc.Childs.Length - 1; i >= 0; i--)
                            {
                                var Object1 = doc.Childs[i].Name;

                                var aWidth = 0;
                                var aHeight = 0;
                                int nIndex1 = 0;

                                for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                {
                                    for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                    {
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                        nIndex1++;
                                    }
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                }

                                byte ColorR1 = 0;
                                byte ColorG1 = 0;
                                byte ColorB1 = 0;
                                if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                {
                                    ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                    ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                    ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                }

                                var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                string value1 = RGBToHexadecimal(data1);

                                for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                {
                                    int nIndex2 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                            nIndex2++;
                                        }
                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                    }

                                    var Object2 = doc.Childs[i].Childs[j].Name;

                                    byte ColorR2 = 0;
                                    byte ColorG2 = 0;
                                    byte ColorB2 = 0;

                                    if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                    {
                                        ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                        ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                        ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                    }

                                    var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                    var value2 = RGBToHexadecimal(data2);

                                    var ColorVerify1 = "";
                                    if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    //else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                    //{
                                    //ColorVerify1 = "OK";
                                    //okCnt++;
                                    //}
                                    else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "고속도로icon")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else if (Object2 == "배경")
                                    {
                                        ColorVerify1 = "OK";
                                        okCnt++;
                                    }
                                    else
                                    {
                                        ColorVerify1 = "NG";
                                        ngCnt++;
                                    }

                                    for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                    {
                                        var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;
                                        for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                        {
                                            int nIndex4 = 0;
                                            for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                            {
                                                for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                {
                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                    nIndex4++;
                                                }

                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                            }

                                            var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                            byte ColorR4 = 0;
                                            byte ColorG4 = 0;
                                            byte ColorB4 = 0;
                                            if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                            {
                                                ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                            }
                                            var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                            string value4 = RGBToHexadecimal(data4);


                                            // 도로 rgb 
                                            for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                            {
                                                int nIndex5 = 0;
                                                for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                    {
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                        nIndex5++;
                                                    }
                                                    if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                }
                                                var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                var roadObj = "";

                                                if ("도로" == doc.Childs[i].Childs[j].Name)
                                                {
                                                    roadObj = doc.Childs[i].Childs[j].Childs[0].Childs[1].Childs[0].Name;
                                                }


                                                var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                string value5 = RGBToHexadecimal(data5);
                                                var ColorVerify2 = "";

                                                if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                {
                                                    ColorVerify2 = "OK";
                                                    okCnt++;
                                                }
                                                else
                                                {
                                                    ColorVerify2 = "NG";
                                                    ngCnt++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            var verify = "";
                            if (ngCnt == 0)
                            {
                                verify = "OK";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            else if (ngCnt != 0)
                            {
                                verify = "NG";
                                gridValidate.Rows.Add("색상정보", "이미지의 색상정보가 상이한 경우 NG", verify);
                            }
                            if (verify == "NG")
                            {
                                break;
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            var ImageIconVerify = "";
                            var IconName = "";
                            bool IconVisible;


                            for (int i = doc.Childs[0].Childs.Length - 1; i >= 0; i--)
                            {
                                if ("고속도로icon" == doc.Childs[0].Childs[i].Name)
                                {
                                    IconName = doc.Childs[0].Childs[i].Name;
                                }
                            }

                            try
                            {
                                if (IconName == "고속도로icon")
                                {
                                    ImageIconVerify = "OK";
                                }
                                else if (IconName != "고속도로icon")
                                {
                                    ImageIconVerify = "NG";
                                }
                            }
                            catch (Exception)
                            {
                                //예외처리 안해주면 폴더선택후 스타트할시 인덱스 오류
                            }
                            gridValidate.Rows.Add("아이콘유무", "아이콘유무에 따라 OK or NG발생! OK일 경우 이미지출력", ImageIconVerify);
                            if (ImageIconVerify == "NG")
                            {
                                break;
                            }
                        }
                        gridValidate.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                    #endregion
                }
                catch
                {
                    //OEM_G3_상위레벨 객체ID 부재로인한 예외처리
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다");
                }
            }
        }
        private void VerifyOEM_AM(string[] files)
        {
            if (rbtnOEM_AM.Checked == true)
            {
                try
                {
                    foreach (var file in files)
                    {
                        #region doc list
                        List<string> LayerForderTree = new List<string>();
                        List<string> LayerList = new List<string>();
                        List<bool> LayerVisible = new List<bool>();
                        List<string> LayerSturct = new List<string>();
                        List<string> LayerNumberList = new List<string>();
                        var doc = PsdDocument.Create(file);

                        for (int a = doc.Childs.Length - 1; a >= 0; a--)
                        {
                            LayerForderTree.Add(doc.Childs[a].Name);
                            LayerList.Add(doc.Childs[a].Name);
                            LayerVisible.Add(doc.Childs[a].IsVisible);

                            LayerSturct.Add(doc.Childs[a].Name);
                            for (int b = doc.Childs[a].Childs.Length - 1; b >= 0; b--)
                            {
                                LayerForderTree.Add(doc.Childs[a].Childs[b].Name);
                                LayerList.Add(doc.Childs[a].Childs[b].Name);
                                LayerVisible.Add(doc.Childs[a].Childs[b].IsVisible);

                                LayerSturct.Add(doc.Childs[a].Childs[b].Name);

                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd');
                                if (ImageID[0] == doc.Childs[a].Name)
                                {
                                    LayerNumberList.Add(doc.Childs[a].Childs[b].Name);
                                }

                                for (int c = doc.Childs[a].Childs[b].Childs.Length - 1; c >= 0; c--)
                                {
                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Name);
                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].IsVisible);

                                    for (int d = doc.Childs[a].Childs[b].Childs[c].Childs.Length - 1; d >= 0; d--)
                                    {
                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Name);
                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].IsVisible);

                                        for (int e = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs.Length - 1; e >= 0; e--)
                                        {
                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Name);
                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].IsVisible);

                                            for (int f = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs.Length - 1; f >= 0; f--)
                                            {
                                                LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Name);
                                                LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].IsVisible);

                                                for (int g = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Name);
                                                    LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].IsVisible);

                                                    for (int h = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs.Length - 1; h >= 0; h--)
                                                    {
                                                        LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Name);
                                                        LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].IsVisible);

                                                        for (int i = doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs.Length - 1; i >= 0; i--)
                                                        {
                                                            LayerList.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].Name);
                                                            LayerVisible.Add(doc.Childs[a].Childs[b].Childs[c].Childs[d].Childs[e].Childs[f].Childs[g].Childs[h].Childs[i].IsVisible);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 구조" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어구조", "레이어구조");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStruct1 = "";
                                var layerStrucVerify = "";
                                var cnt = 0;

                                for (int i = 0; i < LayerSturct.Count; i++)
                                {
                                    cnt++;
                                    layerStruct1 = LayerSturct[i];
                                    var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명

                                    if (layerStruct1.ToString() == "객체" && cnt == 1)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == doc.Childs[1].Childs[0].Name && cnt == 2)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == ImageID[0] && cnt == 3) //파일명과 이미지ID가 일치한지 확인
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "동그라미" && cnt == 4)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로" && cnt == 5)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "고속도로icon" && cnt == 6)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 7)
                                        layerStrucVerify = "OK";
                                    else if (layerStruct1.ToString() == "배경" && cnt == 8)
                                        layerStrucVerify = "OK";
                                    else
                                    {
                                        layerStrucVerify = "NG";
                                        //return;
                                    }
                                    gridResult.Rows.Add(layerStruct1, layerStrucVerify);
                                }
                                foreach (DataGridViewRow item in this.gridResult.Rows)
                                {
                                    if (item.Cells["레이어구조"].Value.ToString() == "고속도로icon")
                                    {
                                        gridResult.Rows.RemoveAt(item.Index);
                                    }
                                }

                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 순서" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("No.", "No.");
                                gridResult.Columns.Add("레이어순서", "레이어순서");
                                gridResult.Columns.Add("결과", "결과");

                                var cnt = 0;
                                var SequenceVerify = "";
                                var layerNumberList1 = "";

                                for (int i = 0; i < LayerNumberList.Count; i++)
                                {
                                    cnt++;
                                    layerNumberList1 = LayerNumberList[i];
                                    if (layerNumberList1.ToString() == "동그라미" && cnt == 1)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로" && cnt == 2)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "고속도로icon" && cnt == 3)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "도로명칭_표준형4.5 현대" && cnt == 4)
                                        SequenceVerify = "OK";
                                    else if (layerNumberList1.ToString() == "배경" && cnt == 5)
                                        SequenceVerify = "OK";
                                    else
                                    {
                                        SequenceVerify = "NG";
                                    }
                                    gridResult.Rows.Add(cnt, layerNumberList1, SequenceVerify);
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("레이어 ON" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("레이어 ON", "레이어 ON");
                                gridResult.Columns.Add("결과", "결과");

                                var Show = "";
                                var Visible = true;
                                var layerName = "";

                                for (int i = 0; i < LayerVisible.Count; i++)
                                {
                                    layerName = LayerList[i];
                                    Visible = LayerVisible[i];

                                    if (Visible == true)
                                    {
                                        Show = "OK";
                                    }
                                    else
                                        Show = "NG";
                                    gridResult.Rows.Add(layerName, Show);
                                }

                                //foreach (DataGridViewRow item in this.gridResult.Rows)
                                //{
                                //if (item.Cells["레이어 ON"].Value.ToString() == "고속도로icon")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //else if (item.Cells["레이어 ON"].Value.ToString() == "<Group>")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //else if (item.Cells["레이어 ON"].Value.ToString() == "<Image>")
                                //{
                                //gridResult.Rows.RemoveAt(item.Index);
                                //}
                                //}


                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("고유코드" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString() /*&& "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString()*/)
                            {
                                //gridResult.Columns.Add("고유코드", "고유코드");
                                gridResult.Columns.Add("객체별 고유ID", "객체별 고유ID");
                                gridResult.Columns.Add("결과", "결과");

                                var layerStrucVerify = "";
                                var Code = doc.Childs[1].Childs[0].Name;
                                int TrueCount = 0;
                                int FalseCount = 0;
                                var ImageID = Path.GetFileName(file).Split('.', 'p', 's', 'd'); //psd파일명
                                int j = 0;
                                int k = 0;

                                //char[] CodeTrim = { '+' };
                                //string[] CodeTrim2 = " + ";
                                //var CodeSplit = Code;
                                //var tmp = "";
                                //string[] array = CodeSplit.Split('+');
                                //for (int arr1 = 0; arr1 < array.Length; arr1++)
                                //{
                                //tmp = array[arr1].Trim();
                                //array[arr1] = tmp;
                                //}

                                for (j = 0; j < gridExcelInfo.Rows.Count; j++) //엑셀파일에서 psd파일에 맞는 구간id 찾기
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[j].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리
                                    }

                                    if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    {
                                        layerStrucVerify = "OK";
                                        j++;
                                    }
                                    else if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                               && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                TrueCount--;
                                                layerStrucVerify = "NG";
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                        FalseCount++;
                                    }
                                }

                                int TrueCount2 = 0;

                                for (k = FalseCount + 3; k < gridExcelInfo.Rows.Count; k++)
                                {
                                    var IntervalID = "";
                                    try
                                    {
                                        IntervalID = gridExcelInfo.Rows[k].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        //엑셀 널값으로 인한 예외처리1
                                    }


                                    //if (Code.Contains(IntervalID) && IntervalID == ImageID[0])
                                    //{
                                    //layerStrucVerify = "OK";
                                    //k++;
                                    //}
                                    if (Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "OK";
                                        TrueCount2++;

                                        try
                                        {
                                            if (!Code.Contains(gridExcelInfo.Rows[k + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[k + 4].Cells[0].Value.ToString())
                                                && Code.Contains(gridExcelInfo.Rows[k - 1].Cells[0].Value.ToString()) && IntervalID == "")
                                            {
                                                TrueCount2--;
                                                break;  //맞는 구간 마지막 부분이랑 널값에 ok 뜨는거고쳐야함
                                            }
                                            else if (IntervalID == "" && Code.Contains(gridExcelInfo.Rows[j + 1].Cells[0].Value.ToString()) && !Code.Contains(gridExcelInfo.Rows[j - 1].Cells[0].Value.ToString())
                                                     && !Code.Contains(gridExcelInfo.Rows[j - 3].Cells[0].Value.ToString()))
                                            {
                                                TrueCount2--;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        try
                                        {
                                            if (IntervalID == "")
                                            {
                                                layerStrucVerify = "NG";
                                                TrueCount2--;
                                            }
                                        }
                                        catch
                                        {
                                        }

                                    }
                                    else if (!Code.Contains(IntervalID))
                                    {
                                        layerStrucVerify = "NG";
                                    }
                                    gridResult.Rows.Add(IntervalID, layerStrucVerify);
                                }
                            }
                        }


                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                        {
                            if ("색상정보" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                gridResult.Columns.Add("색상정보", "색상정보");
                                gridResult.Columns.Add("색상", "색상");
                                gridResult.Columns.Add("코드", "코드");
                                gridResult.Columns.Add("결과", "결과");

                                for (int i = doc.Childs.Length - 1; i >= 0; i--)
                                {
                                    var Object1 = doc.Childs[i].Name;

                                    var aWidth = 0;
                                    var aHeight = 0;
                                    int nIndex1 = 0;

                                    for (aWidth = 0; aWidth < doc.Childs[i].Width; aWidth++)
                                    {
                                        for (aHeight = 0; aHeight < doc.Childs[i].Height; aHeight++)
                                        {
                                            if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                            nIndex1++;
                                        }
                                        if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1]) break;
                                    }

                                    byte ColorR1 = 0;
                                    byte ColorG1 = 0;
                                    byte ColorB1 = 0;
                                    if (doc.Childs[i].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Channels[0].Data[nIndex1])
                                    {
                                        ColorR1 = doc.Childs[i].Channels[1].Data[nIndex1];
                                        ColorG1 = doc.Childs[i].Channels[2].Data[nIndex1];
                                        ColorB1 = doc.Childs[i].Channels[3].Data[nIndex1];
                                    }

                                    var data1 = new RGB(ColorR1, ColorG1, ColorB1);
                                    string value1 = RGBToHexadecimal(data1);
                                    //gridResult.Rows.Add(Object1/*, (ColorR1, ColorG1, ColorB1), value1*/);

                                    for (int j = doc.Childs[i].Childs.Length - 1; j >= 0; j--)
                                    {
                                        int nIndex2 = 0;

                                        for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Width; aWidth++)
                                        {
                                            for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Height; aHeight++)
                                            {
                                                if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                                nIndex2++;
                                            }
                                            if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2]) break;
                                        }

                                        var Object2 = doc.Childs[i].Childs[j].Name;

                                        byte ColorR2 = 0;
                                        byte ColorG2 = 0;
                                        byte ColorB2 = 0;

                                        if (doc.Childs[i].Childs[j].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Channels[0].Data[nIndex2])
                                        {
                                            ColorR2 = doc.Childs[i].Childs[j].Channels[1].Data[nIndex2];
                                            ColorG2 = doc.Childs[i].Childs[j].Channels[2].Data[nIndex2];
                                            ColorB2 = doc.Childs[i].Childs[j].Channels[3].Data[nIndex2];
                                        }

                                        var data2 = new RGB(ColorR2, ColorG2, ColorB2);
                                        var value2 = RGBToHexadecimal(data2);

                                        try
                                        {
                                            var ColorVerify1 = "";
                                            if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == doc.Childs[1].Childs[0].Name)
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 255 && ColorG2 == 255 && ColorB2 == 255 && value2 == "FFFFFF" && Object2 == "동그라미")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            //else if (ColorR2 == 35 && ColorG2 == 31 && ColorB2 == 32 && value2 == "231F20" && Object2 == "도로")
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.0 기아")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else if (ColorR2 == 0 && ColorG2 == 0 && ColorB2 == 0 && value2 == "000000" && Object2 == "도로명칭_표준형4.5 현대")
                                            {
                                                ColorVerify1 = "OK";
                                            }
                                            else
                                                ColorVerify1 = "NG";

                                            gridResult.Rows.Add(Object2, (ColorR2, ColorG2, ColorB2), value2, ColorVerify1);
                                        }
                                        catch
                                        { }

                                        foreach (DataGridViewRow item in this.gridResult.Rows)
                                        {
                                            if (item.Cells["색상정보"].Value.ToString() == "고속도로icon")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            else if (item.Cells["색상정보"].Value.ToString() == "배경")
                                            {
                                                gridResult.Rows.RemoveAt(item.Index);
                                            }
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Group>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                            //else if (item.Cells["색상정보"].Value.ToString() == "<Image>")
                                            //{
                                            //gridResult.Rows.RemoveAt(item.Index);
                                            //}
                                        }

                                        for (int k = doc.Childs[i].Childs[j].Childs.Length - 1; k >= 0; k--)
                                        {
                                            var Object3 = doc.Childs[i].Childs[j].Childs[k].Name;

                                            for (int h = doc.Childs[i].Childs[j].Childs[k].Childs.Length - 1; h >= 0; h--)
                                            {
                                                int nIndex4 = 0;
                                                for (aWidth = 0; aWidth <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Width - 1; aWidth++)
                                                {
                                                    for (aHeight = 0; aHeight <= doc.Childs[i].Childs[j].Childs[k].Childs[h].Height - 1; aHeight++)
                                                    {
                                                        if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                        nIndex4++;
                                                    }

                                                    if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4]) break;
                                                }

                                                var Object4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Name;
                                                byte ColorR4 = 0;
                                                byte ColorG4 = 0;
                                                byte ColorB4 = 0;
                                                if (doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data.LongLength != 0 && 0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[0].Data[nIndex4])
                                                {
                                                    ColorR4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[1].Data[nIndex4];
                                                    ColorG4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[2].Data[nIndex4];
                                                    ColorB4 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Channels[3].Data[nIndex4];
                                                }
                                                var data4 = new RGB(ColorR4, ColorG4, ColorB4);
                                                string value4 = RGBToHexadecimal(data4);
                                                //gridResult.Rows.Add(Object4, (ColorR4, ColorG4, ColorB4), value4);


                                                // 도로 rgb 
                                                for (int g = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs.Length - 1; g >= 0; g--)
                                                {
                                                    int nIndex5 = 0;
                                                    for (aWidth = 0; aWidth < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Width; aWidth++)
                                                    {
                                                        for (aHeight = 0; aHeight < doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Height; aHeight++)
                                                        {
                                                            if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                            nIndex5++;
                                                        }
                                                        if (0 != doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[0].Data[nIndex5]) break;
                                                    }
                                                    var Object5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Name;

                                                    //var roadObj = doc.Childs[i].Childs[4].Childs[0].Childs[1].Childs[0].Name;

                                                    var ColorR5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[1].Data[nIndex5];
                                                    var ColorG5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[2].Data[nIndex5];
                                                    var ColorB5 = doc.Childs[i].Childs[j].Childs[k].Childs[h].Childs[g].Channels[3].Data[nIndex5];

                                                    var data5 = new RGB(ColorR5, ColorG5, ColorB5);
                                                    string value5 = RGBToHexadecimal(data5);
                                                    var ColorVerify2 = "";

                                                    if (ColorR5 == 63 && ColorG5 == 229 && ColorB5 == 255 && value5 == "3FE5FF" || ColorR5 == 63 && ColorG5 == 230 && ColorB5 == 255 && value5 == "3FE6FF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 243 && ColorG5 == 235 && ColorB5 == 19 && value5 == "F3EB13" || ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 0 && value5 == "FFFF00")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 255 && ColorG5 == 255 && ColorB5 == 255 && value5 == "FFFFFF")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else if (ColorR5 == 35 && ColorG5 == 31 && ColorB5 == 32 && value5 == "231F20")
                                                    {
                                                        ColorVerify2 = "OK";
                                                    }
                                                    else
                                                        ColorVerify2 = "NG";

                                                    gridResult.Rows.Add(Object5, (ColorR5, ColorG5, ColorB5), value5, ColorVerify2); //도로명칭 나오는 부분
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString() && "OK" == gridValidate.CurrentRow.Cells[2].FormattedValue.ToString())
                        {
                            if ("아이콘유무" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                            {
                                var fileName = gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString();
                                ImageIconForm iconForm = new ImageIconForm();
                                ImageIconForm.PassValue = txtFolderPath.Text;
                                iconForm.Show();
                            }
                        }
                        gridResult.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    }
                }
                catch
                {
                    //MessageBox.Show("OEM 레벨이 맞지 않습니다");
                }
            }
        }


        public void gridLayerInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (rbtnOEM.Checked)
            {
                CheckType("OEM");
            }
            else if (rbtnOEM_G3_ALL.Checked)
            {
                CheckType("OEM_G3_ALL");
            }
            else if (rbtnOEM_G3_UpperLevel.Checked)
            {
                CheckType("OEM_G3_UpperLevel");
            }
            else if (rbtnOEM_AM.Checked)
            {
                CheckType("OEM_AM");
            }
        }

        private void gridValidate_SelectionChanged(object sender, EventArgs e)
        {
            if (rbtnOEM.Checked)
            {
                CheckType1("OEM");
            }
            else if (rbtnOEM_G3_ALL.Checked)
            {
                CheckType1("OEM_G3_ALL");
            }
            else if (rbtnOEM_G3_UpperLevel.Checked)
            {
                CheckType1("OEM_G3_UpperLevel");
            }
            else if (rbtnOEM_AM.Checked)
            {
                CheckType1("OEM_AM");
            }
        }


        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();

            if (result != DialogResult.OK)
                return;

            txtFolderPath.Text = fbd.SelectedPath; // 선택한 폴더명
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtExcel.Text;
            fdlg.Filter = "Excel File| *.xlsx; *.xls";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtExcel.Text = fdlg.FileName;
            }
        }

        private void gridResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var selectPath = txtFolderPath.Text;
            var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
                    {

                        if ("색상정보" == gridValidate.CurrentRow.Cells[0].FormattedValue.ToString())
                        {

                            if (e.ColumnIndex == 1 && e.RowIndex != this.gridResult.NewRowIndex)
                            {
                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.0 기아")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.5 현대")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;
                            }

                            if (e.ColumnIndex == 2 && e.RowIndex != this.gridResult.NewRowIndex)
                            {
                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.0 기아")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.5 현대")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;
                            }
                            if (e.ColumnIndex == 3 && e.RowIndex != this.gridResult.NewRowIndex)
                            {
                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.0 기아")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로명칭_표준형4.5 현대")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;

                                if (this.gridResult[0, e.RowIndex].Value.ToString() == "도로")
                                {
                                    e.Value = "";
                                    this.gridResult[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                                    e.FormattingApplied = true;
                                }
                                else
                                    e.FormattingApplied = false;
                            }

                        }
                    }
                }
                catch
                {
                    //OEM에서 이미 cellformatting이 진행되었기 때문에 다른 OEM구조에서는 null이 뜨므로 예외처리해주어야한다
                    return;
                }
            }
        }
        private void gridExcelInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3 && e.RowIndex != this.gridExcelInfo.NewRowIndex)
                {
                    if (this.gridExcelInfo[0, e.RowIndex].Value.ToString() == "구간ID")
                    {
                        e.Value = "";
                        this.gridExcelInfo[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                        e.FormattingApplied = true;
                    }
                    else
                        e.FormattingApplied = false;
                }
            }
            catch
            {
                //구간ID null로 인한 예외처리
            }
        }
        private void gridLayerInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //var selectPath = txtFolderPath.Text;
            //var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);

            //var verify = "";

            //int LayerInfoCnt;

            //foreach (var file in files)
            //{
            //    if (e.ColumnIndex == 1 && e.RowIndex != this.gridLayerInfo.NewRowIndex)
            //    {
            //        if (this.gridLayerInfo[0, e.RowIndex].Value.ToString() == file.ToString())
            //        {
            //            try
            //            {
            //                if (file.ToString() == gridLayerInfo.CurrentRow.Cells[0].FormattedValue.ToString())
            //                {
            //                    for (int i = 0; i < gridValidate.Rows.Count; i++)
            //                    {
            //                        if (gridValidate.Rows[i].Cells[2].Value.ToString() == "OK")   // 원본파일로 selectionchanged 이벤트가 발생되지 않았기 때문에 안먹음 파일명이 원본이여도 gridvalidate에 적용되지않음
            //                        {
            //                            verify = "OK";
            //                        }
            //                        else
            //                        {
            //                            verify = "NG";
            //                            break;
            //                        }
            //                    }

            //                    if (verify == "OK")
            //                    {
            //                        e.Value = "OK";
            //                        this.gridValidate[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            //                        e.FormattingApplied = true;
            //                        //break;
            //                    }
            //                    else
            //                    {
            //                        e.Value = "NG";
            //                        this.gridValidate[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            //                        e.FormattingApplied = true;
            //                        //break;
            //                    }
            //                }
            //            }
            //            catch { }
            //        }
            //        else
            //            e.FormattingApplied = false;
            //    }
            //}
        }



        public struct RGB
        {
            private byte _r;
            private byte _g;
            private byte _b;

            public RGB(byte r, byte g, byte b)
            {
                this._r = r;
                this._g = g;
                this._b = b;
            }

            public byte R
            {
                get { return this._r; }
                set { this._r = value; }
            }

            public byte G
            {
                get { return this._g; }
                set { this._g = value; }
            }

            public byte B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public bool Equals(RGB rgb)
            {
                return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
            }
        }

        public static string RGBToHexadecimal(RGB rgb)
        {
            string rs = DecimalToHexadecimal(rgb.R);
            string gs = DecimalToHexadecimal(rgb.G);
            string bs = DecimalToHexadecimal(rgb.B);

            return rs + gs + bs;
        }

        private static string DecimalToHexadecimal(int dec)
        {
            if (dec <= 0)
                return "00";

            int hex = dec;
            string hexStr = string.Empty;

            while (dec > 0)
            {
                hex = dec % 16;

                if (hex < 10)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                dec /= 16;
            }

            return hexStr;
        }


    }
}
