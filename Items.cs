using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SQLite;
using WarZLocal_Admin.Properties;

namespace WarZLocal_Admin
{
    public class AttachmentSQL
    {
        public int ItemID { get; set; }
        [MaxLength(32)]
        public string FNAME { get; set; }
        public int Type { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [MaxLength(64)]
        public string MuzzleParticle { get; set; }
        [MaxLength(256)]
        public string FireSound { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }
        public float Firerate { get; set; }
        public float Recoil { get; set; }
        public float Spread { get; set; }
        public int Clipsize { get; set; }
        public float ScopeMag { get; set; }
        [MaxLength(32)]
        public string ScopeType { get; set; }
        [MaxLength(32)]
        public string AnimPrefix { get; set; }
        public int SpecID { get; set; }
        public int Category { get; set; }

        public int Price1 { get; set; }
        public int Price7 { get; set; }
        public int Price30 { get; set; }
        public int PriceP { get; set; }

        public int GPrice1 { get; set; }
        public int GPrice7 { get; set; }
        public int GPrice30 { get; set; }
        public int GPriceP { get; set; }

        public int IsNew { get; set; }
        public int LevelRequired { get; set; }
        public int Weight { get; set; }

        public static void Add(SQLiteConnection db, Items i)
        {
            if (i.pricePerm == -999)
                i.pricePerm = 0;
            AttachmentSQL a = new AttachmentSQL();
            a.ItemID = i.itemID;
            a.FNAME = i.fname;
            a.Type = i.attachType;
            a.Name = i.name;
            a.Description = i.desc;
            a.MuzzleParticle = i.MuzzleParticle;
            a.FireSound = i.FireSound;
            a.Damage = i.upgradeDamage;
            a.Range = i.upgradeRange;
            a.Firerate = i.upgradeFirerate;
            a.Recoil = i.upgradeRecoil;
            a.Spread = i.upgradeSpread;
            a.Clipsize = i.upgradeClipsize;
            a.ScopeMag = i.upgradeScopeMag;
            a.ScopeType = i.upgradeScopeType;
            a.AnimPrefix = i.animPrefix;
            a.SpecID = i.SpecID;
            a.Category = i.category;
            a.Price1 = i.pricePerm;
            a.Price7 = i.pricePerm;
            a.Price30 = i.pricePerm;
            a.PriceP = i.pricePerm;
            a.GPrice1 = i.pricePerm;
            a.GPrice7 = i.pricePerm;
            a.GPrice30 = i.pricePerm;
            a.GPriceP = i.pricePerm;
            a.IsNew = 0;
            a.LevelRequired = 0;
            a.Weight = i.weight;
            //var s = db.Insert(a);
            /*
            db.ExecuteScalarAsync<int>("select count(*) from AttachmentSQL WHERE itemID=?", a.ItemID).ContinueWith((t) =>
            {
                if (t.Result == 0)
                {
                    db.InsertAsync(a).ContinueWith((t2) =>
                    {
                        Console.WriteLine("New Weapon itemID: {0}", a.ItemID);
                    });
                }
            });*/
        }
    }

