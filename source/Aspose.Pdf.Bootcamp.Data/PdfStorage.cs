using System;
using System.Configuration;
using System.IO;
using Aspose.Pdf.Bootcamp.Domain;
using Aspose.Storage.Cloud.Sdk.Api;
using Aspose.Storage.Cloud.Sdk.Model;
using Aspose.Storage.Cloud.Sdk.Model.Requests;
using RestSharp.Extensions;
using Configuration = Aspose.Storage.Cloud.Sdk.Configuration;

namespace Aspose.Pdf.Bootcamp.Data
{
    public class PdfStorage : IPdfStorage
    {
        private readonly string _apiKey;
        private readonly string _appSid;

        public PdfStorage()
        {
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _appSid = ConfigurationManager.AppSettings["AppSid"];
        }

        public UploadResult UploadTemplate(string localFilePath, string storageFileName)
        {
            if (string.IsNullOrWhiteSpace(storageFileName))
            {
                var errorResult = new UploadResult {Successful = false};
                errorResult.AddError("Storage Filename cannot be whitespace or null");
                return errorResult;
            }

            var storageApi = CreateStorageApi();

            var fileBytes = FetchFileBytes(localFilePath);
            using (var stream = new MemoryStream(fileBytes))
            {
                var response = UploadPdfTemplate(localFilePath, stream, storageApi);
                return IsOkResponse(response);
            }
        }

        public string CloneTemplate(string fileName)
        {
            var storageApi = CreateStorageApi();
            var newFileName = $"{Guid.NewGuid().ToString()}-{fileName}";
            var copyRequest = new PutCopyRequest(fileName, newFileName);
            storageApi.PutCopy(copyRequest);
            return newFileName;
        }

        public byte[] Download(string documentName)
        {
            var storageConfiguration = CreateStorageConfiguration();
            var storageApi = CreateStorageApi(storageConfiguration);

            var downloadRequest = new GetDownloadRequest(documentName);
            using (var stream = storageApi.GetDownload(downloadRequest))
            {
                return stream.ReadAsBytes();
            }
        }

        private StorageApi CreateStorageApi()
        {
            var storageConfiguration = CreateStorageConfiguration();
            var storageApi = CreateStorageApi(storageConfiguration);
            return storageApi;
        }
        
        private UploadResponse UploadPdfTemplate(string localFilePath, MemoryStream stream, StorageApi storageApi)
        {
            var createFileRequest = new PutCreateRequest(localFilePath, stream);
            var response = storageApi.PutCreate(createFileRequest);
            return response;
        }

        private byte[] FetchFileBytes(string localFilePath)
        {
            var fileBytes = File.ReadAllBytes(localFilePath);
            return fileBytes;
        }

        private UploadResult IsOkResponse(UploadResponse response)
        {
            var result = new UploadResult
            {
                Successful = response?.Status == "OK"
            };

            return result;
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
                AppKey = _apiKey,
                AppSid = _appSid
            };
            return storageConfiguration;
        } 
    }
}
