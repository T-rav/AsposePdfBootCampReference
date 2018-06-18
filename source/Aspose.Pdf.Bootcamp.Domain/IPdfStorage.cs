namespace Aspose.Pdf.Bootcamp.Domain
{
    public interface IPdfStorage
    {
        string CloneTemplate(string fileName);
        byte[] Download(string documentName);
        UploadResult UploadTemplate(string localFilePath, string storageFileName);
    }
}