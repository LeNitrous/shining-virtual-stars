using osu.Framework.IO.Stores;

namespace Hololive.Game.IO
{
    public class VideoStore : ResourceStore<byte[]>
    {
        public VideoStore(IResourceStore<byte[]> underlyingStore)
            : base(underlyingStore)
        {
            AddExtension(@"mp4");
        }
    }
}
