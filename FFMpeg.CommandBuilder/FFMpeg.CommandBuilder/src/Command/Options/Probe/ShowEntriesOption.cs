
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    ///     Set list of entries to show.
    /// 
    ///     Entries are specified according to the following syntax. section_entries contains a list of section entries separated by :. 
    ///     Each section entry is composed by a section name (or unique name), optionally followed by a list of entries local 
    ///     to that section, separated by ,.
    /// 
    ///     If section name is specified but is followed by no =, all entries are printed to output, together with all the contained sections. 
    ///     Otherwise only the entries specified in the local section entries list are printed. In particular, if = is specified but the 
    ///     list of local entries is empty, then no entries will be shown for that section.
    /// 
    ///     Note that the order of specification of the local section entries is not honored in the output, and the usual display order 
    ///     will be retained. 
    /// </summary>
    public class ShowEntriesOption : ProbeOptionBase
    {
        public const string Key = "-show_entries";

        public string Entries { get; set; }

        public ShowEntriesOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Entries);
        }
    }
}