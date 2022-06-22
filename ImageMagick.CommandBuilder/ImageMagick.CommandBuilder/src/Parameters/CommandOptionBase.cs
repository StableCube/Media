
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public abstract class CommandOptionBase : ICommandOption
    {
        public virtual bool Alternate { get; set; }
        
        public abstract string Key { get; }

        public abstract string Value { get; }

        public override string ToString()
        {
            string command = Alternate ? "+" : "-";
            command += Key;

            if(!Alternate)
                command += " " + Value;

            return command;
        }

        public ImageMagickParameter GetParameter()
        {
            string key = Alternate ? "+" : "-";
            key += Key;
            
            string value = null;
            if(!Alternate)
                value = Value;

            return new ImageMagickParameter(key, value);
        }
    }
}