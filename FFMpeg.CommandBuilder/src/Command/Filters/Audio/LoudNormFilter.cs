using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// EBU R128 loudness normalization. Includes both dynamic and linear normalization modes.
    /// 
    /// https://ffmpeg.org/ffmpeg-all.html#loudnorm
    /// </summary>
    public class LoudNormFilter : FilterBase
    {
        /// <summary>
        /// Set integrated loudness target. Range is -70.0 - -5.0. Default value is -24.0. 
        /// </summary>
        public double? TargetIntegratedLoudness { get; set; }

        /// <summary>
        /// Set maximum true peak. Range is -9.0 - +0.0. Default value is -2.0. 
        /// </summary>
        public double? MaximumTruePeak { get; set; }

        /// <summary>
        /// Set loudness range target. Range is 1.0 - 50.0. Default value is 7.0. 
        /// </summary>
        public double? TargetLoudnessRange { get; set; }

        public LoudNormFilter()
        {
            FilterName = "loudnorm";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(TargetIntegratedLoudness.HasValue)
                paramList.Add(new CommandParam("I", TargetIntegratedLoudness.Value.ToString()));

            if(MaximumTruePeak.HasValue)
                paramList.Add(new CommandParam("TP", MaximumTruePeak.ToString()));

            if(TargetLoudnessRange.HasValue)
                paramList.Add(new CommandParam("LRA", TargetLoudnessRange.ToString()));

            return paramList;
        }
    }
}