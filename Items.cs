using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace WarZLocal_Admin
{
    public class Items
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable SpecifyACultureInStringConversionExplicitly
        /*<Weapon itemID="101002" category="20" upgrade="1" FNAME="ASR_M16" Weight="4000">
            <Model file="Data/ObjectsDepot/Weapons/ASR_M16.sco" AnimPrefix="ASR_M16" muzzlerOffset.x="0" muzzlerOffset.y="0" muzzlerOffset.z="0" />
            <MuzzleModel file="muzzle_asr" />
            <HudIcon file="$Data/Weapons/HudIcons/ASR_M16.dds" />
            <Store name="M16" icon="$Data/Weapons/StoreIcons/ASR_M16.dds" desc="The M16 rifle is a gas operated standard infantry assault rifle, chambered for 5.56x45mm NATO round." LevelRequired="0" />
            <PrimaryFire bullet="5.56" damage="27" immediate="false" mass="1" decay="300" speed="500" area="0" delay="0" timeout="0" numShells="0" clipSize="1" reloadTime="2.5" activeReloadTick="0.43"
         *      rateOfFire="625" spread="9" recoil="7" numgrenades="0" grenadename="asr_grenade" firemode="101" ScopeType="default" ScopeZoom="0" />
            <Animation type="assault" />
            <Sound shoot="Sounds/NewWeapons/Assault/ColtM16" reload="Sounds/Weapons/New Reloads/M16-Reload" />
            <FPS IsFPS="1" i0="2001" i1="5001" i2="3001" i3="1001" i4="4001" i5="0" i6="0" i7="0" i8="0" d0="0" d1="400127" d2="0" d3="0" d4="400016" d5="0" d6="0" d7="0" d8="0" />
        </Weapon>
         <Item itemID="101292" category="30" Weight="350">
			<Model file="Data/ObjectsDepot/Weapons/Consumables_Can_Spam_01.sco" />
			<Store name="Can of Ham" icon="$Data/Weapons/StoreIcons/Consumables_Can_Spam_01.dds" desc="Mechanically separated Spiced Ham in a 5-ounce cans. May contain pig parts. Relieves moderate hunger." LevelRequired="0" />
			<Property health="5" toxicity="0" water="0" food="35" stamina="0" shopSS="1" />
		</Item>
         */

        // <Weapon>
        public int itemID = -999;          // 0
        public int internalCategory = -999;// 1
        //-------------------------------- ATTACHMENT SPECIAL
        public int attachType = -999;          // 2
        public int SpecID = -999;              // 3
        //----------------------------------------------------
        public int upgrade = -999;         // 2
        public string fname;        // 3
        public int weight = -999;          // 4

        // <Model>
        public string modelFile;        // 0
        //-------------------------------- ATTACHMENT SPECIAL
        public string MuzzleParticle;       // 1
        public string FireSound;            // 2
        public string ScopeAnim;            // 3
        //----------------------------------------------------
        public string animPrefix;       // 1
        public int muzzlerOffesetX = -999;     // 2
        public int muzzlerOffesetY = -999;     // 3
        public int muzzlerOffesetZ = -999;     // 4

        // <MuzzleModel>
        public string muzzleModelFile; // 0

        // <HudIcon>
        public string hudIconFile; // 0

        // <Store>
        public string name;         // 0
        public string image;        // 1
        public string desc;         // 2
        public int levelRequired = -999;   // 3
        
        // <PrimaryFire>
        public float bullet = -999;            // 0
        public int damage = -999;              // 1
        public Nullable<bool> immediate = null;          // 2
        public int mass = -999;                // 3
        public int decay = -999;               // 4
        public int speed = -999;               // 5
        public int area = -999;                // 6
        public int delay = -999;               // 7
        public int timeout = -999;             // 8
        public int numShells = -999;           // 9
        public int clipSize = -999;            // 10
        public float reloadTime = -999;        // 11
        public float activeReloadTick = -999;  // 12
        public int rateOfFire = -999;          // 13
        public int spread = -999;              // 14
        public int recoil = -999;              // 15
        public int numgrenades = -999;         // 16
        public string grenadename;      // 17
        public int firemode = -999;            // 18
        public string ScopeType;        // 19
        public int ScopeZoom = -999;           // 20

        // <Animation>
        public string type; // 0

        // <Sound>
        public string shoot; // 0
        public string reload; // 1

        // <FPS>
        public int IsFPS = -999;   // 0
        public int i0 = -999;      // 1
        public int i1 = -999;      // 2
        public int i2 = -999;      // 3
        public int i3 = -999;      // 4
        public int i4 = -999;      // 5
        public int i5 = -999;      // 6
        public int i6 = -999;      // 7
        public int i7 = -999;      // 8
        public int i8 = -999;      // 9
        public int d0 = -999;      // 10
        public int d1 = -999;      // 11
        public int d2 = -999;      // 12
        public int d3 = -999;      // 13
        public int d4 = -999;      // 14
        public int d5 = -999;      // 15
        public int d6 = -999;      // 16
        public int d7 = -999;      // 17
        public int d8 = -999;      // 18

        /*---------- Item -----------*/
        // <Property>
        public int health = -999;
        public int toxicity = -999;
        public int water = -999;
        public int food = -999;
        public int stamina = -999;
        public int shopSS = -999;

        /*---------- Gear ------------*/
        // <Armor>
        public int damagePerc = -999;
        public int damageMax = -999;
        public int bulkiness = -999;
        public int inaccuracy = -999;
        public int stealth = -999;
        public int protectionLevel = -999;

        /*---------- Hero ------------*/
        // <HeroDesc>
        public int heroDescDamagePerc = -999;
        public int heroDescDamageMax = -999;
        public int maxHeads = -999;
        public int maxBodys = -999;
        public int maxLegs = -999;
        public int heroDescProtectionLevel = -999;

        /*---------- Backpack ----------*/
        // <Desc>
        public int maxSlots = -999;
        public int maxWeight = -999;

        /*---------- Attachment ---------*/
        // <Upgrade>
        public int upgradeDamage = -999;
        public int upgradeRange = -999;
        public int upgradeFirerate = -999;
        public int upgradeRecoil = -999;
        public int upgradeSpread = -999;
        public int upgradeClipsize = -999;
        public int upgradeScopeMag = -999;
        public string upgradeScopeType;

        public int pricePerm = -999;
        
        /*
         * 0 - Weapon
         * 1 - Gear
         * 2 - Attachment
         * 3 - LootBox
         * 4 - Items
         */
        public int category;

        public int binding;

        public static bool showEditForm(Items i)
        {
            var items = editForm(i);
            Form frm = new Form();
            frm.BackColor = Color.White;
            //frm.Size = new Size(480, 580);
            frm.Size = new Size(255, 580);
            frm.Text = "Edit " + i.name;

            Panel pan = new Panel();
            pan.AutoScroll = true;
            pan.Dock = DockStyle.Fill;
            pan.BackColor = Color.White;
            pan.AutoScrollMargin = new Size(10, 10);

            PictureBox pb = new PictureBox();
            pb.Size = new Size(128, 128);
            //pb.Location = new Point(176, 5);
            pb.Location = new Point(64, 5);
            pan.Controls.Add(pb);

            Point start = new Point(5, 138);
            int rows = 0;
            int cols = 1;

            foreach (var iv in items)
            {
                if (iv.Value == null || iv.Key == null || string.IsNullOrEmpty(iv.Value) || iv.Value == "-999" || iv.Key == "binding" || iv.Key == "internalCategory")
                    continue;

                if (iv.Key == "image")
                {
                    string img = iv.Value.Replace("$Data", "D:/Server/Open-WarZ/source/bin/Data");
                    img = img.Replace(".dds", ".png");
                    if (File.Exists(img))
                        pb.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                            new Size(128, 128));
                    else
                        pb.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile("D:/Server/Open-WarZ/source/bin/Data/Weapons/no_icon.png"), new Size(128, 128));
                }

                /*
                if ((rows >= 15 && cols < 2) || (cols >= 2 && rows >= 20))
                {
                    int y = 138;
                    if (cols >= 1)
                        y = 15;
                    start = new Point(cols*250, y);
                    cols++;
                    rows = 0;
                }*/
                Label lb = new Label();
                lb.Location = start;
                lb.Text = iv.Key;

                TextBox tb = new TextBox();
                tb.Location = new Point(start.X + lb.Size.Width + 6, start.Y);
                tb.Text = iv.Value;

                start.Y += tb.Size.Height + 5;

                pan.Controls.Add(lb);
                pan.Controls.Add(tb);

                rows++;
            }

            frm.Controls.Add(pan);
            frm.ShowDialog();

            frm.Dispose();

            return true;
        }

        public static Dictionary<string, string> editForm(Items i)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var s = i;
            foreach (var p in s.GetType().GetFields())
            {
                if (p.GetValue(i) == null)
                {
                    Console.WriteLine(p.Name);
                    continue;
                }
                try
                {
                    dic.Add(p.Name, p.GetValue(i).ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return dic;
        }
    }
}
