using System;
using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.DataModel
{
    public struct FFMpegProgressLogEntry
    {
        public long CurrentFrame { get; set; }
        public float FramesPerSecond { get; set; }
        public IList<StreamProgress> Streams { get; set; }
        public string Bitrate { get; set; }
        public long TotalSize { get; set; }
        public TimeSpan OutTime { get; set; }
        public int DupFrames { get; set; }
        public int DropFrames { get; set; }
        public decimal Speed { get; set; }
        public string Progress { get; set; }

        public FFMpegProgressLogEntry(IList<string> entryLines)
        {
            if(!entryLines.Last().StartsWith("progress"))
                throw new IndexOutOfRangeException($"Last log entry line '{entryLines.Last()}' must start with 'progress'");

            Streams = new List<StreamProgress>();
            CurrentFrame = -1;
            FramesPerSecond = -1;
            Bitrate = null;
            TotalSize = -1;
            OutTime = TimeSpan.Zero;
            DupFrames = -1;
            DropFrames = -1;
            Speed = -1;
            Progress = null;

            foreach (var entryLine in entryLines)
            {
                string[] linePair = entryLine.Split('=');
                if(linePair.Length != 2)
                    throw new IndexOutOfRangeException($"Line does not contain a valid key value pair {entryLine}");
                
                string lineKey = linePair[0].Trim();
                string lineValue = linePair[1].Trim();

                if(lineKey.StartsWith("stream_"))
                {
                    string[] streamKeyParts = lineKey.Split('_');

                    Streams.Add(new StreamProgress(
                        number: Convert.ToInt32(streamKeyParts[1]),
                        index: Convert.ToInt32(streamKeyParts[2]),
                        value: ParseFloatValue(lineValue)
                    ));
                }
                else
                {
                    switch (lineKey)
                    {
                        case "frame":
                            CurrentFrame = ParseLongValue(lineValue);
                        break;
                        case "fps":
                            FramesPerSecond = ParseFloatValue(lineValue);
                        break;
                        case "bitrate":
                            Bitrate = lineValue;
                        break;
                        case "total_size":
                            TotalSize = ParseLongValue(lineValue);
                        break;
                        case "out_time_us":
                        break;
                        case "out_time_ms":
                            OutTime = TimeSpan.FromMilliseconds(ConvertToRealMilliseconds(ParseLongValue(lineValue)));
                        break;
                        case "out_time":
                        break;
                        case "dup_frames":
                            DupFrames = ParseIntValue(lineValue);
                        break;
                        case "drop_frames":
                            DropFrames = ParseIntValue(lineValue);
                        break;
                        case "speed":
                            Speed = ParseSpeedValue(lineValue);
                        break;
                        case "progress":
                            Progress = lineValue;
                        break;
                    }
                }
            }
        }

        private static decimal ParseSpeedValue(string value)
        {
            string speedTrimmed = value.Remove(value.Length - 1, 1);

            decimal speed;
            if(!decimal.TryParse(speedTrimmed, out speed))
                speed = 0;

            return speed;
        }

        private static int ParseIntValue(string value)
        {
            int val = 0;
            if(!Int32.TryParse(value, out val))
                return -1;

            return val;
        }

        private static float ParseFloatValue(string value)
        {
            float val = 0;
            if(!float.TryParse(value, out val))
                return -1;

            return val;
        }

        private static long ParseLongValue(string value)
        {
            long val = 0;
            if(!long.TryParse(value, out val))
                return -1;

            return val;
        }

        /// <summary>
        /// FFMpeg stores it's "milliseconds" as nanoseconds. This converts it 
        /// to actual milliseconds
        /// </summary>
        /// <param name="ffmpegMilliseconds"></param>
        /// <returns></returns>
        public static long ConvertToRealMilliseconds(long ffmpegMilliseconds)
        {
            return ffmpegMilliseconds / 10000;
        }
    }
}