using Hololive.Game.Audio;
using Hololive.Game.Grpahics;
using Hololive.Game.IO;
using Hololive.Game.Screens.Intro;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Screens;
using System;

namespace Hololive.Game
{
    public abstract class HololiveGame : osu.Framework.Game
    {
        private BackgroundTrack theme;

        private ScrollingBackground scrollingBackground;

        private DependencyContainer dependencies;

        private Container overlays;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(@"Hololive.Game.Resources.dll"));

            dependencies.CacheAs(new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, "Textures"))));
            dependencies.CacheAs(new TextStore(new NamespacedResourceStore<byte[]>(Resources, "Text")));
            dependencies.CacheAs(new VideoStore(new NamespacedResourceStore<byte[]>(Resources, "Videos")));
            dependencies.CacheAs(this);

            AddFont(Resources, @"Fonts/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Compatibility");
            AddFont(Resources, @"Fonts/Chicago");

            Child = new DrawSizePreservingFillContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new TapEffectContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                        {
                            theme = new BackgroundTrack("sss_orch_inst", 13557, 86401),
                            new Box { RelativeSizeAxes = Axes.Both },
                            scrollingBackground = new ScrollingBackground
                            {
                                Alpha = 0.25f,
                                Colour = Colour4.Aqua,
                            },
                            new ScreenStack(new SplashScreen())
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            overlays = new Container { RelativeSizeAxes = Axes.Both }
                        },
                },
            };

            theme.Start();
        }

        public BackgroundTrack GetTrack() => theme;

        public void FadeBackgroundColor(Colour4 to)
        {
            Schedule(() => scrollingBackground.FadeColour(to, 500));
        }

        public void StartLoading(Action onComplete = null, double interval = 1000)
        {
            Schedule(() => overlays.Add(new DownloadProgressOverlay(onComplete, interval)));
        }
    }
}
