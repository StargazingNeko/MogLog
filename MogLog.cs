using Dalamud.Plugin;

namespace MogLog
{
    public class MogLog : IDalamudPlugin
    {
        private DalamudPluginInterface PluginInterface { get; set; }
        private Configuration Config { get; set; }


        public string Name => "MogLog";
        public string AssemblyLocation { get => assemblyLocation; set => assemblyLocation = value; }
        private string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

        

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            FileHandler.SetLogDirectory();

            this.PluginInterface = pluginInterface;

            this.Config = (Configuration)this.PluginInterface.GetPluginConfig() ?? new Configuration();
            this.Config.Initialize(this.PluginInterface);

            this.PluginInterface.Framework.Gui.Chat.Print("MogLog Loaded!");

            this.PluginInterface.Framework.Gui.Chat.OnChatMessage += Chat.MessageRecieved;
        }

        public DalamudPluginInterface GetPluginInterface()
        {
            return PluginInterface;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;


            PluginInterface.Framework.Gui.Chat.OnChatMessage -= Chat.MessageRecieved;
            this.PluginInterface.Dispose();
        }
    }
}
