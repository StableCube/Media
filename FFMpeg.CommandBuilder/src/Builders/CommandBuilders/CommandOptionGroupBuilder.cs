using System;
using System.Collections.Generic;
using System.Linq;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public struct CommandOptionGroupBuilder
    {
        private FFMpegCommandOptionGroup _optionGroup;

        public FFMpegCommandOptionGroup CommandOptionGroup 
        { 
            get 
            {
                return _optionGroup; 
            } 
        }

        /// <summary>
        /// Global commands that appear before input
        /// </summary>
        public GlobalScopeBuilder GlobalScope 
        { 
            get 
            { 
                return new GlobalScopeBuilder(_optionGroup.GlobalOptions); 
            } 
        }

        /// <summary>
        /// Commands that appear before out file declaration
        /// </summary>
        public OutputScopeBuilder OutputScope 
        { 
            get 
            { 
                return new OutputScopeBuilder(_optionGroup.OutputFileOptions); 
            } 
        }

        public FilterGraphBuilder FilterGraphScope 
        { 
            get 
            { 
                return new FilterGraphBuilder(_optionGroup.ComplexFilterGraph); 
            } 
        }

        public CommandOptionGroupBuilder(FFMpegCommandOptionGroup source)
        {
            _optionGroup = source;
        }

        public CommandOptionGroupBuilder AddInput(int index, string filePath)
        {
            FFMpegInput fileInput = new FFMpegInput(filePath);
            _optionGroup.AddInputFile(fileInput, index);

            return this;
        }

        public CommandOptionGroupBuilder AddInput(string filePath)
        {
            FFMpegInput fileInput = new FFMpegInput(filePath);
            _optionGroup.AddInputFile(fileInput, 0);

            return this;
        }

        public CommandOptionGroupBuilder AddOutput(int index, string filePath)
        {
            _optionGroup.AddOutputFile(filePath, index);

            return this;
        }

        public CommandOptionGroupBuilder AddOutput(string filePath)
        {
            _optionGroup.AddOutputFile(filePath, 0);

            return this;
        }

        /// <summary>
        /// Returns an input builder by creating a new input or if index exists using that
        /// </summary>
        public InputScopeBuilder InputScope(int index, string filePath)
        {
            FFMpegInput fileInput = null;
            if(_optionGroup.InputFiles.TryGetValue(index, out fileInput))
                return new InputScopeBuilder(fileInput.Options);

            fileInput = new FFMpegInput(filePath);
            int lastkey = _optionGroup.InputFiles.Keys.Last();
            int newKey = lastkey + 1;

            _optionGroup.AddInputFile(fileInput, newKey);

            return new InputScopeBuilder(fileInput.Options);
        }

        /// <summary>
        /// Returns an input builder by index
        /// </summary>
        public InputScopeBuilder InputScope(int index)
        {
            FFMpegInput fileInput = null;
            if(_optionGroup.InputFiles.TryGetValue(index, out fileInput))
                return new InputScopeBuilder(fileInput.Options);

            throw new KeyNotFoundException($"index: {index}");
        }
    }
}