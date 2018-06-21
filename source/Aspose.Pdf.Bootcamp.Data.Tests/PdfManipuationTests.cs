using System.Collections.Generic;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    [TestFixture]
    public class PdfManipuationTests
    {
        [TestFixture]
        public class PopulateTemplate
        {
            [Test]
            public void WhenValidFormInformation_ShouldReturnNewFileNameOfPopulatedPdf()
            {
                // arrange
                var pdfUtils = new PdfTestUtils();
                var fileName = "BootCampForm-v2.pdf";
                pdfUtils.UploadTemplateToCloud(fileName);
                var pdfManipuation = new PdfManipuation();
                var fields = new List<SimplePdfFormField>
                {
                    new SimplePdfFormField {Name = "FirstName", Value = "Bob"},
                    new SimplePdfFormField {Name = "Surname", Value = "Smith"},
                    new SimplePdfFormField {Name = "DateOfBirth", Value = "1981-04-29"}
                };
                // act
                var actual = pdfManipuation.PopulateTemplate(fileName, fields);
                // assert
                var expected = $"-{fileName}";
                actual.Should().Contain(expected);
            }
        }

        [TestFixture]
        public class MarkFieldsAsReadOnly
        {
            [Test]
            public void WhenValidFormInformation_ShouldReturnBytesOfModifedPdf()
            {
                // arrange
                var pdfUtils = new PdfTestUtils();
                var fileName = "BootCampForm-v2.pdf";
                pdfUtils.UploadTemplateToCloud(fileName);
                var pdfManipuation = new PdfManipuation();
                var fields = new List<SimplePdfFormField>
                {
                    new SimplePdfFormField{Name = "FirstName", Value = "Travis"},
                    new SimplePdfFormField{Name = "Surname", Value = "Frisinger"},
                    new SimplePdfFormField{Name = "DateOfBirth", Value = "1981-04-29"},
                };
                var cloudStorageName = pdfManipuation.PopulateTemplate(fileName, fields);
                // act
                var actual = pdfManipuation.MarkFieldsAsReadOnly(cloudStorageName, fields);
                // assert
                var expectedLength = pdfUtils.FetchExpectedFileLength("readonly.pdf");
                actual.Length.Should().Be(expectedLength);
            }
        }

        [TestFixture]
        public class SetPassword
        {
            [Test]
            public void WhenNonEmptyPassword_ShouldReturnBytesOfPasswordProtectedPdf()
            {
                // arrange
                var fileName = "BootCampForm-v2.pdf";
                var password = "1234";
                var pdfUtils = new PdfTestUtils();
                var pdfBytes = pdfUtils.FetchFileFromLocal(fileName);
                var pdfManipuation = new PdfManipuation();
                // act
                var actual = pdfManipuation.PasswordProtect(pdfBytes, password);
                // assert
                Assert.DoesNotThrow(()=> pdfUtils.DecryptBytes(actual, password));
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void WhenEmptyPassword_ShouldReturnZeroBytes(string password)
            {
                // arrange
                var fileName = "BootCampForm-v2.pdf";
                var pdfUtils = new PdfTestUtils();
                var pdfBytes = pdfUtils.FetchFileFromLocal(fileName);
                var pdfManipuation = new PdfManipuation();
                // act
                var actual = pdfManipuation.PasswordProtect(pdfBytes, password);
                // assert
                actual.Length.Should().Be(0);
            }
        }

        [TestFixture]
        public class BarCode
        {
            [Test]
            public void WhenNonEmptyPassword_ShouldReturnBytesOfPasswordProtectedPdf()
            {
                // arrange
                var fileName = "BootCampForm-v2.pdf";
                var password = "1234";
                var pdfUtils = new PdfTestUtils();
                var pdfBytes = pdfUtils.FetchFileFromLocal(fileName);
                var pdfManipuation = new PdfManipuation();
                // act
                var actual = pdfManipuation.PasswordProtect(pdfBytes, password);
                // assert
                Assert.DoesNotThrow(() => pdfUtils.DecryptBytes(actual, password));
            }
        }
    }
}