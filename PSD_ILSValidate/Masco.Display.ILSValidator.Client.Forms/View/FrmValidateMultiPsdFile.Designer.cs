namespace Masco.Display.ILSValidator.Client.Forms
{
    partial class FrmValidateMultiPsdFile
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnExportLayerName = new System.Windows.Forms.Button();
            this.chboxFilterByArrow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(821, 21);
            this.txtPath.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(839, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(93, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "선택";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(12, 51);
            this.grid.Name = "grid";
            this.grid.RowTemplate.Height = 23;
            this.grid.Size = new System.Drawing.Size(920, 386);
            this.grid.TabIndex = 2;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.Location = new System.Drawing.Point(726, 443);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(206, 54);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "검증";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnExportLayerName
            // 
            this.btnExportLayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportLayerName.Location = new System.Drawing.Point(12, 443);
            this.btnExportLayerName.Name = "btnExportLayerName";
            this.btnExportLayerName.Size = new System.Drawing.Size(127, 54);
            this.btnExportLayerName.TabIndex = 4;
            this.btnExportLayerName.Text = "리스트 출력";
            this.btnExportLayerName.UseVisualStyleBackColor = true;
            this.btnExportLayerName.Click += new System.EventHandler(this.btnExportLayerName_Click);
            // 
            // chboxFilterByArrow
            // 
            this.chboxFilterByArrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chboxFilterByArrow.AutoSize = true;
            this.chboxFilterByArrow.Location = new System.Drawing.Point(145, 481);
            this.chboxFilterByArrow.Name = "chboxFilterByArrow";
            this.chboxFilterByArrow.Size = new System.Drawing.Size(107, 16);
            this.chboxFilterByArrow.TabIndex = 9;
            this.chboxFilterByArrow.Text = "Filter By Arrow";
            this.chboxFilterByArrow.UseVisualStyleBackColor = true;
            // 
            // FrmValidateMultiPsdFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 509);
            this.Controls.Add(this.chboxFilterByArrow);
            this.Controls.Add(this.btnExportLayerName);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtPath);
            this.Name = "FrmValidateMultiPsdFile";
            this.Text = "FrmValidateMultiPsdFile";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnExportLayerName;
        private System.Windows.Forms.CheckBox chboxFilterByArrow;
    }
}