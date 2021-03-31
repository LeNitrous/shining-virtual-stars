using osu.Framework;

namespace Hololive.Game.Desktop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var host = Host.GetSuitableHost(@"ShiningVirtualStars", false, true);
            using var game = new HololiveGameDesktop(args);
            host.Run(game);
        }
    }
}