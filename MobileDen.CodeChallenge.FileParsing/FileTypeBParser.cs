using MobileDen.CodeChallenge.FileParsing.Model;
using System;
using System.IO;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing
{
    public class FileTypeBParser : BaseParser
    {
        IFileSystem _fileSystem;

        public FileTypeBParser(string fileName)
            : base(fileName)
        {

        }

        public FileTypeBParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public FileTypeBParser()
            : base()
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
                    return new Book(s.Substring(ColumnStartIndex[0], 29).Trim(),
                    s.Substring(ColumnStartIndex[1], 21).Trim(),
                    s.Substring(ColumnStartIndex[2]).Trim());
                });
        }

        private int[] ColumnStartIndex = new int[] { 0, 30, 51 }; 

        public override string ToString()
        {
            return ParserType.FileTypeB.ToString();
        }
    }
}
