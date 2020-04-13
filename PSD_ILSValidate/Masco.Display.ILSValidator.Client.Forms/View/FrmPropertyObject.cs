using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Masco.Display.ILSValidator.Client.Forms
{
    public partial class FrmPropertyObject : Form
    {
        public FrmPropertyObject()
        {
            InitializeComponent();
        }

        public void Setup(object obj)
        {
            propertyGrid1.SelectedObject = obj;
        }
    }
}
