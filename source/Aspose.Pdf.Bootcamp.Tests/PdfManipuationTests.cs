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
                var cloudStorageName = pdfManipuation.PopulateTemplate(fileName, fields);
                // act
                var actual = pdfManipuation.MarkFieldsAsReadOnly(cloudStorageName, fields);
                // assert
                var expected = 108590;
                actual.Length.Should().Be(expected);
            }
        } 
    }
}