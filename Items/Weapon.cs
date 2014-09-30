using System.Collections.Generic;
using System.IO;
using System.Xml;
using SQLite;

namespace WarZLocal_Admin
{
    class Weapon
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

        /*
         <Weapon itemID="101002" category="20" upgrade="1" FNAME="ASR_M16" Weight="4000">
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
         */

        public static Weapon readXML(XmlReader reader)
        {
            Weapon i = new Weapon();

            i.ItemID = Helper.getInt(reader.GetAttribute(0));
            i.Category = Helper.getInt(reader.GetAttribute(1));
            //i.internalCategory = 0;
            if (i.Category != 30)
            {
                i.IsUpgradeable = Helper.getInt(reader.GetAttribute(2));
                i.FNAME = reader.GetAttribute(3);
                i.Weight = Helper.getInt(reader.GetAttribute(4));
            }else
                i.Weight = Helper.getInt(reader.GetAttribute(2));

            XmlReader subs = reader.ReadSubtree();
            while (subs.Read())
            {
                if (!subs.HasAttributes)
                    continue;
                switch (subs.Name)
                {
                    case "Model":
                        //i.file = subs.GetAttribute(0);
                        if (i.Category == 30)
                            i.FNAME = Path.GetFileNameWithoutExtension(subs.GetAttribute(0));
                        else
                        {
                            i.AnimPrefix = subs.GetAttribute(1);
                            i.MuzzleOffset = subs.GetAttribute(2) + " " + subs.GetAttribute(3) + " " +
                                             subs.GetAttribute(4);
                        }
                        break;
                    case "MuzzleModel":
                        i.MuzzleParticle = subs.GetAttribute(0);
                        break;
                    case "HudIcon":
                        //i. = subs.GetAttribute(0);
                        break;
                    case "Store":
                        i.Name = subs.GetAttribute(0);
                        //i.icon = subs.GetAttribute(1);
                        i.Description = subs.GetAttribute(2);
                        i.LevelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "PrimaryFire":
                        i.BulletID = Helper.getFloat(subs.GetAttribute(0));
                        i.Damage = Helper.getInt(subs.GetAttribute(1));
                        i.isImmediate = (subs.GetAttribute(2).ToLower() == "true") ? 1 : 0;
                        i.Mass = Helper.getFloat(subs.GetAttribute(3));
                        i.DamageDecay = Helper.getFloat(subs.GetAttribute(4));
                        i.Speed = Helper.getFloat(subs.GetAttribute(5));
                        i.Area = Helper.getFloat(subs.GetAttribute(6));
                        i.Delay = Helper.getFloat(subs.GetAttribute(7));
                        i.NumClips = Helper.getInt(subs.GetAttribute(8));
                        i.Clipsize = Helper.getInt(subs.GetAttribute(9));
                        i.ReloadTime = Helper.getFloat(subs.GetAttribute(10));
                        i.ActiveReloadTick = Helper.getFloat(subs.GetAttribute(11));
                        i.RateOfFire = Helper.getInt(subs.GetAttribute(12));
                        i.Spread = Helper.getFloat(subs.GetAttribute(13));
                        i.Recoil = Helper.getFloat(subs.GetAttribute(14));
                        i.NumGrenades = Helper.getInt(subs.GetAttribute(15));
                        i.GrenadeName = subs.GetAttribute(16);
                        i.Firemode = subs.GetAttribute(17);
                        i.ScopeType = subs.GetAttribute(18);
                        i.ScopeZoom = Helper.getInt(subs.GetAttribute(19));
                        break;
                    case "Animation":
                        i.Animation = subs.GetAttribute(0);
                        break;
                    case "Sound":
                        i.Sound_Shot = subs.GetAttribute(0);
                        i.Sound_Reload = subs.GetAttribute(1);
                        break;
                    case "FPS":
                        i.IsFPS = Helper.getInt(subs.GetAttribute(0));
                        i.FPSAttach0 = Helper.getInt(subs.GetAttribute(1));
                        i.FPSAttach1 = Helper.getInt(subs.GetAttribute(2));
                        i.FPSAttach2 = Helper.getInt(subs.GetAttribute(3));
                        i.FPSAttach3 = Helper.getInt(subs.GetAttribute(4));
                        i.FPSAttach4 = Helper.getInt(subs.GetAttribute(5));
                        i.FPSAttach5 = Helper.getInt(subs.GetAttribute(6));
                        i.FPSAttach6 = Helper.getInt(subs.GetAttribute(7));
                        i.FPSAttach7 = Helper.getInt(subs.GetAttribute(8));
                        i.FPSAttach8 = Helper.getInt(subs.GetAttribute(9));

                        i.FPSSpec0 = Helper.getInt(subs.GetAttribute(10));
                        i.FPSSpec1 = Helper.getInt(subs.GetAttribute(11));
                        i.FPSSpec2 = Helper.getInt(subs.GetAttribute(12));
                        i.FPSSpec3 = Helper.getInt(subs.GetAttribute(13));
                        i.FPSSpec4 = Helper.getInt(subs.GetAttribute(14));
                        i.FPSSpec5 = Helper.getInt(subs.GetAttribute(15));
                        i.FPSSpec6 = Helper.getInt(subs.GetAttribute(16));
                        i.FPSSpec7 = Helper.getInt(subs.GetAttribute(17));
                        i.FPSSpec8 = Helper.getInt(subs.GetAttribute(18));

                        break;
                }
            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            writer.WriteStartElement("Weapon");
            writer.WriteAttributeString("itemID", "" + i.Value.itemID);
            writer.WriteAttributeString("category", "" + i.Value.internalCategory);
            writer.WriteAttributeString("upgrade", "" + i.Value.upgrade);
            writer.WriteAttributeString("FNAME", i.Value.fname);
            writer.WriteAttributeString("Weight", "" + i.Value.weight);

            writer.WriteStartElement("Model");
            writer.WriteAttributeString("file", i.Value.modelFile);
            writer.WriteAttributeString("muzzlerOffset.x", i.Value.muzzlerOffesetX.ToString());
            writer.WriteAttributeString("muzzlerOffset.y", i.Value.muzzlerOffesetY.ToString());
            writer.WriteAttributeString("muzzlerOffset.z", i.Value.muzzlerOffesetZ.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("MuzzleModel");
            writer.WriteAttributeString("file", i.Value.muzzleModelFile);
            writer.WriteEndElement();

            writer.WriteStartElement("HudIcon");
            writer.WriteAttributeString("file", i.Value.hudIconFile);
            writer.WriteEndElement();

            writer.WriteStartElement("Store");
            writer.WriteAttributeString("name", i.Value.name);
            writer.WriteAttributeString("icon", i.Value.image);
            writer.WriteAttributeString("desc", i.Value.desc);
            writer.WriteAttributeString("LevelRequired", i.Value.levelRequired.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("PrimaryFire");
            writer.WriteAttributeString("bullet", "" + i.Value.bullet);
            writer.WriteAttributeString("damage", i.Value.damage.ToString());
            writer.WriteAttributeString("immediate", i.Value.immediate.ToString());
            writer.WriteAttributeString("mass", i.Value.mass.ToString());
            writer.WriteAttributeString("decay", i.Value.decay.ToString());
            writer.WriteAttributeString("speed", i.Value.speed.ToString());
            writer.WriteAttributeString("area", i.Value.area.ToString());
            writer.WriteAttributeString("delay", i.Value.delay.ToString());
            writer.WriteAttributeString("timeout", i.Value.timeout.ToString());
            writer.WriteAttributeString("numShells", i.Value.numShells.ToString());
            writer.WriteAttributeString("clipSize", i.Value.clipSize.ToString());
            writer.WriteAttributeString("reloadTime", i.Value.reloadTime.ToString());
            writer.WriteAttributeString("activeReloadTick", i.Value.activeReloadTick.ToString());
            writer.WriteAttributeString("rateOfFire", i.Value.rateOfFire.ToString());
            writer.WriteAttributeString("spread", i.Value.spread.ToString());
            writer.WriteAttributeString("recoil", i.Value.recoil.ToString());
            writer.WriteAttributeString("numgrenades", i.Value.numgrenades.ToString());
            writer.WriteAttributeString("grenadename", i.Value.grenadename);
            writer.WriteAttributeString("firemode", i.Value.firemode.ToString());
            writer.WriteAttributeString("ScopeType", i.Value.ScopeType);
            writer.WriteAttributeString("ScopeZoom", i.Value.ScopeZoom.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Animation");
            writer.WriteAttributeString("type", i.Value.type);
            writer.WriteEndElement();

            writer.WriteStartElement("Sound");
            writer.WriteAttributeString("shoot", i.Value.shoot);
            writer.WriteAttributeString("reload", i.Value.reload);
            writer.WriteEndElement();

            writer.WriteStartElement("FPS");
            writer.WriteAttributeString("IsFPS", i.Value.IsFPS.ToString());
            writer.WriteAttributeString("i0", i.Value.i0.ToString());
            writer.WriteAttributeString("i1", i.Value.i1.ToString());
            writer.WriteAttributeString("i2", i.Value.i2.ToString());
            writer.WriteAttributeString("i3", i.Value.i3.ToString());
            writer.WriteAttributeString("i4", i.Value.i4.ToString());
            writer.WriteAttributeString("i5", i.Value.i5.ToString());
            writer.WriteAttributeString("i6", i.Value.i6.ToString());
            writer.WriteAttributeString("i7", i.Value.i7.ToString());
            writer.WriteAttributeString("i8", i.Value.i8.ToString());

            writer.WriteAttributeString("d0", i.Value.i0.ToString());
            writer.WriteAttributeString("d1", i.Value.i1.ToString());
            writer.WriteAttributeString("d2", i.Value.i2.ToString());
            writer.WriteAttributeString("d3", i.Value.i3.ToString());
            writer.WriteAttributeString("d4", i.Value.i4.ToString());
            writer.WriteAttributeString("d5", i.Value.i5.ToString());
            writer.WriteAttributeString("d6", i.Value.i6.ToString());
            writer.WriteAttributeString("d7", i.Value.i7.ToString());
            writer.WriteAttributeString("d8", i.Value.i8.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
