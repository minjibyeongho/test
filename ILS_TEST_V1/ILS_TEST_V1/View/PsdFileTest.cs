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
using ILS_TEST_V1.Model;
using Ntreev.Library.Psd.UserModel;

namespace ILS_TEST_V1.View
{
    public partial class PsdFileTest : Form
    {
        BindingList<IPsdLayer> layerAdd = new BindingList<IPsdLayer>(); //총 레이어를 담기위한 BindingList (2020.03.17 최정웅)
        BindingList<layerModel> gridlist = new BindingList<layerModel>();   // 레이어의 정보를 담은 BindingList ( 2020.03.17 민병호 )
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

        // dataGridView1 데이터그리드뷰 이름

        //child 레이어 list add, 재귀함수 ( 2020.03.17 최정웅)
        private void GetLayer(IPsdLayer layer, int layerDepth, int layerSeq)
        {
            Console.WriteLine(layer);
            Console.WriteLine("IDX:{0}, Depth:{1}, Seq:{2}",layerAdd.Count, layerDepth, layerSeq);
            Console.WriteLine();

            // 새로운 VM 생성
            layerModel tmpVm = new layerModel();

            // 각 레이어의 속성 정보를 넣어줌
            tmpVm.Name = layer.Name;
            tmpVm.BlendMode = layer.BlendMode;

            //int
            tmpVm.Depth = layer.Depth;
            tmpVm.Top = layer.Top;
            tmpVm.Bottom = layer.Bottom;
            tmpVm.Left = layer.Left;
            tmpVm.Right = layer.Right;
            tmpVm.Width = layer.Width;
            tmpVm.Height = layer.Height;

            // bool
            tmpVm.HasImage = layer.HasImage;
            tmpVm.HasMask = layer.HasMask;  //bool
            tmpVm.IsClippinig = layer.IsClipping;
            
            
            // floatasd
            tmpVm.Opacity = layer.Opacity;  //불투명도
            var descriptionList = layer.GetDescription();
            
            /*
            layer.BlendMode;    //BlendMode
            layer.Channels.; // [], iChannel
            layer.LinkedLayer;  //????
            layer.Parent;
            layer.Resources;
             */

            gridlist.Add(tmpVm);
            // bindingList에 담는다.
            layerAdd.Add(layer);

            var childSeq = 1;
            foreach (var y in layer.Childs.Reverse()) //IPsdLayer는 첫번째 child가 최상위 폴더가 아닌 그하위 폴더 즉 ETC1 이런거가 나옴 그래서 최상위 폴더가 안나오는 것임.
            {
                //layerAdd.Add(y);
                GetLayer(y, layerDepth + 1, childSeq++);
            }
        }

        private string GetDictionaryValue(Dictionary<DescriptionMode, object> descList, DescriptionMode mode)
        {
            return (descList.ContainsKey(mode) == false) ? string.Empty : string.Format("{0}", descList[mode]);
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

            // layer 정보를 읽어온다
            ReadFile(filePath.Text);

            // bindingList가 모두 add된 상태
            dataGridView1.DataSource = gridlist;

        }

        //2020.02.27 파일 정보 텍스트 박스 입력 메서드 생성 (박찬규)
        private void FileInformaion(string filePath)
        {
            var doc = PsdDocument.Create(filePath);
            var imageSource = doc.ImageResources;
            var properties = imageSource["Resolution"] as IEnumerable<KeyValuePair<string, object>>;
            // 속성을 뽑아오는 것까지는 성공! ( IEnumerable<KeyValuePair<string, object>> 되어 있는 데이터를 어떻게 뽑아서 가져올지가 필요 )
            //var tlist = properties.ToList();
             
            // 2020/03/18 pixel 정보 뽑아 오는 것 테스트 중( 민병호 )
            // 현재 상황 : properties에 pixel정보 들어가 있음
            foreach (var item in properties )
            {
                Console.WriteLine("#######속성뽑기");
                var key = item.Key;
                Console.WriteLine("{0}", item.Value);
            }    
            /*
            String TypeTemp = null;
            String TypeTemp1 = null;
            */

            //2020.02.27  파일 정보 텍스트 박스 값 입력
            FileNameBox.Text = Path.GetFileNameWithoutExtension(filePath);  //파일명 텍스트박스  2020.02.27 파일의 확장자를 제외한 파일명을 가져온다. (박찬규)            
            FileExtensionName.Text = Path.GetExtension(filePath);  //파일 확장명 텍스트 박스 2020.02.27 파일의 확장자만 가져온다. (박찬규)
            FileHeight.Text = doc.Height.ToString();
            FileWidth.Text = doc.Width.ToString();
            FileDepth.Text = doc.Depth.ToString();
            FileColorMode.Text = doc.FileHeaderSection.ColorMode.ToString();
            FileDepth.Text = doc.FileHeaderSection.Depth.ToString();
            FileChannelCount.Text = doc.FileHeaderSection.NumberOfChannels.ToString();


            var imageSource11 = doc as IImageSource;
            var iproperties = imageSource11 as IProperties;
            // var imageResource11 = doc as 
            // iproperties
            List<string> tmp = new List<string>();
            foreach (var channel in imageSource11.Channels)
            {
                tmp.Add(channel.Type.ToString());
            }
            string tmpChannelType = string.Join( " / ", tmp );
            FileChannelType.Text = tmpChannelType;

            /*
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
            */
        }

    }
}
