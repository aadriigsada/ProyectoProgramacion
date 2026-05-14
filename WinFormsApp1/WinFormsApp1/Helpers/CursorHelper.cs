using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    internal static class CursorHelper
    {
        public static void ApplyCustomCursor(Form form)
        {
            if (form is null)
            {
                return;
            }

            try
            {
                string cursorPath = Path.Combine(Application.StartupPath, "BoxingGlove.cur");
                if (!File.Exists(cursorPath))
                {
                    return;
                }

                IntPtr cursorHandle = LoadCursorFromFile(cursorPath);
                if (cursorHandle == IntPtr.Zero)
                {
                    return;
                }

                form.Cursor = new Cursor(cursorHandle);
            }
            catch
            {
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
    }
}
