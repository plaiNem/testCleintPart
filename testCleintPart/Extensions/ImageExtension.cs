using System.IO;
using System.Windows.Media.Imaging;

namespace testCleintPart.Extensions
{
    public static class ImageExtension
    {
        public static byte[] BitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            using var stream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(stream);
            return stream.ToArray();
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using var stream = new MemoryStream(byteArray);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            return bitmap;
        }
    }
}
