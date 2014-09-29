using System.Collections.Generic;
using System.Xml;

namespace WarZLocal_Admin
{
    class Gear
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 1;
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
                        break;
                    case "Store":
                        i.name = subs.GetAttribute(0);
                        i.image = subs.GetAttribute(1);
                        i.desc = subs.GetAttribute(2);
                        i.levelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "Armor":
                        i.damagePerc = Helper.getInt(subs.GetAttribute(0));
                        i.damageMax = Helper.getInt(subs.GetAttribute(1));
                        i.bulkiness = Helper.getInt(subs.GetAttribute(2));
                        i.inaccuracy = Helper.getInt(subs.GetAttribute(3));
                        i.stealth = Helper.getInt(subs.GetAttribute(4));
                        i.protectionLevel = Helper.getInt(subs.GetAttribute(5));
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
