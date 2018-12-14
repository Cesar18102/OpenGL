using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace $rootnamespace$
{
    [ToolboxItem(false)]
    [ToolboxBitmap("OpenGL.ico"), DefaultEvent("Dummy")]
    public partial class OpenGL : UserControl
    {
        public const int CS_VREDRAW      = 0x01;
        public const int CS_HREDRAW      = 0x02;
        public const int CS_OWNDC        = 0x20;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_CHILD        = 0x40000000;
        public const int WS_VISIBLE      = 0x10000000;
        public const int GCL_HBRBACKGROUND = (-10);


        private string errorMessage;

        public OpenGL()
        {
            InitializeComponent();
            InitializeOpenGL();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_VREDRAW | CS_HREDRAW | CS_OWNDC;
                cp.Style = cp.Style | WS_CLIPCHILDREN | WS_CLIPSIBLINGS | WS_CHILD | WS_VISIBLE;
                return cp;
            }
        }

        /// <summary>
        /// OpenGL render context
        /// </summary>
        protected IntPtr hRC  { get;  private set; }
        protected IntPtr hWnd { get { return this.Handle; } }

        private void CheckAction(string message, int error)
        {
            if (error == 0)
                throw new Exception( message );
        }

        public static bool IsInDesignMode
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"); }
        }

        private void InitializeOpenGL()
        {
            if (IsInDesignMode)
            {
                Paint += OnPaintDesigner;
                return;
            }

            SetClassLong(hWnd, GCL_HBRBACKGROUND, 0);
            SetStyle(ControlStyles.Opaque, true);

            IntPtr hDC = GetDC(hWnd);
            try
            {
                PIXELFORMATDESCRIPTOR pfd = pfdDefault;
                int pixelFormat = ChoosePixelFormat(hDC, ref pfd); // If the function fails, the return value is zero
                CheckAction("ChoosePixelFormat failed.", pixelFormat);

                int result = SetPixelFormat(hDC, pixelFormat, ref pfd); // If the function fails, the return value is FALSE. 
                CheckAction("SetPixelFormat failed.", result);

                hRC = wglCreateContext(hDC); // If the function fails, the return value is NULL
                CheckAction("wglCreateContext failed.", hRC.ToInt32());

                wglMakeCurrent(hDC, hRC);

                LoadFont(hDC);
                OnActivitedOpenGL();
                Paint += OnPaint;

                wglMakeCurrent(hDC, IntPtr.Zero);
            }
            catch (Exception x)
            {
                errorMessage = x.Message;
                this.Paint += OnPaintError;
            }

            ReleaseDC(hWnd, hDC);
        }

        void FinalizeOpenGL()
        {
            IntPtr hDC = GetDC(hWnd);
            wglMakeCurrent(hDC, hRC);

            OnDeactivitingOpenGL();

            wglMakeCurrent(hDC, IntPtr.Zero);
            wglDeleteContext(hRC);
            ReleaseDC(hWnd, hDC);
        }

        #if PERFORMANCE
            Stopwatch sw = new Stopwatch();
            ulong counter = 0;
        #endif

        private void OnPaint(object sender, PaintEventArgs e)
        {
            #if PERFORMANCE
                sw.Start();
            #endif
            IntPtr hDC = e.Graphics.GetHdc();

            wglMakeCurrent(hDC, hRC);

                try 
                {
                    OnRender();
                }
                catch (Exception x)
                {
                    OnRenderError(x.Message);
                }
                glFinish();
                wglSwapBuffers(hDC);

            wglMakeCurrent(hDC, IntPtr.Zero);
            #if PERFORMANCE
                sw.Stop();
                Debug.WriteLine("FPS = " + ((++counter * 1000.0) / sw.ElapsedMilliseconds).ToString());
            #endif
        }

        private void OnPaintDesigner(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush( BackColor ), this.ClientRectangle);
            g.DrawString("Open GL control. Design mode", Font, new SolidBrush( ForeColor ), 5, 10);
        }

        private void OnPaintError(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, this.ClientRectangle);
            g.DrawString("GDI. " + errorMessage, DefaultFont, Brushes.Red, 10, 10);
        }

        public virtual void OnRenderError(string msg)
        {
            glLoadIdentity();
            glClearColor(Color.White);
            glClear(GL_COLOR_BUFFER_BIT);
            glViewport(0, 0, Width, Height);
            glOrtho(0, Width, Height, 0, -1, 1);

            glColor(Color.Red);
            OutText("OpenGL. " + msg, 10, 15);
        }

        public virtual void OnActivitedOpenGL() { }
        public virtual void OnDeactivitingOpenGL() { }

        public virtual void OnRender() { }

    }
}
