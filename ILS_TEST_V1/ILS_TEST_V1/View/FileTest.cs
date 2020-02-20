using Ntreev.Library.Psd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILS_TEST_V1
{
    public partial class FileTest : Form
    {
        public FileTest()
        {
            InitializeComponent();
        }


        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();

            if (result != DialogResult.OK)
                return;
            txtFolderPath.Text = fbd.SelectedPath; // 선택한 폴더명


            var selectPath = txtFolderPath.Text;
            var files = Directory.GetFiles(selectPath, "*.psd", SearchOption.AllDirectories);

            #region PSD FILE LIST
            gridVerify.Columns.Add("FileName", "FileName");
            #endregion
            foreach (var file in files)
            {
                var doc = PsdDocument.Create(file);
                gridVerify.Rows.Add(file);
            }

        }
    }
}
