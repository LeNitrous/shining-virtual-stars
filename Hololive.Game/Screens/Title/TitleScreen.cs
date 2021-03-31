using Hololive.Game.Audio;
using Hololive.Game.Screens.Disclaimer;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;

namespace Hololive.Game.Screens.Title
{
    public class TitleScreen : Screen
    {
        private bool tapped;

        private TapSound tap;

        private TapToStartFlyout flyout;

        [Resolved]
        private HololiveGame game { get; set; }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChildren = new Drawable[]
            {
                tap = new TapSound(),
                new TitleCharacters(),
                new Sprite
                {
                    Y = 150,
                    Scale = new Vector2(1.5f),
                    Texture = textures.Get("logo"),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                flyout = new TapToStartFlyout
                {
                    Y = -100,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
                new SpriteText
                {
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Margin = new MarginPadding(10),
                    Text = "ver. 0.72.7b (D) This is not a real game."
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (!tapped)
            {
                tap.Play();
                flyout.FadeOut(100);
                game.StartLoading(() => this.Push(new DisclaimerScreen()));
                tapped = true;
            }

            return base.OnMouseDown(e);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);
            this
                .MoveToX(-100, 500)
                .FadeOut(500);
        }
    }
}
