
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Generic
{
    public abstract class GenericOptionBase : OptionBase, IGenericOption
    {
        public const string OptionTypeId = "Generic";

        public GenericOptionBase(string key) : base(OptionTypeId, key) {}
    }
}