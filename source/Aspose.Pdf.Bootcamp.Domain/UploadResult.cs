using System.Collections.Generic;

namespace Aspose.Pdf.Bootcamp.Domain
{
    public class UploadResult
    {
        public bool Successful { get; set; }
        public List<string> Errors { get; private set; }

        public UploadResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string message)
        {
            if(string.IsNullOrWhiteSpace(message)) return;

            Errors.Add(message);
        }
    }
}