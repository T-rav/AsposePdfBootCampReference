using System;
using System.IO;
using System.Linq;
using Com.Aspose.Barcode.Api;
using Com.Aspose.Barcode.Model;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Aspose.Pdf.Bootcamp.Data.Tests
{
    [TestFixture]
    public class BarCodeServiceTests
    {
        [TestFixture]
        public class Create
        {
            [Test]
            public void WhenResponseNotNull_ExpectQrCode()
            {
                // arrange 
                var localPath = Path.GetTempFileName() + ".png";
                var text = Guid.NewGuid().ToString();
                var sut = new BarCodes();
                // act
                var actual = sut.With_Text(text)
                                     .With_Default_Resolution()
                                     .With_Default_Dimension()
                                     .Of_Type_QR_Code(true)
                                     .As_Png()
                                     .Create();
                // assert
                actual.Length.Should().BeGreaterThan(0);
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void WhenNullOrEmptyText_ExpectNoQrCode(string text)
            {
                // arrange 
                var localPath = Path.GetTempFileName() + ".png";
                var sut = new BarCodes();
                //act
                var actual = sut.With_Text(text)
                    .With_Default_Resolution()
                    .With_Default_Dimension()
                    .Of_Type_QR_Code(true)
                    .As_Png()
                    .Create();
                // assert
                actual.Length.Should().Be(0);
            }
        }

        [TestFixture]
        public class Save_To
        {
            [Test]
            public void WhenResponseNotNull_ExpectQrCodeSavedToFileSystem()
            {
                // arrange 
                var localPath = Path.GetTempFileName() + ".png";
                var text = Guid.NewGuid().ToString();
                var sut = new BarCodes();
                // act
                sut.With_Text(text)
                    .With_Default_Resolution()
                    .With_Default_Dimension()
                    .Of_Type_QR_Code(true)
                    .As_Png()
                    .Save_To(localPath);
                var qrCodeBytes = File.ReadAllBytes(localPath);
                // assert
                qrCodeBytes.Length.Should().BeGreaterThan(0);
            }
        }

        [TestFixture]
        public class Read
        {
            [Test]
            public void WhenQrCode_ExpectGuid()
            {
                // arrange 
                var barcodePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Barcodes", "barcode3.png");
                var bytes = File.ReadAllBytes(barcodePath);
                var sut = new BarCodes();
                // act 
                var actual = sut.With_Image(bytes)
                                .Of_Type_QR_Code(true)
                                .As_Png()
                                .Extract_Text();
                // assert
                var expected = "4b744914-00cc-4d29-99b9-d6c1a92db7a8";
                Assert.AreEqual(expected, actual);
            }
        }
    }
}