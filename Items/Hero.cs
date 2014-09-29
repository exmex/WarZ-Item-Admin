using System.Collections.Generic;
using System.Xml;

namespace WarZLocal_Admin
{
    class Hero
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 5;
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
                    case "HeroDesc":
                        i.heroDescDamagePerc = Helper.getInt(subs.GetAttribute(0));
                        i.heroDescDamageMax = Helper.getInt(subs.GetAttribute(1));
                        i.maxHeads = Helper.getInt(subs.GetAttribute(2));
                        i.maxBodys = Helper.getInt(subs.GetAttribute(3));
                        i.maxLegs = Helper.getInt(subs.GetAttribute(4));
                        i.heroDescProtectionLevel = Helper.getInt(subs.GetAttribute(5));
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*<Hero itemID="20170" category="16" Weight="-1">
                <Model file="Data/ObjectsDepot/Characters/Zombie" />
                <Store name="Basic Zombie Character" icon="$Data/Weapons/StoreIcons/Zombie.dds" desc="" LevelRequired="0" />
                <HeroDesc damagePerc="0" damageMax="0" maxHeads="5" maxBodys="5" maxLegs="5" ProtectionLevel="1" />
            </Hero>
             */

            writer.WriteStartElement("Hero");

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

            writer.WriteStartElement("HeroDesc");
            writer.WriteAttributeString("damagePerc", i.Value.damagePerc.ToString());
            writer.WriteAttributeString("damageMax", i.Value.damageMax.ToString());
            writer.WriteAttributeString("maxHeads", i.Value.maxHeads.ToString());
            writer.WriteAttributeString("maxBodys", i.Value.maxBodys.ToString());
            writer.WriteAttributeString("maxLegs", i.Value.maxLegs.ToString());
            writer.WriteAttributeString("ProtectionLevel", i.Value.protectionLevel.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
