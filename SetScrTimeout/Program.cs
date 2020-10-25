using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SetScrTimeout
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);
        static int Main(string[] args)
        {
            Console.WriteLine(string.Format("{0} {1}\r\n{2}\r\nhttps://github.com/DavisNT/SetScrTimeout\r\n", typeof(Program).Assembly.GetName().Name, typeof(Program).Assembly.GetName().Version.ToString(3), ((AssemblyDescriptionAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description));
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: SetScrTimeout timeout\r\ntimeout is specified in seconds\r\n");
                return args.Length == 0 ? 0 : 1;
            }
            else
            {
                var timeout = uint.Parse(args[0], CultureInfo.InvariantCulture);
                Console.Write(string.Format(CultureInfo.InvariantCulture, "Setting screen saver timeout to {0} seconds... ", timeout));
                var result = SystemParametersInfo(15 /* SPI_SETSCREENSAVETIMEOUT */, timeout, (IntPtr)null, 3 /* SPIF_UPDATEINIFILE | SPIF_SENDCHANGE */);
                Console.WriteLine(result ? "DONE\r\n" : "FAILED\r\n");
                return result ? 0 : 1;
            }
        }
    }
}
