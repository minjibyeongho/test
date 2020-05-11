namespace ILS_TEST_V1
{
    partial class FileTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTest));
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.gridVerify = new System.Windows.Forms.DataGridView();
            this.btnListPrint = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridVerify)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(12, 13);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(762, 21);
            this.txtFolderPath.TabIndex = 0;
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderSelect.Location = new System.Drawing.Point(780, 11);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(103, 25);
            this.btnFolderSelect.TabIndex = 1;
            this.btnFolderSelect.Text = "폴더선택";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // gridVerify
            // 
            this.gridVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridVerify.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridVerify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVerify.Location = new System.Drawing.Point(12, 47);
            this.gridVerify.Name = "gridVerify";
            this.gridVerify.ReadOnly = true;
            this.gridVerify.RowTemplate.Height = 23;
            this.gridVerify.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridVerify.Size = new System.Drawing.Size(871, 427);
            this.gridVerify.TabIndex = 2;
            this.gridVerify.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridVerify_CellMouseDoubleClick);
            // 
            // btnListPrint
            // 
            this.btnListPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnListPrint.Location = new System.Drawing.Point(13, 491);
            this.btnListPrint.Name = "btnListPrint";
            this.btnListPrint.Size = new System.Drawing.Size(123, 67);
            this.btnListPrint.TabIndex = 3;
            this.btnListPrint.Text = "리스트 출력";
            this.btnListPrint.UseVisualStyleBackColor = true;
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(728, 491);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(155, 63);
            this.btnVerify.TabIndex = 4;
            this.btnVerify.Text = "검증";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // FileTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 572);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnListPrint);
            this.Controls.Add(this.gridVerify);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.txtFolderPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileTest";
            this.Text = "파일 검증";
            ((System.ComponentModel.ISupportInitialize)(this.gridVerify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.DataGridView gridVerify;
        private System.Windows.Forms.Button btnListPrint;
        private System.Windows.Forms.Button btnVerify;
    }
}