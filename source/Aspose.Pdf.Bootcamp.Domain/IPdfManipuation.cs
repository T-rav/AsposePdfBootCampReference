﻿using System.Collections.Generic;

namespace Aspose.Pdf.Bootcamp.Domain
{
    public interface IPdfManipuation
    {
        byte[] MarkFieldsAsReadOnly(string cloudStorageName, List<string> readonlyFields);
        string PopulateTemplate(string templateName, List<SimplePdfFormField> fields);
    }
}