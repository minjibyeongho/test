using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    [Masco.Core.Bootstrapper.EntryPoint]
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.Shown += FrmLogin_Shown;
        }

        void FrmLogin_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userID = txtUserID.Text;
            if (userID.Equals("display", StringComparison.OrdinalIgnoreCase) == false &&
                userID.Equals("survey", StringComparison.OrdinalIgnoreCase) == false)
            {
                MessageBox.Show("등록되지 않은 사용자입니다.");
                return;
            }
            var userPwd = txtUserPWD.Text;
            if (userID.Equals("display", StringComparison.OrdinalIgnoreCase))
            {
                if (userPwd.Equals("masco@1", StringComparison.OrdinalIgnoreCase) == false)
                {
                    MessageBox.Show("암호가 일치하지 않습니다.");
                    return;
                }
            }

            if (userID.Equals("survey", StringComparison.OrdinalIgnoreCase))
            {
                if (userPwd.Equals("masco@2", StringComparison.OrdinalIgnoreCase) == false)
                {
                    MessageBox.Show("암호가 일치하지 않습니다.");
                    return;
                }
            }

            var connectInfo = new WebClient().DownloadString("http://ipinfo.io/json");
            var strIpInfo = System.Text.RegularExpressions.Regex.Replace(connectInfo, @"{|\n  |}", String.Empty);
            strIpInfo = strIpInfo.Replace("\"", string.Empty);

            if (strIpInfo.IndexOf(",") != -1)
            {
                var arrIpInfo = strIpInfo.Split(',');
                if (arrIpInfo.Count() > 0)
                {
                    if (arrIpInfo[0].IndexOf(":") != -1)
                    {
                        var ipInfo = arrIpInfo[0].Split(':');
                        if (ipInfo.Count() > 1)
                        {
                            var ip = ipInfo[1].Trim();
                            txtLog.AppendText(string.Format("{0}\r\n", ip));
                            Application.DoEvents();
                            if (ip == "175.121.89.99" || ip == "175.121.89.100" || ip == "175.121.88.132" || ip == "175.121.88.133")
                            {
                                this.Visible = false;
                                var dlg = new FrmMain();
                                dlg.ShowDialog(this);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Masco로 등록된 IP 가 아닙니다.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("IP 정보가 없습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("IP 정보가 없습니다.");
                    }
                }
                else
                {
                    MessageBox.Show("IP 정보가 올바르지 않습니다.");
                }
            }
            else
            {
                MessageBox.Show("IP정보를 조회할수없습니다. 인터넷을 연결해주세요");
            }

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtLog.AppendText("ver 20190108\r\n");
            txtLog.AppendText("- 사용기간 연장\r\n");


            txtLog.AppendText("ver 20180726\r\n");
            txtLog.AppendText("- 검증항목 추가 (일반교차로)\r\n");
            txtLog.AppendText("- 검증항목 추가 (JC)\r\n");
            txtLog.AppendText("- 검증항목 추가 (도시고속)\r\n");
            txtLog.AppendText("- 검증항목 추가 (ETC)\r\n");
            txtLog.AppendText("- 검증항목 추가 (모식도)\r\n");
            txtLog.AppendText("- 검증항목 추가 (3D 교차점)\r\n");
            txtLog.AppendText("- 검증항목 추가 (휴게소 요약도(맵피))\r\n");
            txtLog.AppendText("- 검증항목 추가 (휴게소 요약도(지니))\r\n");
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