    public class GearSQL
    {
        public int ItemID { get; set; }
        [MaxLength(32)]
        public string FNAME { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public int Category { get; set; }
        public int Weight { get; set; }
        public int DamagePerc { get; set; }
        public int DamageMax { get; set; }
        public int Bulkiness { get; set; }
        public int Inaccuracy { get; set; }
        public int Stealth { get; set; }

        public int Price1 { get; set; }
        public int Price7 { get; set; }
        public int Price30 { get; set; }
        public int PriceP { get; set; }

        public int IsNew { get; set; }
        public int ProtectionLevel { get; set; }
        public int LevelRequired { get; set; }

        public int GPrice1 { get; set; }
        public int GPrice7 { get; set; }
        public int GPrice30 { get; set; }
        public int GPriceP { get; set; }

        public static void Add(SQLiteConnection db, Items i)
        {
            if (i.pricePerm == -999)
                i.pricePerm = 0;
            GearSQL a = new GearSQL();
            a.ItemID = i.itemID;
            a.FNAME = i.fname;
            a.Name = i.name;
            a.Description = i.desc;
            a.Category = i.category;
            a.Weight = i.weight;
            a.DamagePerc = i.damagePerc;
            a.DamageMax = i.damageMax;
            a.Bulkiness = i.bulkiness;
            a.Inaccuracy = i.inaccuracy;
            a.Stealth = i.stealth;
            a.Price1 = i.pricePerm;
            a.Price7 = i.pricePerm;
            a.Price30 = i.pricePerm;
            a.PriceP = i.pricePerm;

            a.IsNew = 0;
            a.LevelRequired = 0;
            a.ProtectionLevel = i.protectionLevel;

            a.GPrice1 = i.pricePerm;
            a.GPrice7 = i.pricePerm;
            a.GPrice30 = i.pricePerm;
            a.GPriceP = i.pricePerm;
            //var s = db.Insert(a);
            /*
            db.ExecuteScalarAsync<int>("select count(*) from GearSQL WHERE itemID=?", a.ItemID).ContinueWith((t) =>
            {
                if (t.Result == 0)
                {
                    db.InsertAsync(a).ContinueWith((t2) =>
                    {
                        Console.WriteLine("New Gear itemID: {0}", a.ItemID);
                    });
                }
            });*/
        }
    }

    public class GenericSQL
    {
        public int ItemID { get; set; }
        [MaxLength(32)]
        public string FNAME { get; set; }
        public int Category { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string Description { get; set; }

        public int Price1 { get; set; }
        public int Price7 { get; set; }
        public int Price30 { get; set; }
        public int PriceP { get; set; }

        public int IsNew { get; set; }
        public int LevelRequired { get; set; }

        public int GPrice1 { get; set; }
        public int GPrice7 { get; set; }
        public int GPrice30 { get; set; }
        public int GPriceP { get; set; }
        public int Weight { get; set; }

        public static void Add(SQLiteConnection db, Items i)
        {
            if (i.pricePerm == -999)
                i.pricePerm = 0;
            GenericSQL a = new GenericSQL();
            a.ItemID = i.itemID;
            a.FNAME = i.fname;
            a.Category = i.category;
            a.Name = i.name;
            a.Description = i.desc;
            a.Price1 = i.pricePerm;
            a.Price7 = i.pricePerm;
            a.Price30 = i.pricePerm;
            a.PriceP = i.pricePerm;

            a.IsNew = 0;
            a.LevelRequired = 0;

            a.GPrice1 = i.pricePerm;
            a.GPrice7 = i.pricePerm;
            a.GPrice30 = i.pricePerm;
            a.GPriceP = i.pricePerm;
            a.Weight = i.weight;
            //var s = db.Insert(a);
            /*
            db.ExecuteScalarAsync<int>("select count(*) from GenericSQL WHERE itemID=?", a.ItemID).ContinueWith((t) =>
            {
                if (t.Result == 0)
                {
                    db.InsertAsync(a).ContinueWith((t2) =>
                    {
                        Console.WriteLine("New Generic itemID: {0}", a.ItemID);
                    });
                }
            });*/
        }
    }

    public class LootDataSQL
    {
        [PrimaryKey, AutoIncrement]
        public int RecordID { get; set; }

        public int LootID { get; set; }
        public float Chance { get; set; }
        public int ItemID { get; set; }
        public int GDMin { get; set; }
        public int GDMax { get; set; }
    }

