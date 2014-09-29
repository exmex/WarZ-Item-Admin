using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace WarZLocal_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<int, Items> itemsDB = new Dictionary<int, Items>();
        private Size currentRes = new Size(128, 128);
        private Size currentRes2 = new Size(128, 128);
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;

            listView2.Clear();
            imageList2.ImageSize = currentRes;
            imageList2.Images.Clear();
            imageList2.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), currentRes));

            using (XmlReader reader = XmlReader.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\itemsDB.xml"))
            {
                Items i;

                try
                {
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            default:
                                continue;
                            case "Attachment":
                                i = Attachment.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "Backpack":
                                i = Backpack.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "Item":
                                // Hack because of several items.
                                // 1 - Generic
                                // 30 - Food
                                i = Helper.getInt(reader.GetAttribute(1)) == 1 ? Generic.readXML(reader) : Food.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "Gear":
                                i = Gear.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "Hero":
                                i = Hero.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "LootBox":
                                i = Lootbox.readXML(reader);
                                itemsDB.Add(i.itemID, i);
                                break;
                            case "Weapon":
                                i = Weapon.readXML(reader);
                                
                                i.binding = listView2.Items.Count;

                                itemsDB.Add(i.itemID, i);
                                string icon = i.image;

                                icon = icon.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                                icon = icon.Replace(".dds", ".png");
                                //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                                ListViewItem lvi = new ListViewItem(i.name + "\n(" + i.itemID + ")");
                                lvi.ImageIndex = imageList2.Images.Count;

                                if (File.Exists(icon))
                                    imageList2.Images.Add(ImageUtilities.getThumb((Bitmap) Image.FromFile(icon),
                                        currentRes));
                                else
                                    lvi.ImageIndex = 0;

                                listView2.Items.Add(lvi);

                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Critical Error occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (XmlReader reader = XmlReader.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\shopDB.xml"))
            {
                while (reader.Read())
                {
                    if (reader.Name == "si" && reader.IsStartElement())
                    {
                        int itemID;
                        int price;
                        int.TryParse(reader.GetAttribute(0), out itemID);
                        int.TryParse(reader.GetAttribute(1), out price);

                        if (price == 0)
                            int.TryParse(reader.GetAttribute(2), out price);

                        if (!itemsDB.ContainsKey(itemID))
                            continue;

                        Items i = itemsDB[itemID];
                        i.pricePerm = price;
                    }
                }
            }
        }

        public void LoadItemsDB()
        {
            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                i.Value.binding = -1;
            }

            panel1.Visible = false;
            imageList2.ImageSize = currentRes;
            imageList2.Images.Clear();
            imageList2.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), currentRes));
            listView2.Clear();

            List<ListViewItem> lvil = new List<ListViewItem>();

            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                bool checkForText = true;
                if (textBox1.Text != "")
                {
                    string vars = "img|i|n|d|c";
                    string ops = "\\$|!\\$|&|!&|!=|<=|>=|=|>|<";

                    bool match = Regex.Match(textBox1.Text, "(" + vars + ")(" + ops + ")\"(.*?)\"").Success;
                    bool match2 = Regex.Match(textBox1.Text, "\"(.*?)\"").Success;
                    if (match)
                    {
                        MatchCollection m = Regex.Matches(textBox1.Text, "(" + vars + ")(" + ops + ")\"(.*?)\"");

                        string text = textBox1.Text.Replace("\"", ""), compareString = "";
                        int number = -999, compareInt = 0;

                        text = text.Replace(m[0].Groups[1].Value, "");
                        text = text.Replace(m[0].Groups[2].Value, "");
                        int.TryParse(text, out number);
                        switch (m[0].Groups[1].Value)
                        {
                            case "i":
                                compareInt = i.Value.itemID;
                                if (i.Value.itemID == -999)
                                    continue;
                                break;
                            case "n":
                                compareString = i.Value.name;
                                if (string.IsNullOrEmpty(i.Value.name))
                                    continue;
                                break;
                            case "d":
                                compareString = i.Value.desc;
                                if (string.IsNullOrEmpty(i.Value.desc))
                                    continue;
                                break;
                            case "img":
                                compareString = i.Value.image;
                                if (string.IsNullOrEmpty(i.Value.image))
                                    continue;
                                break;
                            case "c":
                                if(number != -999)
                                    compareInt = i.Value.category;
                                else
                                    compareString = Helper.fromItemsCategory(i.Value.category);
                                if (i.Value.category == -999)
                                    continue;
                                break;
                        }
                        switch (m[0].Groups[2].Value)
                        {
                            default:
                                errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                errorProvider1.SetError(textBox1, "No valid operator found!");
                                return;
                            case "&":
                                if (!string.IsNullOrEmpty(compareString))
                                    checkForText =
                                        File.Exists(
                                            i.Value.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data")
                                                .Replace(".dds", ".png"));
                                else
                                {
                                    errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                    errorProvider1.SetError(textBox1, "No valid operator found!");
                                }
                                break;
                            case "!&":
                                if (!string.IsNullOrEmpty(compareString))
                                    checkForText =
                                        !File.Exists(
                                            i.Value.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data")
                                                .Replace(".dds", ".png"));
                                else
                                {
                                    errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                    errorProvider1.SetError(textBox1, "No valid operator found!");
                                    continue;
                                }
                                break;
                            case "$":
                                if (!string.IsNullOrEmpty(compareString))
                                    checkForText = compareString.Contains(text);
                                else
                                {
                                    errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                    errorProvider1.SetError(textBox1, "No valid operator found!");
                                    continue;
                                }
                                break;
                            case "!$":
                                if (!string.IsNullOrEmpty(compareString))
                                    checkForText = !compareString.Contains(text);
                                else
                                {
                                    errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                    errorProvider1.SetError(textBox1, "No valid operator found!");
                                    continue;
                                }
                                break;
                            case "=":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = compareInt.Equals(number);
                                else
                                    checkForText = (compareString == text);
                                break;
                            case "!=":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = !compareInt.Equals(number);
                                else
                                    checkForText = (compareString != text);
                                break;
                            case ">":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = (compareInt > number);
                                else
                                    checkForText = (compareString.Length > text.Length);
                                break;
                            case "<":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = (compareInt < number);
                                else
                                    checkForText = (compareString.Length < text.Length);
                                break;
                            case ">=":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = (compareInt >= number);
                                else
                                    checkForText = (compareString.Length >= text.Length);
                                break;
                            case "<=":
                                if (string.IsNullOrEmpty(compareString))
                                    checkForText = (compareInt <= number);
                                else
                                    checkForText = (compareString.Length <= text.Length);
                                break;
                        }
                    }
                    else if (match2)
                    {
                        string text = textBox1.Text.Replace("\"", "");
                        checkForText = i.Value.name.Contains(text);
                    }
                    else
                        checkForText = i.Value.name.ToLower().Contains(textBox1.Text.ToLower());
                }
                if (checkForText && i.Value.internalCategory == comboBox1.SelectedIndex)
                {
                    string img = i.Value.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                    img = img.Replace(".dds", ".png");
                    //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                    if(tabControl1.SelectedIndex == 0)
                        i.Value.binding = lvil.Count;

                    ListViewItem lvi = new ListViewItem(i.Value.name + "\n(" + i.Value.itemID + ")");
                    lvi.ImageIndex = imageList2.Images.Count;

                    if (File.Exists(img))
                        imageList2.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile(img), currentRes));
                    else
                        lvi.ImageIndex = 0;

                    //listView2.Items.Add(lvi);
                    lvil.Add(lvi);
                }
            }
            listView2.Items.AddRange(lvil.ToArray());
        }

        public void LoadShopXML()
        {
            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                i.Value.binding = -1;
            }

            panel1.Visible = false;
            listView1.Clear();
            imageList1.ImageSize = currentRes2;
            imageList1.Images.Clear();
            imageList1.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), currentRes2));

            List<ListViewItem> lvil = new List<ListViewItem>();

            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                if (i.Value.pricePerm <= 0 || i.Value.pricePerm == -999)
                    continue;
                if (tabControl1.SelectedIndex == 1)
                    i.Value.binding = listView1.Items.Count;
                string img = i.Value.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                img = img.Replace(".dds", ".png");
                //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                ListViewItem lvi = new ListViewItem(i.Value.name + "\n" + i.Value.pricePerm + " GD");
                lvi.ImageIndex = imageList1.Images.Count;

                if (File.Exists(img))
                    imageList1.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                        currentRes));
                else
                    lvi.ImageIndex = 0;
                //listView1.Items.Add(lvi);
                lvil.Add(lvi);
            }
            listView1.Items.AddRange(lvil.ToArray());

            /*using (XmlReader reader = XmlReader.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\shopDB.xml"))
            {
                while (reader.Read())
                {
                    if (reader.Name == "si" && reader.IsStartElement())
                    {
                        int itemID;
                        int price;
                        int.TryParse(reader.GetAttribute(0), out itemID);
                        int.TryParse(reader.GetAttribute(1), out price);
                        string pName = "GP";
                        ListViewItem lvi;

                        if (price == 0)
                        {
                            int.TryParse(reader.GetAttribute(2), out price);
                            pName = "GD";
                        }

                        if (!itemsDB.ContainsKey(itemID))
                        {
                            lvi = new ListViewItem("Invalid ItemID (" + itemID + ")\n" + price + " " + pName);
                            lvi.ImageIndex = 0;
                        }
                        else
                        {
                            Items i = itemsDB[itemID];
                            if(price != i.pricePerm)
                                i.pricePerm = price;
                                
                            if(tabControl1.SelectedIndex == 1)
                                i.binding = listView1.Items.Count;
                            string img = i.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                            img = img.Replace(".dds", ".png");
                            //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                            lvi = new ListViewItem(i.name + "\n" + price + " " + pName);
                            lvi.ImageIndex = imageList1.Images.Count;

                            if (File.Exists(img))
                                imageList1.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                                    currentRes));
                            else
                                lvi.ImageIndex = 0;
                        }
                        listView1.Items.Add(lvi);
                    }
                }
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItemsDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, String.Empty);
            LoadItemsDB();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                default:
                    currentRes = new Size(32, 32);
                break;

                case 1:
                    currentRes = new Size(64, 64);
                break;

                case 2:
                    currentRes = new Size(128, 128);
                break;

                case 3:
                    currentRes = new Size(256, 256);
                break;
            }
            LoadItemsDB();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                default:
                    currentRes2 = new Size(32, 32);
                    break;

                case 1:
                    currentRes2 = new Size(64, 64);
                    break;

                case 2:
                    currentRes2 = new Size(128, 128);
                    break;

                case 3:
                    currentRes2 = new Size(256, 256);
                    break;
            }

            LoadShopXML();
        }

        public void LoadInformation(ListView obj)
        {
            if (obj.SelectedIndices.Count == 1)
            {
                panel1.Visible = true;
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.binding == obj.SelectedIndices[0])
                    {
                        string price = i.Value.pricePerm + " GD";
                        if (i.Value.pricePerm == 0)
                        {
                            label4.Visible = false;
                            label7.Visible = false;
                        }
                        else
                        {
                            label4.Visible = true;
                            label7.Visible = true;
                        }
                        string img = i.Value.image.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                        img = img.Replace(".dds", ".png");
                        if (File.Exists(img))
                            pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                                new Size(128, 128));
                        else
                            pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), new Size(128, 128));

                        label1.Text = i.Value.desc;
                        label5.Text = i.Value.name + " (" + i.Value.itemID + ")";
                        label6.Text = Helper.fromItemsCategory(i.Value.category);
                        if (Helper.fromItemsCategory(i.Value.category) == "Unknown")
                            Console.WriteLine(i.Value.name + " has an invalid category of " + i.Value.category);
                        label7.Text = price;
                        break;
                    }
                }
            }
            else
            {
                panel1.Visible = false;
                pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), new Size(128, 128));
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInformation(listView2);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInformation(listView1);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                default:
                    LoadItemsDB();
                    break;
                case 1:
                    LoadShopXML();
                    break;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView2.SelectedIndices.Count < 1)
                e.Cancel = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                if (i.Value.binding == listView2.SelectedIndices[0])
                {
                    itemsDB.Remove(i.Key);
                    LoadItemsDB();
                    break;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                if (i.Value.binding == listView2.SelectedIndices[0])
                {
                    Items.showEditForm(i.Value);

                    EditItem ei = new EditItem(i.Value);
                    if (ei.ShowDialog() == DialogResult.OK)
                    {
                        itemsDB[i.Key] = ei.buffer;
                        ei.Dispose();
                        tabControl1_SelectedIndexChanged(sender, e);
                    }
                    break;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlWriter writer;
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.NewLineOnAttributes = false;
            xmlWriterSettings.Indent = true;
            if (tabControl1.SelectedIndex == 0)
            {
                writer = XmlWriter.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\out.xml",
                    xmlWriterSettings);
                writer.WriteStartElement("DB");
                writer.WriteStartElement("WeaponsArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.internalCategory == 0)
                        Weapon.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("FoodArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 30 && i.Value.internalCategory == 4)
                        Food.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("GearArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 13 && i.Value.internalCategory == 1)
                        Gear.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("HeroArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 16 && i.Value.internalCategory == 5)
                        Hero.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("BackpackArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 12 && i.Value.internalCategory == 6)
                        Backpack.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("ItemsDB");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 1 && i.Value.internalCategory == 4)
                        Generic.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("AttachmentArmory");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 19 && i.Value.internalCategory == 2)
                        Attachment.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("LootBoxDB");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.category == 7 && i.Value.internalCategory == 3)
                        Lootbox.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Close();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                /***********/
                writer = XmlWriter.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\out2.xml", xmlWriterSettings);
                writer.WriteStartElement("shopInfo");
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    if (i.Value.pricePerm <= 0)
                        continue;

                    writer.WriteStartElement("si");
                    writer.WriteAttributeString("itemId", i.Value.itemID.ToString());
                    writer.WriteAttributeString("pricePerm", "0");
                    writer.WriteAttributeString("gdPricePerm", i.Value.pricePerm.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Close();
            }
        }
    }
}
