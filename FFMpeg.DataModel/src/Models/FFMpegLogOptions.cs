using System;

namespace StableCube.Media.FFMpeg.DataModel
{
    public class FFMpegLogOptions
    {
        public string LogFilePath { get; set; }

        public string SourceFilePath { get; set; }

        public IProgress<ProgressResult> ProgressData { get; set; }
    }
}
