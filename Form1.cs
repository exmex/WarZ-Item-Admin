using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using SQLite;
using WarZLocal_Admin.Properties;

namespace WarZLocal_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<int, Items> itemsDB = new Dictionary<int, Items>();

        private Dictionary<int, Attachment> attachments = new Dictionary<int, Attachment>();
        private Dictionary<int, Gear> gears = new Dictionary<int, Gear>();
        private Dictionary<int, Generic> generics = new Dictionary<int, Generic>();
        private Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

        private SQLiteConnection db;
        private Thread temp_thread;

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
            imageList2.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes));

            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            db = new SQLiteConnection("Database.dat");
            db.CreateTable<Attachment>();
            db.CreateTable<Gear>();
            db.CreateTable<Generic>();
            db.CreateTable<LootDataSQL>();
            db.CreateTable<Weapon>();

            using (XmlReader reader = XmlReader.Create(Properties.Settings.Default.shopDBFile))
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

            loadingCircle1.Stop();
            loadingCircle1.Visible = false;
        }

        public void LoadItemsDB()
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            panel1.Visible = false;
            imageList2.ImageSize = currentRes;
            imageList2.Images.Clear();
            imageList2.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes));
            listView2.Clear();

            List<ListViewItem> lvil = new List<ListViewItem>();

            foreach (KeyValuePair<int, Items> i in itemsDB)
            {
                i.Value.binding = -1;

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
                                            i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder)
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
                                            i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder)
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
                    string img = i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder);
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
            loadingCircle1.Visible = false;
            loadingCircle1.Stop();
        }

        public void LoadShopXML()
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            panel1.Visible = false;
            listView1.Clear();
            imageList1.ImageSize = currentRes2;
            imageList1.Images.Clear();
            imageList1.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes2));

            List<ListViewItem> lvil = new List<ListViewItem>();

            Thread td = new Thread((() =>
            {
                foreach (KeyValuePair<int, Items> i in itemsDB)
                {
                    i.Value.binding = -1;

                    if (i.Value.pricePerm <= 0 || i.Value.pricePerm == -999)
                        continue;
                    //if (tabControl1.SelectedIndex == 1)
                        i.Value.binding = listView1.Items.Count;
                    string img = i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder);
                    img = img.Replace(".dds", ".png");
                    //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                    ListViewItem lvi = new ListViewItem(i.Value.name + "\n" + i.Value.pricePerm + " GD");
                    lvi.ImageIndex = imageList1.Images.Count;

                    if (File.Exists(img))
                        imageList1.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                            currentRes2));
                    else
                        lvi.ImageIndex = 0;
                    //listView1.Items.Add(lvi);
                    lvil.Add(lvi);
                }
                listView1.Invoke((MethodInvoker)(() => listView1.Items.AddRange(lvil.ToArray())));
                loadingCircle1.Invoke((MethodInvoker)(() =>
                {
                       loadingCircle1.Visible = false;
                        loadingCircle1.Stop();
                }));
            }));
            td.Start();

            /*using (XmlReader reader = XmlReader.Create(Properties.Settings.Default.shopDBFile))
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
                            string img = i.image.Replace("$Data", Properties.Settings.Default.dataFolder);
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
                        string img = i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder);
                        img = img.Replace(".dds", ".png");
                        if (File.Exists(img))
                            pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                                new Size(128, 128));
                        else
                            pictureBox1.Image = ImageUtilities.getThumb(Resources.no_icon, new Size(128, 128));

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
                pictureBox1.Image = ImageUtilities.getThumb(Resources.no_icon, new Size(128, 128));
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (s.ShowDialog() == DialogResult.OK)
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
        }

        private void importItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "ItemsDB.xml|itemsDB.xml";
            of.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (of.ShowDialog() != DialogResult.OK)
                return;
            Panel tp = new Panel();
            tp.Size = Size;
            tp.Location = new Point(0, 0);
            tp.BackColor = SystemColors.Control;

            LoadingCircle lc = new LoadingCircle();
            lc.ThemeColor = Color.DodgerBlue;
            lc.OutnerCircleRadius = 30;
            lc.InnerCircleRadius = 10;
            lc.LineWidth = 3;
            lc.Size = new Size(64, 64);
            lc.Location = new Point((Size.Width / 2) - lc.Size.Width, (Size.Height / 2) - lc.Size.Height);

            Label lb = new Label();
            lb.Text = "Import your items...";
            lb.AutoSize = true;
            lb.SizeChanged += (EventHandler)((s, ev) => { lb.Location = new Point((Size.Width / 2) - (lb.Size.Width/2) - 20, ((Size.Height / 2) - lb.Size.Height) + (lc.Size.Height / 2)); });
            lb.Location = new Point((Size.Width / 2) - (lb.Size.Width /2)-20, ((Size.Height / 2) - lb.Size.Height) + (lc.Size.Height / 2));

            temp_thread = new Thread((() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    Controls.Add(tp);
                    Controls.Add(lc);
                    Controls.Add(lb);
                    tp.BringToFront();
                    lc.BringToFront();
                    lb.BringToFront();

                    lc.Start();

                    lb.Text = "Loading items from XML...";
                }));

                using (XmlReader reader = XmlReader.Create(of.FileName))
                {
                    try
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType != XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
                                continue;

                            switch (reader.Name)
                            {
                                default:
                                    continue;
                                case "Attachment":
                                    Attachment attach = Attachment.readXML(reader);
                                    attachments.Add(attach.ItemID, attach);
                                    break;
                                case "Backpack":
                                    Gear _backpack = Gear.readXML(reader);
                                    gears.Add(_backpack.ItemID, _backpack);
                                    break;
                                case "Item":
                                    // Hack because of several items.
                                    // 1 - Generic
                                    // 30 - Food
                                    int cat = Helper.getInt(reader.GetAttribute(1));
                                    //i =  cat == 1 ? Generic.readXML(reader) : Food.readXML(reader);

                                    if (cat == 1)
                                    {
                                        Generic _generic = Generic.readXML(reader);
                                        generics.Add(_generic.ItemID, _generic);
                                    }
                                    else if (cat == 30)
                                    {
                                        Weapon wSQL = Weapon.readXML(reader);
                                        weapons.Add(wSQL.ItemID, wSQL);
                                    }
                                    break;
                                case "Gear":
                                    Gear _gear = Gear.readXML(reader);
                                    gears.Add(_gear.ItemID, _gear);
                                    break;
                                case "Hero":
                                    Gear _hero = Gear.readXML(reader);
                                    gears.Add(_hero.ItemID, _hero);
                                    break;
                                case "LootBox":
                                    Generic _lootbox = Generic.readXML(reader);
                                    generics.Add(_lootbox.ItemID, _lootbox);
                                    break;
                                case "Weapon":
                                    Weapon wSQL2 = Weapon.readXML(reader);
                                    //WeaponSQL wSQL2 = WeaponSQL.Add(db, i);

                                    weapons.Add(wSQL2.ItemID, wSQL2); // Bugfix

                                    /*i.binding = listView2.Items.Count;

                                itemsDB.Add(i.itemID, i);
                                string icon = i.image;

                                icon = icon.Replace("$Data", Properties.Settings.Default.dataFolder);
                                icon = icon.Replace(".dds", ".png");
                                //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                                ListViewItem lvi = new ListViewItem(i.name + "\n(" + i.itemID + ")");
                                lvi.ImageIndex = imageList2.Images.Count;

                                if (File.Exists(icon))
                                    imageList2.Images.Add(ImageUtilities.getThumb((Bitmap) Image.FromFile(icon),
                                        currentRes));
                                else
                                    lvi.ImageIndex = 0;

                                listView2.Items.Add(lvi);*/

                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Critical Error occured!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                foreach (KeyValuePair<int, Attachment> a in attachments)
                {
                    int count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Attachment WHERE itemID=?", a.Value.ItemID);
                    if (count == 0)
                    {
                        db.Insert(a.Value);
                        Console.WriteLine("New Attachment itemID: {0}", a.Value.ItemID);
                        Invoke((MethodInvoker)(() =>
                        {
                            lb.Text = "Adding Attachment \""+a.Value.Name+"\" ("+a.Value.ItemID+")...";
                        }));
                    }
                }

                foreach (KeyValuePair<int, Gear> g in gears)
                {
                    int count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Gear WHERE itemID=?", g.Value.ItemID);
                    if (count == 0)
                    {
                        db.Insert(g.Value);
                        Console.WriteLine("New Gear itemID: {0}", g.Value.ItemID);
                        Invoke((MethodInvoker)(() =>
                        {
                            lb.Text = "Adding Gear \"" + g.Value.Name + "\" (" + g.Value.ItemID + ")...";
                        }));
                    }
                }

                foreach (KeyValuePair<int, Generic> g in generics)
                {
                    int count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Generic WHERE itemID=?", g.Value.ItemID);
                    if (count == 0)
                    {
                        db.Insert(g.Value);
                        Console.WriteLine("New Generic itemID: {0}", g.Value.ItemID);
                        Invoke((MethodInvoker)(() =>
                        {
                            lb.Text = "Adding Generic \""+g.Value.Name+"\" ("+g.Value.ItemID+")...";
                        }));
                    }
                }

                foreach (KeyValuePair<int, Weapon> w in weapons)
                {
                    int count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Weapon WHERE itemID=?", w.Value.ItemID);
                    if (count == 0)
                    {
                        db.Insert(w.Value);
                        Console.WriteLine("New Weapon itemID: {0}", w.Value.ItemID);
                        Invoke((MethodInvoker)(() =>
                        {
                            lb.Text = "Adding Weapon \""+w.Value.Name+"\" ("+w.Value.ItemID+")...";
                        }));
                    }
                }

                Invoke((MethodInvoker)(() =>
                {
                    Controls.Remove(tp);
                    Controls.Remove(lc);
                    Controls.Remove(lb);
                }));
            }));
            temp_thread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(temp_thread != null && temp_thread.ThreadState == ThreadState.Running)
                temp_thread.Abort();
        }
    }
}
