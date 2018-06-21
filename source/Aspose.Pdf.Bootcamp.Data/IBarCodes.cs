namespace Aspose.Pdf.Bootcamp.Data
{
    public interface IBarCodes
    {
        IBarCodeResolution With_Text(string text);
        IBarCodeType With_Image(byte[] imageBytes);
    }

    public interface IBarCodeResolution
    {
        IBarCodeDimension With_Custom_Resolution(float x, float y);
        IBarCodeDimension With_Default_Resolution();
    }

    public interface IBarCodeDimension
    {
        IBarCodeType With_Custom_Dimension(float x, float y);
        IBarCodeType With_Default_Dimension();
    }

    public interface IBarCodeType
    {
        IBarCodeFormat Of_Type_QR_Code(bool checksumEnabled);
    }

    public interface IBarCodeFormat
    {
        IBarCodeOperations As_Png();
    }

    public interface IBarCodeOperations
    {
        byte[] Create();
        void Save_To(string localFile);
        string Extract_Text();
    }
}