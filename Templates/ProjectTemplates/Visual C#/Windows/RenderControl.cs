using System.ComponentModel;

namespace $safeprojectname$
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
        }

        public override void OnRender()
        {
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            glViewport(0, 0, Width, Height);
            glOrtho(0, Width + 1, Height + 1, 0, -1, 1);

            OutText("OpenGL version - " + glGetString(GL_VERSION), 10, (3 + FontHeight) * 1);
            OutText("OpenGL vendor - " + glGetString(GL_VENDOR), 10, (3 + FontHeight) * 2);
            // OutText( "Cyrilic test  - ъЪ эЭ юЮ яЯ ёЁ іІ їЇ", 10,(3 + FontHeight) * 3);
        }

    }
}
