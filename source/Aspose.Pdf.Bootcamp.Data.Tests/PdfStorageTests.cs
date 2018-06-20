using System.IO;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    [TestFixture]
    public class PdfStorageTests
    {
        [TestFixture]
        public class UploadTemplate
        {
            [Test]
            public void WhenTemplateExistLocally_ShouldUploadPdf()
            {
                // arrange
                var pdfTestUtils = new PdfTestUtils();
                var templateName = "BootCampForm-v2.pdf";
                var filePath = pdfTestUtils.CreateTemplatePath(templateName);
                var pdfStorage = new PdfStorage();
                // act
                var actual = pdfStorage.UploadTemplate(filePath, templateName);
                // assert
                var expected = new UploadResult { Successful = true };
                actual.Should().BeEquivalentTo(expected);
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void WhenWhiteSpaceOrNullStorageFilename_ShouldReturnErrors(string fileName)
            {
                // arrange
                var pdfTestUtils = new PdfTestUtils();
                var templateName = "BootCampForm-v2.pdf";
                var filePath = pdfTestUtils.CreateTemplatePath(templateName);
                var pdfStorage = new PdfStorage();
                // act
                var actual = pdfStorage.UploadTemplate(filePath, fileName);
                // assert
                var expected = new UploadResult { Successful = false, Errors = { "Storage Filename cannot be whitespace or null" } };
                actual.Should().BeEquivalentTo(expected);
            }

            [Test]
            public void WhenTemplateNotPresent_ShouldReturnErrors()
            {
                // arrange
                var pdfTestUtils = new PdfTestUtils();
                var templateName = "BootCampForm-DOESNOTEXIST-v2.pdf";
                var filePath = pdfTestUtils.CreateTemplatePath(templateName);
                var pdfStorage = new PdfStorage();
                // act
                var actual = pdfStorage.UploadTemplate(filePath, templateName);
                // assert
                var expected = new UploadResult { Successful = false, Errors = { "Cannot locate pdf template [BootCampForm-DOESNOTEXIST-v2.pdf]" } };
                actual.Should().BeEquivalentTo(expected);
            }
        }

        [TestFixture]
        public class Download
        {
            [Test]
            public void WhenFileExistInCloud_ShouldBeAbleToDownloadFile()
            {
                // arrange
                var pdfTestUtils = new PdfTestUtils();
                var templateName = "BootCampForm-v2.pdf";
                var filePath = pdfTestUtils.CreateTemplatePath(templateName);
                var pdfStorage = new PdfStorage();
                pdfStorage.UploadTemplate(filePath, templateName);
                // act
                var actual = pdfStorage.Download(templateName);
                // assert
                var expectedLength = FetchBootCampFormExpectedLength(filePath);
                actual.Length.Should().Be(expectedLength);
            }

            private int FetchBootCampFormExpectedLength(string filePath)
            {
                var expectedBytes = File.ReadAllBytes(filePath);
                return expectedBytes.Length;
            }
        }

        [TestFixture]
        public class CloneTemplate
        {
            [Test]
            public void WhenTemplateExistInCloud_ShouldReturnClonedFileName()
            {
                // arrange
                var pdfTestUtils = new PdfTestUtils();
                var templateName = "BootCampForm-v2.pdf";
                var filePath = pdfTestUtils.CreateTemplatePath(templateName);
                var pdfStorage = new PdfStorage();
                pdfStorage.UploadTemplate(filePath, templateName);
                // act
                var actual = pdfStorage.CloneTemplate(templateName);
                // assert
                actual.Should().Contain(templateName);
            }
        }
    }
}
