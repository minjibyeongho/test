using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Masco.Display.ILSValidator.Client.Common
{
    public class UserVm
    {
        public string USER_ID { get; set; }
        public string USER_PASSWORD { get; set; }
        public string ETC { get; set; }

        public UserVm(string userID, string userPwd)
        {
            // TODO: Complete member initialization
            this.USER_ID = userID;
            this.USER_PASSWORD = userPwd;
        }
    }
}
