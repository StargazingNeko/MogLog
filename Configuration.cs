using Dalamud.Configuration;
using Dalamud.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MogLog
{
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; }

        public bool LogGeneral { get; set; } = true;

        public bool LogNoviceNetwork { get; set; } = true;
        public bool LogSay { get; set; } = true;
        public bool LogYell { get; set; } = true;
        public bool LogShout { get; set; } = true;
        public bool LogParty { get; set; } = true;
        public bool LogFreeCompany { get; set; } = true;
        public bool LogAlliance { get; set; } = true;
        public bool LogTells { get; set; } = true;

        public bool LogLinkShell1 { get; set; } = true;
        public bool LogLinkShell2 { get; set; } = true;
        public bool LogLinkShell3 { get; set; } = true;
        public bool LogLinkShell4 { get; set; } = true;
        public bool LogLinkShell5 { get; set; } = true;
        public bool LogLinkShell6 { get; set; } = true;
        public bool LogLinkShell7 { get; set; } = true;
        public bool LogLinkShell8 { get; set; } = true;

        public bool LogCrossLinkShell1 { get; set; } = true;
        public bool LogCrossLinkShell2 { get; set; } = true;
        public bool LogCrossLinkShell3 { get; set; } = true;
        public bool LogCrossLinkShell4 { get; set; } = true;
        public bool LogCrossLinkShell5 { get; set; } = true;
        public bool LogCrossLinkShell6 { get; set; } = true;
        public bool LogCrossLinkShell7 { get; set; } = true;
        public bool LogCrossLinkShell8 { get; set; } = true;

        public bool EnableQueueing { get; set; } = true;

        [JsonIgnore] private DalamudPluginInterface? pluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
            
            Chat.SetQueueOption(EnableQueueing);
        }

        public void Save()
        {
            this.pluginInterface?.SavePluginConfig(this);
        }
    }
}