    public class WeaponSQL
    {
        public int ItemID { get; set; }
        [MaxLength(32)]
        public string FNAME { get; set; }
        public int Category { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string MuzzleOffset { get; set; }
        [MaxLength(32)]
        public string MuzzleParticle { get; set; }
        [MaxLength(32)]
        public string Animation { get; set; }
        public float BulletID { get; set; }
        [MaxLength(255)]
        public string Sound_Shot { get; set; }
        [MaxLength(255)]
        public string Sound_Reload { get; set; }
        public float Damage { get; set; }
        public int isImmediate { get; set; }
        public float Mass { get; set; }
        public float Speed { get; set; }
        public float DamageDecay { get; set; }
        public float Area { get; set; }
        public float Delay { get; set; }
        public float Timeout { get; set; }
        public int NumClips { get; set; }
        public int Clipsize { get; set; }
        public float ReloadTime { get; set; }
        public float ActiveReloadTick { get; set; }
        public int RateOfFire { get; set; }
        public float Spread { get; set; }
        public float Recoil { get; set; }
        public int NumGrenades { get; set; }
        [MaxLength(32)]
        public string GrenadeName { get; set; }
        [MaxLength(3)]
        public string Firemode { get; set; }
        public int DetectionRadius { get; set; }
        [MaxLength(32)]
        public string ScopeType { get; set; }
        public int ScopeZoom { get; set; }

        public int Price1 { get; set; }
        public int Price7 { get; set; }
        public int Price30 { get; set; }
        public int PriceP { get; set; }

        public int IsNew { get; set; }
        public int LevelRequired { get; set; }

        public int GPrice1 { get; set; }
        public int GPrice7 { get; set; }
        public int GPrice30 { get; set; }
        public int GPriceP { get; set; }


        /* War Inc shit? */
        public int ShotsFired { get; set; }
        public int ShotsHits { get; set; }
        public int KillsCQ { get; set; }
        public int KillsDM { get; set; }
        public int KillsSB { get; set; }
        public int IsUpgradeable { get; set; }
        /* --------------- */
        public int IsFPS { get; set; }
        public int FPSSpec0 { get; set; }
        public int FPSSpec1 { get; set; }
        public int FPSSpec2 { get; set; }
        public int FPSSpec3 { get; set; }
        public int FPSSpec4 { get; set; }
        public int FPSSpec5 { get; set; }
        public int FPSSpec6 { get; set; }
        public int FPSSpec7 { get; set; }
        public int FPSSpec8 { get; set; }

        public int FPSAttach0 { get; set; }
        public int FPSAttach1 { get; set; }
        public int FPSAttach2 { get; set; }
        public int FPSAttach3 { get; set; }
        public int FPSAttach4 { get; set; }
        public int FPSAttach5 { get; set; }
        public int FPSAttach6 { get; set; }
        public int FPSAttach7 { get; set; }
        public int FPSAttach8 { get; set; }

        [MaxLength(32)]
        public string AnimPrefix { get; set; }
        public int Weight { get; set; }

        public static WeaponSQL Add(SQLiteConnection db, Items i)
        {
            foreach (var p in i.GetType().GetFields())
            {
                if (p.GetValue(i) == null || string.IsNullOrEmpty(p.GetValue(i).ToString()) ||
                    p.GetValue(i).ToString() == "-999")
                {
                    if (p.GetValue(i) is int)
                        p.SetValue(i, 0);
                    else if(p.GetValue(i) is float)
                        p.SetValue(i, 0.0f);
                    else if (p.GetValue(i) is bool?)
                        p.SetValue(i, false);
                    else if (p.GetValue(i) is string)
                        p.SetValue(i, "");
                }
            }
            WeaponSQL a = new WeaponSQL();
            a.ItemID = i.itemID;
            a.FNAME = i.fname;
            a.Category = i.category;
            a.Name = i.name;
            a.Description = i.desc;
            a.MuzzleOffset = i.muzzlerOffesetX + " " + i.muzzlerOffesetY + " " + i.muzzlerOffesetZ;
            a.MuzzleParticle = i.MuzzleParticle;
            a.Animation = i.type;
            a.BulletID = i.bullet;
            a.Sound_Shot = i.shoot;
            a.Sound_Reload = i.reload;
            a.Damage = i.damage;
            a.isImmediate = (i.immediate == true) ? 1 : 0;
            a.Mass = i.mass;
            a.Speed = i.speed;
            a.DamageDecay = i.decay;
            a.Area = i.area;
            a.Delay = i.delay;
            a.Timeout = i.timeout;
            a.NumClips = i.numShells;
            a.Clipsize = i.clipSize;
            a.ReloadTime = i.reloadTime;
            a.ActiveReloadTick = i.activeReloadTick;
            a.RateOfFire = i.rateOfFire;
            a.Spread = i.spread;
            a.Recoil = i.recoil;
            a.NumGrenades = i.numgrenades;
            a.GrenadeName = i.grenadename;
            a.Firemode = i.firemode.ToString();
            a.DetectionRadius = 0; // Unknown
            a.ScopeType = i.ScopeType;
            a.ScopeZoom = i.ScopeZoom;
            a.Price1 = i.pricePerm;
            a.Price7 = i.pricePerm;
            a.Price30 = i.pricePerm;
            a.PriceP = i.pricePerm;
            a.IsNew = 0;
            a.LevelRequired = 0;
            a.GPrice1 = i.pricePerm;
            a.GPrice7 = i.pricePerm;
            a.GPrice30 = i.pricePerm;
            a.GPriceP = i.pricePerm;
            a.ShotsFired = 0;
            a.ShotsHits = 0;
            a.KillsCQ = 0;
            a.KillsDM = 0;
            a.KillsSB = 0;
            a.IsUpgradeable = 0;
            a.IsFPS = i.IsFPS;
            a.FPSSpec0 = i.i0;
            a.FPSSpec1 = i.i1;
            a.FPSSpec2 = i.i2;
            a.FPSSpec3 = i.i3;
            a.FPSSpec4 = i.i4;
            a.FPSSpec5 = i.i5;
            a.FPSSpec6 = i.i6;
            a.FPSSpec7 = i.i7;
            a.FPSSpec8 = i.i8;

            a.FPSAttach0 = i.d0;
            a.FPSAttach1 = i.d1;
            a.FPSAttach2 = i.d2;
            a.FPSAttach3 = i.d3;
            a.FPSAttach4 = i.d4;
            a.FPSAttach5 = i.d5;
            a.FPSAttach6 = i.d6;
            a.FPSAttach7 = i.d7;
            a.FPSAttach8 = i.d8;

        
            a.AnimPrefix = i.animPrefix;
            a.Weight = i.weight;

            a.ItemID = i.itemID;
            a.FNAME = i.fname;
            a.Category = i.category;
            a.Name = i.name;
            a.Description = i.desc;
            a.Price1 = i.pricePerm;
            a.Price7 = i.pricePerm;
            a.Price30 = i.pricePerm;
            a.PriceP = i.pricePerm;

            a.IsNew = 0;
            a.LevelRequired = 0;

            a.GPrice1 = i.pricePerm;
            a.GPrice7 = i.pricePerm;
            a.GPrice30 = i.pricePerm;
            a.GPriceP = i.pricePerm;
            a.Weight = i.weight;

            return a;

            //var s = db.Insert(a);
            /*db.ExecuteScalarAsync<int>("select count(*) from WeaponSQL WHERE itemID=?", a.ItemID).ContinueWith((t) =>
            {
                if (t.Result == 0)
                {
                    db.InsertAsync(a).ContinueWith((t2) =>
                    {
                        Console.WriteLine("New Weapon itemID: {0}", a.ItemID);
                    });
                }
            });*/
        }
    }

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

        public int ItemID = 0;
        public string Name = "";
        public string FNAME = "";
        public string Description = "";
        public int Category = 0;
        public string StoreIcon = "";

        
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
            pb.Location = new Point(64, 5);
            if (Properties.Settings.Default.itemEditDisplay == 1)
                pb.Location = new Point(176, 5);
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
                    string img = iv.Value.Replace("$Data", Properties.Settings.Default.dataFolder);
                    img = img.Replace(".dds", ".png");
                    if (File.Exists(img))
                        pb.Image = ImageUtilities.getThumb((Bitmap)Image.FromFile(img),
                            new Size(128, 128));
                    else
                        pb.Image = ImageUtilities.getThumb(Resources.no_icon, new Size(128, 128));
                }

                if (Properties.Settings.Default.itemEditDisplay == 1)
                {
                    if ((rows >= 15 && cols < 2) || (cols >= 2 && rows >= 20))
                    {
                        int y = 138;
                        if (cols >= 1)
                            y = 15;
                        start = new Point(cols*250, y);
                        cols++;
                        rows = 0;
                    }
                }
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
