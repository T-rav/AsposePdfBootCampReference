﻿using System.Collections.Generic;

namespace Aspose.Pdf.Bootcamp.Domain
{
    public interface IPdfRendering
    {
        IPdfServiceWithFormData WithTemplate(string templateName, string localPath);
    }

    public interface IPdfServiceWithFormData
    {
        IPdfServicePopulate WithFormData(List<SimplePdfFormField> formFields);

    }

    public interface IPdfServicePopulate
    {
        IPdfServicePopulate WithPassword(string password);
        byte[] Populate();
    }
}