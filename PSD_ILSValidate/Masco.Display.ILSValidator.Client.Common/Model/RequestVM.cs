using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class RequestVM : ModelBase<RequestVM>
    {
        [DisplayName("순번")]
        public int IDX { get; set; }

        [DisplayName("작성일")]
        public DateTime CREATE_DT { get; set; }

        [DisplayName("요청구분")]
        public string REQ_KIND { get; set; }

        [DisplayName("요청내용")]
        public string DESCRIPTION { get; set; }
        public int CHECKLIST_PK { get; set; }

        public bool IS_DELETE{ get; set; }


        internal static ServiceReferenceCheckList.REQUEST CreateEntity(RequestVM _vm)
        {
            var item = new ServiceReferenceCheckList.REQUEST();
            Common.Helper.CopyProperties(_vm, item);
            return item;
        }
    }

}
