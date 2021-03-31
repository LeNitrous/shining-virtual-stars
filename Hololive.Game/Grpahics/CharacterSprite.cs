using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace Hololive.Game.Grpahics
{
    public class CharacterSprite : Sprite
    {
        public string AssetName { get; private set; }

        public CharacterSprite(string asset)
        {
            AssetName = asset;

            Size = new Vector2(1000);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures)
        {
            Texture = textures.Get(AssetName);
        }
    }
}
