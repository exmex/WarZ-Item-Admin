using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO.Compression;

namespace WarZLocal_Admin
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //folderBrowserDialog1.SelectedPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Arktos Entertainment Group\WarZ", "Installed", null);
            folderBrowserDialog1.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void Settings_Resize(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Height - 29;
            button1.Location = new Point(splitContainer2.Panel2.Width - button1.Width - button2.Width - 5, 3);
            button2.Location = new Point(splitContainer2.Panel2.Width - button2.Width - 5, 3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ItemsDB|itemsDB.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ShopDB|shopDB.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string dirpath = @"C:\Users\gooman\Documents\Visual Studio 2010\Projects\WarZLocal_Admin\WarZLocal_Admin\Data\";

            using (var zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory(dirpath, "/");
                zip.Save("Data.zip");
            }
        }
    }
}
