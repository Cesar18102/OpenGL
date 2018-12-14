using System.Text;
using System;

namespace OPGL13
{
    public partial class OpenGL
    {
        protected uint nFont;

        protected void LoadFont(IntPtr hDC)
        {
            if (glIsList(nFont) != 0)
                glDeleteLists(nFont, 1);

            SelectObject(hDC, Font.ToHfont());
            nFont = glGenLists(1);
            wglUseFontBitmaps(hDC, 0, 256, nFont);
        }

        private void OnFontChange(object sender, EventArgs e)
        {
            IntPtr hDC = GetDC(hWnd);
            wglMakeCurrent(hDC, hRC);

            LoadFont(hDC);

            wglMakeCurrent(hDC, IntPtr.Zero);
            ReleaseDC(hWnd, hDC);
        }

        protected void OutText(string s, double x, double y, double z = 0)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(1251);
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(s);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            glListBase(nFont);
            glRasterPos3d(x, y, z);
            glCallLists(s.Length, GL_UNSIGNED_BYTE, asciiBytes);
        }
    }
}