using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Presentation.Helpers
{
    class WinAPI
    {
        public const int EM_SETCUEBANNER = 0x1501;
        public const int WM_SYSCOMMAND = 0x112;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public extern static void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    }
}
