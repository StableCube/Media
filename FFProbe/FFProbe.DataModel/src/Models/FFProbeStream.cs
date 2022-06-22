using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StableCube.Media.FFProbe.DataModel
{
    public class FFProbeStream
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("codec_name")]
        public string CodecName { get; set; }

        [JsonPropertyName("codec_type")]
        public string CodecType { get; set; }

        [JsonPropertyName("codec_long_name")]
        public string CodecLongName { get; set; }

        [JsonPropertyName("codec_time_base")]
        public string CodecTimeBase { get; set; }

        [JsonPropertyName("codec_tag_string")]
        public string CodecTagString { get; set; }

        [JsonPropertyName("codec_tag")]
        public string CodecTag { get; set; }

        [JsonPropertyName("duration")]
        public float Duration  { get; set; }

        [JsonPropertyName("r_frame_rate")]
        public string RFrameRate { get; set; }

        [JsonPropertyName("avg_frame_rate")]
        public string AvgFrameRate { get; set; }

        [JsonPropertyName("time_base")]
        public string TimeBase { get; set; }

        [JsonPropertyName("start_time")]
        public float StartTime { get; set; }

        [JsonPropertyName("start_pts")]
        public float StartPTS { get; set; }

        [JsonPropertyName("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonPropertyName("disposition")]
        public Dictionary<string, int> Disposition { get; set; }

        [JsonPropertyName("profile")]
        public string Profile { get; set; }

        [JsonPropertyName("nb_frames")]
        public long TotalFrames  { get; set; }

        [JsonPropertyName("nb_read_frames")]
        public long TotalReadFrames  { get; set; }


        //---------- Video Specific ----------
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("coded_width")]
        public int CodedWidth { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("coded_height")]
        public int CodedHeight { get; set; }

        [JsonPropertyName("has_b_frames")]
        public int HasBFrames { get; set; }

        [JsonPropertyName("sample_aspect_ratio")]
        public string SampleAspectRatio { get; set; }

        [JsonPropertyName("display_aspect_ratio")]
        public string DisplayAspectRatio { get; set; }

        [JsonPropertyName("pix_fmt")]
        public string PixelFormat { get; set; }

        [JsonPropertyName("level")]
        public int Level  { get; set; }

        [JsonPropertyName("color_range")]
        public string ColorRange  { get; set; }

        [JsonPropertyName("color_space")]
        public string ColorSpace  { get; set; }

        [JsonPropertyName("color_transfer")]
        public string ColorTransfer  { get; set; }

        [JsonPropertyName("color_primaries")]
        public string ColorPrimaries  { get; set; }

        [JsonPropertyName("chroma_location")]
        public string ChromaLocation  { get; set; }

        [JsonPropertyName("refs")]
        public int Refs  { get; set; }

        [JsonPropertyName("is_avc")]
        public bool IsAvc  { get; set; }

        [JsonPropertyName("nal_length_size")]
        public int NalLengthSize  { get; set; }

        [JsonPropertyName("bits_per_raw_sample")]
        public int BitsPerRawSample  { get; set; }


        //---------- Audio Specific ----------
        [JsonPropertyName("sample_fmt")]
        public string SampleFMT { get; set; }

        [JsonPropertyName("sample_rate")]
        public int SampleRate { get; set; }

        [JsonPropertyName("channels")]
        public int Channels { get; set; }

        [JsonPropertyName("channel_layout")]
        public string ChannelLayout { get; set; }

        [JsonPropertyName("bits_per_sample")]
        public int BitsPerSample { get; set; }
    }
}