using System.Reflection;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;

namespace MogLog
{
    public class MogLog : IDalamudPlugin
    {
        private DalamudPluginInterface PluginInterface { get; init; }

        [PluginService]
        private ChatGui ChatGui { get; init; }

        private Configuration Configuration { get; init; }


        public string Name => "MogLog";
        public string AssemblyLocation { get; set; } = Assembly.GetExecutingAssembly().Location;


        public MogLog(DalamudPluginInterface pluginInterface)
        {
            FileHandler.SetLogDirectory();

            this.PluginInterface = pluginInterface;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            Chat.Initialize(PluginInterface);
            FileHandler.Initialize(PluginInterface);

            PluginLog.Debug("MogLog Loaded!");
            this.ChatGui.ChatMessage += Chat.MessageRecieved;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.ChatGui.ChatMessage -= Chat.MessageRecieved;
            this.PluginInterface.Dispose();
        }
    }
}
