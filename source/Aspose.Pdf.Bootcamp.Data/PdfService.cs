using System.Collections.Generic;
using Aspose.Pdf.Bootcamp.Domain;

namespace Aspose.Pdf.Bootcamp.Data
{
    public class PdfService : IPdfService
    {
        private static IPdfStorage _pdfStorage;
        private static IPdfManipuation _pdfManipulation;

        public PdfService() :this(new PdfStorage(), new PdfManipuation()) { }

        private PdfService(IPdfStorage pdfStorage, IPdfManipuation pdfManipuation)
        {
            _pdfStorage = pdfStorage;
            _pdfManipulation = pdfManipuation;
        }

        public IPdfServiceWithFormData WithTemplate(string templateName, string localPath)
        {
            var builder = new PdfServiceBuilder();
            builder.WithTemplate(templateName, localPath);

            return builder;
        }

        private class PdfServiceBuilder : IPdfService, IPdfServiceWithFormData, IPdfServicePopulate
        {
            private string _templateName;
            private List<SimplePdfFormField> _formFields;
            private string _localPath;
            private string _password;

            public IPdfServiceWithFormData WithTemplate(string templateName, string localPath)
            {
                _templateName = templateName;
                _localPath = localPath;
                return this;
            }

            public IPdfServicePopulate WithFormData(List<SimplePdfFormField> formFields)
            {
                _formFields = formFields;
                return this;
            }

            public IPdfServicePopulate WithPassword(string password)
            {
                _password = password;
                return this;
            }

            public byte[] Populate()
            {
                var result = _pdfStorage.UploadTemplate(_localPath, _templateName);
                if (result.Successful)
                {
                    var cloudStorageName = _pdfManipulation.PopulateTemplate(_templateName, _formFields);
                    var readonlyBytes = _pdfManipulation.MarkFieldsAsReadOnly(cloudStorageName, _formFields);

                    if (string.IsNullOrEmpty(_password))
                    {
                        return readonlyBytes;
                    }

                    var signedBytes = _pdfManipulation.PasswordProtect(readonlyBytes, _password);
                    return signedBytes;
                }

                return new byte[0];
            }
        }
    }
}
