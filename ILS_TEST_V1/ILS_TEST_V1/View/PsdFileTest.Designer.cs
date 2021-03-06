﻿namespace ILS_TEST_V1.View
{
    partial class PsdFileTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PsdFileTest));
            this.filePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.FileExtensionName = new System.Windows.Forms.TextBox();
            this.FileWidth = new System.Windows.Forms.TextBox();
            this.FileColorMode = new System.Windows.Forms.TextBox();
            this.FileHeight = new System.Windows.Forms.TextBox();
            this.FileChannelCount = new System.Windows.Forms.TextBox();
            this.FileDepth = new System.Windows.Forms.TextBox();
            this.FileChannelType = new System.Windows.Forms.TextBox();
            this.FilePixel = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ExcellPrint = new System.Windows.Forms.Button();
            this.gridValidCode = new System.Windows.Forms.DataGridView();
            this.gridErrorMsg = new System.Windows.Forms.DataGridView();
            this.btnVerify = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtn8 = new System.Windows.Forms.RadioButton();
            this.rbtn7 = new System.Windows.Forms.RadioButton();
            this.rbtn6 = new System.Windows.Forms.RadioButton();
            this.rbtn5 = new System.Windows.Forms.RadioButton();
            this.rbtn4 = new System.Windows.Forms.RadioButton();
            this.rbtn3 = new System.Windows.Forms.RadioButton();
            this.rbtn2 = new System.Windows.Forms.RadioButton();
            this.rbtn1 = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopySelectedLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValidCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorMsg)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePath.Location = new System.Drawing.Point(21, 20);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(1018, 21);
            this.filePath.TabIndex = 0;
            this.filePath.TextChanged += new System.EventHandler(this.filePath_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1045, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "선택";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.filePath);
            this.groupBox1.Location = new System.Drawing.Point(29, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1163, 60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PSD 파일 선택";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "파일명";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "파일확장명";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "ColorMode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(456, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(456, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "채널수";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(650, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Depth";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(647, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "채널타입";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(841, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "Pixel";
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(80, 26);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.ReadOnly = true;
            this.FileNameBox.Size = new System.Drawing.Size(111, 21);
            this.FileNameBox.TabIndex = 12;
            // 
            // FileExtensionName
            // 
            this.FileExtensionName.Location = new System.Drawing.Point(80, 54);
            this.FileExtensionName.Name = "FileExtensionName";
            this.FileExtensionName.ReadOnly = true;
            this.FileExtensionName.Size = new System.Drawing.Size(111, 21);
            this.FileExtensionName.TabIndex = 13;
            // 
            // FileWidth
            // 
            this.FileWidth.Location = new System.Drawing.Point(304, 27);
            this.FileWidth.Name = "FileWidth";
            this.FileWidth.ReadOnly = true;
            this.FileWidth.Size = new System.Drawing.Size(111, 21);
            this.FileWidth.TabIndex = 14;
            // 
            // FileColorMode
            // 
            this.FileColorMode.Location = new System.Drawing.Point(304, 54);
            this.FileColorMode.Name = "FileColorMode";
            this.FileColorMode.ReadOnly = true;
            this.FileColorMode.Size = new System.Drawing.Size(111, 21);
            this.FileColorMode.TabIndex = 15;
            // 
            // FileHeight
            // 
            this.FileHeight.Location = new System.Drawing.Point(502, 27);
            this.FileHeight.Name = "FileHeight";
            this.FileHeight.ReadOnly = true;
            this.FileHeight.Size = new System.Drawing.Size(111, 21);
            this.FileHeight.TabIndex = 16;
            // 
            // FileChannelCount
            // 
            this.FileChannelCount.Location = new System.Drawing.Point(502, 54);
            this.FileChannelCount.Name = "FileChannelCount";
            this.FileChannelCount.ReadOnly = true;
            this.FileChannelCount.Size = new System.Drawing.Size(111, 21);
            this.FileChannelCount.TabIndex = 17;
            // 
            // FileDepth
            // 
            this.FileDepth.Location = new System.Drawing.Point(706, 26);
            this.FileDepth.Name = "FileDepth";
            this.FileDepth.ReadOnly = true;
            this.FileDepth.Size = new System.Drawing.Size(111, 21);
            this.FileDepth.TabIndex = 18;
            // 
            // FileChannelType
            // 
            this.FileChannelType.Location = new System.Drawing.Point(706, 58);
            this.FileChannelType.Name = "FileChannelType";
            this.FileChannelType.ReadOnly = true;
            this.FileChannelType.Size = new System.Drawing.Size(284, 21);
            this.FileChannelType.TabIndex = 19;
            // 
            // FilePixel
            // 
            this.FilePixel.Location = new System.Drawing.Point(880, 27);
            this.FilePixel.Name = "FilePixel";
            this.FilePixel.ReadOnly = true;
            this.FilePixel.Size = new System.Drawing.Size(111, 21);
            this.FilePixel.TabIndex = 20;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1045, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 20);
            this.button2.TabIndex = 21;
            this.button2.Text = "PSD 읽기";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ExcellPrint
            // 
            this.ExcellPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcellPrint.Location = new System.Drawing.Point(1045, 57);
            this.ExcellPrint.Name = "ExcellPrint";
            this.ExcellPrint.Size = new System.Drawing.Size(94, 20);
            this.ExcellPrint.TabIndex = 22;
            this.ExcellPrint.Text = "리스트 출력";
            this.ExcellPrint.UseVisualStyleBackColor = true;
            this.ExcellPrint.Click += new System.EventHandler(this.ExcellPrint_Click);
            // 
            // gridValidCode
            // 
            this.gridValidCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridValidCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridValidCode.Location = new System.Drawing.Point(6, 20);
            this.gridValidCode.Name = "gridValidCode";
            this.gridValidCode.Size = new System.Drawing.Size(538, 191);
            this.gridValidCode.TabIndex = 25;
            // 
            // gridErrorMsg
            // 
            this.gridErrorMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridErrorMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridErrorMsg.Location = new System.Drawing.Point(12, 20);
            this.gridErrorMsg.Name = "gridErrorMsg";
            this.gridErrorMsg.RowTemplate.Height = 23;
            this.gridErrorMsg.Size = new System.Drawing.Size(570, 190);
            this.gridErrorMsg.TabIndex = 26;
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(1015, 725);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(153, 36);
            this.btnVerify.TabIndex = 27;
            this.btnVerify.Text = "검증 시작";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.gridValidCode);
            this.groupBox3.Location = new System.Drawing.Point(31, 476);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(554, 225);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "검증기준";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.gridErrorMsg);
            this.groupBox4.Location = new System.Drawing.Point(601, 476);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(590, 225);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "검증결과";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(848, 90);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(299, 261);
            this.propertyGrid1.TabIndex = 31;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 90);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(823, 261);
            this.dataGridView1.TabIndex = 32;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Controls.Add(this.ExcellPrint);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.FilePixel);
            this.groupBox2.Controls.Add(this.FileChannelType);
            this.groupBox2.Controls.Add(this.FileDepth);
            this.groupBox2.Controls.Add(this.FileChannelCount);
            this.groupBox2.Controls.Add(this.FileHeight);
            this.groupBox2.Controls.Add(this.FileColorMode);
            this.groupBox2.Controls.Add(this.FileWidth);
            this.groupBox2.Controls.Add(this.FileExtensionName);
            this.groupBox2.Controls.Add(this.FileNameBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(30, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1162, 387);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "파일정보";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.rbtn8);
            this.groupBox5.Controls.Add(this.rbtn7);
            this.groupBox5.Controls.Add(this.rbtn6);
            this.groupBox5.Controls.Add(this.rbtn5);
            this.groupBox5.Controls.Add(this.rbtn4);
            this.groupBox5.Controls.Add(this.rbtn3);
            this.groupBox5.Controls.Add(this.rbtn2);
            this.groupBox5.Controls.Add(this.rbtn1);
            this.groupBox5.Location = new System.Drawing.Point(33, 720);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(901, 40);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ILS_Type";
            // 
            // rbtn8
            // 
            this.rbtn8.AutoSize = true;
            this.rbtn8.Location = new System.Drawing.Point(771, 15);
            this.rbtn8.Name = "rbtn8";
            this.rbtn8.Size = new System.Drawing.Size(129, 16);
            this.rbtn8.TabIndex = 0;
            this.rbtn8.TabStop = true;
            this.rbtn8.Text = "휴게소요약맵(지니)";
            this.rbtn8.UseVisualStyleBackColor = true;
            // 
            // rbtn7
            // 
            this.rbtn7.AutoSize = true;
            this.rbtn7.Location = new System.Drawing.Point(636, 15);
            this.rbtn7.Name = "rbtn7";
            this.rbtn7.Size = new System.Drawing.Size(129, 16);
            this.rbtn7.TabIndex = 0;
            this.rbtn7.TabStop = true;
            this.rbtn7.Text = "휴게소요약맵(맵피)";
            this.rbtn7.UseVisualStyleBackColor = true;
            // 
            // rbtn6
            // 
            this.rbtn6.AutoSize = true;
            this.rbtn6.Location = new System.Drawing.Point(553, 15);
            this.rbtn6.Name = "rbtn6";
            this.rbtn6.Size = new System.Drawing.Size(77, 16);
            this.rbtn6.TabIndex = 0;
            this.rbtn6.TabStop = true;
            this.rbtn6.Text = "3D 교차로";
            this.rbtn6.UseVisualStyleBackColor = true;
            // 
            // rbtn5
            // 
            this.rbtn5.AutoSize = true;
            this.rbtn5.Location = new System.Drawing.Point(386, 15);
            this.rbtn5.Name = "rbtn5";
            this.rbtn5.Size = new System.Drawing.Size(161, 16);
            this.rbtn5.TabIndex = 0;
            this.rbtn5.TabStop = true;
            this.rbtn5.Text = "모식도(MimeticDiagram)";
            this.rbtn5.UseVisualStyleBackColor = true;
            // 
            // rbtn4
            // 
            this.rbtn4.AutoSize = true;
            this.rbtn4.Location = new System.Drawing.Point(302, 15);
            this.rbtn4.Name = "rbtn4";
            this.rbtn4.Size = new System.Drawing.Size(78, 16);
            this.rbtn4.TabIndex = 0;
            this.rbtn4.TabStop = true;
            this.rbtn4.Text = "ETC (ET)";
            this.rbtn4.UseVisualStyleBackColor = true;
            // 
            // rbtn3
            // 
            this.rbtn3.AutoSize = true;
            this.rbtn3.Location = new System.Drawing.Point(198, 15);
            this.rbtn3.Name = "rbtn3";
            this.rbtn3.Size = new System.Drawing.Size(98, 16);
            this.rbtn3.TabIndex = 0;
            this.rbtn3.TabStop = true;
            this.rbtn3.Text = "도시고속(CE)";
            this.rbtn3.UseVisualStyleBackColor = true;
            // 
            // rbtn2
            // 
            this.rbtn2.AutoSize = true;
            this.rbtn2.Location = new System.Drawing.Point(125, 15);
            this.rbtn2.Name = "rbtn2";
            this.rbtn2.Size = new System.Drawing.Size(67, 16);
            this.rbtn2.TabIndex = 0;
            this.rbtn2.TabStop = true;
            this.rbtn2.Text = "JC (JC)";
            this.rbtn2.UseVisualStyleBackColor = true;
            // 
            // rbtn1
            // 
            this.rbtn1.AutoSize = true;
            this.rbtn1.Location = new System.Drawing.Point(8, 15);
            this.rbtn1.Name = "rbtn1";
            this.rbtn1.Size = new System.Drawing.Size(111, 16);
            this.rbtn1.TabIndex = 0;
            this.rbtn1.TabStop = true;
            this.rbtn1.Text = "일반교차로(NC)";
            this.rbtn1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopySelectedLayer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 26);
            // 
            // tsmCopySelectedLayer
            // 
            this.tsmCopySelectedLayer.Name = "tsmCopySelectedLayer";
            this.tsmCopySelectedLayer.Size = new System.Drawing.Size(182, 22);
            this.tsmCopySelectedLayer.Text = "toolStripMenuItem1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PsdFileTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 777);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PsdFileTest";
            this.Text = "PSD 파일 검증";
            this.Load += new System.EventHandler(this.PsdFileTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridValidCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorMsg)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FileNameBox;
        private System.Windows.Forms.TextBox FileExtensionName;
        private System.Windows.Forms.TextBox FileWidth;
        private System.Windows.Forms.TextBox FileColorMode;
        private System.Windows.Forms.TextBox FileHeight;
        private System.Windows.Forms.TextBox FileChannelCount;
        private System.Windows.Forms.TextBox FileDepth;
        private System.Windows.Forms.TextBox FileChannelType;
        private System.Windows.Forms.TextBox FilePixel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ExcellPrint;
        private System.Windows.Forms.DataGridView gridValidCode;
        private System.Windows.Forms.DataGridView gridErrorMsg;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtn8;
        private System.Windows.Forms.RadioButton rbtn7;
        private System.Windows.Forms.RadioButton rbtn6;
        private System.Windows.Forms.RadioButton rbtn5;
        private System.Windows.Forms.RadioButton rbtn4;
        private System.Windows.Forms.RadioButton rbtn3;
        private System.Windows.Forms.RadioButton rbtn2;
        private System.Windows.Forms.RadioButton rbtn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmCopySelectedLayer;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}