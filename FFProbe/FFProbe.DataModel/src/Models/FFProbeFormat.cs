using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StableCube.Media.FFProbe.DataModel
{
    public class FFProbeFormat
    {
        [JsonPropertyName("filename")]
        public string FileName  { get; set; }

        [JsonPropertyName("nb_streams")]
        public int StreamCount  { get; set; }

        [JsonPropertyName("nb_programs")]
        public int ProgramCount  { get; set; }

        [JsonPropertyName("format_name")]
        public string FormatName  { get; set; }

        [JsonPropertyName("format_long_name")]
        public string FormatLongName  { get; set; }

        [JsonPropertyName("start_time")]
        public float StartTime  { get; set; }

        [JsonPropertyName("duration")]
        public float Duration  { get; set; }

        [JsonPropertyName("size")]
        public long Size  { get; set; }

        [JsonPropertyName("bit_rate")]
        public int BitRate  { get; set; }

        [JsonPropertyName("probe_score")]
        public int ProbeScore  { get; set; }

        [JsonPropertyName("tags")]
        public Dictionary<string, string> Tags { get; set; }
    }
}