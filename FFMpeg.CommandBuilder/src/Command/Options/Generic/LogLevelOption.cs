using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Generic
{
    public enum FFMpegLogLevel
    {
        Quiet,
        Panic,
        Fatal,
        Error,
        Warning,
        Info,
        Verbose,
        Debug,
        Trace
    }
    
    public class LogLevelOption : GenericOptionBase
    {
        public const string Key = "-loglevel";

        /// <summary>
        ///     Set logging level and flags used by the library.
        /// 
        ///     The optional flags prefix can consist of the following values:
        /// 
        ///     ‘repeat’
        ///         Indicates that repeated log output should not be compressed to the first line and the 
        ///         "Last message repeated n times" line will be omitted. 
        /// 
        ///     ‘level’
        ///         Indicates that log output should add a [level] prefix to each message line. 
        ///         This can be used as an alternative to log coloring, e.g. when dumping the log to file. 
        /// 
        ///     Flags can also be used alone by adding a ’+’/’-’ prefix to set/reset a single flag without affecting 
        ///     other flags or changing loglevel. When setting both flags and loglevel, a ’+’ separator is expected 
        ///     between the last flags value and before loglevel.
        /// 
        ///     loglevel is a string or a number containing one of the following values:
        ///     ‘quiet, -8’
        ///         Show nothing at all; be silent. 
        ///     ‘panic, 0’
        ///         Only show fatal errors which could lead the process to crash, such as an assertion failure. 
        ///         This is not currently used for anything. 
        ///     ‘fatal, 8’
        ///         Only show fatal errors. These are errors after which the process absolutely cannot continue. 
        ///     ‘error, 16’
        ///         Show all errors, including ones which can be recovered from. 
        ///     ‘warning, 24’
        ///         Show all warnings and errors. Any message related to possibly incorrect or unexpected events will be shown.  
        ///     ‘info, 32’
        ///         Show informative messages during processing. This is in addition to warnings and errors. 
        ///         This is the default value. 
        ///     ‘verbose, 40’
        ///         Same as info, except more verbose.  
        ///     ‘debug, 48’
        ///         Show everything, including debugging information. 
        ///     ‘trace, 56’
        /// </summary>
        public FFMpegLogLevel LogingLevel { get; set; }

        public bool Repeat { get; set; } = false;

        public bool LevelPrefix { get; set; } = false;

        public LogLevelOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            string cmd = String.Join(String.Empty, new String[] 
            { 
                (Repeat == true) ? "repeat+" : "",
                (LevelPrefix == true) ? "level+" : "",
                GetLogLevelNumber().ToString()
            });

            return new CommandParam(OptionKey, cmd);
        }

        public int GetLogLevelNumber()
        {
            return (((int)LogingLevel - 1) * 8);
        }
    }
}