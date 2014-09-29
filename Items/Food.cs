using System.Collections.Generic;
using System.Xml;

namespace WarZLocal_Admin
{
    class Food
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 7;
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
                    case "Property":
                        i.health = Helper.getInt(subs.GetAttribute(0));
                        i.toxicity = Helper.getInt(subs.GetAttribute(1));
                        i.water = Helper.getInt(subs.GetAttribute(2));
                        i.food = Helper.getInt(subs.GetAttribute(3));
                        i.stamina = Helper.getInt(subs.GetAttribute(4));
                        i.shopSS = Helper.getInt(subs.GetAttribute(5));
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*<Item itemID="101283" category="30" Weight="350">
			    <Model file="Data/ObjectsDepot/Weapons/consumables_bag_chips_01.sco" />
			    <Store name="Bag of Chips" icon="$Data/Weapons/StoreIcons/consumables_bag_chips_01.dds" desc="A bag of potato chips.  Relieves minor hunger. BBQ Flavor!" LevelRequired="0" />
			    <Property health="0" toxicity="0" water="-5" food="10" stamina="0" shopSS="1" />
		    </Item>*/

            writer.WriteStartElement("Item");

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

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("health", i.Value.health.ToString());
            writer.WriteAttributeString("toxicity", i.Value.toxicity.ToString());
            writer.WriteAttributeString("water", i.Value.water.ToString());
            writer.WriteAttributeString("food", i.Value.food.ToString());
            writer.WriteAttributeString("stamina", i.Value.stamina.ToString());
            writer.WriteAttributeString("shopSS", i.Value.shopSS.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
