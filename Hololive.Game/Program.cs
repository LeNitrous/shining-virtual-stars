using osu.Framework;

namespace Hololive.Game
{
    public static class Program
    {
        public static void Main()
        {
            using var host = Host.GetSuitableHost(@"hololive-conceptual-game", false, true);
            using var game = new HololiveGame();
            host.Run(game);
        }
    }
}
