using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class ILSValidatorVM : ModelBase<ILSValidatorVM>
    {
        [DisplayName("코드")]
        public int CHECKLIST_PK { get; set; }

        [DisplayName("작업상태")]
        public string WORK_STATE{ get; set; }

        [DisplayName("요청일시")]
        public DateTime CREATE_DT { get;set; }

        [DisplayName("수정일시")]
        public DateTime UPDATE_DT { get; set; }

        [DisplayName("요청자명")]
        public string REQ_UNAME{ get; set; }

        [DisplayName("수정자명")]
        public string UPDATE_UNAME{ get; set; }

        [DisplayName("요청구분")]
        public string REQ_TYPE { get; set; }

        [DisplayName("ILS구분")]
        public string ILS_TYPE { get; set; }

        [DisplayName("메인코드")]
        public string MAIN_CODE { get; set; }

        [DisplayName("명칭")]
        public string NAME { get; set; }

        [DisplayName("명칭없음")]
        public bool IS_NAME { get; set; }

        [DisplayName("맵ID")]
        public int MAP_ID { get; set; }

        [DisplayName("노드ID")]
        public int NODE_ID { get; set; }

        [DisplayName("좌표(MMS)")]
        public string MMS_LONLAT { get; set; }
        //public bool LaneInfo { get; set; }

        [DisplayName("바닥명칭")]
        public string BOTTOM_NAME { get; set; }
        
        //public Stream CaptureBaseMap { get; set; }
        //public Stream CaptureReference { get; set; }
        //public int CI_PK_BASEMAP{ get; set; }
        //public int CI_PK_REFERENCE { get; set; }

        //public string CaptureBasemapImgPath { get; set; }
        //public string CaptureReferenceImgPath { get; set; }

        [DisplayName("적용차수AM")]
        public string APPLY_AM { get; set; }

        [DisplayName("적용차수OBN")]
        public string APPLY_OBN { get; set; }

        [DisplayName("적용차수OEM")]
        public string APPLY_OEM { get; set; }

        public int ImagePk_BaseMap { get; set; }
        public int ImagePk_Reference { get; set; }
        public string Remarks{ get; set; }


        internal static ILSValidatorVM Create(ServiceReferenceCheckList.CHECKLIST x)
        {
            var item = new ILSValidatorVM();
            Helper.CopyProperties(x, item);
            return item;
        }

        internal static ServiceReferenceCheckList.CHECKLIST CreateEntity(ILSValidatorVM x)
        {
            var item = new ServiceReferenceCheckList.CHECKLIST();
            Helper.CopyProperties(x, item);
            return item;
        }

    }
}
