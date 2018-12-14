using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Text;

namespace $rootnamespace$
{
    public partial class OpenGL
    {
        public static void glClearColor(Color c)
        {
            const float d = 255.0f;
            glClearColor(c.R / d, c.G / d, c.B / d, c.A / d);
        }

        public static void glColor(Color c)
        {
            const double d = 255.0;
            glColor4d(c.R / d, c.G / d, c.B / d, c.A / d);
        }

        public static string glGetString(uint name)
        {
            IntPtr ca = glGetAsciiString(name);
            byte[] err = new byte[1024];
            Marshal.Copy(ca, err, 0, 512);
            int idx = Array.IndexOf<byte>(err, 0);
            string txt = Encoding.ASCII.GetString(err, 0, idx);
            return txt;
        }

        public static readonly PIXELFORMATDESCRIPTOR pfdDefault = new PIXELFORMATDESCRIPTOR
        {
            nSize = (ushort)Marshal.SizeOf(pfdDefault),
            nVersion = 1,
            dwFlags = (PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER),
            iPixelType = (byte)(PFD_TYPE_RGBA),
            cColorBits = 32,
            cRedBits = 0,
            cRedShift = 0,
            cGreenBits = 0,
            cGreenShift = 0,
            cBlueBits = 0,
            cBlueShift = 0,
            cAlphaBits = 0,
            cAlphaShift = 0,
            cAccumBits = 0,
            cAccumRedBits = 0,
            cAccumGreenBits = 0,
            cAccumBlueBits = 0,
            cAccumAlphaBits = 0,
            cDepthBits = 24,
            cStencilBits = 8,
            cAuxBuffers = 0,
            iLayerType = (byte)(PFD_MAIN_PLANE),
            bReserved = 0,
            dwLayerMask = 0,
            dwVisibleMask = 0,
            dwDamageMask = 0
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct PIXELFORMATDESCRIPTOR
        {
            public ushort nSize;
            public ushort nVersion;
            public uint dwFlags;
            public byte iPixelType;
            public byte cColorBits;
            public byte cRedBits;
            public byte cRedShift;
            public byte cGreenBits;
            public byte cGreenShift;
            public byte cBlueBits;
            public byte cBlueShift;
            public byte cAlphaBits;
            public byte cAlphaShift;
            public byte cAccumBits;
            public byte cAccumRedBits;
            public byte cAccumGreenBits;
            public byte cAccumBlueBits;
            public byte cAccumAlphaBits;
            public byte cDepthBits;
            public byte cStencilBits;
            public byte cAuxBuffers;
            public byte iLayerType;
            public byte bReserved;
            public uint dwLayerMask;
            public uint dwVisibleMask;
            public uint dwDamageMask;
            // 40 bytes total
        }

        public static void ClearPixelDescriptor(ref PIXELFORMATDESCRIPTOR pfd)
        {
            pfd.nSize = (ushort)Marshal.SizeOf(pfd); // 40 bytes total
            pfd.nVersion = 0;
            pfd.dwFlags = 0;
            pfd.iPixelType = 0;
            pfd.cColorBits = 0;
            pfd.cRedBits = 0;
            pfd.cRedShift = 0;
            pfd.cGreenBits = 0;
            pfd.cGreenShift = 0;
            pfd.cBlueBits = 0;
            pfd.cBlueShift = 0;
            pfd.cAlphaBits = 0;
            pfd.cAlphaShift = 0;
            pfd.cAccumBits = 0;
            pfd.cAccumRedBits = 0;
            pfd.cAccumGreenBits = 0;
            pfd.cAccumBlueBits = 0;
            pfd.cAccumAlphaBits = 0;
            pfd.cDepthBits = 0;
            pfd.cStencilBits = 0;
            pfd.cAuxBuffers = 0;
            pfd.iLayerType = 0;
            pfd.bReserved = 0;
            pfd.dwLayerMask = 0;
            pfd.dwVisibleMask = 0;
            pfd.dwDamageMask = 0;
        }

        /* pixel types */
        public const uint PFD_TYPE_RGBA = 0;
        public const uint PFD_TYPE_COLORINDEX = 1;

        /* layer types */
        public const uint PFD_MAIN_PLANE = 0;
        public const uint PFD_OVERLAY_PLANE = 1;
        public const uint PFD_UNDERLAY_PLANE = 0xff; // (-1)

        /* PIXELFORMATDESCRIPTOR flags */
        public const uint PFD_DOUBLEBUFFER = 0x00000001;
        public const uint PFD_STEREO = 0x00000002;
        public const uint PFD_DRAW_TO_WINDOW = 0x00000004;
        public const uint PFD_DRAW_TO_BITMAP = 0x00000008;
        public const uint PFD_SUPPORT_GDI = 0x00000010;
        public const uint PFD_SUPPORT_OPENGL = 0x00000020;
        public const uint PFD_GENERIC_FORMAT = 0x00000040;
        public const uint PFD_NEED_PALETTE = 0x00000080;
        public const uint PFD_NEED_SYSTEM_PALETTE = 0x00000100;
        public const uint PFD_SWAP_EXCHANGE = 0x00000200;
        public const uint PFD_SWAP_COPY = 0x00000400;
        public const uint PFD_SWAP_LAYER_BUFFERS = 0x00000800;
        public const uint PFD_GENERIC_ACCELERATED = 0x00001000;
        public const uint PFD_SUPPORT_DIRECTDRAW = 0x00002000;

        /* PIXELFORMATDESCRIPTOR flags for use in ChoosePixelFormat only */
        public const uint PFD_DEPTH_DONTCARE = 0x20000000;
        public const uint PFD_DOUBLEBUFFER_DONTCARE = 0x40000000;
        public const uint PFD_STEREO_DONTCARE = 0x80000000;

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern IntPtr LoadLibrary(string dllName);

        [DllImport("user32", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll")]
        private static extern uint SetClassLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("gdi32", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr dc, IntPtr obj);

        [DllImport("opengl32", EntryPoint = "wglUseFontBitmaps")]
        public static extern int wglUseFontBitmaps(IntPtr dc, uint first, uint count, uint ListBase);

        /// <summary>
        /// Retrieves an index for a pixel format closest to what is passed
        /// </summary>
        /// <param name="hdc">Device context</param>
        /// <param name="p_pfd">Pixel Format Descriptor struct</param>
        /// <returns></returns>
        [DllImport("gdi32", EntryPoint = "ChoosePixelFormat")]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR p_pfd);

        /// <summary>
        /// Sets the pixel format for the device context to the format specified by the index
        /// </summary>
        /// <param name="hdc">Device Context</param>
        /// <param name="iPixelFormat">Index to a pixel format returned ChoosePixelFormat</param>
        /// <param name="p_pfd">Pixel Format Descriptor</param>
        /// <returns></returns>
        [DllImport("gdi32", EntryPoint = "SetPixelFormat")]
        public static extern int _SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR p_pfd);

        public static int SetPixelFormat(IntPtr deviceContext, int pixelFormat, ref PIXELFORMATDESCRIPTOR pixelFormatDescriptor) 
        {
            LoadLibrary("opengl32.dll");
            return _SetPixelFormat(deviceContext, pixelFormat, ref pixelFormatDescriptor);    
        }

        /// <summary>
        /// Creates a rendering context for the Device context.
        /// </summary>
        /// <param name="hdc">Device Context</param>
        /// <returns></returns>
        [DllImport("opengl32", EntryPoint = "wglCreateContext")]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        /// <summary>
        /// Sets the current rendering context
        /// </summary>
        /// <param name="hdc">Device Context</param>
        /// <param name="hglrc">Rendering Context</param>
        /// <returns></returns>
        [DllImport("opengl32", EntryPoint = "wglMakeCurrent")]
        public static extern int wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

        /// <summary>
        /// Deletes the rendering context
        /// </summary>
        /// <param name="hglrc">Rendering context to delet</param>
        /// <returns></returns>
        [DllImport("opengl32", EntryPoint = "wglDeleteContext")]
        public static extern int wglDeleteContext(IntPtr hglrc);

        /// <summary>
        /// Swaps the display buffers in a double buffer context
        /// </summary>
        /// <param name="hdc">Device context</param>
        /// <returns></returns>
        [DllImport("opengl32", EntryPoint = "wglSwapBuffers")]
        public static extern uint wglSwapBuffers(IntPtr hdc);
    }
}