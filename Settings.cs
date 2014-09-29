using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.Win32;
using System.IO.Compression;

namespace WarZLocal_Admin
{
    public partial class Settings : Form
    {
        private Thread td;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //folderBrowserDialog1.SelectedPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Arktos Entertainment Group\WarZ", "Installed", null);

            textBox1.Text = Properties.Settings.Default.dataFolder;
            textBox2.Text = Properties.Settings.Default.itemsDBFile;
            textBox3.Text = Properties.Settings.Default.shopDBFile;

            folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathing = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data";
            if (Directory.Exists(pathing) && File.Exists(pathing+".zip"))
            {
                if (File.Exists(pathing + ".zip"))
                {
                    FileInfo fINfo = new FileInfo(pathing + ".zip");
                    Console.WriteLine(fINfo.Length);
                    if (fINfo.Length >= 49969215)
                    {
                        button6.Visible = false;
                    }
                }
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (td != null && td.ThreadState == ThreadState.Running)
                td.Abort();
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

        private void button6_Click(object sender, EventArgs ev)
        {
            //if(File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data.zip"))
            if (Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data"))
            {
                if (
                    MessageBox.Show("Folder already present.\nContinue and delete?", "Folder present!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }
                Directory.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data", true);
            }

            label4.Visible = true;
            label4.Text = "Starting..";
            label4.Location = new Point(215 - (label4.Size.Width / 2), label4.Location.Y);
            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            td = new Thread((() =>
            {
                string sDestinationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data.zip";
                long iFileSize = 0;
                int iBufferSize = 1024;
                iBufferSize *= 1000;
                long iExistLen = 0;
                FileStream saveFileStream;
                if (File.Exists(sDestinationPath))
                {
                    FileInfo fINfo = new FileInfo(sDestinationPath);
                    iExistLen = fINfo.Length;
                }
                if (iExistLen > 0)
                    saveFileStream = new FileStream(sDestinationPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                else
                    saveFileStream = new FileStream(sDestinationPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                label4.Invoke((MethodInvoker)(() =>
                {
                    label4.Text = "Requesting File...";
                    label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
                }));

                HttpWebRequest hwRq;
                HttpWebResponse hwRes;
                hwRq = (HttpWebRequest)HttpWebRequest.Create("https://github.com/exmex/ItemsDB_Class/raw/master/Data.zip");
                hwRq.AddRange((int)iExistLen);
                Stream smRespStream;
                hwRes = (HttpWebResponse)hwRq.GetResponse();
                smRespStream = hwRes.GetResponseStream();

                iFileSize = hwRes.ContentLength;

                int iByteSize;
                byte[] downBuffer = new byte[iBufferSize];

                while ((iByteSize = smRespStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                {
                    double dIndex = saveFileStream.Length;
                    double dTotal = iFileSize;
                    if (iExistLen > 0)
                        dTotal = (double) iFileSize + iExistLen;
                    double dProgressPercentage = (dIndex / dTotal);
                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                    label4.Invoke((MethodInvoker)(() =>
                    {
                        if (iExistLen > 0)
                            label4.Text = "Resuming Download (" + iProgressPercentage + "%)...";
                        else
                            label4.Text = "Downloading Files (" + iProgressPercentage + "%)...";
                        label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
                    }));
                    saveFileStream.Write(downBuffer, 0, iByteSize);
                }

                using (var zip = new ZipFile())
                {
                    using (
                        ZipFile zip1 =
                            ZipFile.Read(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data.zip"))
                    {
                        label4.Invoke((MethodInvoker)(() =>
                        {
                            label4.Text = "Extracting Files...";
                            label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
                        }));

                        foreach (ZipEntry e in zip1)
                        {
                            label4.Invoke((MethodInvoker)(() =>
                            {
                                label4.Text = "Extracting "+e.FileName+"...";
                                label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
                            }));
                            e.Extract(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data",
                                ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }

                label4.Invoke((MethodInvoker)(() =>
                {
                    textBox1.Text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data";
                    label4.Visible = false;
                    loadingCircle1.Visible = false;
                    loadingCircle1.Stop();
                }));
            }));
            td.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dataFolder = textBox1.Text;
            Properties.Settings.Default.itemsDBFile = textBox2.Text;
            Properties.Settings.Default.shopDBFile = textBox3.Text;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}
