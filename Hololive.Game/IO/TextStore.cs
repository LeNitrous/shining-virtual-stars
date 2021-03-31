using osu.Framework.IO.Stores;
using System.Text;

namespace Hololive.Game.IO
{
    public class TextStore : ResourceStore<string>
    {
        private readonly IResourceStore<byte[]> store;

        public TextStore(IResourceStore<byte[]> store)
        {
            this.store = store;
            AddExtension(@"md");
            AddExtension(@"txt");
        }

        public string Get(Encoding encoding, string name) => encoding.GetString(store.Get(name));

        public override string Get(string name) => Get(Encoding.UTF8, name);
    }
}
