using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.Psd
{
    public enum DescriptionMode
    {
        Records_Flags_Number,
        Records_Flags_IsVisible,
        Records_Flags_IsTransparency,
        //Records_Flags,
        Records_SectionType,

        Docuemnt_HorizontalRes,
        Docuemnt_HorizontalResUnit,
        Docuemnt_WidthUnit,
        Docuemnt_VerticalRes,
        Docuemnt_VerticalResUnit,
        Docuemnt_HeightUnit,

        Path_LayerCount,

        Document_ImageSource_ChannelTypes,
    }
}
