using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace StableCube.Media.FFMpeg.DataModel
{
    public class FFMpegProgressLog
    {
        /// <summary>
        /// This is the same default buffer size as
        /// <see cref="StreamReader"/> and <see cref="FileStream"/>.
        /// </summary>
        private const int DefaultBufferSize = 4096;

        /// <summary>
        /// Indicates that
        /// 1. The file is to be used for asynchronous reading.
        /// 2. The file is to be accessed sequentially from beginning to end.
        /// </summary>
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public async static Task<List<FFMpegProgressLogEntry>> ParseLogFileAsync(string path)
        {
            List<FFMpegProgressLogEntry> logEntries = new List<FFMpegProgressLogEntry>();
            string[] lines = await ReadAllLinesAsync(path);
            List<string> entryLines = new List<string>();
            
            if(lines.Count() == 0)
                return logEntries;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                entryLines.Add(line);

                //A line with starting with "progress" ends an entry IE progress=continue or progress=end
                if(line.StartsWith("progress"))
                {
                    logEntries.Add(new FFMpegProgressLogEntry(entryLines));
                    entryLines.Clear();
                }
            }

            return logEntries;
        }

        public static Task<string[]> ReadAllLinesAsync(string path)
        {
            return ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public static async Task<string[]> ReadAllLinesAsync(
            string path, 
            Encoding encoding
        )
        {
            var lines = new List<string>();

            // Open the FileStream with the same FileMode, FileAccess
            // and FileShare as a call to File.OpenText would've done.
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}