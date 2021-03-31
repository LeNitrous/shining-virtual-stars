using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace Hololive.Game.Screens.Title
{
    public class TapToStartFlyout : Container
    {
        public TapToStartFlyout()
        {
            AutoSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                new Box
                {
                    Alpha = 0.8f,
                    Width = 0.5f,
                    Colour = ColourInfo.GradientHorizontal(Colour4.Black.Opacity(0), Colour4.Black),
                    RelativeSizeAxes = Axes.Both,
                },
                new Box
                {
                    Alpha = 0.8f,
                    Width = 0.5f,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Colour = ColourInfo.GradientHorizontal(Colour4.Black, Colour4.Black.Opacity(0)),
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText
                {
                    Text = "TAP TO START",
                    Font = FrameworkFont.Condensed.With(size: 32),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Margin = new MarginPadding
                    {
                        Horizontal = 250,
                        Vertical = 5,
                    },
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this
                .FadeTo(0.25f, 1000)
                .Then()
                .FadeTo(0.8f, 1000)
                .Loop();
        }
    }
}
