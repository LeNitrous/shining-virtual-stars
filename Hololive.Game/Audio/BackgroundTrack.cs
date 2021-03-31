using System;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;

namespace Hololive.Game.Audio
{
    public class BackgroundTrack : Component
    {
        private Track track;

        private readonly string name;

        private readonly double loopStart;

        private readonly double loopEnd;

        public BackgroundTrack(string name, double loopStart, double loopEnd)
        {
            this.name = name;
            this.loopStart = loopStart;
            this.loopEnd = loopEnd;

            if (this.loopStart >= this.loopEnd)
                throw new ArgumentException($"{nameof(loopStart)} cannot be greater than {nameof(loopEnd)}");
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager manager)
        {
            track = manager.GetTrackStore().Get(name);
        }

        public void Start()
        {
            Schedule(() => track?.Start());
        }

        public void Stop()
        {
            Schedule(() => track?.Stop());
        }

        protected override void Update()
        {
            base.Update();

            if ((track?.CurrentTime ?? 0) >= loopEnd)
                track?.Seek(loopStart);
        }
    }
}
