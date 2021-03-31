using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Hololive.Game.Grpahics
{
    public class ScrollingBackground : CompositeDrawable
    {
        private Sprite l, r;

        private const float speed = 0.05f;

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new[]
            {
                l = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Texture = textures.Get("pattern-bg"),
                },
                r = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Texture = textures.Get("pattern-bg"),
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            r.X = DrawWidth;
        }

        protected override void Update()
        {
            base.Update();

            l.X -= speed;
            r.X -= speed;

            if (l.X < -DrawWidth)
                l.X = DrawWidth;

            if (r.X < -DrawWidth)
                r.X = DrawWidth;
        }
    }
}
