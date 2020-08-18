using System.Drawing;
using System.IO;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace auto_android.AutoHelper
{
    public class QRCode
    {
        public static string DecodeQR(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var QrCode = (Bitmap)Image.FromStream(fileStream);
                var reader = new BarcodeReader();
                var result = reader.Decode(QrCode);
                return result?.Text;
            }
        }


        public Bitmap GenerateQR(int width, int height, string text, string imgPath)
        {
            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            Bitmap bm = bw.Write(text);
            Bitmap overlay = new Bitmap(imgPath);

            int deltaHeigth = bm.Height - overlay.Height;
            int deltaWidth = bm.Width - overlay.Width;

            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

            return bm;
        }
    }
}
