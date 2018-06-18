using System.Collections.Generic;

namespace Aspose.Pdf.Bootcamp.Domain
{
    public interface IPdfService
    {
        IPdfWithFormData WithTemplate(string templateName, string localPath);
    }

    public interface IPdfWithFormData
    {
        IPdfServicePopulate WithFormData(List<SimplePdfFormField> formFields);

    }

    public interface IPdfServicePopulate
    {
        byte[] Populate();
    }
}