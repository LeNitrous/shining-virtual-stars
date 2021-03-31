using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osuTK;
using System.Linq;
using System.Collections.Generic;
using osu.Framework.Threading;
using Hololive.Game.Grpahics;

namespace Hololive.Game.Screens.Title
{
    public class TitleCharacters : Container<CharacterSprite>
    {
        private ScheduledDelegate scrollSchedule;

        private readonly List<Vector2> characterOffsets = new List<Vector2>
        {
            new Vector2(500, 100),
            new Vector2(-500, 100),
            new Vector2(250, 50),
            new Vector2(-250, 50),
        };

        private readonly List<float> characterScales = new List<float>
        {
            1.0f,
            1.0f,
            0.85f,
            0.85f,
        };

        private static string[] assets => new[]
        {
            "aqua",
            "flare",
            "fubuki",
            "haachama",
            "korone",
            "marine",
            "matsuri",
            "miko",
            "mio",
            "okayu",
            "pekora",
            "roboco",
            "suisei",
            // "sora", // never changes
            "u0",
            "u1",
            "u2",
            "u3",
            "u4",
            "u5",
            "u6",
            "u7",
            "u8",
        };

        public TitleCharacters()
        {
            RelativeSizeAxes = Axes.Both;
            Child = new CharacterSprite("sora")
            {
                Depth = int.MaxValue,
                Scale = new Vector2(0.8f),
            };
        }

        public void Refresh(bool animate = true)
        {
            foreach (var character in Children)
            {
                if (character.AssetName == "sora")
                    continue;

                character
                    .MoveToOffset(new Vector2(-100, 0), 500)
                    .FadeOut(500)
                    .Expire();
            }

            for (int i = 0; i < 4; i++)
            {
                var character = getNextCharacter().With(c =>
                {
                    c.Depth = i - 1;
                    c.Scale = new Vector2(characterScales[i]);
                    c.Position = characterOffsets[i];

                    if (animate)
                    {
                        c.Position += new Vector2(100, 0);
                        c.Alpha = 0;
                    }
                });

                Add(character);

                if (animate)
                {
                    character
                        .MoveToOffset(new Vector2(-100, 0), 500)
                        .FadeIn(500);
                }
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Schedule(() => Refresh(false));
            scrollSchedule = Scheduler.AddDelayed(() => Refresh(), 5000, true);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            scrollSchedule.Cancel();
        }

        private CharacterSprite getNextCharacter()
        {
            string next = assets[RNG.Next(assets.Length)];

            if (Children.Any(s => s.AssetName == next))
                    return getNextCharacter();

            return new CharacterSprite(next);
        }
    }
}
