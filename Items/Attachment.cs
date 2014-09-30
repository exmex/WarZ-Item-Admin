using System.Collections.Generic;
using System.IO;
using System.Xml;
using SQLite;

namespace WarZLocal_Admin
{
    class Attachment
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

        public static Attachment readXML(XmlReader reader)
        {
            Attachment i = new Attachment();
            i.ItemID = Helper.getInt(reader.GetAttribute(0));
            i.Category = Helper.getInt(reader.GetAttribute(1));
            //i.internalCategory = 2;

            i.Type = Helper.getInt(reader.GetAttribute(2));
            i.SpecID = Helper.getInt(reader.GetAttribute(3));

            i.Weight = Helper.getInt(reader.GetAttribute(4));

            XmlReader subs = reader.ReadSubtree();
            while (subs.Read())
            {
                if (!subs.HasAttributes)
                    continue;
                switch (subs.Name)
                {
                    case "Model":
                        //i.modelFile = subs.GetAttribute(0);
                        i.FNAME = Path.GetFileNameWithoutExtension(subs.GetAttribute(0));
                        i.MuzzleParticle = subs.GetAttribute(1);
                        i.FireSound = subs.GetAttribute(2);
                        i.ScopeType = subs.GetAttribute(3);
                        break;
                    case "Store":
                        i.Name = subs.GetAttribute(0);
                        //i.image = subs.GetAttribute(1);
                        i.Description = subs.GetAttribute(2);
                        i.LevelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "Upgrade":
                        i.Damage = Helper.getInt(subs.GetAttribute(0));
                        i.Range = Helper.getInt(subs.GetAttribute(1));
                        i.Firerate = Helper.getInt(subs.GetAttribute(2));
                        i.Recoil = Helper.getInt(subs.GetAttribute(3));
                        i.Spread = Helper.getInt(subs.GetAttribute(4));
                        i.Clipsize = Helper.getInt(subs.GetAttribute(5));
                        i.ScopeMag = Helper.getInt(subs.GetAttribute(6));
                        i.ScopeType = subs.GetAttribute(7);
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*
		    <Attachment itemID="400000" category="19" type="3" SpecID="1001" Weight="600">
			    <Model file="Data/ObjectsDepot/Weapons/ATTM_Grip_01.sco" MuzzleParticle="" FireSound="" ScopeAnim="Grip" />
			    <Store name="Forward Grip" icon="$Data/Weapons/StoreIcons/ATTM_Grip_01.dds" desc="The bottom rail grip will allow the shooter more control of the weapon by use of the attached grip." LevelRequired="9" />
			    <Upgrade damage="0" range="0" firerate="0" recoil="0" spread="-5" clipsize="0" ScopeMag="0" ScopeType="" />
		    </Attachment>
             */

            writer.WriteStartElement("Attachment");

            writer.WriteAttributeString("itemID", "" + i.Value.itemID);
            writer.WriteAttributeString("category", "" + i.Value.internalCategory);
            writer.WriteAttributeString("type", "" + i.Value.attachType);
            writer.WriteAttributeString("SpecID", "" + i.Value.SpecID);
            writer.WriteAttributeString("Weight", "" + i.Value.weight);

            writer.WriteStartElement("Model");
            writer.WriteAttributeString("file", i.Value.modelFile);
            writer.WriteAttributeString("MuzzleParticle", i.Value.MuzzleParticle);
            writer.WriteAttributeString("FireSound", i.Value.FireSound);
            writer.WriteAttributeString("ScopeAnim", i.Value.ScopeAnim);
            writer.WriteEndElement();

            writer.WriteStartElement("Store");
            writer.WriteAttributeString("name", i.Value.name);
            writer.WriteAttributeString("icon", i.Value.image);
            writer.WriteAttributeString("desc", i.Value.desc);
            writer.WriteAttributeString("LevelRequired", i.Value.levelRequired.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Upgrade");
            writer.WriteAttributeString("damage", i.Value.damage.ToString());
            writer.WriteAttributeString("range", i.Value.upgradeRange.ToString());
            writer.WriteAttributeString("firerate", i.Value.upgradeFirerate.ToString());
            writer.WriteAttributeString("recoil", i.Value.recoil.ToString());
            writer.WriteAttributeString("spread", i.Value.spread.ToString());
            writer.WriteAttributeString("clipsize", i.Value.upgradeClipsize.ToString());
            writer.WriteAttributeString("ScopeMag", i.Value.upgradeScopeMag.ToString());
            writer.WriteAttributeString("ScopeType", i.Value.ScopeType);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
