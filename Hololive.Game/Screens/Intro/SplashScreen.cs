using Hololive.Game.Screens.Title;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace Hololive.Game.Screens.Intro
{
    public class SplashScreen : Screen
    {
        public SplashScreen()
        {
            InternalChildren = new Drawable[]
            {
                new Box { RelativeSizeAxes = Axes.Both },
            };
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            LoadComponentAsync(new TitleScreen(), loaded =>
            {
                this
                    .Delay(5000)
                    .OnComplete(_ => this.Push(loaded));
            });
        }
    }
}
