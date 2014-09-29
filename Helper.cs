using System;
using System.IO;
using System.IO.Compression;

namespace WarZLocal_Admin
{
    class Helper
    {
        public static int toItemsCategory(string name)
        {
            /*
             * 1 - Generic
             * 7 - LootBox
             * 12 - Backpack
             * 13 - Gear
             * 16 - Hero
             * 19 - Attachment
             * 21 - Weapon
             * 33 - Food
             */
            switch (name)
            {

                /*
                 * 1 - Account
                 * 2 - Boost
                 * 7 - LootBox
                 * 11 - Armor
                 * 12 - Backpack
                 * 13 - Helmet
                 * 16 - Hero
                 * 19 - Attachment
                 * 20 - Assault Rifle
                 * 21 - Sniper Rifle
                 * 22 - Shotgun
                 * 23 - Machine gun
                 * 25 - Handguns
                 * 26 - Submachinegun
                 * 27 - Grenades
                 * 29 - Melee
                 * 30 - Food
                 * 33 - Water
                 */
                default:
                    return 1;
                case "Boost":
                    return 2;
                case "LootBox":
                    return 7;
                case "Armor":
                    return 11;
                case "Backpack":
                    return 12;
                case "Helmet":
                    return 13;
                case "Hero":
                    return 16;
                case "Attachment":
                    return 19;
                case "Assault Rifle":
                    return 20;
                case "Sniper Rifle":
                    return 21;
                case "Shotgun":
                    return 22;
                case "Machine gun":
                    return 23;
                case "Handguns":
                    return 25;
                case "Submachinegun":
                    return 26;
                case "Grenades":
                    return 27;
                case "Melee":
                    return 29;
                case "Food":
                    return 30;
                case "Water":
                    return 33;

            }
        }

        public static string fromItemsCategory(int cat)
        {
            /*
             * 1 - Account
             * 2 - Boost
             * 7 - LootBox
             * 11 - Armor
             * 12 - Backpack
             * 13 - Helmet
             * 16 - Hero
             * 19 - Attachment
             * 20 - Assault Rifle
             * 21 - Sniper Rifle
             * 22 - Shotgun
             * 23 - Machine gun
             * 25 - Handguns
             * 26 - Submachinegun
             * 27 - Grenades
             * 29 - Melee
             * 30 - Food
             * 33 - Water
             */
            switch (cat)
            {
                default:
                    return "Unknown";
                case 1:
                    return "Account";
                case 2:
                    return "Boost";
                case 7:
                    return "LootBox";
                case 11:
                    return "Armor";
                case 12:
                    return "Backpack";
                case 13:
                    return "Helmet";
                case 16:
                    return "Hero";
                case 19:
                    return "Attachment";
                case 20:
                    return "Assault Rifle";
                case 21:
                    return "Sniper Rifle";
                case 22:
                    return "Shotgun";
                case 23:
                    return "Machine Gun";
                case 25:
                    return "Handguns";
                case 26:
                    return "Submachinegun";
                case 27:
                    return "Grenades";
                case 29:
                    return "Melee";
                case 30:
                    return "Food";
                case 33:
                    return "Water";

            }
        }

        public static int toCategory(string name)
        {
            /*
             * 0 - Weapon
             * 1 - Gear
             * 2 - Attachment
             * 3 - LootBox
             * 4 - Generic
             * 5 - Hero
             * 6 - Backpack
             * 7 - Food
             */
            switch (name)
            {
                case "Weapon":
                    return 0;
                case "Gear":
                    return 1;
                case "Attachment":
                    return 2;
                case "LootBox":
                    return 3;
                default:
                    return 4;
                case "Hero":
                    return 5;
                case "Backpack":
                    return 6;
                case "Food":
                    return 7;
            }
        }

        public static string fromCategory(int cat)
        {
            switch (cat)
            {
                case 0:
                    return "Weapon";
                case 1:
                    return "Gear";
                case 2:
                    return "Attachment";
                case 3:
                    return "LootBox";
                default:
                    return "Item";
                case 5:
                    return "Hero";
                case 6:
                    return "Backpack";
                case 7:
                    return "Food";
            }
        }

        public static float getFloat(string s)
        {
            float i;
            if (float.TryParse(s, out i))
                return i;

            return 0.0f;
        }

        public static int getInt(string s)
        {
            int i;
            if (int.TryParse(s, out i))
                return i;

            return 0;
        }

        public static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                                  CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }
    }
}
