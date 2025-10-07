using System;
using System.Collections.Generic;

namespace StableCube.Media.FFProbe.DataModel
{
    public class FFProbeFileInfo
    {
        public FFProbeStream[] Streams { get; set; }
        
        public FFProbeFormat Format { get; set; }

        /// <summary>
        /// Gets the first video stream
        /// </summary>
        public FFProbeStream DefaultVideoStream { get { return GetVideoStreams()[0]; } }

        public FFProbeFileInfo()
        {
        }

        public List<FFProbeStream> GetVideoStreams()
        {
            if(Streams == null)
                throw new NullReferenceException("FFProbeFileInfo.Streams");

            List<FFProbeStream> videoStreams = new List<FFProbeStream>();

            foreach(var stream in Streams)
            {
                if(stream.CodecType == "video")
                    videoStreams.Add(stream);
            }

            return videoStreams;
        }

        public List<FFProbeStream> GetAudioStreams()
        {
            if(Streams == null)
                throw new NullReferenceException("FFProbeFileInfo.Streams");

            List<FFProbeStream> audioStreams = new List<FFProbeStream>();

            foreach(var stream in Streams)
            {
                if(stream.CodecType == "audio")
                    audioStreams.Add(stream);
            }

            return audioStreams;
        }

        public List<FFProbeStream> GetSubtitleStreams()
        {
            if(Streams == null)
                throw new NullReferenceException("FFProbeFileInfo.Streams");

            List<FFProbeStream> subtitleStreams = new List<FFProbeStream>();

            foreach(var stream in Streams)
            {
                if(stream.CodecType == "subtitle")
                    subtitleStreams.Add(stream);
            }

            return subtitleStreams;
        }
    }
}