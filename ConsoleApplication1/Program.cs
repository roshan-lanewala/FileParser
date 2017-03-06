using MobileDen.CodeChallenge.FileParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // FileSystem can be injected using Dependency Injection eg: Unity
            IFileSystem fileSystem = new FileSystem();
            var facade = new FileParserFacade(fileSystem);

            facade.ParseFile(@".\TestData\A.TXT");
            facade.ParseFile(@".\TestData\B.TXT");

            var booksForFileTypeA = facade.GetBooks(ParserType.FileTypeA);
            var booksForFileTypeB = facade.GetBooks(ParserType.FileTypeB);

            Console.ReadLine();
        }
    }
}
