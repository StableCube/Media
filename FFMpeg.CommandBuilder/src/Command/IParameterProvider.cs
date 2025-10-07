using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public interface IParameterProvider
    {
        List<CommandParam> GetParameters();
    }
}