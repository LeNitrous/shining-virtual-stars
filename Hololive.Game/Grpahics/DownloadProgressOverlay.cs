using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Threading;
using osu.Framework.Utils;
using osuTK;
using System;

namespace Hololive.Game.Grpahics
{
    public class DownloadProgressOverlay : Container
    {
        private Action onComplete;

        private readonly Box progress;

        private readonly double interval;

        private ScheduledDelegate progressUpdater;

        public DownloadProgressOverlay(Action onComplete = null, double interval = 1000)
        {
            this.onComplete = onComplete;
            this.interval = interval;

            Size = new Vector2(400, 65);
            Masking = true;
            CornerRadius = 5;
            BorderColour = Colour4.LightGray;
            BorderThickness = 1.5f;
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            Margin = new MarginPadding(20);

            Children = new Drawable[]
            {
                new Box { RelativeSizeAxes = Axes.Both },
                new SpriteText
                {
                    Colour = Colour4.Black,
                    Margin = new MarginPadding(10),
                    Text = "Downloading..."
                },
                new Container
                {
                    Masking = true,
                    CornerRadius = 5,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 10 },
                    RelativeSizeAxes = Axes.X,
                    Height = 20,
                    Width = 0.9f,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.LightGray,
                        },
                        progress = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Aqua,
                            Width = 0,
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateProgressBar();   
        }

        private void updateProgressBar()
        {
            if (progress.Width >= 1.0f)
            {
                onComplete?.Invoke();
                Expire();
                return;
            }

            progressUpdater = Scheduler.AddDelayed(() =>
            {
                progress.ResizeWidthTo(progress.Width + RNG.NextSingle(0.1f, 0.5f), 100);
                updateProgressBar();
            }, RNG.NextDouble() * interval);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            progressUpdater.Cancel();
        }
    }
}
