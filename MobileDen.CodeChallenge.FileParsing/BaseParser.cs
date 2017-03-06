using MobileDen.CodeChallenge.FileParsing.Model;
using System.Collections.Generic;

namespace MobileDen.CodeChallenge.FileParsing
{
    public abstract class BaseParser
    {
        public BaseParser(string fileName)
        {
            FileName = fileName;
        }

        public BaseParser()
        {
            Books = new List<IBook>();
        }

        public abstract void Read();

        public string FileName { get; set; }
        public IEnumerable<IBook> Books { get; set; }

        // Assuming tab(\t) = 4 spaces
        protected string TabReplacement = "    ";
    }
}
