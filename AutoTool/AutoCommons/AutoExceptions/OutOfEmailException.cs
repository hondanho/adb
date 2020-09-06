using System;


namespace AutoTool.AutoCommons.AutoExceptions
{
    public class OutOfEmailException : Exception
    {
        public OutOfEmailException() : base("Out of email address.")
        {
        }
    }
}
