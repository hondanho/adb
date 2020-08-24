using AutoTool.AutoCommons;
using AutoTool.Models;
using System.Collections.Generic;
using System.Drawing;

namespace AutoTool.AutoMethods
{
    public interface IEmulatorFunc
    {
        List<EmulatorInfo> GetDevices();
        bool StartDevice(EmulatorInfo device);
        bool StopDevice(EmulatorInfo device);
        bool IsRunning(EmulatorInfo device);
        bool RemoveDevice(EmulatorInfo device);
        bool RenameDevice(EmulatorInfo device, string deviceName);
        bool RestoreDevice(string source);
        bool CloneDevice(EmulatorInfo sourceDevice, string newDeviceName);
        bool StartApp(EmulatorInfo device, string appPackage);
        bool StopApp(EmulatorInfo device, string appPackage);
        bool ClearAppData(EmulatorInfo device, string appPackage);
        bool SendKey(EmulatorInfo device, AdbKeyEvent keyEvent);
        bool LongPress(EmulatorInfo device, int x, int y, int duration = 1000);
        bool LongPress(EmulatorInfo device, Point point, int duration = 1000);
        bool Tap(EmulatorInfo device, double x, double y);
        bool Tap(EmulatorInfo device, Point point);
        bool Swipe(EmulatorInfo device, Point from, Point to);
        bool SwipeLong(EmulatorInfo device, Point from, Point to, int duration = 1000);
        bool ScreenShot(EmulatorInfo device, string destination);
        bool Input(EmulatorInfo device, string text);
        bool Input(EmulatorInfo device, char[] text);
    }
}
