
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public abstract class StreamOptionBase : OptionBase, IStreamOption
    {
        public StreamSpecifier Stream { get; set; }

        protected string StreamOptionKey 
        { 
            get 
            {
                if(Stream == null)
                    return OptionKey; 

                return $"{OptionKey}:{Stream.ToString()}"; 
            } 
        }

        public StreamOptionBase(string optionType, string optionKey) : base(optionType, optionKey)
        {
        }

        public StreamOptionBase(string optionType, string optionKey, StreamSpecifier stream) : base (optionType, optionKey)
        {
            Stream = stream;
        }
    }
}