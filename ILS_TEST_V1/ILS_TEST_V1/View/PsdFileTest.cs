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
        BindingList<IPsdLayer> layerAdd = new BindingList<IPsdLayer>(); //총 레이어를 담기위한 BindingList (2020.03.17 최정웅)
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

        //child 레이어 list add, 재귀함수 ( 2020.03.17 최정웅)
        private void GetLayer(IPsdLayer layer, int layerDepth, int layerSeq)
        {
            Console.WriteLine(layer);
            Console.WriteLine("IDX:{0}, Depth:{1}, Seq:{2}",layerAdd.Count, layerDepth, layerSeq);
            Console.WriteLine();

            layerAdd.Add(layer);

            var childSeq = 1;
            foreach (var y in layer.Childs.Reverse()) //IPsdLayer는 첫번째 child가 최상위 폴더가 아닌 그하위 폴더 즉 ETC1 이런거가 나옴 그래서 최상위 폴더가 안나오는 것임.
            {
                //layerAdd.Add(y);
                GetLayer(y, layerDepth + 1, childSeq++);
            }
        }

        // layer 카운트, parent layer카운트 및 레이어별 속성 list add 수정중임 (2020.03.17 최정웅)
        public void ReadFile(string filename)
        {
            var doc = PsdDocument.Create(filePath.Text);

            var layerSeq = 1;
            foreach (var x in doc.Childs.Reverse())
            {
                GetLayer(x, 1, layerSeq++);
            }
        }


        // FileTest에서 더블클릭시 파일 경로 읽어오면 Text변경 이벤트 발생 -> PSD파일 읽을 수 있도록 구현( 2020.02.24 민병호 ) 
        private void filePath_TextChanged(object sender, EventArgs e)
        {
            var doc = PsdDocument.Create(filePath.Text);
            //Console.WriteLine(filePath.Text);

            // 2020.02.24 민병호
            // 읽어오는 psd libaray 구현
            // 각자 구현해보고( 공통 util로 만드는 작업 )
            // PSD Library 활용

            //2020.02.27 파일 정보 텍스트 박스 입력 메서드 생성 (박찬규)
            FileInformaion(filePath.Text);

            ReadFile(filePath.Text);
        }

        //2020.02.27 파일 정보 텍스트 박스 입력 메서드 생성 (박찬규)
        private void FileInformaion(string x)
        {
            var doc = PsdDocument.Create(x);
            var imageSource = doc as Ntreev.Library.Psd.IImageSource;

            String TypeTemp = null;
            String TypeTemp1 = null;

            //2020.02.27  파일 정보 텍스트 박스 값 입력
            FileNameBox.Text = Path.GetFileNameWithoutExtension(x);  //파일명 텍스트박스  2020.02.27 파일의 확장자를 제외한 파일명을 가져온다. (박찬규)            
            FileExtensionName.Text = Path.GetExtension(x);  //파일 확장명 텍스트 박스 2020.02.27 파일의 확장자만 가져온다. (박찬규)
            FileHeight.Text = doc.Height.ToString();
            FileWidth.Text = doc.Width.ToString();
            FileDepth.Text = doc.Depth.ToString();
            FileColorMode.Text = doc.FileHeaderSection.ColorMode.ToString();
            FileDepth.Text = doc.FileHeaderSection.Depth.ToString();
            FileChannelCount.Text = doc.FileHeaderSection.NumberOfChannels.ToString();


            //2020.02.27 채널 개수에 따른 채널 타입 추출 (박찬규)
            for (int i = 0; i < doc.FileHeaderSection.NumberOfChannels; i++)
            {
                if (TypeTemp != null)
                {
                    TypeTemp += "/";
                }
                TypeTemp1 = imageSource.Channels[i].Type.ToString();
                TypeTemp += TypeTemp1;
            }
            FileChannelType.Text = TypeTemp;
        }

    }
}
