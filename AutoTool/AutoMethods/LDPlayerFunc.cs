using AutoTool.AutoCommons;
using AutoTool.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoTool.AutoMethods
{
    public class LdPlayerFunc : IEmulatorFunc
    {
        public List<EmulatorInfo> GetDevices()
        {
            throw new NotImplementedException();
        }

        public bool ClearAppData(EmulatorInfo device, string appPackage)
        {
            throw new NotImplementedException();
        }

        public bool CloneDevice(EmulatorInfo sourceDevice, string newDeviceName)
        {
            throw new NotImplementedException();
        }

        public bool Input(EmulatorInfo device, string text)
        {
            throw new NotImplementedException();
        }

        public bool Input(EmulatorInfo device, char[] text)
        {
            throw new NotImplementedException();
        }

        public bool LongPress(EmulatorInfo device, int x, int y, int duration = 1000)
        {
            throw new NotImplementedException();
        }

        public bool LongPress(EmulatorInfo device, Point point, int duration = 1000)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDevice(EmulatorInfo device)
        {
            throw new NotImplementedException();
        }

        public bool RenameDevice(EmulatorInfo device, string deviceName)
        {
            throw new NotImplementedException();
        }

        public bool RestoreDevice(string source)
        {
            throw new NotImplementedException();
        }

        public bool ScreenShot(EmulatorInfo device, string destination)
        {
            throw new NotImplementedException();
        }

        public bool SendKey(EmulatorInfo device, AdbKeyEvent keyEvent)
        {
            throw new NotImplementedException();
        }

        public bool StartApp(EmulatorInfo device, string appPackage)
        {
            throw new NotImplementedException();
        }

        public bool StartDevice(EmulatorInfo device)
        {
            throw new NotImplementedException();
        }

        public bool StopApp(EmulatorInfo device, string appPackage)
        {
            throw new NotImplementedException();
        }

        public bool StopDevice(EmulatorInfo device)
        {
            throw new NotImplementedException();
        }

        public bool IsRunning(EmulatorInfo device)
        {
            throw new NotImplementedException();
        }

        public bool Swipe(EmulatorInfo device, Point from, Point to)
        {
            throw new NotImplementedException();
        }

        public bool SwipeLong(EmulatorInfo device, Point from, Point to, int duration = 1000)
        {
            throw new NotImplementedException();
        }

        public bool Tap(EmulatorInfo device, double x, double y)
        {
            throw new NotImplementedException();
        }

        public bool Tap(EmulatorInfo device, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
