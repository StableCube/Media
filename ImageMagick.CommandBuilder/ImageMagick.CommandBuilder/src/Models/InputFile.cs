
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class InputFile
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Used to select frames. Should be in the format [0] or [0-3] or [3,2,4]
        /// 
        /// In the command this is appended to the filename for something like this
        /// magick 'images.gif[0]' image.png
        /// </summary>
        public string FrameSelector { get; set; }

        /// <summary>
        /// Options for this input
        /// </summary>
        public CommandOptionSequence OptionSequence { get; set; } = new CommandOptionSequence();

        public InputFile()
        {
        }

        public InputFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}