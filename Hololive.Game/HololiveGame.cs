using Hololive.Game.Audio;
using Hololive.Game.Grpahics;
using Hololive.Game.IO;
using Hololive.Game.Screens.Title;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Framework.Screens;
using System;
using System.Drawing;
using System.Reflection;

namespace Hololive.Game
{
    public class HololiveGame : osu.Framework.Game
    {
        private BackgroundTrack theme;

        private ScrollingBackground scrollingBackground;

        private DependencyContainer dependencies;

        private Container overlays;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config)
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(HololiveGame).Assembly), @"Resources"));

            dependencies.CacheAs(new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, "Textures"))));
            dependencies.CacheAs(new TextStore(new NamespacedResourceStore<byte[]>(Resources, "Text")));
            dependencies.CacheAs(this);

            AddFont(Resources, @"Fonts/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Compatibility");
            AddFont(Resources, @"Fonts/Chicago");

            config.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = new Size(1366, 768);
            config.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.SingleThread;

            Children = new Drawable[]
            {
                theme = new BackgroundTrack("sss_orch_inst", 13557, 86401),
                new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new TapEffectContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new Box { RelativeSizeAxes = Axes.Both },
                            scrollingBackground = new ScrollingBackground
                            {
                                Alpha = 0.25f,
                                Colour = Colour4.Aqua,
                            },
                            new ScreenStack(new TitleScreen())
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            overlays = new Container { RelativeSizeAxes = Axes.Both }
                        },
                    },
                },
            };

            theme.Start();
        }

        public void FadeBackgroundColor(Colour4 to)
        {
            Schedule(() => scrollingBackground.FadeColour(to, 500));
        }

        public void StartLoading(Action onComplete = null, double interval = 1000)
        {
            Schedule(() => overlays.Add(new DownloadProgressOverlay(onComplete, interval)));
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
        }
    }
}
