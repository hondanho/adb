using IronOcr;
using static IronOcr.AdvancedOcr;

namespace AutoTool.AutoCommons
{
    internal class GetTextFromImage
	{
		public static string GetTextFromImg(string path)
		{
            var Ocr = new AdvancedOcr()
            {
                CleanBackgroundNoise = true,
                EnhanceContrast = true,
                EnhanceResolution = true,
                Language = IronOcr.Languages.English.OcrLanguagePack,
                Strategy = OcrStrategy.Advanced,
                ColorSpace = OcrColorSpace.Color,
                DetectWhiteTextOnDarkBackgrounds = true,
                InputImageType = InputTypes.Document,
                RotateAndStraighten = true,
                ReadBarCodes = true,
                ColorDepth = 4
            };
            var Results = Ocr.Read(path);
            return string.Empty;
		}
    }
}
