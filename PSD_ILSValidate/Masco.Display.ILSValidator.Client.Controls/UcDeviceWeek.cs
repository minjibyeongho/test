using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Controls
{
    public partial class UcDeviceWeek : UserControl
    {
        class WeekInfo
        {
            public string Work { get; set; }
            public string Team1 { get; set; }
            public string Team2 { get; set; }
            public string Team3 { get; set; }
            public override string ToString()
            {
                return string.Format("{0}|{1}|{2}|{3}", Work, Team1, Team2, Team3);
            }
        }

        private IList<WeekInfo> _items = new List<WeekInfo>();

        public UcDeviceWeek()
        {
            InitializeComponent();
        }

        public string ControlValue
        {
            get
            {
                return string.Join(",", _items);
            }
            set
            {
                _items.Clear();

                var arrWork = value.Split(new char[] { ',' });
                foreach (var x in arrWork)
                {
                    var item = new WeekInfo();
                    var arr = x.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    item.Work = arr[0];
                    item.Team1 = arr[1];
                    item.Team2 = arr[2];
                    item.Team3 = arr[3];
                    _items.Add(item);
                }
            }
        }
    }
}
