using System.Collections.Generic;
using System.IO;
using System.Xml;
using SQLite;

namespace WarZLocal_Admin
{
    class Gear
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

        public static Gear readXML(XmlReader reader)
        {
            Gear i = new Gear();

            i.ItemID = Helper.getInt(reader.GetAttribute(0));
            i.Category = Helper.getInt(reader.GetAttribute(1));
            //i.internalCategory = 1;
            i.Weight = Helper.getInt(reader.GetAttribute(2));

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
                        break;
                    case "Store":
                        i.Name = subs.GetAttribute(0);
                        //i.image = subs.GetAttribute(1);
                        i.Description = subs.GetAttribute(2);
                        if(i.Category != 12)
                            i.LevelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "Armor":
                        i.DamagePerc = Helper.getInt(subs.GetAttribute(0));
                        i.DamageMax = Helper.getInt(subs.GetAttribute(1));
                        i.Bulkiness = Helper.getInt(subs.GetAttribute(2));
                        i.Inaccuracy = Helper.getInt(subs.GetAttribute(3));
                        i.Stealth = Helper.getInt(subs.GetAttribute(4));
                        i.ProtectionLevel = Helper.getInt(subs.GetAttribute(5));
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*
		        <Gear itemID="20006" category="13" Weight="1000">
			        <Model file="Data/ObjectsDepot/Characters/HEADHELMET.sco" />
			        <Store name="K. Style Helmet" icon="$Data/Weapons/StoreIcons/HEADHELMET.dds" desc="The good ol' Turtle Head is guaranteed to protect your brain. (Well, at least a little bit)&#13;Level 2 Protection" LevelRequired="20" />
			        <Armor damagePerc="30" damageMax="300" bulkiness="0" inaccuracy="0" stealth="0" ProtectionLevel="2" />
		        </Gear>
             */

            writer.WriteStartElement("Gear");

            writer.WriteAttributeString("itemID", "" + i.Value.itemID);
            writer.WriteAttributeString("category", "" + i.Value.internalCategory);
            writer.WriteAttributeString("Weight", "" + i.Value.weight);

            writer.WriteStartElement("Model");
            writer.WriteAttributeString("file", i.Value.modelFile);
            writer.WriteEndElement();

            writer.WriteStartElement("Store");
            writer.WriteAttributeString("name", i.Value.name);
            writer.WriteAttributeString("icon", i.Value.image);
            writer.WriteAttributeString("desc", i.Value.desc);
            writer.WriteAttributeString("LevelRequired", i.Value.levelRequired.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Armor");
            writer.WriteAttributeString("damagePerc", i.Value.damagePerc.ToString());
            writer.WriteAttributeString("damageMax", i.Value.damageMax.ToString());
            writer.WriteAttributeString("bulkiness", i.Value.bulkiness.ToString());
            writer.WriteAttributeString("inaccuracy", i.Value.inaccuracy.ToString());
            writer.WriteAttributeString("stealth", i.Value.stealth.ToString());
            writer.WriteAttributeString("ProtectionLevel", i.Value.protectionLevel.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
