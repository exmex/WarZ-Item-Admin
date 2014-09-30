using System.Collections.Generic;
using System.IO;
using System.Xml;
using SQLite;

namespace WarZLocal_Admin
{
    class Generic
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

        public static Generic readXML(XmlReader reader)
        {
            Generic i = new Generic();

            i.ItemID = Helper.getInt(reader.GetAttribute(0));
            i.Category = Helper.getInt(reader.GetAttribute(1));
            //i.internalCategory = 4;
            i.Weight = Helper.getInt(reader.GetAttribute(2));

            XmlReader subs = reader.ReadSubtree();
            while (subs.Read())
            {
                if (!subs.HasAttributes)
                    continue;
                switch (subs.Name)
                {
                    case "Store":
                        i.Name = subs.GetAttribute(0);
                        //i.image = subs.GetAttribute(1);
                        i.FNAME = Path.GetFileNameWithoutExtension(subs.GetAttribute(1));
                        i.Description = subs.GetAttribute(2);
                        i.LevelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                }

            }
            return i;
        }

        public static void writeXML(XmlWriter writer, KeyValuePair<int, Items> i)
        {
            /*
		    <Item itemID="301151" category="1" Weight="0">
			    <Store name="Account_ClanCreate" icon="$Data/Weapons/StoreIcons/Account_ClanCreate.dds" desc="clan create item" LevelRequired="0" />
		    </Item>
             */

            writer.WriteStartElement("Item");

            writer.WriteAttributeString("itemID", "" + i.Value.itemID);
            writer.WriteAttributeString("category", "" + i.Value.internalCategory);
            writer.WriteAttributeString("Weight", "" + i.Value.weight);

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
