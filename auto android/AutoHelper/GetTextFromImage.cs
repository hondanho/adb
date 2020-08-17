using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace auto_android.AutoHelper
{
	internal class GetTextFromImage
	{
		private static int saisot = 5;

		private static int red = 217;

		private static int collor_Byte_Start = 160;

		private static string path_langue = "C:\\";

		private static string TempFolder = "image_temp";

		private static string StandarFolder = "image_standand";

		private static List<Color> TemplateColors = new List<Color>
		{
			Color.FromArgb(255, 0, 0, 0)
		};

		public static void information(string Path_Langue)
		{
			path_langue = Path_Langue;
		}

		public static string Get_Text(Bitmap Bm_image_sour)
		{
			Bitmap image = make_new_image(Bm_image_sour.ToImage<Gray, byte>().ToBitmap());
			Bm_image_sour.Dispose();
			int cout_picture = split_image(image);
			return Get_Text(cout_picture);
		}

		public static Bitmap make_new_image(Bitmap Bm_image_sour)
		{
			int _width = Bm_image_sour.Width;
			int _height = Bm_image_sour.Height;
			Bitmap Bm_image = new Bitmap(_width, _height);
			int num = 230;
			for (int i = collor_Byte_Start; i < num; i++)
			{
				red = i;
				for (int j = 0; j < _width; j++)
				{
					for (int k = 0; k < _height; k++)
					{
						Color pixel = Bm_image_sour.GetPixel(j, k);
						if (Check_sailenh_Color(pixel, TemplateColors, saisot))
						{
							try
							{
								Bm_image.SetPixel(j, k, Color.Black);
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
			return Bm_image;
		}

		static bool Check_sailenh_Color(Color indexColor, List<Color> templateColor, int sailech)
		{
			bool result = false;
			foreach (Color item in templateColor)
			{
				if (indexColor.R + sailech >= item.R && indexColor.R - sailech <= item.R && indexColor.G + sailech >= item.G && indexColor.G - sailech <= item.G && indexColor.B + sailech >= item.B && indexColor.B - sailech <= item.B)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public static int split_image(Bitmap image, string name = "")
		{
			int cout_picture = 0;
			bool flag = false;
			int width_start = 0;
			int width_stop = 0;
			int _height_top = 200;
			int _height_bottom = 0;
			int width = image.Width;
			int height = image.Height;
			for (int i = 0; i < width; i++)
			{
				int num = 0;
				for (int j = 0; j < height; j++)
				{
					if (image.GetPixel(i, j).Name != "0")
					{
						num++;
						if (_height_top > j)
						{
							_height_top = j;
						}
						if (_height_bottom < j)
						{
							_height_bottom = j;
						}
					}
				}
				if (num > 1 && !flag)
				{
					width_start = i - 1;
					flag = true;
				}
				if (num < 1 && flag)
				{
					width_stop = i + 1;
					flag = false;
					save_image_splip();
					cout_picture++;
					_height_top = 200;
					_height_bottom = 0;
				}
			}
			return cout_picture;
			void save_image_splip()
			{
				int num2 = width_stop - width_start;
				int num3 = _height_bottom - _height_top;
				Bitmap bitmap = new Bitmap(num2, num3);
				for (int k = 0; k < num2; k++)
				{
					for (int l = 0; l < num3; l++)
					{
						try
						{
							Color pixel = image.GetPixel(width_start + k, _height_top + l);
							bitmap.SetPixel(k, l, pixel);
						}
						catch
						{
						}
					}
				}
				string tempFolder = TempFolder;
				check_folder_exists(tempFolder);
				string filename = tempFolder + "\\" + name + cout_picture + ".jpg";
				bitmap.Save(filename);
				bitmap.Dispose();
			}
		}

		protected static string Get_Text(int cout_picture)
		{
			string text = "";
			List<string> list = new List<string>
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9"
			};
			for (int i = 0; i < cout_picture; i++)
			{
				List<double> list2 = new List<double>();
				for (int j = 0; j < list.Count; j++)
				{
					try
					{
						string str = list[j];
						double num = 0.0;
						double num2 = 0.0;
						string path = StandarFolder + "\\" + str;
						DirectoryInfo directoryInfo = new DirectoryInfo(path);
						FileInfo[] files = directoryInfo.GetFiles();
						foreach (FileInfo fileInfo in files)
						{
							string fullName = fileInfo.FullName;
							Bitmap bitmap = new Bitmap(fullName);
							string filename = TempFolder + "\\" + i + ".jpg";
							Bitmap bitmap2 = new Bitmap(filename);
							num2 = Image_Equal(bitmap2, bitmap);
							bitmap.Dispose();
							bitmap2.Dispose();
							if (num2 > num)
							{
								num = num2;
							}
						}
						list2.Add(num);
					}
					catch
					{
					}
				}
				int index = 0;
				double num3 = 0.0;
				for (int l = 0; l < list.Count; l++)
				{
					if (num3 < list2[l])
					{
						num3 = list2[l];
						index = l;
					}
				}
				text += list[index];
			}
			return text;
		}

		public static double Image_Equal(Bitmap main, Bitmap standand)
		{
			double num = 0.0;
			double num2 = 0.0;
			Bitmap bitmap = new Bitmap(main, new Size(standand.Width, standand.Height));
			for (int i = 0; i < standand.Width; i++)
			{
				for (int j = 0; j < standand.Height; j++)
				{
					num += 1.0;
					if (bitmap.GetPixel(i, j).Equals(standand.GetPixel(i, j)))
					{
						num2 += 1.0;
					}
				}
			}
			return num2 / num;
		}

		protected static void check_folder_exists(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}
