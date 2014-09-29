using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WarZLocal_Admin
{
    class Weapon
    {
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

        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 0;
            i.weight = Helper.getInt(reader.GetAttribute(2));

            XmlReader subs = reader.ReadSubtree();
            while (subs.Read())
            {
                if (!subs.HasAttributes)
                    continue;
                switch (subs.Name)
                {
                    case "Model":
                        i.modelFile = subs.GetAttribute(0);
                        i.muzzlerOffesetX = Helper.getInt(subs.GetAttribute(1));
                        i.muzzlerOffesetY = Helper.getInt(subs.GetAttribute(2));
                        i.muzzlerOffesetZ = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "MuzzleModel":
                        i.muzzleModelFile = subs.GetAttribute(0);
                        break;
                    case "HudIcon":
                        i.hudIconFile = subs.GetAttribute(0);
                        break;
                    case "Store":
                        i.name = subs.GetAttribute(0);
                        i.image = subs.GetAttribute(1);
                        i.desc = subs.GetAttribute(2);
                        i.levelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "PrimaryFire":
                        i.bullet = Helper.getFloat(subs.GetAttribute(0));
                        i.damage = Helper.getInt(subs.GetAttribute(1));
                        i.immediate = (subs.GetAttribute(2).ToLower() == "true");
                        i.mass = Helper.getInt(subs.GetAttribute(3));
                        i.decay = Helper.getInt(subs.GetAttribute(4));
                        i.speed = Helper.getInt(subs.GetAttribute(5));
                        i.area = Helper.getInt(subs.GetAttribute(6));
                        i.delay = Helper.getInt(subs.GetAttribute(7));
                        i.numShells = Helper.getInt(subs.GetAttribute(8));
                        i.clipSize = Helper.getInt(subs.GetAttribute(9));
                        i.reloadTime = Helper.getFloat(subs.GetAttribute(10));
                        i.activeReloadTick = Helper.getFloat(subs.GetAttribute(11));
                        i.rateOfFire = Helper.getInt(subs.GetAttribute(12));
                        i.spread = Helper.getInt(subs.GetAttribute(13));
                        i.recoil = Helper.getInt(subs.GetAttribute(14));
                        i.numgrenades = Helper.getInt(subs.GetAttribute(15));
                        i.grenadename = subs.GetAttribute(16);
                        i.firemode = Helper.getInt(subs.GetAttribute(17));
                        i.ScopeType = subs.GetAttribute(18);
                        i.ScopeZoom = Helper.getInt(subs.GetAttribute(19));
                        break;
                    case "Animation":
                        i.type = subs.GetAttribute(0);
                        break;
                    case "Sound":
                        i.shoot = subs.GetAttribute(0);
                        i.reload = subs.GetAttribute(1);
                        break;
                    case "FPS":
                        i.IsFPS = Helper.getInt(subs.GetAttribute(0));
                        i.i0 = Helper.getInt(subs.GetAttribute(1));
                        i.i1 = Helper.getInt(subs.GetAttribute(2));
                        i.i2 = Helper.getInt(subs.GetAttribute(3));
                        i.i3 = Helper.getInt(subs.GetAttribute(4));
                        i.i4 = Helper.getInt(subs.GetAttribute(5));
                        i.i5 = Helper.getInt(subs.GetAttribute(6));
                        i.i6 = Helper.getInt(subs.GetAttribute(7));
                        i.i7 = Helper.getInt(subs.GetAttribute(8));
                        i.i8 = Helper.getInt(subs.GetAttribute(9));

                        i.d0 = Helper.getInt(subs.GetAttribute(10));
                        i.d1 = Helper.getInt(subs.GetAttribute(11));
                        i.d2 = Helper.getInt(subs.GetAttribute(12));
                        i.d3 = Helper.getInt(subs.GetAttribute(13));
                        i.d4 = Helper.getInt(subs.GetAttribute(14));
                        i.d5 = Helper.getInt(subs.GetAttribute(15));
                        i.d6 = Helper.getInt(subs.GetAttribute(16));
                        i.d7 = Helper.getInt(subs.GetAttribute(17));
                        i.d8 = Helper.getInt(subs.GetAttribute(18));

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
