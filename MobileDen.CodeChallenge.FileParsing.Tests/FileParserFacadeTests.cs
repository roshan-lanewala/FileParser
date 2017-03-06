using System;
using NUnit.Framework;
using FluentAssertions;
using System.IO;
using System.Reflection;
using MobileDen.CodeChallenge.FileParsing.Model;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing.Tests
{
    [TestFixture]
    public class FileParserFacadeTests
    {
        private IEnumerable<IBook> mockFileAData = new List<Book>() 
        { 
            new Book("Charlie Bone Series", "1234567890", "Jenny Nimmo"),
            new Book("Chimera", "4343445454", "John Barth")
        };

        private IEnumerable<IBook> mockFileBData = new List<Book>() 
        { 
            new Book("A Fine and Private Place", "4334445345564", "Peter S. Beagle"),
            new Book("The Anubis Gates", "7567564545454", "Tim Powers")
        };

        private IEnumerable<string> mockFileAStrings = new List<string>()
        {
            "A",
            "Charlie Bone Series 1234567890           Jenny Nimmo",
            "Chimera 			4343445454			 John Barth"
        };

        private IEnumerable<string> mockFileBStrings = new List<string>()
        {
            "B",
            "A Fine and Private Place 	  4334445345564		   Peter S. Beagle",
            "The Anubis Gates 			  7567564545454		   Tim Powers"
        };

        private IEnumerable<string> mockFileCStrings = new List<string>()
        {
            "C",
            "The Alchemist 	  9780060834838		   Paulo_Coelho"
        };

        private FileParserFacade _facade;
        private IFileSystem _fileSystem;

        [SetUp]
        public void Setup()
        { 
            _fileSystem = Substitute.For<IFileSystem>();
            
            _facade = new FileParserFacade(_fileSystem);
        }

        [Test]
        public void CanInstantiateFileParserFacade()
        {
            Assert.DoesNotThrow(() => new FileParserFacade(Substitute.For<IFileSystem>()));
        }

        [TestCase(ParserType.FileTypeA)]
        [TestCase(ParserType.FileTypeB)]
        [Test]
        public void CanGetEmptyBooksWithoutParsingAnyFile(ParserType type)
        {
            _facade.GetBooks(type).Should().BeEmpty();
        }

        [TestCase(@"TestData\A.TXT", ParserType.FileTypeA)]
        [TestCase(@"TestData\B.TXT", ParserType.FileTypeB)]
        [Test]
        public void GivenATestDataFileCanReadFormatType(string filePath, ParserType expectedResult)
        {
            var mockReturnResult = Enumerable.Empty<string>();
            switch (expectedResult)
            {
                case ParserType.FileTypeA:
                    mockReturnResult = new string[] { "A" };
                    break;
                case ParserType.FileTypeB:
                    mockReturnResult = new string[] { "B" };
                    break;
                default:
                    mockReturnResult = Enumerable.Empty<string>();
                    break;
            }
            _fileSystem.ReadLines(FinalFilePath(filePath)).Returns(mockReturnResult);
            _facade.GetFileTypeFormat(FinalFilePath(filePath)).Should().Be(expectedResult);
        }

        [Test]
        public void GivenAnUnknownParserTypeTheParserTypeWillDefaultToA()
        {
            _fileSystem.ReadLines(FinalFilePath("UNKNONW")).Returns(Enumerable.Empty<string>());
            _facade.GetFileTypeFormat(FinalFilePath("UNKNONW")).Should().Be(ParserType.FileTypeA);
        }

        [Test]
        public void WillThrowFileNotFoundExceptionWhenParsingFiel()
        {
            _fileSystem.Exists("InvalidFile.txt").Returns(false);
            Assert.Throws<FileNotFoundException>(() => _facade.ParseFile("InvalidFile.txt"));
        }

        [Test]
        public void CanParseFileFormatWithTypeA()
        {
            _fileSystem.Exists(FinalFilePath(@"TestData\A.TXT")).Returns(true);
            _fileSystem.ReadLines(FinalFilePath(@"TestData\A.TXT")).Returns(mockFileAStrings);

            _facade.ParseFile(FinalFilePath(@"TestData\A.TXT"));
            var actualBooks = _facade.GetBooks(ParserType.FileTypeA);

            actualBooks.Should().NotBeNullOrEmpty()
                .ShouldBeEquivalentTo(mockFileAData, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public void CanParseFileFormatWithTypeB()
        {
            _fileSystem.Exists(FinalFilePath(@"TestData\B.TXT")).Returns(true);
            _fileSystem.ReadLines(FinalFilePath(@"TestData\B.TXT")).Returns(mockFileBStrings);

            _facade.ParseFile(FinalFilePath(@"TestData\B.TXT"));
            var actualBooks = _facade.GetBooks(ParserType.FileTypeB);

            actualBooks.Should().NotBeNullOrEmpty()
                .ShouldBeEquivalentTo(mockFileBData, options =>
                options.ExcludingMissingMembers());
        }

        private Func<string, string> FinalFilePath = (filepath) =>
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filepath);
    }
}
