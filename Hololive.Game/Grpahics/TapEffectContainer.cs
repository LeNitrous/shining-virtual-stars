using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using System;

namespace Hololive.Game.Grpahics
{
    public class TapEffectContainer : Container
    {
        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Add(new TapExplosion { Position = e.MouseDownPosition });
            return base.OnMouseDown(e);
        }

        private class TapExplosion : Container
        {
            private const int particle_count = 5;

            private const double duration = 200;

            private static Colour4[] colours = new[]
            {
                Colour4.Aqua,
                Colour4.LightGreen,
                Colour4.Pink,
                Colour4.LightPink,
                Colour4.OrangeRed,
                Colour4.LightYellow,
            };

            public TapExplosion()
            {
                Origin = Anchor.Centre;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                for (int i = 0; i < particle_count; i++)
                {
                    var sprite = new Sprite
                    {
                        Texture = textures.Get("star"),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = colours[RNG.Next(colours.Length)],
                    };

                    float angle = RNG.NextSingle() * 360;

                    Add(sprite);
                    sprite
                        .Spin(duration, RotationDirection.Clockwise, 0, 10)
                        .MoveToOffset(new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * 60, duration)
                        .ResizeTo(new Vector2(RNG.NextSingle() * 60), duration)
                        .FadeOut(duration);

                    this
                        .Delay(duration)
                        .Expire();
                }
            }
        }
    }
}
