using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Input;
using osu.Framework.Platform;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Hololive.Game.Desktop
{
    public class HololiveGameDesktop : HololiveGame
    {
        private readonly string[] args;

        public HololiveGameDesktop(string[] args)
        {
            this.args = args;
        }

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config)
        {
            config.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = new Size(1366, 768);
            config.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.MultiThreaded;
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (host.Window is not SDL2DesktopWindow window)
                return;

            window.ConfineMouseMode.Value = ConfineMouseMode.Never;
            window.WindowMode.Value = WindowMode.Windowed;
            window.Resizable = false;
            window.Title = "hololive IDOL PROJECT: Shining Virtual Stars";
            window.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "app.ico"));

            if (args.Contains("--fullscreen"))
                window.WindowMode.Value = WindowMode.Borderless;
        }
    }
}
