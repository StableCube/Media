using System;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class StreamSpecifier
    {
        /// <summary>
        /// Matches the stream with this index. E.g. -threads:1 4 would set the thread count for the second 
        /// stream to 4. If stream_index is used as an additional stream specifier (see below), then it 
        /// selects stream number stream_index from the matching streams. Stream numbering is based on 
        /// the order of the streams as detected by libavformat except when a program ID is also specified. 
        /// In this case it is based on the ordering of the streams in the program.
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// stream_type is one of following: ’v’ or ’V’ for video, ’a’ for audio, ’s’ for subtitle, ’d’ for data, 
        /// and ’t’ for attachments. ’v’ matches all video streams, ’V’ only matches video streams which are not 
        /// attached pictures, video thumbnails or cover arts. If additional_stream_specifier is used, then it 
        /// matches streams which both have this type and match the additional_stream_specifier. Otherwise, 
        /// it matches all streams of the specified type. 
        /// </summary>
        public char? Type { get; set; }

        public static StreamSpecifier Video { get { return new StreamSpecifier('v'); } }

        public static StreamSpecifier Audio { get { return new StreamSpecifier('a'); } }

        public static StreamSpecifier Subtitle { get { return new StreamSpecifier('s'); } }

        public static StreamSpecifier Data { get { return new StreamSpecifier('d'); } }

        public static StreamSpecifier Attachment { get { return new StreamSpecifier('t'); } }

        public StreamSpecifier()
        {
        }

        public StreamSpecifier(int streamIndex)
        {
            Index = streamIndex;
        }

        public StreamSpecifier(char streamType)
        {
            ValidateStreamType(streamType);

            Type = streamType;
        }

        public StreamSpecifier(char streamType, int streamIndex)
        {
            ValidateStreamType(streamType);

            Type = streamType;
            Index = streamIndex;
        }

        private void ValidateStreamType(char streamType)
        {
            if(streamType != 'a' && streamType != 'v' && streamType != 's' && streamType != 'd' && streamType != 't')
                throw new ArgumentOutOfRangeException($"{streamType} is not a valid stream type");
        }

        public override string ToString()
        {
            if(Type.HasValue)
                ValidateStreamType(Type.Value);

            if(Index.HasValue && !Type.HasValue)
                return $"{Index.Value.ToString()}";

            if(!Index.HasValue && Type.HasValue)
                return $"{Type.Value.ToString()}";

            if(Index.HasValue && Type.HasValue)
                return $"{Type.Value.ToString()}:{Index.Value.ToString()}";

            return String.Empty;
        }
    }
}