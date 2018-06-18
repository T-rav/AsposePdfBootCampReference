using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Bootcamp.Data;
using Aspose.Pdf.Bootcamp.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Aspose.Pdf.Bootcamp.Tests
{
    [TestFixture]
    public class PdfManipuationTests
    {
        [Test]
        public void PopulateTemplate_WhenValidFormInformation_ShouldReturnNewFileName()
        {
            // arrange
            var fileName = "BootCampForm-v2.pdf";
            var pdfManipuation = new PdfManipuation();
            StageTemplate(fileName);
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

        [Test]
        public void MarkFieldsAsReadOnly_WhenValidFormInformation_ShouldReturnNewFileName()
        {
            // arrange
            var fileName = "BootCampForm-v2.pdf";
            var pdfManipuation = new PdfManipuation();
            StageTemplate(fileName);
            var fields = new List<SimplePdfFormField>
            {
                new SimplePdfFormField {Name = "FirstName", Value = "Bob"},
                new SimplePdfFormField {Name = "Surname", Value = "Smith"},
                new SimplePdfFormField {Name = "DateOfBirth", Value = "1981-04-29"}
            };
            var cloudStorageName = pdfManipuation.PopulateTemplate(fileName, fields);
            var readonlyFields = new List<string> {"FirstName", "Surname", "DateOfBirth"};
            // act
            var actual = pdfManipuation.MarkFieldsAsReadOnly(cloudStorageName, readonlyFields);
            // assert
            actual.Length.Should().Be(110038);
        }

        private void StageTemplate(string fileName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory,"Forms",fileName);
            var pdfStorage = new PdfStorage();
            pdfStorage.UploadTemplate(filePath, fileName);
        }
    }
}