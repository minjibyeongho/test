using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Ntreev.Library.Psd;

namespace ILS_TEST_V1.Model
{
    public class layerModel : ModelBase<layerModel>
    {
        [DisplayName("순번")]
        public int Index { get; set; }

        [DisplayName("상위")]
        public int ParentIndex { get; set; }

        [DisplayName("단계")]
        public int LayerDepth { get; set; }

        [DisplayName("순서")]
        public int LayerSeq { get; set; }

        [DisplayName("패턴번호")]
        public int LayerPattern { get; set; }

        [DisplayName("레이어명")]
        public string Name { get; set; }

        public BlendMode BlendMode { get; set; }
        public int ChildCount { get; set; }
        public int Depth { get; set; }

        [Category("동작")]
        public bool HasImage { get; set; }

        [Category("동작")]
        public bool HasMask { get; set; }

        [Category("스타일")]
        public float Opacity { get; set; }

        [Category("스타일")]
        public bool IsClippinig { get; set; }

        [Category("스타일")]
        public bool IsVisible { get; set; }

        [Category("스타일")]
        public bool IsLock { get; set; }

        [Category("스타일")]
        public string RecordsFlags { get; set; }

        [Category("스타일")]
        public string SectionType { get; set; }

        //public IChannel[] Channels { get; set; }
        [Category("Channel")]
        [DisplayName("채널수")]
        public int ChannelCount { get; set; }

        [Category("Channel")]
        [DisplayName("채널타입")]
        public string ChannelTypes { get; set; }

        [Category("Channel")]
        [DisplayName("채널별크기")]
        public string ChannelSize { get; set; }

        [Category("Channel")]
        [DisplayName("채널별값")]
        public string ChannelARGB { get; set; }

        [Category("Layout")]
        public int Width { get; set; }
        [Category("Layout")]
        public int Height { get; set; }
        [Category("Layout")]
        public int Left { get; set; }
        [Category("Layout")]
        public int Top { get; set; }
        [Category("Layout")]
        public int Right { get; set; }
        [Category("Layout")]
        public int Bottom { get; set; }



        [Category("Resolution")]
        public string HorizontalRes { get; set; }
        [Category("Resolution")]
        public string HorizontalResUnit { get; set; }
        [Category("Resolution")]
        public string WidthUnit { get; set; }
        [Category("Resolution")]
        public string VerticalRes { get; set; }
        [Category("Resolution")]
        public string VerticalResUnit { get; set; }
        [Category("Resolution")]
        public string HeightUnit { get; set; }

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2} | {3} | {4}", Index, ParentIndex, LayerDepth, LayerSeq, Name);
        }
    }
}
