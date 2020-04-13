using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public class PsdReader
    {
        IList<PsdLayerVM> _vmList = new List<PsdLayerVM>();
        // ILIST(interface) - List ( upcasting 목적 )
        private int _index = 0;
        // _는 C계열에서 private 멤버변수를 구분하기 위해 쓴다고 함? or 예약어, 시동어 등이 걸려 있을 지 모르니 피하기 위해서

        public int FileSectionWidth { get; set; }
        public int FileSectionHeight { get; set; }
        public int FileSectionDepth { get; set; }
        public int NumberOfChannels { get; set; }
        public string FileSectionColorMode { get; set; }
        public string FileChannelTypes { get; set; }
        public string FileSectionPixel { get; set; }


        public IList<PsdLayerVM> GetList()
        {
            return _vmList;
        }

        // 1. Bit 확인
        // 2. 그룹명/레이어명 공란 없음
        // 3. 레이어구조
        // 4. RGB(Road Background_Color, Transparency_Color)
        // 5. Arrow 그룹내위치(Road Background_Color, Transparency_Color)
        // 6. 채널
        // 7. 패스
        // 8. size(2048x2048 or 800x600)
        // 9. 해상도 72pixel
        // 10. 불투명도 Direction ->100%
        // 11. 불투명도 Transparency ->50%
        // 12. 이미지 크기, 위치(Road Background_Color, Transparency_Color)
        private void GetLayer(IPsdLayer layer, int layerDepth, int layerSeq)
        {
            // Layer 정보를 얻어오는 Method

            var vm = new PsdLayerVM();
            // dto 객체를 만들고
            // dto 객체에 set 메소드를 통해 한개의 객체를 완성한다
            vm.Index = ++_index;
            vm.LayerDepth = layerDepth;
            vm.LayerSeq = layerSeq;

            vm.Name = layer.Name;
            vm.BlendMode = layer.BlendMode;
            vm.Bottom = layer.Bottom;

            //vm.Channels = layer.Channels;
            vm.ChannelCount = layer.Channels.Count();
            var sb = new List<string>();
            var sb2 = new List<string>();
            var sb3 = new List<string>();
            foreach (var x in layer.Channels)
            {
                sb.Add(string.Format("{0}", x.Type));
                sb2.Add(string.Format("{0}", x.Data.Length));
                sb3.Add(string.Format("{0}", x.Data.Length > 0 ? x.Data[0].ToString() : "-"));
                // 삼항연산자 이용 조건 ? 참 : 거짓
            }

            // string.join( string(seperator), 배열 또는 컬렉션 멤버 ) : 배열 또는 컬렉션 멤버의 요소들을 seperator를 사용하여 연결
            vm.ChannelTypes = string.Join("/", sb);
            vm.ChannelSize = string.Join("/", sb2);
            vm.ChannelARGB = string.Join("/", sb3);

            vm.ChildCount = layer.Childs.Count();
            vm.Depth = layer.Depth;

            vm.HasImage = layer.HasImage;
            vm.HasMask = layer.HasMask;
            vm.Height = layer.Height;
            vm.IsClippinig = layer.IsClipping;

            vm.Left = layer.Left;

            vm.Opacity = layer.Opacity;


            vm.Right = layer.Right;
            vm.Top = layer.Top;
            vm.Width = layer.Width;

            //layer.LinkedLayer
            var descList = layer.GetDescription();

            vm.IsVisible = GetDictionaryValue(descList, DescriptionMode.Records_Flags_IsVisible) != "True";
            vm.IsLock = GetDictionaryValue(descList, DescriptionMode.Records_Flags_IsTransparency) == "True"; 
            var flagNumber = GetDictionaryValue(descList, DescriptionMode.Records_Flags_Number);
            vm.RecordsFlags = ToBin(int.Parse(flagNumber), 8);
            vm.SectionType = GetDictionaryValue(descList, DescriptionMode.Records_SectionType);
            vm.HeightUnit = GetDictionaryValue(descList, DescriptionMode.Docuemnt_HeightUnit);
            vm.HorizontalRes = GetDictionaryValue(descList, DescriptionMode.Docuemnt_HorizontalRes);
            vm.HorizontalResUnit = GetDictionaryValue(descList, DescriptionMode.Docuemnt_HorizontalResUnit);
            vm.VerticalRes = GetDictionaryValue(descList, DescriptionMode.Docuemnt_VerticalRes);
            vm.VerticalResUnit = GetDictionaryValue(descList, DescriptionMode.Docuemnt_VerticalResUnit);
            vm.WidthUnit = GetDictionaryValue(descList, DescriptionMode.Docuemnt_WidthUnit);


            //vm.Parent = layer.Parent;
            //vm.Document = layer.Document;
            //vm.LinkedLayer = layer.LinkedLayer;
            //vm.Resources = layer.Resources;

            Console.WriteLine("######vm#######");
            Console.WriteLine(vm);
            Console.WriteLine("###############");

            _vmList.Add(vm);

            var childLayerSeq = 1;
            foreach (var x in layer.Childs.Reverse())
            {
                GetLayer(x, layerDepth + 1, childLayerSeq++);   // 재귀함수 활용
            }
            //_vmList.Insert(vm);

        }
        public static string ToBin(int value, int len)
        {
            return (len > 1 ? ToBin(value >> 1, len - 1) : null) + "01"[value & 1];
        }
        private string GetDictionaryValue(Dictionary<DescriptionMode, object> descList, DescriptionMode mode)
        {
            return (descList.ContainsKey(mode) == false) ? string.Empty : string.Format("{0}", descList[mode]);
        }

        private string GetChannelData(IChannel[] channels, int chIndex)
        {
            if (channels.Length <= chIndex)
                return "-:-";

            var ch = channels[chIndex];
            var result = string.Format("{0}:{1}", ch.Type, ch.Data.Length > 0 ? ch.Data[0].ToString() : "-");
            return result;
        }

        
      

        public void ReadFile(string filename)
        {
            _vmList.Clear();
            //filename = @"E:\KRJM11DF005A8008E00203.psd";\
            var document = PsdDocument.Create(filename);
            if (document.Depth != 8)//검증 1
            {
                // document.Depth가 의미하는 것은..?
                //MessageBox.Show("{0} psd 파일의 비트수가 틀립니다.", Path.GetFileName(filename));
                return;
            }

            //this.ColorModeData = document.ColorModeData
            this.FileSectionColorMode = document.FileHeaderSection.ColorMode.ToString();

            this.FileSectionDepth = document.FileHeaderSection.Depth;
            this.FileSectionHeight = document.FileHeaderSection.Height;
            this.FileSectionWidth = document.FileHeaderSection.Width;
            this.NumberOfChannels = document.FileHeaderSection.NumberOfChannels;

            //var descList =((IPsdLayer)document).GetDescription();
            //this.ChannelTypes = GetDictionaryValue(descList, DescriptionMode.Document_ImageSource_ChannelTypes);

            var imageSource = document as Ntreev.Library.Psd.IImageSource;
            // as 연산자는 객체 casting시 사용하는 연산자로 class type이 맞는지 확인하는 용도라고함( 에러가 발생하진 않지만... 왜쓰는거지? )
            if (imageSource != null)
            {
                var colorList = new List<string>();
                foreach (var x in imageSource.Channels)
                {
                    colorList.Add(x.Type.ToString());
                }
                var colors = string.Join("/", colorList.ToArray());
                this.FileChannelTypes = colors;
            }
           
            var sb = new StringBuilder();

            var layerSeq = 1;
            foreach (var layer in document.Childs.Reverse())
            {
                // Reverse()는 배열 순서 역전 하는 메소드
                //var layer = document.Childs[k - 1];
                // GetLayer 메소드 활용 : 파라미터정보( layer : IPsdLayer, Layer Depth : 1, layerSeq : default(1) ++ )
                GetLayer(layer, 1, layerSeq++);
            }


            //var idx = _vmList.Count;
            //var prevLayerDepth = 0;
            //var stackParentIndex = new Stack<int>();
            //stackParentIndex.Push(0);

            //foreach (var x in _vmList)
            //{
            //    x.Index = idx--;
            //    if (prevLayerDepth == x.LayerDepth)
            //    {
            //        stackParentIndex.Pop();

            //    }
            //    else if (prevLayerDepth < x.LayerDepth)
            //    {
            //    }
            //    else if (prevLayerDepth > x.LayerDepth)
            //    {
            //        stackParentIndex.Pop();
            //        stackParentIndex.Pop();
            //    }
            //    x.ParentIndex = stackParentIndex.Peek();
            //    stackParentIndex.Push(x.Index);
            //    prevLayerDepth = x.LayerDepth;
            //}

            var idx = _vmList.Count;
            

            // 상위 인덱스 찾는 로직
            foreach (var x in _vmList)
            {
                x.Index = idx--;
                if (x.LayerDepth == 1)
                {
                    x.ParentIndex = 0;
                }
                else
                {
                    x.ParentIndex = -1;
                }
            }

            int layerDepth = 1;
            while (true)
            {
                ++layerDepth;
                var subLayerList = _vmList.Where(x => x.LayerDepth == layerDepth && x.ParentIndex < 0);
                if (subLayerList.Any() == false)
                    break;

                foreach(var x in subLayerList)
                {
                    var parentIndex = _vmList.Where(y => y.Index > x.Index 
                        && y.LayerDepth == x.LayerDepth - 1
                        ).Min(y=>y.Index);
                    x.ParentIndex = parentIndex;
                }
            }

            var firstVM = _vmList.FirstOrDefault();
            if (firstVM != null)
            {
                this.FileSectionPixel = string.Format("{0}*{1}", firstVM.HorizontalRes, firstVM.VerticalRes);
            }

            document.Dispose();
        }

    }
}
