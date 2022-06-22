using System.Collections.Generic;

namespace StableCube.Media.FFProbe.DataModel
{
    public enum VideoFormat
    {
        Unknown,
        Flv, 
        Mpeg, 
        Asf, 
        Avi, 
        Mov, 
        Mp4, 
        M4a, 
        _3gp, 
        _3g2, 
        Mj2, 
        Ogg,
        Matroska, 
        Webm, 
        Mpegts
    }

    public class FileTypeInfo
    {
        public bool IsVideo { get; set; }

        public List<VideoFormat> Format { get; set; }

        public string CodecName { get; set; }
        
        public string CodecType { get; set; }
    }
}