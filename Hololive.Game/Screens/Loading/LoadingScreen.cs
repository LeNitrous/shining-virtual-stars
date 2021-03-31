using Hololive.Game.Grpahics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osu.Framework.Utils;
using osuTK;
using System.Collections.Generic;
using System.Linq;

namespace Hololive.Game.Screens.Loading
{
    public class LoadingScreen : Screen
    {
        private ScheduledDelegate randomizer;

        private CharacterDetail current;

        private string lastCharacter;

        private static Dictionary<string, string> characterNameMap = new Dictionary<string, string>
        {
            { "aqua", "Minato Aqua" },
            { "flare", "Shiranui Flare" },
            { "pekora", "Usada Pekora" },
            { "roboco", "Robocco" },
            { "sora", "Tokino Sora" },
            { "fubuki", "Shirakami Fubuki" },
            { "miko", "Sakura Miko" },
            { "marine", "Senchou Marine" },
            { "matsuri", "Natsuiro Matsuri" },
            { "mio", "Ookami Mio" },
            { "suisei", "Hoshimachi Suisei" },
            { "haachama", "Akai Haato" },
            { "okayu", "Nekomata Okayu" },
            { "korone", "Inugami Korone" },
            { "u0", "Aki Rosenthal" },
            { "u1", "Oozora Subaru" },
            { "u2", "Nakiri Ayame" },
            { "u3", "Murasaki Shion" },
            { "u4", "Yozora Mel" },
            { "u5", "Yuzuki Choco" },
            { "u6", "AZki" },
            { "u7", "Shirogane Noel" },
            { "u8", "Uruha Rushia" },
        };

        private static Dictionary<string, Colour4> characterColourMap = new Dictionary<string, Colour4>
        {
            { "sora", Colour4.FromHex("587fdb") },
            { "fubuki", Colour4.FromHex("98e5f5") },
            { "korone", Colour4.FromHex("f7ba63") },
            { "aqua", Colour4.FromHex("f280e5") },
            { "u5", Colour4.FromHex("f7ce5c") },
            { "u7", Colour4.FromHex("9babb0") },
            { "flare", Colour4.FromHex("ffac40") },
            { "roboco", Colour4.FromHex("f04a4a") },
            { "u0", Colour4.FromHex("5fadf5") },
            { "okayu", Colour4.FromHex("f483fc") },
            { "mio", Colour4.FromHex("e33a27") },
            { "u3", Colour4.FromHex("ab32db") },
            { "u1", Colour4.FromHex("98f562") },
            { "pekora", Colour4.FromHex("b3edff") },
            { "marine", Colour4.FromHex("f52f5d") },
            { "miko", Colour4.FromHex("ff87c1") },
            { "haachama", Colour4.FromHex("d93452") },
            { "u2", Colour4.FromHex("ff91ab") },
            { "u8", Colour4.FromHex("8cf5d0") },
            { "u6", Colour4.FromHex("f5316f") },
            { "u4", Colour4.FromHex("ffec19") },
        };

        public LoadingScreen()
        {
            AddInternal(new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Rotation = 45,
                RelativeSizeAxes = Axes.X,
                Height = 400,
                Width = 1.2f,
            });
        }

        [BackgroundDependencyLoader]
        private void load(HololiveGame game)
        {
            game.StartLoading(() => { while (true) ; }, 15000);

            current = generateNext();
            AddInternal(current);
            game.FadeBackgroundColor(getColour(current.AssetName));

            randomizer = Scheduler.AddDelayed(() =>
            {
                var next = generateNext();
                next.Alpha = 0;
                next.X += 100;

                game.FadeBackgroundColor(getColour(next.AssetName));

                current?
                        .MoveToOffset(new Vector2(-100, 0), 500)
                        .FadeOut(500)
                        .Expire();

                next.Depth = current?.Depth - 1 ?? 0;

                AddInternal(next);
                current = next;

                next
                    .MoveToOffset(new Vector2(-100, 0), 500)
                    .FadeIn(500);
            }, 10000, true);
        }

        private Colour4 getColour(string name)
        {
            if (characterColourMap.TryGetValue(name, out Colour4 colour))
                return colour;
            else
                return Colour4.Aqua;
        }

        private CharacterDetail generateNext()
        {
            var nextCharacter = getRandomCharacter();
            var detail = new CharacterDetail(nextCharacter.Key, nextCharacter.Value, getColour(nextCharacter.Key));
            return detail;
        }

        private KeyValuePair<string, string> getRandomCharacter()
        {
            var next = characterNameMap.ElementAt(RNG.Next(characterNameMap.Count));
            
            if (next.Value == lastCharacter)
                return getRandomCharacter();

            lastCharacter = next.Value;
            return next;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            randomizer?.Cancel();
        }

        private class CharacterDetail : Container
        {
            public string AssetName { get; private set; }

            public CharacterDetail(string assetName, string fullName, Colour4 colour)
            {
                AssetName = assetName;
                RelativeSizeAxes = Axes.Both;

                Children = new Drawable[]
                {
                    new SpriteText
                    {
                        X = 500,
                        Spacing = new Vector2(-20, 0),
                        Rotation = 90,
                        Colour = colour,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = new FontUsage("Chicago", 25),
                        Text = fullName.ToUpperInvariant(),
                    },
                    new CharacterSprite(assetName)
                    {
                        Alpha = 0.5f,
                        Position = new Vector2(-10, 280),
                        Scale = new Vector2(1.5f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Blending = new BlendingParameters
                        {
                            Source = BlendingType.Zero,
                            SourceAlpha = BlendingType.Zero,
                            Destination = BlendingType.OneMinusSrcAlpha,
                            DestinationAlpha = BlendingType.OneMinusSrcAlpha,
                        },
                    },
                    new CharacterSprite(assetName)
                    {
                        Position = new Vector2(0, 250),
                        Scale = new Vector2(1.5f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                };
            }
        }
    }
}
