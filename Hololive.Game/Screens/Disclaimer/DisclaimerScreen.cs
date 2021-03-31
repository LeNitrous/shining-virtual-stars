using Hololive.Game.IO;
using Hololive.Game.Screens.Loading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;

namespace Hololive.Game.Screens.Disclaimer
{
    public class DisclaimerScreen : Screen
    {
        private BasicScrollContainer scrollContainer;

        private BasicButton button;

        [BackgroundDependencyLoader]
        private void load(TextStore store)
        {
            InternalChild = new Container
            {
                Width = 720,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    new Box { RelativeSizeAxes = Axes.Both },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding
                        {
                            Horizontal = 20,
                            Bottom = 120,
                        },
                        Child = scrollContainer = new BasicScrollContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = new MarkdownContainer
                            {
                                Colour = Colour4.Black,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Text = store.Get("disclaimer-text.md"),
                            },
                        },
                    },
                    button = new BasicButton
                    {
                        Text = "Accept",
                        Size = new Vector2(300, 40),
                        Action = () => this.Push(new LoadingScreen()),
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        Y = -40,
                    },
                },
            };

            button.Enabled.Value = false;
        }

        protected override void Update()
        {
            base.Update();

            if (scrollContainer.Current > 0 && scrollContainer.IsScrolledToEnd() && !button.Enabled.Value)
                button.Enabled.Value = true;
        }
    }
}
