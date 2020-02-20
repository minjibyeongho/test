using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILS_TEST_V1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
    

        private void File_Test_Click(object sender, EventArgs e)
        {
            FileTest dlg = new FileTest();
            dlg.Show();
        }

        private void ArrowCode_Test_Click(object sender, EventArgs e)
        {
            ArrowTest dlg = new ArrowTest();
            dlg.Show();
        }
    }
}
