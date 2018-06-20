using System.Configuration;
using System.IO;
using Aspose.Storage.Cloud.Sdk.Api;
using Aspose.Storage.Cloud.Sdk.Model.Requests;
using iTextSharp.text.pdf;
using NUnit.Framework;
using RestSharp.Extensions;
using Configuration = Aspose.Storage.Cloud.Sdk.Configuration;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    internal class PdfTestUtils
    {
        internal string CreateTemplatePath(string templateName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Forms", templateName);
            return filePath;
        }

        internal byte[] FetchFileFromCloud(string fileName)
        {
            var storageConfiguration = CreateStorageConfiguration();
            var storageApi = CreateStorageApi(storageConfiguration);

            var downloadRequest = new GetDownloadRequest(fileName);
            using (var stream = storageApi.GetDownload(downloadRequest))
            {
                return stream.ReadAsBytes();
            }
        }

        internal StorageApi CreateStorageApi(Configuration storageConfiguration)
        {
            var storageApi = new StorageApi(storageConfiguration);
            return storageApi;
        }

        internal Configuration CreateStorageConfiguration()
        {
            var storageConfiguration = new Configuration
            {
                AppKey = ConfigurationManager.AppSettings["ApiKey"],
                AppSid = ConfigurationManager.AppSettings["AppSid"]
            };

            return storageConfiguration;
        }

        internal void UploadTemplateToCloud(string fileName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Forms", fileName);
            var pdfStorage = new PdfStorage();
            pdfStorage.UploadTemplate(filePath, fileName);
        }

        internal byte[] FetchFileFromLocal(string fileName)
        {
            var localPath = CreateTemplatePath(fileName);
            return File.ReadAllBytes(localPath);
        }

        internal int FetchExpectedFileLength(string fileName)
        {
            var filePath = CreateExpectedPath(fileName);
            var bytes = File.ReadAllBytes(filePath);
            return bytes.Length;
        }

        private string CreateExpectedPath(string fileName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Expected", fileName);
            return filePath;
        }

        public byte[] DecryptBytes(byte[] encryptedBytes, string password)
        {
            var ownerPassword = new System.Text.ASCIIEncoding().GetBytes(password);
            var reader = new PdfReader(encryptedBytes, ownerPassword);

            using (var memoryStream = new MemoryStream())
            {
                using (var stamper = new PdfStamper(reader, memoryStream))
                {
                    stamper.Close();
                    reader.Close();
                     return memoryStream.ToArray();
                }
            }
        }
    }
}
