using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf.Bootcamp.Data;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Aspose.Pdf.Bootcamp.Tests
{
    [TestFixture]
    public class PdfServiceTests
    {
        [Test]
        public void Populate_WhenValidData_ShouldReturnPdfBytes()
        {
            // arrange
            var templateName = "BootCampForm-v2.pdf";
            var localPath = CreateFilePath(templateName);
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
            var expectedLength = 110052;
            actual.Length.Should().Be(expectedLength);
            //File.WriteAllBytes("C:\\Systems\\build.pdf", actual); // here for demo
        }

        [Test]
        public void Populate_WhenInvalidTemplate_ShouldReturnZeroBytes()
        {
            // arrange
            var templateName = "BootCampForm-DOESNOTEXIST-v2.pdf";
            var localPath = CreateFilePath(templateName);
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

        private string CreateFilePath(string templateName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Forms", templateName);
            return filePath;
        }
    }
}
