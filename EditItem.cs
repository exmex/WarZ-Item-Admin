using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WarZLocal_Admin
{
    public partial class EditItem : Form
    {
        public static string dataFolder = Properties.Settings.Default.dataFolder;

        public Items buffer;

        public EditItem(Items it)
        {
            InitializeComponent();
            textBox1.Text = it.name;
            textBox2.Text = ""+ it.damage;
            textBox3.Text = ""+ it.weight;
            textBox4.Text = it.image;
            textBox5.Text = ""+ it.itemID;
            textBox6.Text = "" + it.pricePerm;
            richTextBox1.Text = it.desc;
            buffer = it;
            string image = it.image.Replace("$Data", dataFolder).Replace(".dds", ".png");
            if (File.Exists(image))
                pictureBox1.Image = ImageUtilities.getThumb((Bitmap) Image.FromFile(image), new Size(128, 128));
            else
                pictureBox1.Image =
                    ImageUtilities.getThumb(
                        (Bitmap)Image.FromFile(dataFolder + "/Weapons/no_icon.png"), new Size(128, 128));
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(pictureBox2, "0 means hidden from shop");

            textBox4.LostFocus += (EventHandler)((s, ev) =>
            {
                string image = textBox4.Text.Replace("$Data", dataFolder).Replace(".dds", ".png");
                if(File.Exists(image))
                    pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(image), new Size(128, 128));
                else
                    pictureBox1.Image =
                    ImageUtilities.getThumb(
                        (Bitmap)Image.FromFile(dataFolder + "/Weapons/no_icon.png"), new Size(128, 128));
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int itemID, price;
            int.TryParse(textBox5.Text, out itemID);
            int.TryParse(textBox6.Text, out price);

            buffer.name = textBox1.Text;
            buffer.image = textBox4.Text;
            buffer.itemID = itemID;
            buffer.pricePerm = price;
            buffer.desc = richTextBox1.Text;
            //textBox2.Text = "" + it.itemID;
            //textBox3.Text = "" + it.itemID;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
