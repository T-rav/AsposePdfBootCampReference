using System.Configuration;
using System.IO;
using System.Linq;
using Com.Aspose.Barcode.Api;
using Com.Aspose.Barcode.Model;

namespace Aspose.Pdf.Bootcamp.Data
{
    public class BarCodes : IBarCodes
    {
        private readonly BarCodeBuilder _builder;

        public BarCodes()
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var appSid = ConfigurationManager.AppSettings["AppSid"];
            _builder = new BarCodeBuilder(apiKey, appSid);
        }

        public IBarCodeResolution With_Text(string text)
        {
            _builder.With_Text(text);
            return _builder;
        }

        public IBarCodeType With_Image(byte[] image)
        {
            _builder.With_Image(image);
            return _builder;
        }

        private class BarCodeBuilder : IBarCodes, IBarCodeResolution, IBarCodeDimension, IBarCodeType, IBarCodeFormat, IBarCodeOperations
        {
            private readonly string _apiKey;
            private readonly string _appSid;

            private string _text;
            private float _resolutionX;
            private float _resolutionY;
            private float _dimensionX;
            private float _dimensionY;
            private string _type;
            private string _format;
            private string _enableChecksum;
            private byte[] _imageBytes;

            public BarCodeBuilder(string apiKey, string appSid)
            {
                _apiKey = apiKey;
                _appSid = appSid;
            }

            public IBarCodeResolution With_Text(string text)
            {
                _text = text;
                return this;
            }

            public IBarCodeType With_Image(byte[] imageBytes)
            {
                _imageBytes = imageBytes;
                return this;
            }

            public IBarCodeDimension With_Default_Resolution()
            {
                _resolutionX = 192.0f;
                _resolutionY = 192.0f;
                return this;
            }


            public IBarCodeDimension With_Custom_Resolution(float x, float y)
            {
                _resolutionX = x;
                _resolutionY = y;
                return this;
            }

            public IBarCodeType With_Default_Dimension()
            {
                _dimensionX = 1.4f;
                _dimensionY = 4.0f;
                return this;
            }

            public IBarCodeType With_Custom_Dimension(float x, float y)
            {
                _dimensionX = x;
                _dimensionY = y;
                return this;
            }

            public IBarCodeFormat Of_Type_QR_Code(bool checksumEnabled)
            {
                _type = "QR";

                _enableChecksum = "NO";
                if (checksumEnabled)
                {
                    _enableChecksum = "YES";
                }

                return this;
            }

            public IBarCodeOperations As_Png()
            {
                _format = "PNG";
                return this;
            }

            public byte[] Create()
            {
                if (string.IsNullOrWhiteSpace(_text))
                {
                    return new byte[0];
                }

                var barcodeApi = new BarcodeApi(_apiKey, _appSid, "http://api.aspose.cloud/v1.1");

                var apiResponse = barcodeApi.GetBarcodeGenerate(_text, _type, _format, _resolutionX, _resolutionY, _dimensionX, _dimensionY, _enableChecksum);
                if (apiResponse == null)
                {
                    return new byte[0];
                }

                return apiResponse.ResponseStream;
            }

            public void Save_To(string localFile)
            {
                var bytes = Create();
                File.WriteAllBytes(localFile, bytes);
            }

            public string Extract_Text()
            {
                var barcodeApi = new BarcodeApi(_apiKey, _appSid, "http://api.aspose.cloud/v1.1");
                var apiResponse = barcodeApi.PostBarcodeRecognizeFromUrlorContent(_type, _enableChecksum, false, 0, null, _imageBytes);

                if (apiResponse.Status == "OK")
                {
                    var code = apiResponse.Barcodes.First();
                    return code.BarcodeValue;
                }

                return string.Empty;
            }
        }
    }
}