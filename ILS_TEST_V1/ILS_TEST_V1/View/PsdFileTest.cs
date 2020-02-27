using System;
using System.IO; // 2020.02.27 파일 이름 가져오는 거 (박찬규)
using Ntreev.Library.Psd;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILS_TEST_V1.View
{
    public partial class PsdFileTest : Form
    {

        private string filepath;

        // 파라미터 없는 생성자( 2020.02.24 민병호 )
        public PsdFileTest()
        {
            InitializeComponent();
        }

        // 파라미터 있는 생성자 생성 ( 2020.02.24 민병호 )
        public PsdFileTest(string filepath)
        {
            InitializeComponent();

            filePath.Text = filepath;

        }
        
        // FileTest에서 더블클릭시 파일 경로 읽어오면 Text변경 이벤트 발생 -> PSD파일 읽을 수 있도록 구현( 2020.02.24 민병호 ) 
        private void filePath_TextChanged(object sender, EventArgs e)
        {
            
            Console.WriteLine(filePath.Text);

            // 2020.02.24 민병호
            // 읽어오는 psd libaray 구현
            // 각자 구현해보고( 공통 util로 만드는 작업 )
            // PSD Library 활용

            
            var doc = PsdDocument.Create(filePath.Text);

            //2020.02.27  파일 정보 텍스트 박스 값 입력
            FileNameBox.Text = Path.GetFileNameWithoutExtension(filePath.Text);  //파일명 텍스트박스  2020.02.27 파일의 확장자를 제외한 파일명을 가져온다. (박찬규)            
            FileExtensionName.Text = Path.GetExtension(filePath.Text);  //파일 확장명 텍스트 박스 2020.02.27 파일의 확장자만 가져온다. (박찬규)
            FileHeight.Text = doc.Height.ToString(); 
            FileWidth.Text = doc.Width.ToString();
            FileDepth.Text = doc.Depth.ToString();
            FileColorMode.Text = doc.FileHeaderSection.ColorMode.ToString();
            FileDepth.Text = doc.FileHeaderSection.Depth.ToString();
            FileChannelCount.Text = doc.FileHeaderSection.NumberOfChannels.ToString();
         

        }
    }
}
