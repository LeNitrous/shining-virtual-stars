using osu.Framework.IO.Stores;
using System.IO;

namespace Hololive.Game.IO
{
    public class TextStore : ResourceStore<string>
    {
        private readonly IResourceStore<byte[]> store;

        public TextStore(IResourceStore<byte[]> store)
        {
            this.store = store;
            AddExtension(@"md");
        }

        public override string Get(string name)
        {
            using var reader = new StreamReader(store.GetStream(name));
            return reader.ReadToEnd();
        }
    }
}
