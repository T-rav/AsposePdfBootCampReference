using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Domain.Tests
{
    [TestFixture]
    public class UploadResultTests
    {
        [TestFixture]
        public class WhenCreatingUploadResult
        {
            [Test]
            public void ShouldInitalizeErrorsCollection()
            {
                // arrange
                // act
                var sut = new UploadResult();
                // assert
                Assert.IsNotNull(sut.Errors);
            }
        }

        [TestFixture]
        public class AddError
        {
            [Test]
            public void WhenNotNullOrWhitespace_ShouldAddToCollection()
            {
                // arrange
                var sut = new UploadResult();
                // act
                sut.AddError("test error");
                // assert
                var expected = new List<string> { "test error" };
                sut.Errors.Should().BeEquivalentTo(expected);
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void WhenNullOrWhitespace_ShouldNotAddToCollection(string error)
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
}