
using System.Collections.Generic;
namespace MobileDen.CodeChallenge.FileParsing
{
    public interface IFileSystem
    {
        bool Exists(string fileName);

        IEnumerable<string> ReadLines(string path);
    }
}
