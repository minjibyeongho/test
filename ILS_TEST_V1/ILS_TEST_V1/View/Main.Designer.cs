namespace ILS_TEST_V1
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.File_Test = new System.Windows.Forms.Button();
            this.ArrowCode_Test = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사용방법ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // File_Test
            // 
            this.File_Test.Font = new System.Drawing.Font("한컴 윤체 B", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.File_Test.Location = new System.Drawing.Point(145, 225);
            this.File_Test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.File_Test.Name = "File_Test";
            this.File_Test.Size = new System.Drawing.Size(120, 40);
            this.File_Test.TabIndex = 0;
            this.File_Test.Text = "파일 검증";
            this.File_Test.UseVisualStyleBackColor = true;
            this.File_Test.Click += new System.EventHandler(this.File_Test_Click);
            // 
            // ArrowCode_Test
            // 
            this.ArrowCode_Test.Font = new System.Drawing.Font("한컴 윤체 B", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ArrowCode_Test.Location = new System.Drawing.Point(145, 282);
            this.ArrowCode_Test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ArrowCode_Test.Name = "ArrowCode_Test";
            this.ArrowCode_Test.Size = new System.Drawing.Size(120, 40);
            this.ArrowCode_Test.TabIndex = 1;
            this.ArrowCode_Test.Text = "ArrowCode 검증";
            this.ArrowCode_Test.UseVisualStyleBackColor = true;
            this.ArrowCode_Test.Click += new System.EventHandler(this.ArrowCode_Test_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.정보ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 정보ToolStripMenuItem
            // 
            this.정보ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사용방법ToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.정보ToolStripMenuItem.Text = "정보";
            // 
            // 사용방법ToolStripMenuItem
            // 
            this.사용방법ToolStripMenuItem.Name = "사용방법ToolStripMenuItem";
            this.사용방법ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.사용방법ToolStripMenuItem.Text = "사용방법";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.ArrowCode_Test);
            this.Controls.Add(this.File_Test);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("한컴 윤체 B", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "ILS_TOOL_V1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button File_Test;
        private System.Windows.Forms.Button ArrowCode_Test;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사용방법ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

