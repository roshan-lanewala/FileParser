using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing
{
    /// <summary>
    /// A File System wrapper
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }
    }
}
