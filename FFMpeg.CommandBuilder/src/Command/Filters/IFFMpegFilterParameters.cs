using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public interface IFFMpegFilterParameters
    {
        List<CommandParam> GetFilterParams();

        string ToString();
    }
}