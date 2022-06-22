using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public struct VideoInfo
    {
        public string FilePath { get; set; }

        public VideoFormatInfo Format { get; set; }

        public SortedDictionary<int, VideoStreamInfo> Streams { get; set; }

        public VideoStreamInfo DefaultVideoStream { get { return GetDefaultStream(StreamCodecType.Video); } }
        public IList<VideoStreamInfo> VideoStreams { get { return GetStreamsByType(StreamCodecType.Video); } }
        public VideoStreamInfo DefaultAudioStream { get { return GetDefaultStream(StreamCodecType.Audio); } }
        public IList<VideoStreamInfo> AudioStreams { get { return GetStreamsByType(StreamCodecType.Audio); } }
        public VideoStreamInfo DefaultSubtitleStream { get { return GetDefaultStream(StreamCodecType.Subtitle); } }
        public IList<VideoStreamInfo> SubtitleStreams { get { return GetStreamsByType(StreamCodecType.Subtitle); } }
        public VideoStreamInfo DefaultDataStream { get { return GetDefaultStream(StreamCodecType.Data); } }
        public IList<VideoStreamInfo> DataStreams { get { return GetStreamsByType(StreamCodecType.Data); } }
        public VideoStreamInfo DefaultAttachmentStream { get { return GetDefaultStream(StreamCodecType.Attachment); } }
        public IList<VideoStreamInfo> AttachmentStreams { get { return GetStreamsByType(StreamCodecType.Attachment); } }

        /// <summary>
        /// Returns the first stream of type by index id
        /// </summary>
        public VideoStreamInfo GetDefaultStream(StreamCodecType type)
        {
            foreach (var pair in Streams)
            {
                if(pair.Value.CodecType == type)
                    return pair.Value;
            }

            return null;
        }

        /// <summary>
        /// All streams by type
        /// </summary>
        public IList<VideoStreamInfo> GetStreamsByType(StreamCodecType type)
        {
            IList<VideoStreamInfo> result = new List<VideoStreamInfo>();
            foreach (var pair in Streams)
            {
                if(pair.Value.CodecType == type)
                    result.Add(pair.Value);
            }

            return result;
        }
    }
}