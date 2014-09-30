using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace WarZLocal_Admin
{
    class Lootbox
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 3;
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
                        i.fname = Path.GetFileNameWithoutExtension(i.modelFile);
                        break;
                    case "Store":
                        i.name = subs.GetAttribute(0);
                        i.image = subs.GetAttribute(1);
                        i.desc = subs.GetAttribute(2);
                        i.levelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*
		    <LootBox itemID="301118" category="7" Weight="0">
			    <Model file="Data/ObjectsDepot/Weapons/Loot_Drop_Premium_01.sco" />
			    <Store name="WEAP - Melee" icon="$Data/Weapons/StoreIcons/Loot_Drop_Premium_01.dds" desc="spawns of War Z Weapons" LevelRequired="0" />
		    </LootBox>
             */
            writer.WriteStartElement("LootBox");

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

            writer.WriteEndElement();
        }
    }
}
