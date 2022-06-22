using System;

namespace StableCube.Media.ImageMagick.DataModel
{
    public struct ProgressResult
    {
        public int Progress { get; private set; }
        
        public ProgressResult(int progress)
        {
            Progress = progress;
        }
    }
}