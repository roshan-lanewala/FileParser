using MobileDen.CodeChallenge.FileParsing.Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing
{
    public class FileParserFacade
    {
        ParserFactory factory;
        BaseParser parser;
        IFileSystem _fileSystem;

        public FileParserFacade(IFileSystem fileSystem)
        {
            factory = new ParserFactory(fileSystem);
            _fileSystem = fileSystem;
        }

        public void ParseFile(string fileName)
        {
            if (!_fileSystem.Exists(fileName))
                throw new FileNotFoundException("Unable to locate the file", fileName);

            ParserType type = GetFileTypeFormat(fileName);
            parser = factory.GetObject(type.ToString());

            parser.FileName = fileName;
            parser.Read();
        }

        public ParserType GetFileTypeFormat(string fileName)
        {
            var firstLine = _fileSystem.ReadLines(fileName).FirstOrDefault();

            if (string.IsNullOrEmpty(firstLine))
                firstLine = string.Empty;

            switch (firstLine.Trim().ToUpper())
            {
                case "A":
                    return ParserType.FileTypeA;
                case "B":
                    return ParserType.FileTypeB;
                default:
                    return ParserType.FileTypeA;    // Defaulting to FileTypeA
            }
        }

        public IEnumerable<IBook> GetBooks(ParserType type)
        {
            return factory.GetObject(type.ToString()).Books;
        }
    }
}
