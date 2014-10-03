using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable RedundantAssignment
        // ReSharper disable InconsistentNaming
        // ReSharper disable LocalizableElement

        private Dictionary<int, Items> itemsDB = new Dictionary<int, Items>();

        private Dictionary<int, Attachment> attachments = new Dictionary<int, Attachment>();
        private Dictionary<int, Gear> gears = new Dictionary<int, Gear>();
        private Dictionary<int, Generic> generics = new Dictionary<int, Generic>();
        private Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

        public Dictionary<int, string> Categories = new Dictionary<int, string>();

        private SQLiteConnection db;
        private Thread temp_thread;

        private Size currentRes = new Size(128, 128);
        private Size currentRes2 = new Size(128, 128);
        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;

            listView2.Font = Properties.Settings.Default.listFont;

            listView2.Clear();
            imageList2.ImageSize = currentRes;
            imageList2.Images.Clear();
            imageList2.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes));

            db = new SQLiteConnection("Database.dat");
            db.CreateTable<Attachment>();
            db.CreateTable<Gear>();
            db.CreateTable<Generic>();
            db.CreateTable<LootDataSQL>();
            db.CreateTable<Weapon>();

            /* Populate Combobox */

            //Categories
            Categories.Add(0, "Everything");
            Categories.Add(1, "Account");
            Categories.Add(2, "Boost");
            Categories.Add(7, "LootBox");
            Categories.Add(11, "Armor");
            Categories.Add(12, "Backpack");
            Categories.Add(13, "Helmet");
            Categories.Add(16, "Hero");
            Categories.Add(19, "Attachment");
            Categories.Add(20, "Assault Rifle");
            Categories.Add(21, "Sniper Rifle");
            Categories.Add(22, "Shotgun");
            Categories.Add(23, "Machine gun");
            Categories.Add(25, "Handgun");
            Categories.Add(26, "Submachinegun");
            Categories.Add(27, "Grenade");
            Categories.Add(29, "Melee");
            Categories.Add(30, "Food");
            Categories.Add(33, "Water");

            comboBox1.DataSource = new BindingSource(Categories, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            /* ----------------- */

            temp_thread = new Thread((() =>
            {
                loadingCircle1.Invoke((MethodInvoker)(() =>
                {
                    loadingCircle1.Visible = true;
                    loadingCircle1.Start();
                    label8.Text = "Loading Items from Database...";
                }));

                var att = db.Table<Attachment>();
                foreach (var val in att)
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Loading Attachment \"" + val.Name + "\"..."));

                    attachments.Add(val.ItemID, val);

                    var it = new Items
                    {
                        ItemID = val.ItemID,
                        Category = val.Category,
                        FNAME = val.FNAME,
                        Name = val.Name,
                        StoreIcon = val.FNAME + ".png",
                        Description = val.Description
                    };
                    itemsDB.Add(val.ItemID, it);
                }

                var ge = db.Table<Gear>();
                foreach (var val in ge)
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Loading Gear \"" + val.Name + "\"..."));

                    gears.Add(val.ItemID, val);

                    var it = new Items
                    {
                        ItemID = val.ItemID,
                        Category = val.Category,
                        FNAME = val.FNAME,
                        Name = val.Name,
                        StoreIcon = val.FNAME + ".png",
                        Description = val.Description
                    };
                    itemsDB.Add(val.ItemID, it);
                }

                var gen = db.Table<Generic>();
                foreach (var val in gen)
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Loading Generic \"" + val.Name + "\"..."));

                    generics.Add(val.ItemID, val);

                    var it = new Items
                    {
                        ItemID = val.ItemID,
                        Category = val.Category,
                        FNAME = val.FNAME,
                        Name = val.Name,
                        StoreIcon = val.FNAME + ".png",
                        Description = val.Description
                    };
                    itemsDB.Add(val.ItemID, it);
                }

                var lvil = new List<ListViewItem>();
                var weap = db.Table<Weapon>();
                foreach (var val in weap)
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Loading Weapon \"" + val.Name + "\"..."));

                    weapons.Add(val.ItemID, val);

                    var it = new Items
                    {
                        ItemID = val.ItemID,
                        Category = val.Category,
                        FNAME = val.FNAME,
                        Name = val.Name,
                        StoreIcon = val.FNAME + ".png",
                        Description = val.Description
                    };
                    itemsDB.Add(val.ItemID, it);
                }

                label8.Invoke((MethodInvoker)(() => label8.Text = "Loading Shop Database..."));
                
                using (var reader = XmlReader.Create(Properties.Settings.Default.shopDBFile))
                {
                    while (reader.Read())
                    {
                        if (reader.Name == "si" && reader.IsStartElement())
                        {
                            int itemId;
                            int price;
                            int.TryParse(reader.GetAttribute(0), out itemId);
                            int.TryParse(reader.GetAttribute(1), out price);

                            if (price == 0)
                                int.TryParse(reader.GetAttribute(2), out price);

                            if (!itemsDB.ContainsKey(itemId))
                                continue;

                            var i = itemsDB[itemId];
                            i.pricePerm = price;
                        }
                    }
                }

                label8.Invoke((MethodInvoker)(() => label8.Text = "Adding Items to cache..."));
                foreach (var item in itemsDB)
                {
                    /*const int cat = 1;
                    if (cat == item.Value.Category)
                    {*/
                        if (string.IsNullOrEmpty(item.Value.Name))
                            item.Value.Name = item.Value.FNAME;
                        var lvi = new ListViewItem(item.Value.Name + "\n(" + item.Value.ItemID + ")")
                        {
                            ImageIndex = imageList2.Images.Count
                        };

                        if(Properties.Settings.Default.showColorsEverything)
                            lvi.ForeColor = Helper.getCategoryColor(item.Value.Category);

                        item.Value.binding = lvil.Count;

                        string img = Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" + item.Value.StoreIcon;
                        if (File.Exists(img))
                            imageList2.Images.Add(ImageUtilities.getThumb((Bitmap)Image.FromFile(img), currentRes));
                        else
                            lvi.ImageIndex = 0;

                        //listView2.Items.Add(lvi);
                        lvil.Add(lvi);
                    //}
                }

                loadingCircle1.Invoke((MethodInvoker)(() =>
                {
                    label8.Text = "Finishing...";
                    listView2.Items.AddRange(lvil.ToArray());

                    loadingCircle1.Stop();
                    loadingCircle1.Visible = false;
                }));
            }));
            temp_thread.Start();
        }

        public void LoadItemsDb()
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            panel1.Visible = false;
            imageList2.ImageSize = currentRes;
            imageList2.Images.Clear();
            imageList2.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes));
            listView2.Clear();

            var lvil = new List<ListViewItem>();

            foreach (var i in itemsDB)
            {
                i.Value.binding = -1;

                var checkForText = true;
                if (textBox1.Text != "")
                {
                    const string vars = "img|i|n|d|c";
                    const string ops = "\\$|!\\$|&|!&|!=|<=|>=|=|>|<";

                    var match = Regex.Match(textBox1.Text, "(" + vars + ")(" + ops + ")\"(.*?)\"").Success;
                    var match3 = Regex.Match(textBox1.Text, "(" + vars + ")(" + ops + ")(.*?)").Success;
                    var match2 = Regex.Match(textBox1.Text, "\"(.*?)\"").Success;
                    if (match || match3)
                    {
                        var m = Regex.Matches(textBox1.Text, "(" + vars + ")(" + ops + ")\"(.*?)\"");
                        var mc2 = Regex.Matches(textBox1.Text, "(" + vars + ")(" + ops + ")(.*?)");

                        string text = textBox1.Text.Replace("\"", ""), compareString = "";
                        int number = 0, compareInt = 0;

                        var m1 = (m.Count >= 1) ? m[0].Groups[1].Value : mc2[0].Groups[1].Value;
                        var m2 = (m.Count >= 1) ? m[0].Groups[2].Value : mc2[0].Groups[2].Value;

                        text = text.Replace(m1, "");
                        text = text.Replace(m2, "");
                        int.TryParse(text, out number);
                        switch (m1)
                        {
                            case "i":
                                compareInt = i.Value.ItemID;
                                if (i.Value.itemID == 0)
                                    continue;
                                break;
                            case "n":
                                compareString = i.Value.Name;
                                compareString = compareString.ToLower();
                                text = text.ToLower();
                                if (string.IsNullOrEmpty(i.Value.Name))
                                    continue;
                                break;
                            case "d":
                                compareString = i.Value.Description;
                                if (match3)
                                {
                                    text = text.ToLower();
                                    compareString = compareString.ToLower();
                                }
                                if (string.IsNullOrEmpty(i.Value.Description))
                                    continue;
                                break;
                            case "img":
                                compareString = Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" + i.Value.StoreIcon;
                                if (string.IsNullOrEmpty(i.Value.StoreIcon))
                                    continue;
                                break;
                            case "c":
                                if(number != -999)
                                    compareInt = i.Value.Category;
                                else
                                    compareString = Helper.fromItemsCategory(i.Value.Category);
                                if (i.Value.Category == 0)
                                    continue;
                                break;
                        }
                        switch (m2)
                        {
                            default:
                                errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleLeft);
                                errorProvider1.SetError(textBox1, "No valid operator found!");
                                return;
                            case "&":
                                if (!string.IsNullOrEmpty(compareString))
                                    checkForText =
                                        File.Exists(
                                            Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" + i.Value.StoreIcon);
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
                                            Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" + i.Value.StoreIcon);
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
                        var text = textBox1.Text.Replace("\"", "");
                        checkForText = i.Value.name.Contains(text);
                    }
                    else
                        checkForText = i.Value.name.ToLower().Contains(textBox1.Text.ToLower());
                }
                if (checkForText && (i.Value.Category.ToString(CultureInfo.InvariantCulture) == comboBox1.SelectedValue.ToString() || comboBox1.SelectedValue.ToString() == "0"))
                {
                    var img = Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" + i.Value.StoreIcon;
                    //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                    if(tabControl1.SelectedIndex == 0)
                        i.Value.binding = lvil.Count;

                    var lvi = new ListViewItem(i.Value.Name + "\n(" + i.Value.ItemID + ")")
                    {
                        ImageIndex = imageList2.Images.Count
                    };

                    if (Properties.Settings.Default.showColorsEverything && comboBox1.SelectedValue.ToString() == "0")
                        lvi.ForeColor = Helper.getCategoryColor(i.Value.Category);

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

        public void LoadShopXml()
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Start();

            panel1.Visible = false;
            listView1.Clear();
            imageList1.ImageSize = currentRes2;
            imageList1.Images.Clear();
            imageList1.Images.Add(ImageUtilities.getThumb(Resources.no_icon, currentRes2));

            var lvil = new List<ListViewItem>();

            var td = new Thread((() =>
            {
                foreach (var i in itemsDB)
                {
                    i.Value.binding = -1;

                    if (i.Value.pricePerm <= 0 || i.Value.pricePerm == -999)
                        continue;
                    //if (tabControl1.SelectedIndex == 1)
                        i.Value.binding = listView1.Items.Count;
                    var img = i.Value.image.Replace("$Data", Properties.Settings.Default.dataFolder);
                    img = img.Replace(".dds", ".png");
                    //DDSImage img = new DDSImage(File.ReadAllBytes(icon));

                    var lvi = new ListViewItem(i.Value.name + "\n" + i.Value.pricePerm + " GD")
                    {
                        ImageIndex = imageList1.Images.Count
                    };

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
            LoadItemsDb();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, String.Empty);
            LoadItemsDb();
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
            LoadItemsDb();
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

            LoadShopXml();
        }

        public void LoadInformation(ListView obj)
        {
            if (obj.SelectedIndices.Count == 1)
            {
                panel1.Visible = true;
                foreach (var i in itemsDB)
                {
                    if (i.Value.binding == obj.SelectedIndices[0])
                    {
                        var price = i.Value.pricePerm + " GD";
                        if (i.Value.pricePerm == -999 || i.Value.pricePerm == 0)
                        {
                            label4.Visible = false;
                            label7.Visible = false;
                        }
                        else
                        {
                            label4.Visible = true;
                            label7.Visible = true;
                        }
                        var img = Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" +
                                  i.Value.StoreIcon;
                        if (File.Exists(img))
                            pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                                new Size(128, 128));
                        else
                            pictureBox1.Image = ImageUtilities.getThumb(Resources.no_icon, new Size(128, 128));

                        label1.Text = i.Value.Description;
                        label5.Text = i.Value.Name + @" (" + i.Value.ItemID + @")";
                        label6.Text = Helper.fromItemsCategory(i.Value.Category);
                        if (Helper.fromItemsCategory(i.Value.Category) == "Unknown")
                            Console.WriteLine(i.Value.Name + @" has an invalid category of " + i.Value.Category);
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
                    LoadItemsDb();
                    break;
                case 1:
                    LoadShopXml();
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
            foreach (var i in itemsDB)
            {
                if (i.Value.binding == listView2.SelectedIndices[0])
                {
                    itemsDB.Remove(i.Key);
                    LoadItemsDb();
                    break;
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (var i in itemsDB)
            {
                if (i.Value.binding == listView2.SelectedIndices[0])
                {
                    Items.showEditForm(i.Value);

                    var ei = new EditItem(i.Value);
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
            var xmlWriterSettings = new XmlWriterSettings {NewLineOnAttributes = false, Indent = true};
            if (tabControl1.SelectedIndex == 0)
            {
                writer = XmlWriter.Create(@"D:\Server\Open-WarZ\source\bin\Data\Weapons\out.xml",
                    xmlWriterSettings);
                writer.WriteStartElement("DB");
                writer.WriteStartElement("WeaponsArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.internalCategory == 0)
                        Weapon.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("FoodArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 30 || i.Value.Category == 33 && i.Value.internalCategory == 4)
                        Food.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("GearArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 13 && i.Value.internalCategory == 1)
                        Gear.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("HeroArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 16 && i.Value.internalCategory == 5)
                        Hero.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("BackpackArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 12 && i.Value.internalCategory == 6)
                        Backpack.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("ItemsDB");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 1 && i.Value.internalCategory == 4)
                        Generic.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("AttachmentArmory");
                foreach (var i in itemsDB)
                {
                    if (i.Value.category == 19 && i.Value.internalCategory == 2)
                        Attachment.writeXML(writer, i);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("LootBoxDB");
                foreach (var i in itemsDB)
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
                foreach (var i in itemsDB)
                {
                    if (i.Value.pricePerm <= 0)
                        continue;

                    writer.WriteStartElement("si");
                    writer.WriteAttributeString("itemId", i.Value.itemID.ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("pricePerm", "0");
                    writer.WriteAttributeString("gdPricePerm", i.Value.pricePerm.ToString(CultureInfo.InvariantCulture));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Close();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = new Settings();
            if (s.ShowDialog() == DialogResult.OK)
            {
                listView2.Font = Properties.Settings.Default.listFont;
                switch (tabControl1.SelectedIndex)
                {
                    default:
                        LoadItemsDb();
                        break;
                    case 1:
                        LoadShopXml();
                        break;
                }
            }
        }

        private void importItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog
            {
                Filter = @"ItemsDB.xml|itemsDB.xml",
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };
            if (of.ShowDialog() != DialogResult.OK)
                return;
            var tp = new Panel {Size = Size, Location = new Point(0, 0), BackColor = SystemColors.Control};

            var lc = new LoadingCircle
            {
                ThemeColor = Color.DodgerBlue,
                OutnerCircleRadius = 30,
                InnerCircleRadius = 10,
                LineWidth = 3,
                Size = new Size(64, 64)
            };
            lc.Location = new Point((Size.Width / 2) - lc.Size.Width, (Size.Height / 2) - lc.Size.Height);

            var lb = new Label {Text = "Import your items...", AutoSize = true};
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

                using (var reader = XmlReader.Create(of.FileName))
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
                                    var attach = Attachment.readXML(reader);

                                    if (!attachments.ContainsKey(attach.ItemID))
                                        attachments.Add(attach.ItemID, attach);
                                    break;
                                case "Backpack":
                                    var _backpack = Gear.readXML(reader);

                                    if (!gears.ContainsKey(_backpack.ItemID))
                                        gears.Add(_backpack.ItemID, _backpack);
                                    break;
                                case "Item":
                                    // Hack because of several items.
                                    // 1 - Generic
                                    // 30 - Food
                                    var cat = Helper.getInt(reader.GetAttribute(1));
                                    //i =  cat == 1 ? Generic.readXML(reader) : Food.readXML(reader);

                                    if (cat == 1)
                                    {
                                        var _generic = Generic.readXML(reader);

                                        if (!generics.ContainsKey(_generic.ItemID))
                                            generics.Add(_generic.ItemID, _generic);
                                    }
                                    else if (cat == 30 || cat == 33)
                                    {
                                        var wSQL = Weapon.readXML(reader);

                                        if (!weapons.ContainsKey(wSQL.ItemID))
                                            weapons.Add(wSQL.ItemID, wSQL);
                                    }
                                    break;
                                case "Gear":
                                    var _gear = Gear.readXML(reader);

                                    if (!gears.ContainsKey(_gear.ItemID))
                                        gears.Add(_gear.ItemID, _gear);
                                    break;
                                case "Hero":
                                    var _hero = Gear.readXML(reader);

                                    if (!gears.ContainsKey(_hero.ItemID))
                                        gears.Add(_hero.ItemID, _hero);
                                    break;
                                case "LootBox":
                                    var _lootbox = Generic.readXML(reader);

                                    if (!generics.ContainsKey(_lootbox.ItemID))
                                        generics.Add(_lootbox.ItemID, _lootbox);
                                    break;
                                case "Weapon":
                                    var wSQL2 = Weapon.readXML(reader);
                                    //WeaponSQL wSQL2 = WeaponSQL.Add(db, i);

                                    if(!weapons.ContainsKey(wSQL2.ItemID))
                                        weapons.Add(wSQL2.ItemID, wSQL2); // Bugfix

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

                foreach (var a in attachments)
                {
                    var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Attachment WHERE itemID=?", a.Value.ItemID);
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

                foreach (var g in gears)
                {
                    var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Gear WHERE itemID=?", g.Value.ItemID);
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

                foreach (var g in generics)
                {
                    var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Generic WHERE itemID=?", g.Value.ItemID);
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

                foreach (var w in weapons)
                {
                    var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Weapon WHERE itemID=?", w.Value.ItemID);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
