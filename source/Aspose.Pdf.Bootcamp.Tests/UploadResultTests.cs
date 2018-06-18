using System.Collections.Generic;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Tests
{
    [TestFixture]
    public class UploadResultTests
    {
        [Test]
        public void Ctor_ShouldInitalizeErrorsCollection()
        {
            // arrange
            // act
            var sut =  new UploadResult();
            // assert
            Assert.IsNotNull(sut.Errors);
        }

        [Test]
        public void AddError_WhenNotNullOrWhitespace_ShouldAddToCollection()
        {
            // arrange
            var sut = new UploadResult();
            // act
            sut.AddError("test error");
            // assert
            var expected = new List<string> {"test error"};
            sut.Errors.Should().BeEquivalentTo(expected);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddError_WhenNullOrWhitespace_ShouldNotAddToCollection(string error)
        {
            // arrange
            var sut = new UploadResult();
            // act
            sut.AddError(error);
            // assert
            sut.Errors.Should().BeEmpty();
        }
    }
}