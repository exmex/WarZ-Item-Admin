using System.Collections.Generic;
using System.Xml;

namespace WarZLocal_Admin
{
    class Attachment
    {
        public static Items readXML(XmlReader reader)
        {
            Items i = new Items();

            i.itemID = Helper.getInt(reader.GetAttribute(0));
            i.category = Helper.getInt(reader.GetAttribute(1));
            i.internalCategory = 2;

            i.attachType = Helper.getInt(reader.GetAttribute(2));
            i.SpecID = Helper.getInt(reader.GetAttribute(3));

            i.weight = Helper.getInt(reader.GetAttribute(4));

            XmlReader subs = reader.ReadSubtree();
            while (subs.Read())
            {
                if (!subs.HasAttributes)
                    continue;
                switch (subs.Name)
                {
                    case "Model":
                        i.modelFile = subs.GetAttribute(0);

                        i.MuzzleParticle = subs.GetAttribute(1);
                        i.FireSound = subs.GetAttribute(2);
                        i.ScopeAnim = subs.GetAttribute(3);
                        break;
                    case "Store":
                        i.name = subs.GetAttribute(0);
                        i.image = subs.GetAttribute(1);
                        i.desc = subs.GetAttribute(2);
                        i.levelRequired = Helper.getInt(subs.GetAttribute(3));
                        break;
                    case "Upgrade":
                        i.upgradeDamage = Helper.getInt(subs.GetAttribute(0));
                        i.upgradeRange = Helper.getInt(subs.GetAttribute(1));
                        i.upgradeFirerate = Helper.getInt(subs.GetAttribute(2));
                        i.upgradeRecoil = Helper.getInt(subs.GetAttribute(3));
                        i.upgradeSpread = Helper.getInt(subs.GetAttribute(4));
                        i.upgradeClipsize = Helper.getInt(subs.GetAttribute(5));
                        i.upgradeScopeMag = Helper.getInt(subs.GetAttribute(6));
                        i.upgradeScopeType = subs.GetAttribute(7);
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
