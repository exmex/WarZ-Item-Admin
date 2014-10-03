using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarZLocal_Admin.Properties;

namespace WarZLocal_Admin
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            loadingCircle1.Start();
        }

        public void ChangeItem(KeyValuePair<int, Items> i)
        {
            if (!panel1.Visible)
            {
                loadingCircle1.Stop();
                panel1.Visible = true;
            }

            var img = Properties.Settings.Default.dataFolder + "\\Weapons\\StoreIcons\\" +
                                  i.Value.StoreIcon;
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

            if (File.Exists(img))
                pictureBox1.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                    new Size(128, 128));
            else
                pictureBox1.Image = ImageUtilities.getThumb(Resources.no_icon, new Size(128, 128));

            label1.Text = i.Value.Description;
            label5.Text = i.Value.Name + @" (" + i.Value.ItemID + @")";
            label6.Text = Helper.fromItemsCategory(i.Value.Category);
            label7.Text = price;
            if (Helper.fromItemsCategory(i.Value.Category) == "Unknown")
                Console.WriteLine(i.Value.Name + @" has an invalid category of " + i.Value.Category);
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
