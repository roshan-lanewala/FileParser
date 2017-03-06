using MobileDen.CodeChallenge.FileParsing.Model;
using System.IO;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing
{
    public class FileTypeAParser : BaseParser
    {
        IFileSystem _fileSystem;

        public FileTypeAParser(string fileName)
            : base(fileName)
        {
        }

        public FileTypeAParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public FileTypeAParser() : base()
        {   
        }

        public override void Read()
        {
            // Skip the first line, as it specifies the format of the File
            Books = _fileSystem.ReadLines(FileName).Skip(1)
                .FilterEmptyLines()
                .ReplaceTabSpaces(TabReplacement)
                .Select(s =>
                {
                    return new Book(s.Substring(ColumnStartIndex[0], 19).Trim(),
                    s.Substring(ColumnStartIndex[1], 21).Trim(),
                    s.Substring(ColumnStartIndex[2]).Trim());
                });
        }

        private int[] ColumnStartIndex = new int[] { 0, 20, 41 }; 

        public override string ToString()
        {
            return ParserType.FileTypeA.ToString();
        }
    }
}
