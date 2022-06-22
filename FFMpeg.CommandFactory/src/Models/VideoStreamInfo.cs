using System;
using StableCube.Media.FFMpeg.CommandBuilder;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public class VideoStreamInfo
    {
        public int Index { get; set; }
        public string CodecName { get; set; }
        public StreamCodecType CodecType { get; set; }
        public string CodecTimeBase { get; set; }
        public TimeSpan Duration { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string RFrameRate  { get; set; }
        public TimeSpan? RFrameRateTime  { get { return GetRFrameRateTime(); } }
        public string AvgFrameRate  { get; set; }
        public TimeSpan? AvgFrameRateTime  { get { return GetRFrameRateTime(); } }
        public StreamSpecifier StreamSpecifier  { get { return new StreamSpecifier(Index); } }

        public VideoStreamInfo()
        {
        }

        /// <summary>
        /// Convert an fps in the format of 30000/1001 to a double like 29.97
        /// </summary>
        public double? FpsNumeric(string fpsStr)
        {
            if(fpsStr == null)
                return null;
                
            string[] fpsStrSplit = fpsStr.Split('/');
            if(fpsStrSplit.Length != 2)
                return null;
            
            double splitL = Convert.ToDouble(fpsStrSplit[0]);
            double splitR = Convert.ToDouble(fpsStrSplit[1]);

            double? fps = null;
            if(splitL != 0 && splitR != 0)
                fps = splitL / splitR;

            return fps;
        }

        public TimeSpan? GetRFrameRateTime()
        {
            if(RFrameRate == null)
                return null;
            
            double? fps = FpsNumeric(RFrameRate);

            return TimeSpan.FromSeconds(fps.Value);
        }

        public TimeSpan? GetAvgFrameRateTime()
        {
            if(AvgFrameRate == null)
                return null;
            
            double? fps = FpsNumeric(AvgFrameRate);
            
            return TimeSpan.FromSeconds(fps.Value);
        }
    }
}