using System;
using System.Linq;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// The structure of an FFMpeg command
    /// 
    /// ffmpeg option_group [[global_options] {[input_file_options] -i input_url} ... {[output_file_options] output_url}] ... 
    /// </summary>
    public class CommandTaskBuilder
    {
        public FFMpegCommandTask Task { get; private set; }

        public CommandTaskBuilder()
        {
            Task = new FFMpegCommandTask();
        }

        public CommandTaskBuilder(FFMpegCommandTask source)
        {
            Task = source;
        }

        public CommandOptionGroupBuilder BuildOptionGroup(int? index = null)
        {
            if(Task == null)
                throw new NullReferenceException("Task");

            if(Task.CommandOptionGroups == null)
                throw new NullReferenceException("Task.CommandOptionGroups");

            FFMpegCommandOptionGroup opGroup = null;
            if(index.HasValue)
            {
                if(!Task.CommandOptionGroups.TryGetValue(index.Value, out opGroup))
                {
                    opGroup = new FFMpegCommandOptionGroup(index.Value);
                    Task.CommandOptionGroups.Add(index.Value, opGroup);
                }
            }
            else
            {
                int lastkey = Task.CommandOptionGroups.Keys.Last();
                int newKey = lastkey + 1;
                opGroup = new FFMpegCommandOptionGroup(newKey);
                Task.CommandOptionGroups.Add(newKey, opGroup);
            }

            return new CommandOptionGroupBuilder(opGroup);
        }
    }
}