using NSubstitute;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace MobileDen.CodeChallenge.FileParsing.Tests
{
    [TestFixture]
    public class FileTypeParserTests
    {
        [TestCase("SomeValue")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        [Test]
        public void CanCreateInstanceOfFileTypeBParserWithConstructorArgument(string args)
        {
            Assert.DoesNotThrow(() => new FileTypeBParser(args));
        }

        [TestCase("SomeValue")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        [Test]
        public void CanCreateInstanceOfFileTypeAParserWithConstructorArgument(string args)
        {
            Assert.DoesNotThrow(() => new FileTypeAParser(args));
        }

        [Test]
        public void CanCreateInstanceOfFileTypeBParserWithNOConstructorArgument()
        {
            Assert.DoesNotThrow(() => new FileTypeBParser());
        }

        [Test]
        public void CanCreateInstanceOfFileTypeAParserWithNoConstructorArgument()
        {
            Assert.DoesNotThrow(() => new FileTypeAParser());
        }

        [Test]
        public void CanReadEmptyFileForTypeA()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.ReadLines(Arg.Any<string>()).ReturnsForAnyArgs(Enumerable.Empty<string>());
            var cut = new FileTypeAParser(fileSystem);
            cut.FileName = "abcd";
            cut.Read();
            cut.Books.Should().BeEmpty();
        }

        [Test]
        public void CanReadEmptyFileForTypeB()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.ReadLines(Arg.Any<string>()).ReturnsForAnyArgs(Enumerable.Empty<string>());
            var cut = new FileTypeBParser(fileSystem);
            cut.FileName = "abcd";
            cut.Read();
            cut.Books.Should().BeEmpty();
        }
    }
}
