using AutoTool.AutoMethods;
using AutoTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoTool.AutoCommons
{
    public static class Extensions
    {
        public static string[] ToStringArray(this ListBox lb)
        {
            List<string> lst = new List<string>();
            foreach (var m in lb.Items)
            {
                lst.Add(m.ToString());
            }
            return lst.ToArray();
        }

        public static bool TapImage(this IEmulatorFunc emulatorFunc, EmulatorInfo device, string path, int timeOutInSecond = 30, ImagePoint offsetPoint = null)
        {
            var point = new WaitHelper(TimeSpan.FromSeconds(timeOutInSecond)).Until(() =>
            {
                try
                {
                    return emulatorFunc.FindOutPoint(device, path);
                }
                catch
                {
                    return null;
                }
            });

            if (point == null) return false;

            if (offsetPoint != null)
            {
                point = new ImagePoint(point.X + offsetPoint.X, point.Y + offsetPoint.Y);
            }
            return emulatorFunc.Tap(device, point.Point);
        }

        public static ImagePoint FindOutPoint(this IEmulatorFunc emulatorFunc, EmulatorInfo device, string subPath, bool getMiddle = true)
        {
            try
            {
                var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
                emulatorFunc.ScreenShot(device, screenPath);

                var point = ImageScanOpenCV.FindOutPoint(screenPath, subPath, getMiddle);
                File.Delete(screenPath);
                return point;
            }
            catch
            {
                return null;
            }
        }
    }
}
