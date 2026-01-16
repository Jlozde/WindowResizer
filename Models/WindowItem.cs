using System;

namespace WindowResizer.Models
{
    public sealed class WindowItem
    {
        public IntPtr Hwnd { get; init; }
        public string Title { get; init; } = "";
        public string ProcessName { get; init; } = "";

        public string HwndHex => "0x" + Hwnd.ToInt64().ToString("X");
    }
}
