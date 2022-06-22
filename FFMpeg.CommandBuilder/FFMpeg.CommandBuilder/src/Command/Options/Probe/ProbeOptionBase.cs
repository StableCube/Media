
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    public abstract class ProbeOptionBase : OptionBase, IProbeOption
    {
        public const string OptionTypeId = "Probe";

        public ProbeOptionBase(string key) : base(OptionTypeId, key) {}
    }
}