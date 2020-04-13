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
    public partial class UcRadioButton : UserControl//, INotifyPropertyChanged
    {
        public int SelectedType { get; set; }
        public string SelectedText
        {
            get
            {
                return GetText();
            }
            set
            {
                SetText(value);
               // OnPropertyChanged("SelectedText");
            }
        }

        private IList<RadioButton> _controls = new List<RadioButton>();

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public UcRadioButton()
        {
            InitializeComponent();

            _controls.Add(radioButton0);
            _controls.Add(radioButton1);
            _controls.Add(radioButton2);
            _controls.Add(radioButton3);

            foreach (var x in _controls)
                x.Visible = false;
        }

        public void Setup(IList<KeyValuePair<int, string>> dic)
        {
            for (int idx = 0; idx < _controls.Count; idx++)
            {
                if (idx >= dic.Count)
                {
                    break;
                }
                var item = dic[idx];
                var rdo = _controls[idx];
                rdo.Text = item.Value;
                rdo.Tag = item.Key;
                rdo.Visible = true;
                rdo.CheckedChanged += rdoType_CheckedChanged;
            }
        }

        private void rdoType_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = (RadioButton)sender;
            var value = rdo.Tag;
            if (value == null)
                SelectedType = 0;
            else
                SelectedType = (int)value;
        }

        public string GetText()
        {
            foreach (var rdo in _controls)
            {
                if (rdo.Checked)
                {
                    return rdo.Text;
                }
            }
            return string.Empty;
        }

        public void SetText(string value)
        {
            foreach (var rdo in _controls)
            {
                if (rdo.Text == value)
                {
                    rdo.Checked = true;
                    break;
                }
            }
        }

    }
}
