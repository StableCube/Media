using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class InputOption : MainOptionBase
    {
        public const string Key = "-i";

        /// <summary>
        /// File path on disk or Url
        /// </summary>
        public string Path { get; set; }

        public InputOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            if(Path == null)
                throw new ArgumentNullException("Path");

            return new CommandParam(OptionKey, Path);
        }
    }
}