using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WarZLocal_Admin
{
    class Backpack
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 6;
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
                        break;
                    case "Desc":
                        i.maxSlots = Helper.getInt(subs.GetAttribute(0));
                        i.maxWeight = Helper.getInt(subs.GetAttribute(1));
                        break;
                }

            }
            return i;
        }
        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*
		        <Backpack itemID="20175" category="12" Weight="0">
			        <Model file="Data/ObjectsDepot/Characters/gear_backpack_16slots.sco" />
			        <Store name="Medium Backpack" icon="$Data/Weapons/StoreIcons/gear_backpack_16slots.dds" desc="A medium sized backpack with plenty of space and durability." />
			        <Desc maxSlots="18" maxWeight="15" />
		        </Backpack>
             */

            writer.WriteStartElement("Backpack");

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

            writer.WriteStartElement("Desc");
            writer.WriteAttributeString("maxSlots", i.Value.maxSlots.ToString());
            writer.WriteAttributeString("maxWeight", i.Value.maxWeight.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
