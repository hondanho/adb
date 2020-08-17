using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace auto_android.AutoHelper
{
	internal class ImageScanOpenCV
	{
		public static Bitmap GetImage(string path)
		{
			using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
				return (Bitmap)Image.FromStream(fileStream);
			}
		}

		public static Bitmap Find(string main, string sub, double percent = 0.9)
		{
			Bitmap image = ImageScanOpenCV.GetImage(main);
			Bitmap image2 = ImageScanOpenCV.GetImage(sub);
			return ImageScanOpenCV.Find(main, sub, percent);
		}

        public static Bitmap Find(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Image<Bgr, byte> image = mainBitmap.ToImage<Bgr, byte>();
            Image<Bgr, byte> image2 = subBitmap.ToImage<Bgr, byte>();
            Image<Bgr, byte> image3 = image.Copy();
            using (Image<Gray, float> image4 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
            {
                double[] array;
                double[] array2;
                Point[] array3;
                Point[] array4;
                image4.MinMax(out array, out array2, out array3, out array4);
                bool flag = array2[0] > percent;
                if (flag)
                {
                    Rectangle rect = new Rectangle(array4[0], image2.Size);
                    image3.Draw(rect, new Bgr(Color.Red), 2, LineType.EightConnected, 0);
                }
                else
                {
                    image3 = null;
                }
            }
            return (image3 == null) ? null : image3.ToBitmap();
        }

        public static Point? FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Image<Bgr, byte> image = mainBitmap.ToImage<Bgr, byte>();
            Image<Bgr, byte> template = subBitmap.ToImage<Bgr, byte>();
            Point? result = null;
            using (Image<Gray, float> image2 = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] array;
                double[] array2;
                Point[] array3;
                Point[] array4;
                image2.MinMax(out array, out array2, out array3, out array4);
                bool flag = array2[0] > percent;
                if (flag)
                {
                    var tmp = new Point?(array4[0]);
                    if (tmp != null)
                    {
                        result = new Point(tmp.Value.X + template.Width / 2, tmp.Value.Y + template.Height / 2);
                    }
                }
            }
            return result;
        }
        public static Point? FindOutPoint(string mainPath, string subPath)
        {
            Bitmap image = GetImage(mainPath);
            Bitmap image2 = GetImage(subPath);
            return FindOutPoint(image, image2);
        }

        //public static List<Point> FindOutPoints(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        //{
        //	//IL_005c: Unknown result type (might be due to invalid IL or missing references)
        //	Image<Bgr, byte> val = new Image<Bgr, byte>(mainBitmap);
        //	Image<Bgr, byte> val2 = new Image<Bgr, byte>(subBitmap);
        //	List<Point> list = new List<Point>();
        //	double[] array = default(double[]);
        //	double[] array2 = default(double[]);
        //	Point[] array3 = default(Point[]);
        //	Point[] array4 = default(Point[]);
        //	while (true)
        //	{
        //		Image<Gray, float> val3 = val.MatchTemplate(val2, (TemplateMatchingType)5);
        //		try
        //		{
        //			val3.MinMax(out array, out array2, out array3, out array4);
        //			if (array2[0] > percent)
        //			{
        //				Rectangle rectangle = new Rectangle(array4[0], ((CvArray<byte>)(object)val2).Size);
        //				val.Draw(rectangle, new Bgr(System.Drawing.Color.Blue), -1, (LineType)8, 0);
        //				list.Add(array4[0]);
        //				continue;
        //			}
        //		}
        //		finally
        //		{
        //			((IDisposable)val3)?.Dispose();
        //		}
        //		break;
        //	}
        //	return list;
        //}

        //public static List<Point> FindColor(Bitmap mainBitmap, System.Drawing.Color color)
        //{
        //	int num = color.ToArgb();
        //	List<Point> list = new List<Point>();
        //	using (Bitmap bitmap = mainBitmap)
        //	{
        //		for (int i = 0; i < bitmap.Width; i++)
        //		{
        //			for (int j = 0; j < bitmap.Height; j++)
        //			{
        //				if (num.Equals(bitmap.GetPixel(i, j).ToArgb()))
        //				{
        //					list.Add(new Point(i, j));
        //				}
        //			}
        //		}
        //	}
        //	return list;
        //}

        ////public static List<Point> FindColor(Bitmap mainBitmap, string color)
        ////{
        ////	System.Drawing.Color color2 = (System.Drawing.Color)System.Windows.Media.ColorConverter.ConvertFromString(color);
        ////	return FindColor(mainBitmap, color2);
        ////}

        //public static string RecolizeText(string imgPath)
        //{
        //	string text = "";
        //	return GetTextFromImage.Get_Text(GetImage(imgPath));
        //}

        //public static string RecolizeText(Bitmap img)
        //{
        //	string text = "";
        //	return GetTextFromImage.Get_Text(img);
        //}

        //public static void SplitImageInFolder(string folderPath)
        //{
        //	DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        //	FileInfo[] files = directoryInfo.GetFiles();
        //	foreach (FileInfo fileInfo in files)
        //	{
        //		Bitmap bitmap = new Bitmap(fileInfo.FullName);
        //		Bitmap image = GetTextFromImage.make_new_image(new Image<Gray, byte>(bitmap).ToBitmap());
        //		bitmap.Dispose();
        //		int num = GetTextFromImage.split_image(image, Path.GetFileNameWithoutExtension(fileInfo.Name));
        //	}
        //}

        //public static Bitmap ThreshHoldBinary(Bitmap bmp, byte threshold = 190)
        //{
        //	//IL_000b: Unknown result type (might be due to invalid IL or missing references)
        //	//IL_0019: Unknown result type (might be due to invalid IL or missing references)
        //	Image<Gray, byte> val = new Image<Gray, byte>(bmp);
        //	Image<Gray, byte> val2 = val.ThresholdBinary(new Gray((double)(int)threshold), new Gray(255.0));
        //	return val2.ToBitmap();
        //}

        //public static Bitmap NotWhiteToTransparentPixelReplacement(Bitmap bmp)
        //{
        //	bmp = CreateNonIndexedImage(bmp);
        //	for (int i = 0; i < bmp.Width; i++)
        //	{
        //		for (int j = 0; j < bmp.Height; j++)
        //		{
        //			System.Drawing.Color pixel = bmp.GetPixel(i, j);
        //			if (pixel.R > 200 && pixel.G > 200 && pixel.B > 200)
        //			{
        //				bmp.SetPixel(i, j, System.Drawing.Color.Transparent);
        //			}
        //		}
        //	}
        //	return bmp;
        //}

        //public static Bitmap WhiteToBlackPixelReplacement(Bitmap bmp)
        //{
        //	bmp = CreateNonIndexedImage(bmp);
        //	for (int i = 0; i < bmp.Width; i++)
        //	{
        //		for (int j = 0; j < bmp.Height; j++)
        //		{
        //			System.Drawing.Color pixel = bmp.GetPixel(i, j);
        //			if (pixel.R > 20 && pixel.G > 230 && pixel.B > 230)
        //			{
        //				bmp.SetPixel(i, j, System.Drawing.Color.Black);
        //			}
        //		}
        //	}
        //	return bmp;
        //}

        //public static Bitmap TransparentToWhitePixelReplacement(Bitmap bmp)
        //{
        //	bmp = CreateNonIndexedImage(bmp);
        //	for (int i = 0; i < bmp.Width; i++)
        //	{
        //		for (int j = 0; j < bmp.Height; j++)
        //		{
        //			if (bmp.GetPixel(i, j).A >= 1)
        //			{
        //				bmp.SetPixel(i, j, System.Drawing.Color.White);
        //			}
        //		}
        //	}
        //	return bmp;
        //}

        //public static Bitmap CreateNonIndexedImage(Image src)
        //{
        //	Bitmap bitmap = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //	using (Graphics graphics = Graphics.FromImage(bitmap))
        //	{
        //		graphics.DrawImage(src, 0, 0);
        //	}
        //	return bitmap;
        //}
    }
}
