using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Graphics;

namespace Hololive.Game.Audio
{
    public class TapSound : Component
    {
        private Sample tapSample;

        [BackgroundDependencyLoader]
        private void load(AudioManager manager)
        {
            tapSample = manager.GetSampleStore().Get("title-tap");
        }

        public void Play()
        {
            tapSample?.Play();
        }
    }
}
