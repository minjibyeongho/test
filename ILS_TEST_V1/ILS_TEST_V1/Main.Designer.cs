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
            this.File_Test = new System.Windows.Forms.Button();
            this.ArrowCode_Test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // File_Test
            // 
            this.File_Test.Location = new System.Drawing.Point(218, 58);
            this.File_Test.Name = "File_Test";
            this.File_Test.Size = new System.Drawing.Size(314, 112);
            this.File_Test.TabIndex = 0;
            this.File_Test.Text = "파일 검증";
            this.File_Test.UseVisualStyleBackColor = true;
            this.File_Test.Click += new System.EventHandler(this.File_Test_Click);
            // 
            // ArrowCode_Test
            // 
            this.ArrowCode_Test.Location = new System.Drawing.Point(218, 239);
            this.ArrowCode_Test.Name = "ArrowCode_Test";
            this.ArrowCode_Test.Size = new System.Drawing.Size(316, 112);
            this.ArrowCode_Test.TabIndex = 1;
            this.ArrowCode_Test.Text = "ArrowCode 검증";
            this.ArrowCode_Test.UseVisualStyleBackColor = true;
            this.ArrowCode_Test.Click += new System.EventHandler(this.ArrowCode_Test_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ArrowCode_Test);
            this.Controls.Add(this.File_Test);
            this.Name = "Main";
            this.Text = "ILS_TOOL_V1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button File_Test;
        private System.Windows.Forms.Button ArrowCode_Test;
    }
}

