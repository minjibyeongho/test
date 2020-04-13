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
    public partial class UcNameList : UserControl
    {
        private BindingList<string> _items = new BindingList<string>();
        public string ControlTitle
        {
            get
            {
                return labelTitle.Text;
            }
            set
            {
                labelTitle.Text = value;
            }
        }

        public int ControlCount
        {
            get
            {
                var count = 0;
                if (int.TryParse(txtCount.Text, out count))
                { }
                return count;
            }
            set
            {
                if (value > 0)
                    txtCount.Text = value.ToString();
                else
                    txtCount.Text = "";
            }
        }
        public string ControlName
        {
            get
            {
                return string.Join("|", _items);
            }
            set
            {
                _items.Clear();
                var arr = value.Split(new char[] { '|' });
                foreach (var x in arr)
                    _items.Add(x);
            }
        }

        public UcNameList()
        {
            InitializeComponent();
            lstName.DataSource = _items;
        }
    }
}
