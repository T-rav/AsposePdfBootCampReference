using System.Configuration;
using System.IO;
using Aspose.Pdf.Bootcamp.Data;
using Aspose.Pdf.Bootcamp.Domain;
using Aspose.Storage.Cloud.Sdk.Api;
using Aspose.Storage.Cloud.Sdk.Model.Requests;
using FluentAssertions;
using NUnit.Framework;
using RestSharp.Extensions;
using Configuration = Aspose.Storage.Cloud.Sdk.Configuration;

namespace Aspose.Pdf.Bootcamp.Tests
{
    [TestFixture]
    public class PdfStorageTests
    {
        [Test]
        public void UploadTemplate_WhenValidData_ShouldUploadPdf()
        {
            // arrange
            var templateName = "BootCampForm-v2.pdf";
            var filePath = CreateFilePath(templateName);
            var pdfStorage = new PdfStorage();
            // act
            var actual = pdfStorage.UploadTemplate(filePath, templateName);
            // assert
            var expected = new UploadResult {Successful = true};
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void UploadTemplate_WhenWhiteSpaceOrNullStorageFilename_ShouldReturnErrors(string fileName)
        {
            // arrange
            var templateName = "BootCampForm-v2.pdf";
            var filePath = CreateFilePath(templateName);
            var pdfStorage = new PdfStorage();
            // act
            var actual = pdfStorage.UploadTemplate(filePath, fileName);
            // assert
            var expected = new UploadResult { Successful = false, Errors = { "Storage Filename cannot be whitespace or null"}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UploadTemplate_WhenTemplateNotPresent_ShouldReturnErrors()
        {
            // arrange
            var templateName = "BootCampForm-DOESNOTEXIST-v2.pdf";
            var filePath = CreateFilePath(templateName);
            var pdfStorage = new PdfStorage();
            // act
            var actual = pdfStorage.UploadTemplate(filePath, templateName);
            // assert
            var expected = new UploadResult { Successful = false,Errors = { "Cannot locate pdf template [BootCampForm-DOESNOTEXIST-v2.pdf]" } };
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Download_WhenFileExist_ShouldBeAbleToDownloadFile()
        {
            // arrange
            var templateName = "BootCampForm-v2.pdf";
            var filePath = CreateFilePath(templateName);
            var pdfStorage = new PdfStorage();
            pdfStorage.UploadTemplate(filePath, templateName);
            // act
            var actual = pdfStorage.Download(templateName);
            // assert
            var expectedBytes = 95685;
            actual.Length.Should().Be(expectedBytes);
        }

        [Test]
        public void CloneTemplate_WhenTemplateExist_ShouldBeAbleToDownloadFile()
        {
            // arrange
            var templateName = "BootCampForm-v2.pdf";
            var filePath = CreateFilePath(templateName);
            var pdfStorage = new PdfStorage();
            pdfStorage.UploadTemplate(filePath, templateName);
            // act
            var fileName = pdfStorage.CloneTemplate(templateName);
            var actual = FetchFileFromCloud(fileName);
            // assert
            var expectedBytes = 95685;
            actual.Length.Should().Be(expectedBytes);
        }

        private string CreateFilePath(string templateName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Forms", templateName);
            return filePath;
        }

        private byte[] FetchFileFromCloud(string fileName)
        {
            var storageConfiguration = CreateStorageConfiguration();
            var storageApi = CreateStorageApi(storageConfiguration);

            var downloadRequest = new GetDownloadRequest(fileName);
            using (var stream = storageApi.GetDownload(downloadRequest))
            {
                return stream.ReadAsBytes();
            }
        }

        private StorageApi CreateStorageApi(Configuration storageConfiguration)
        {
            var storageApi = new StorageApi(storageConfiguration);
            return storageApi;
        }

        private Configuration CreateStorageConfiguration()
        {
            var storageConfiguration = new Configuration
            {
                AppKey = ConfigurationManager.AppSettings["ApiKey"],
                AppSid = ConfigurationManager.AppSettings["AppSid"]
            };

            return storageConfiguration;
        }
    }
}
