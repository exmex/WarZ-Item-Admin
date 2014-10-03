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
            threading.Dock = DockStyle.Fill;
            quickConfig.Dock = DockStyle.Fill;
            colorConfig.Dock = DockStyle.Fill;
            //folderBrowserDialog1.SelectedPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Arktos Entertainment Group\WarZ", "Installed", null);

            textBox1.Text = Properties.Settings.Default.dataFolder;
            checkBox1.Checked = Properties.Settings.Default.loadShopFromFile;
            textBox2.Text = Properties.Settings.Default.shopDBFile;

            label5.ForeColor = Properties.Settings.Default.internalItemsColor;
            label7.ForeColor = Properties.Settings.Default.wearableItemsColor;
            label9.ForeColor = Properties.Settings.Default.weaponItemsColor;
            label13.ForeColor = Properties.Settings.Default.attachmentItemsColor;
            label12.ForeColor = Properties.Settings.Default.consumableItemsColor;

            checkBox3.Checked = Properties.Settings.Default.showColorsEverything;
            label15.Font = Properties.Settings.Default.listFont;
            label15.Text = "Current font: " + Environment.NewLine + Properties.Settings.Default.listFont.OriginalFontName + " (" + Properties.Settings.Default.listFont.Size + ")";

            folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathing = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Data";
            if (Directory.Exists(pathing) && File.Exists(pathing+".zip"))
            {
                if (File.Exists(pathing + ".zip"))
                {
                    FileInfo fINfo = new FileInfo(pathing + ".zip");
                    Console.WriteLine(fINfo.Length);
                    if (fINfo.Length >= 51699554)
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
            //label4.Location = new Point(215 - (label4.Size.Width / 2), label4.Location.Y);
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
                    //label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
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
                        //label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
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
                            //label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
                        }));

                        foreach (ZipEntry e in zip1)
                        {
                            label4.Invoke((MethodInvoker)(() =>
                            {
                                label4.Text = "Extracting "+e.FileName+"...";
                                //label4.Location = new Point(220 - (label4.Size.Width / 2), label4.Location.Y);
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
            Properties.Settings.Default.showColorsEverything = checkBox3.Checked;
            Properties.Settings.Default.dataFolder = textBox1.Text;
            Properties.Settings.Default.shopDBFile = textBox2.Text;
            Properties.Settings.Default.loadShopFromFile = checkBox1.Checked;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = Properties.Settings.Default.shopDBFile;
            else
                textBox2.Text = "";

            button4.Visible = checkBox1.Checked;
            if(checkBox1.Checked)
                label2.Text = "Shop XML File:";
            else
                label2.Text = "Shop XML URL:";
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count >= 1)
                return;
            switch (e.Node.Text)
            {
                default:
                    threading.Visible = false;
                    colorConfig.Visible = false;
                    quickConfig.Visible = true;
                    break;
                case "Fonts & Colors":
                    threading.Visible = false;
                    quickConfig.Visible = false;
                    colorConfig.Visible = true;
                    break;
                case "Threading":
                    threading.Visible = true;
                    quickConfig.Visible = false;
                    colorConfig.Visible = false;
                    break;
            }
        }

        private void colorChange_Click(object sender, EventArgs e)
        {
            if (sender == button5)
            {
                colorDialog1.Color = Properties.Settings.Default.internalItemsColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.internalItemsColor = colorDialog1.Color;
                }
            }
            else if (sender == button7)
            {
                colorDialog1.Color = Properties.Settings.Default.wearableItemsColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.wearableItemsColor = colorDialog1.Color;
                }
            }
            else if (sender == button8)
            {
                colorDialog1.Color = Properties.Settings.Default.weaponItemsColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.weaponItemsColor = colorDialog1.Color;
                }
            }else if (sender == button9)
            {
                colorDialog1.Color = Properties.Settings.Default.attachmentItemsColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.attachmentItemsColor = colorDialog1.Color;
                }
            }
            else if (sender == button10)
            {
                colorDialog1.Color = Properties.Settings.Default.consumableItemsColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.consumableItemsColor = colorDialog1.Color;
                }
            }

            label5.ForeColor = Properties.Settings.Default.internalItemsColor;
            label7.ForeColor = Properties.Settings.Default.wearableItemsColor;
            label9.ForeColor = Properties.Settings.Default.weaponItemsColor;
            label13.ForeColor = Properties.Settings.Default.attachmentItemsColor;
            label12.ForeColor = Properties.Settings.Default.consumableItemsColor;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = checkBox3.Checked;
            label5.Visible = checkBox3.Checked;
            label6.Visible = checkBox3.Checked;
            label7.Visible = checkBox3.Checked;
            label8.Visible = checkBox3.Checked;
            label9.Visible = checkBox3.Checked;
            label10.Visible = checkBox3.Checked;
            label11.Visible = checkBox3.Checked;
            label12.Visible = checkBox3.Checked;
            label13.Visible = checkBox3.Checked;

            button5.Visible = checkBox3.Checked;
            button7.Visible = checkBox3.Checked;
            button8.Visible = checkBox3.Checked;
            button9.Visible = checkBox3.Checked;
            button10.Visible = checkBox3.Checked;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = Properties.Settings.Default.listFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.listFont = fontDialog1.Font;
                label15.Font = Properties.Settings.Default.listFont;
                label15.Text = "Current font:" + Environment.NewLine + Properties.Settings.Default.listFont.Name + " (" + Properties.Settings.Default.listFont.Size + ")";
            }
        }
    }
}
