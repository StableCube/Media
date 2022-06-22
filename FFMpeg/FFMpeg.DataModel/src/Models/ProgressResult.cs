using System;

namespace StableCube.Media.FFMpeg.DataModel
{
    public struct ProgressResult
    {
        public int Progress { get; private set; }

        public decimal Speed { get; private set; }

        public ProgressResult(int progress, decimal speed)
        {
            Progress = progress;
            Speed = speed;
        }

        /// <summary>
        /// Percentage based progress
        /// </summary>
        public ProgressResult(TimeSpan sourceDuration, TimeSpan encodeTime, decimal speed)
        {
            Speed = speed;
            Progress = (int)Math.Floor((double)(((double)encodeTime.TotalMilliseconds / (double)sourceDuration.TotalMilliseconds) * 1000));

            if(Progress > 100)
                Progress = 100;
            
            if(Progress < 0)
                Progress = 0;
        }

        /// <summary>
        /// Simple current frame progress for when the source duration cannot be determined
        /// </summary>
        /// <param name="currentFrame"></param>
        public ProgressResult(long totalFrames, long currentFrame, decimal speed)
        {
            Speed = speed;
            Progress = Convert.ToInt32(currentFrame / totalFrames) * 100;
        }
    }
}