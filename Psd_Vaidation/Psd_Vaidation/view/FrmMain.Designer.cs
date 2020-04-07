namespace Psd_Vaidation
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnOEM_AM = new System.Windows.Forms.RadioButton();
            this.rbtnOEM_G3_UpperLevel = new System.Windows.Forms.RadioButton();
            this.rbtnOEM_G3_ALL = new System.Windows.Forms.RadioButton();
            this.rbtnOEM = new System.Windows.Forms.RadioButton();
            this.gridExcelInfo = new System.Windows.Forms.DataGridView();
            this.gridLayerInfo = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridValidate = new System.Windows.Forms.DataGridView();
            this.gridResult = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcelInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayerInfo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValidate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(12, 19);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(503, 21);
            this.txtFolderPath.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(521, 17);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(100, 23);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.TabStop = false;
            this.btnSelectFolder.Text = "Psd 폴더선택";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 47);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1218, 555);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.gridExcelInfo);
            this.groupBox1.Controls.Add(this.gridLayerInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1218, 258);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PSD FILE LIST";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnOEM_AM);
            this.groupBox4.Controls.Add(this.rbtnOEM_G3_UpperLevel);
            this.groupBox4.Controls.Add(this.rbtnOEM_G3_ALL);
            this.groupBox4.Controls.Add(this.rbtnOEM);
            this.groupBox4.Location = new System.Drawing.Point(8, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 40);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Layer 구조 선택";
            // 
            // rbtnOEM_AM
            // 
            this.rbtnOEM_AM.AutoSize = true;
            this.rbtnOEM_AM.Location = new System.Drawing.Point(307, 16);
            this.rbtnOEM_AM.Name = "rbtnOEM_AM";
            this.rbtnOEM_AM.Size = new System.Drawing.Size(76, 16);
            this.rbtnOEM_AM.TabIndex = 3;
            this.rbtnOEM_AM.Text = "OEM_AM";
            this.rbtnOEM_AM.UseVisualStyleBackColor = true;
            // 
            // rbtnOEM_G3_UpperLevel
            // 
            this.rbtnOEM_G3_UpperLevel.AutoSize = true;
            this.rbtnOEM_G3_UpperLevel.Location = new System.Drawing.Point(165, 17);
            this.rbtnOEM_G3_UpperLevel.Name = "rbtnOEM_G3_UpperLevel";
            this.rbtnOEM_G3_UpperLevel.Size = new System.Drawing.Size(141, 16);
            this.rbtnOEM_G3_UpperLevel.TabIndex = 2;
            this.rbtnOEM_G3_UpperLevel.Text = "OEM_G3_UpperLevel";
            this.rbtnOEM_G3_UpperLevel.UseVisualStyleBackColor = true;
            // 
            // rbtnOEM_G3_ALL
            // 
            this.rbtnOEM_G3_ALL.AutoSize = true;
            this.rbtnOEM_G3_ALL.Location = new System.Drawing.Point(59, 17);
            this.rbtnOEM_G3_ALL.Name = "rbtnOEM_G3_ALL";
            this.rbtnOEM_G3_ALL.Size = new System.Drawing.Size(100, 16);
            this.rbtnOEM_G3_ALL.TabIndex = 1;
            this.rbtnOEM_G3_ALL.Text = "OEM_G3_ALL";
            this.rbtnOEM_G3_ALL.UseVisualStyleBackColor = true;
            // 
            // rbtnOEM
            // 
            this.rbtnOEM.AutoSize = true;
            this.rbtnOEM.Checked = true;
            this.rbtnOEM.Location = new System.Drawing.Point(3, 17);
            this.rbtnOEM.Name = "rbtnOEM";
            this.rbtnOEM.Size = new System.Drawing.Size(51, 16);
            this.rbtnOEM.TabIndex = 0;
            this.rbtnOEM.TabStop = true;
            this.rbtnOEM.Text = "OEM";
            this.rbtnOEM.UseVisualStyleBackColor = true;
            // 
            // gridExcelInfo
            // 
            this.gridExcelInfo.AllowUserToAddRows = false;
            this.gridExcelInfo.AllowUserToDeleteRows = false;
            this.gridExcelInfo.AllowUserToResizeColumns = false;
            this.gridExcelInfo.AllowUserToResizeRows = false;
            this.gridExcelInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridExcelInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExcelInfo.Location = new System.Drawing.Point(629, 62);
            this.gridExcelInfo.Name = "gridExcelInfo";
            this.gridExcelInfo.ReadOnly = true;
            this.gridExcelInfo.RowTemplate.Height = 23;
            this.gridExcelInfo.Size = new System.Drawing.Size(583, 186);
            this.gridExcelInfo.TabIndex = 4;
            this.gridExcelInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridExcelInfo_CellFormatting);
            // 
            // gridLayerInfo
            // 
            this.gridLayerInfo.AllowUserToAddRows = false;
            this.gridLayerInfo.AllowUserToDeleteRows = false;
            this.gridLayerInfo.AllowUserToResizeColumns = false;
            this.gridLayerInfo.AllowUserToResizeRows = false;
            this.gridLayerInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLayerInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridLayerInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLayerInfo.Location = new System.Drawing.Point(5, 62);
            this.gridLayerInfo.Name = "gridLayerInfo";
            this.gridLayerInfo.ReadOnly = true;
            this.gridLayerInfo.RowTemplate.Height = 23;
            this.gridLayerInfo.Size = new System.Drawing.Size(606, 186);
            this.gridLayerInfo.TabIndex = 2;
            this.gridLayerInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridLayerInfo_CellFormatting);
            this.gridLayerInfo.SelectionChanged += new System.EventHandler(this.gridLayerInfo_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(0, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1218, 282);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "검증결과";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.34568F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1212, 262);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridValidate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridResult);
            this.splitContainer1.Size = new System.Drawing.Size(1206, 256);
            this.splitContainer1.SplitterDistance = 651;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 5;
            // 
            // gridValidate
            // 
            this.gridValidate.AllowUserToAddRows = false;
            this.gridValidate.AllowUserToDeleteRows = false;
            this.gridValidate.AllowUserToResizeColumns = false;
            this.gridValidate.AllowUserToResizeRows = false;
            this.gridValidate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridValidate.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridValidate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridValidate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridValidate.Location = new System.Drawing.Point(0, 0);
            this.gridValidate.Name = "gridValidate";
            this.gridValidate.ReadOnly = true;
            this.gridValidate.RowTemplate.Height = 23;
            this.gridValidate.Size = new System.Drawing.Size(651, 256);
            this.gridValidate.TabIndex = 4;
            this.gridValidate.SelectionChanged += new System.EventHandler(this.gridValidate_SelectionChanged);
            // 
            // gridResult
            // 
            this.gridResult.AllowUserToAddRows = false;
            this.gridResult.AllowUserToDeleteRows = false;
            this.gridResult.AllowUserToResizeColumns = false;
            this.gridResult.AllowUserToResizeRows = false;
            this.gridResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResult.Location = new System.Drawing.Point(0, 0);
            this.gridResult.Name = "gridResult";
            this.gridResult.ReadOnly = true;
            this.gridResult.RowTemplate.Height = 23;
            this.gridResult.Size = new System.Drawing.Size(545, 256);
            this.gridResult.TabIndex = 5;
            this.gridResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridResult_CellFormatting);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(1114, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStart_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(1004, 18);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 24);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Text = "Excel 파일선택";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtExcel
            // 
            this.txtExcel.Location = new System.Drawing.Point(639, 19);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.ReadOnly = true;
            this.txtExcel.Size = new System.Drawing.Size(355, 21);
            this.txtExcel.TabIndex = 8;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1242, 614);
            this.Controls.Add(this.txtExcel);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolderPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(0, 200);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcelInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayerInfo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridValidate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridLayerInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TextBox txtExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView gridExcelInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnOEM_AM;
        private System.Windows.Forms.RadioButton rbtnOEM_G3_UpperLevel;
        private System.Windows.Forms.RadioButton rbtnOEM_G3_ALL;
        private System.Windows.Forms.RadioButton rbtnOEM;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridValidate;
        private System.Windows.Forms.DataGridView gridResult;
    }
}

