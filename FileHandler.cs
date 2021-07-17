using Dalamud.Game.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MogLog
{
    public static class FileHandler
    {
        public static string LogDirectory { get; private set; }
        private static DateTime today = DateTime.Today;
        private static Dalamud.Plugin.DalamudPluginInterface PluginInterface = new MogLog().GetPluginInterface();

        private static void CreateLogDirectory()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);

                File.Create($"{LogDirectory}\\General.txt").Close();

                File.Create($"{LogDirectory}\\NoviceNetwork.txt").Close();
                File.Create($"{LogDirectory}\\Say.txt").Close();
                File.Create($"{LogDirectory}\\Yell.txt").Close();
                File.Create($"{LogDirectory}\\Shout.txt").Close();
                File.Create($"{LogDirectory}\\Party.txt").Close();
                File.Create($"{LogDirectory}\\FreeCompany.txt").Close();
                File.Create($"{LogDirectory}\\Alliance.txt").Close();
                File.Create($"{LogDirectory}\\Tell.txt").Close();

                File.Create($"{LogDirectory}\\LS1.txt").Close();
                File.Create($"{LogDirectory}\\LS2.txt").Close();
                File.Create($"{LogDirectory}\\LS3.txt").Close();
                File.Create($"{LogDirectory}\\LS4.txt").Close();
                File.Create($"{LogDirectory}\\LS5.txt").Close();
                File.Create($"{LogDirectory}\\LS6.txt").Close();
                File.Create($"{LogDirectory}\\LS7.txt").Close();
                File.Create($"{LogDirectory}\\LS8.txt").Close();

                File.Create($"{LogDirectory}\\CWLS1.txt").Close();
                File.Create($"{LogDirectory}\\CWLS2.txt").Close();
                File.Create($"{LogDirectory}\\CWLS3.txt").Close();
                File.Create($"{LogDirectory}\\CWLS4.txt").Close();
                File.Create($"{LogDirectory}\\CWLS5.txt").Close();
                File.Create($"{LogDirectory}\\CWLS6.txt").Close();
                File.Create($"{LogDirectory}\\CWLS7.txt").Close();
                File.Create($"{LogDirectory}\\CWLS8.txt").Close();
            }
        }

        public static void SetLogDirectory()
        {
            if (today != DateTime.Today || string.IsNullOrEmpty(LogDirectory))
            {
                LogDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\XIVLauncher\\installedPlugins\\MogLog\\Chats\\{DateTime.Today:MM-dd-yyyy}";
                CreateLogDirectory();
                today = DateTime.Today;
            }
        }

        public static string GetChatFile(XivChatType ChatType)
        {
            string ChatFile = ChatType switch
            {
                XivChatType.NoviceNetwork => "\\NoviceNetwork.txt",
                XivChatType.Say => "\\Say.txt",
                XivChatType.Yell => "\\Yell.txt",
                XivChatType.Shout => "\\Shout.txt",
                XivChatType.Party => "\\Party.txt",
                XivChatType.FreeCompany => "\\FreeCompany.txt",
                XivChatType.Alliance => "\\Alliance.txt",
                XivChatType.TellIncoming => "\\Tell.txt",
                XivChatType.TellOutgoing => "\\Tell.txt",

                XivChatType.Ls1 => "\\LS1.txt",
                XivChatType.Ls2 => "\\LS2.txt",
                XivChatType.Ls3 => "\\LS3.txt",
                XivChatType.Ls4 => "\\LS4.txt",
                XivChatType.Ls5 => "\\LS5.txt",
                XivChatType.Ls6 => "\\LS6.txt",
                XivChatType.Ls7 => "\\LS7.txt",
                XivChatType.Ls8 => "\\LS8.txt",

                XivChatType.CrossLinkShell1 => "\\CWLS1.txt",
                XivChatType.CrossLinkShell2 => "\\CWLS2.txt",
                XivChatType.CrossLinkShell3 => "\\CWLS3.txt",
                XivChatType.CrossLinkShell4 => "\\CWLS4.txt",
                XivChatType.CrossLinkShell5 => "\\CWLS5.txt",
                XivChatType.CrossLinkShell6 => "\\CWLS6.txt",
                XivChatType.CrossLinkShell7 => "\\CWLS7.txt",
                XivChatType.CrossLinkShell8 => "\\CWLS8.txt",

                _ => string.Empty
            };

            if (string.IsNullOrEmpty(ChatFile))
                return string.Empty;

            return LogDirectory + ChatFile;
        }

        public static async Task WriteToFile(FFXIVMessage message)
        {
            if (!string.IsNullOrEmpty(GetChatFile(message.ChatType)))
            {
                SetLogDirectory();

                using StreamWriter sw = new(GetChatFile(message.ChatType), true);
                await sw.WriteLineAsync($"({message.ChatType})[{DateTime.Now.ToString("hh:mm:tt", System.Globalization.CultureInfo.InvariantCulture)}]{message.Sender}: {message.Message}");
                sw.Close();
            }
        }

        private static System.Collections.Generic.List<int> BlacklistedChatTypes = new System.Collections.Generic.List<int>()
        {
            72,
            2091,
            2219,
            2221,
            2222,
            2224,
            2729,
            2730,
            2731,
            2857,
            2858,
            2859,
            8235,
            8236,
            8250,
            2730,
            8746,
            8749,
            8750,
            8752,
        };

        public static async Task WriteToGeneralFile(FFXIVMessage message)
        {
            if(!BlacklistedChatTypes.Contains(((int)message.ChatType)))
            {
                using StreamWriter sw = new($"{LogDirectory}\\General.txt", true);
                if(message.ChatType != XivChatType.StandardEmote && !string.IsNullOrEmpty(message.Sender.TextValue))
                {
                    await sw.WriteLineAsync($"({ message.ChatType })[{ DateTime.Now.ToString("hh:mm:tt", System.Globalization.CultureInfo.InvariantCulture) }]{ message.Sender }: { message.Message }");
                }
                else if (message.ChatType == XivChatType.CustomEmote)
                {
                    await sw.WriteLineAsync($"({ message.ChatType})[{ DateTime.Now.ToString("hh:mm:tt", System.Globalization.CultureInfo.InvariantCulture)}]{message.Sender} { message.Message }");
                }
                else
                {
                    await sw.WriteLineAsync($"({ message.ChatType})[{ DateTime.Now.ToString("hh:mm:tt", System.Globalization.CultureInfo.InvariantCulture)}]{ message.Message }");
                }
                sw.Close();
            }

        }
    }
}
