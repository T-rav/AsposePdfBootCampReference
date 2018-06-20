using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    [TestFixture]
    public class PdfServiceTests
    {
        [TestFixture]
        public class WithTemplate
        {
            [TestFixture]
            public class WithFormData
            {
                [TestFixture]
                public class Populate
                {
                    [Test]
                    public void WhenValidTemplateNameAndFieldsExist_ShouldReturnPopulatedPdfAsBytes()
                    {
                        // arrange
                        var pdfUtil = new PdfTestUtils();
                        var templateName = "BootCampForm-v2.pdf";
                        var localPath = pdfUtil.CreateTemplatePath(templateName);
                        var formFields = new List<SimplePdfFormField>{
                            new SimplePdfFormField{Name = "FirstName", Value = "Travis"},
                            new SimplePdfFormField{Name = "Surname", Value = "Frisinger"},
                            new SimplePdfFormField{Name = "DateOfBirth", Value = "1981-04-29"},
                        };
                        var pdfService = new PdfService();
                        // act
                        var actual = pdfService
                            .WithTemplate(templateName, localPath)
                            .WithFormData(formFields)
                            .Populate();
                        // assert
                        var expectedLength = pdfUtil.FetchExpectedFileLength("readonly.pdf");
                        actual.Length.Should().Be(expectedLength);
                    }

                    [Test]
                    public void WhenInvalidTemplateName_ShouldReturnZeroBytes()
                    {
                        // arrange
                        var pdfUtil = new PdfTestUtils();
                        var templateName = "BootCampForm-DOESNOTEXIST-v2.pdf";
                        var localPath = pdfUtil.CreateTemplatePath(templateName);
                        var formFields = new List<SimplePdfFormField>{
                            new SimplePdfFormField{Name = "FirstName", Value = "Travis"},
                            new SimplePdfFormField{Name = "Surname", Value = "Frisinger"},
                            new SimplePdfFormField{Name = "DateOfBirth", Value = "1981-04-29"},
                        };
                        var pdfService = new PdfService();
                        // act
                        var actual = pdfService
                            .WithTemplate(templateName, localPath)
                            .WithFormData(formFields)
                            .Populate();
                        // assert
                        var expectedLength = 0;
                        actual.Length.Should().Be(expectedLength);
                    }
                }

                [TestFixture]
                public class WithPassword
                {
                    [TestFixture]
                    public class Populate
                    {
                        [Test]
                        public void WhenProtectingNonEmptyPassword_ShouldReturnPasswordProtectedPdf()
                        {
                            // arrange
                            var pdfUtil = new PdfTestUtils();
                            var templateName = "BootCampForm-v2.pdf";
                            var localPath = pdfUtil.CreateTemplatePath(templateName);
                            var formFields = new List<SimplePdfFormField>{
                                new SimplePdfFormField{Name = "FirstName", Value = "Travis"},
                                new SimplePdfFormField{Name = "Surname", Value = "Frisinger"},
                                new SimplePdfFormField{Name = "DateOfBirth", Value = "1981-04-29"},
                            };
                            var pdfService = new PdfService();
                            // act
                            var actual = pdfService
                                .WithTemplate(templateName, localPath)
                                .WithFormData(formFields)
                                .WithPassword("1234")
                                .Populate();
                            // assert
                            var unsignedBytesLength = pdfUtil.FetchExpectedFileLength("readonly.pdf");
                            actual.Length.Should().NotBe(unsignedBytesLength);
                        }
                    }
                }
            }
        }
    }
}
