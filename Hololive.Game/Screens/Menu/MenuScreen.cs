using Hololive.Game.IO;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Video;
using osu.Framework.Screens;
using System;

namespace Hololive.Game.Screens.Menu
{
    public class MenuScreen : Screen
    {
        private Video video;

        [BackgroundDependencyLoader]
        private void load(HololiveGame game, AudioManager audio, VideoStore videos)
        {
            game.GetTrack()?.Stop();

            InternalChild = video = new Video(videos.GetStream("game-intro"))
            {
                RelativeSizeAxes = Axes.Both,
            };

            var track = audio.GetTrackStore().Get("game-intro");
            track.Start();
        }

        protected override void Update()
        {
            base.Update();

            if (video.PlaybackPosition >= 9499)
                throw new NotImplementedException();
        }
    }
}
