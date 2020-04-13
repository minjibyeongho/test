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
    public partial class FrmImage : Form
    {
        public FrmImage()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
        //     // 요약:
        ////     이미지는 System.Windows.Forms.PictureBox의 왼쪽 위 모퉁이에 놓입니다.이미지가 포함된 System.Windows.Forms.PictureBox보다
        ////     크면 클리핑됩니다.
        //Normal = 0,
        ////
        //// 요약:
        ////     System.Windows.Forms.PictureBox 내의 이미지가 System.Windows.Forms.PictureBox의
        ////     크기에 맞게 늘어나거나 줄어듭니다.
        //StretchImage = 1,
        ////
        //// 요약:
        ////     System.Windows.Forms.PictureBox의 크기는 포함되는 이미지의 크기와 동일하게 조정됩니다.
        //AutoSize = 2,
        ////
        //// 요약:
        ////     System.Windows.Forms.PictureBox가 이미지보다 크면 이미지는 가운데에 표시됩니다.이미지가 System.Windows.Forms.PictureBox보다
        ////     크면 그림은 System.Windows.Forms.PictureBox의 가운데에 표시되고 외부 가장자리가 클리핑됩니다.
        //CenterImage = 3,
        ////
        //// 요약:
        ////     크기 비율이 유지되도록 이미지 크기가 확대 또는 축소됩니다.
        //Zoom = 4,

            var list = new Dictionary<PictureBoxSizeMode, RadioButton>();
            list.Add(PictureBoxSizeMode.Normal, radioButton1);
            list.Add(PictureBoxSizeMode.StretchImage, radioButton2);
            list.Add(PictureBoxSizeMode.AutoSize, radioButton3);
            list.Add(PictureBoxSizeMode.CenterImage, radioButton4);
            list.Add(PictureBoxSizeMode.Zoom, radioButton5);

            foreach (var x in list)
            {
                x.Value.Tag = x.Key;
                x.Value.Text = x.Key.ToString();
                x.Value.CheckedChanged += radioButton_CheckedChanged;
            }
        }

        void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = (RadioButton)sender;
            var mode = (PictureBoxSizeMode)rdo.Tag;
            pictureBox.SizeMode = mode;
        }

        public void Setup(Image image, int pk)
        {
            txtPk.Text = pk.ToString();
            pictureBox.Image = image;
        }
    }
}
