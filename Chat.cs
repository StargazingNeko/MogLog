using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MogLog
{
    public static class Chat
    {
        private static readonly string[] Worlds = { "Adamantoise", "Cactuar", "Faerie", "Gilgamesh", "Jenova", "Midgardsormr", "Siren", "Balmung", "Brynhildr", "Coeurl", "Diabolos", "Goblin", "Malboro", "Mateus", "Zalera", "Behemoth", "Excalibur", "Exodus", "Famfrit", "Hyperion", "Lamia", "Leviathan", "Ultros", "Cerberus", "Louisoix", "Moogle", "Omega", "Ragnarok", "Spriggan", "Lich", "Odin", "Phoenix", "Shiva", "Twintania", "Zodiark", "Aegis", "Atomos", "Carbuncle", "Garuda", "Gungnir", "Kujata", "Ramuh", "Tonberry", "Typhon", "Unicorn", "Alexander", "Bahamut", "Durandal", "Fenrir", "Ifrit", "Ridill", "Tiamat", "Ultima", "Valefor", "Yojimbo", "Zeromus", "Anima", "Asura", "Belias", "Chocobo", "Hades", "Ixion", "Mandragora", "Masamune", "Pandemonium", "Shinryu", "Titan" };

        public static void MessageRecieved(XivChatType ChatType, uint SenderId, ref SeString Sender, ref SeString Message, ref bool IsHandled)
        {
            string sender = Sender.TextValue;

            string[] SenderSplit = sender.Split(Worlds, StringSplitOptions.None);
            if (SenderSplit.Length > 1)
            {
                foreach (string World in Worlds)
                {
                    sender = sender.Replace($"{World}", $"@{World}");
                    if (sender.Contains("@"))
                        break;
                }
            }

            FFXIVMessage ffmessage = new(ChatType, SenderId, sender, Message);
            Task.Run(() => FileHandler.WriteToFile(ffmessage));
            Task.Run(() => FileHandler.WriteToGeneralFile(ffmessage)).Wait();
        }
    }
}
