using System.Configuration;
using System.IO;
using Aspose.Storage.Cloud.Sdk.Api;
using Aspose.Storage.Cloud.Sdk.Model.Requests;
using NUnit.Framework;
using RestSharp.Extensions;
using Configuration = Aspose.Storage.Cloud.Sdk.Configuration;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    internal class PdfTestUtils
    {
        internal string CreateFilePath(string templateName)
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
    }
}
