using System;
using NUnit.Framework;
using FluentAssertions;
using NSubstitute;

namespace MobileDen.CodeChallenge.FileParsing.Tests
{
    [TestFixture]
    public class ParserFactoryTests
    {
        private ParserFactory _factory;

        [SetUp]
        public void Setup()
        { 
            var mockFileSystem = Substitute.For<IFileSystem>();
            _factory = new ParserFactory(mockFileSystem);
        }

        [Test]
        public void CanGetInstanceOfTheFactory()
        {
            Assert.DoesNotThrow(() => new ParserFactory(Substitute.For<IFileSystem>()));
        }

        [Test]
        public void CanGetParserObjectForFileTypeA()
        {
            var actual = _factory.GetObject(ParserType.FileTypeA.ToString());

            actual.Should().BeOfType<FileTypeAParser>();
        }

        [Test]
        public void CanGetParserObjectForFileTypeB()
        {
            var actual = _factory.GetObject(ParserType.FileTypeB.ToString());

            actual.Should().BeOfType<FileTypeBParser>();
        }
    }
}
