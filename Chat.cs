using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Logging;
using Dalamud.Plugin;
using System;
using System.Threading.Tasks;

namespace MogLog
{
    public static class Chat
    {
        private static DalamudPluginInterface PluginInterface;
        private static readonly TaskQueue queue = new TaskQueue();
        private static bool EnableQueue;
        private static DateTime date = DateTime.Today;

        public static void Initialize(DalamudPluginInterface pluginInterface)
        {
            PluginInterface = pluginInterface;
        }

        public static void SetQueueOption(bool Enable)
        {
            EnableQueue = Enable;
        }

        private static readonly string[] Worlds = { "Adamantoise", "Cactuar", "Faerie", "Gilgamesh", "Jenova", "Midgardsormr", "Siren", "Balmung", "Brynhildr", "Coeurl", "Diabolos", "Goblin", "Malboro", "Mateus", "Zalera", "Behemoth", "Excalibur", "Exodus", "Famfrit", "Hyperion", "Lamia", "Leviathan", "Ultros", "Cerberus", "Louisoix", "Moogle", "Omega", "Ragnarok", "Spriggan", "Lich", "Odin", "Phoenix", "Shiva", "Twintania", "Zodiark", "Aegis", "Atomos", "Carbuncle", "Garuda", "Gungnir", "Kujata", "Ramuh", "Tonberry", "Typhon", "Unicorn", "Alexander", "Bahamut", "Durandal", "Fenrir", "Ifrit", "Ridill", "Tiamat", "Ultima", "Valefor", "Yojimbo", "Zeromus", "Anima", "Asura", "Belias", "Chocobo", "Hades", "Ixion", "Mandragora", "Masamune", "Pandemonium", "Shinryu", "Titan" };

        public static void MessageRecieved(XivChatType ChatType, uint SenderId, ref SeString Sender, ref SeString Message, ref bool IsHandled)
        {
            if(DateTime.Today != date)
            {
                PluginLog.Log("New date detected, updating directories...");
                date = DateTime.Today;
                PluginLog.Log($"..done, now using directory {FileHandler.SetLogDirectory()}");
            }

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
            if(EnableQueue)
            {
                Task.Run(() => queue.Enqueue(() => FileHandler.WriteToFile(ffmessage)));
                Task.Run(() => queue.Enqueue(() => FileHandler.WriteToGeneralFile(ffmessage)));
            }
            else
            {
                Task.Run(() => FileHandler.WriteToFile(ffmessage));
                Task.Run(() => FileHandler.WriteToGeneralFile(ffmessage));
            }
        }
    }
}
