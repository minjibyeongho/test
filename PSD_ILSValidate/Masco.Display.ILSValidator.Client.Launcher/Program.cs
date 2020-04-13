using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Launcher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var exePath = Application.ExecutablePath;
            //var entryPointType  = (new Masco.Core.Bootstrapper.AppConfig()).GetEntryPointType(exePath);

            var configuration = ConfigurationManager.OpenExeConfiguration(exePath);
            if (configuration.AppSettings.Settings.AllKeys.Contains("EntryPoint") == false)
            {
                configuration.AppSettings.Settings.Add("EntryPoint", "");
                configuration.Save();
                MessageBox.Show("시작점(EntryPoint)을 입력하세요");
                return;
            }

            if (configuration.AppSettings.Settings.AllKeys.Contains("BaseAddress") == false)
            {
                configuration.AppSettings.Settings.Add("BaseAddress", "");
                configuration.Save();
                MessageBox.Show("서비스 기본주소(BaseAddress)를 입력하세요");
                return;
            }
            var entryPoint = configuration.AppSettings.Settings["EntryPoint"].Value;
            var asmm = Assembly.Load(entryPoint);
            if (asmm == null)
            {
                MessageBox.Show("시작점(EntryPoint) 어셈블리를 찾을 수 없습니다.");
                return;
            }
            var types = asmm.GetTypes();
            Type entryPointType = null;
            foreach (var type in types)
            {
                var x = type.GetCustomAttributes(typeof(Masco.Core.Bootstrapper.EntryPointAttribute), false);
                if (x.Count() != 1) continue;

                entryPointType = type;
                break;
            }
            if (entryPointType == null)
            {
                MessageBox.Show("시작점(EntryPoint)을 찾을 수 없습니다.");
                return;
            }

            try
            {
                Application.Run((Form)Activator.CreateInstance(entryPointType));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
