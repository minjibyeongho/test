using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class LaneInfoVM : ModelBase<LaneInfoVM>
    {
        [DisplayName("차선")]
        public int NUMBER { get; set; }

        [DisplayName("방향")]
        public string DIRECTION { get; set; }
        
        [DisplayName("레인명")]
        public string LANE_NAME { get; set; }

        [DisplayName("이미지명")]
        public string IMAGE_NAME { get; set; }

        [DisplayName("화살표코드")]
        public string IMAGE_CODE { get; set; }

        public int LANEINFO_PK { get; set; }
        public int CHECKLIST_PK { get; set; }

        public static ServiceReferenceCheckList.LANEINFO CreateEntity(LaneInfoVM vm)
        {
            var item = new ServiceReferenceCheckList.LANEINFO();
            Helper.CopyProperties(vm, item);
            return item;
        }
    }
}
