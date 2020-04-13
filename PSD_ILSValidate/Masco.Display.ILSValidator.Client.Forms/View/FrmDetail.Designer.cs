namespace Masco.Display.ILSValidator.Client.Forms
{
    partial class FrmSheetList
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
            this.gridSheetList = new System.Windows.Forms.DataGridView();
            this.btnSelectSheet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSheetList)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSheetList
            // 
            this.gridSheetList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSheetList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSheetList.Location = new System.Drawing.Point(12, 12);
            this.gridSheetList.Name = "gridSheetList";
            this.gridSheetList.RowTemplate.Height = 23;
            this.gridSheetList.Size = new System.Drawing.Size(224, 248);
            this.gridSheetList.TabIndex = 0;
            this.gridSheetList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSheetList_CellContentClick);
            // 
            // btnSelectSheet
            // 
            this.btnSelectSheet.Location = new System.Drawing.Point(12, 266);
            this.btnSelectSheet.Name = "btnSelectSheet";
            this.btnSelectSheet.Size = new System.Drawing.Size(224, 43);
            this.btnSelectSheet.TabIndex = 1;
            this.btnSelectSheet.Text = "선택";
            this.btnSelectSheet.UseVisualStyleBackColor = true;
            this.btnSelectSheet.Click += new System.EventHandler(this.btnSelectSheet_Click);
            // 
            // FrmSheetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 321);
            this.Controls.Add(this.btnSelectSheet);
            this.Controls.Add(this.gridSheetList);
            this.Name = "FrmSheetList";
            this.Text = "FrmSheetList";
            ((System.ComponentModel.ISupportInitialize)(this.gridSheetList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSheetList;
        private System.Windows.Forms.Button btnSelectSheet;
    }
}