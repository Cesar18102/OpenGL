using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace OPGL13
{
    public class LineShell
    {
        public float X1 { get; private set; }
        public float Y1 { get; private set; }

        public float X2 { get; private set; }
        public float Y2 { get; private set; }

        public float DY { get; private set; }
        public float DZ { get; private set; }

        private List<Color> Colors = new List<Color>() { new Color(), new Color(), new Color(), 
                                                         new Color(), new Color(), new Color() };

        public LineShell(float x1, float y1, float x2, float y2, float dy, float dz, List<Color> CS)
        {
            X1 = x1;
            Y1 = y1;

            X2 = x2;
            Y2 = y2;

            DY = dy;
            DZ = dz;

            for (int i = 0; i < CS.Count && i < Colors.Count; i++)
                Colors[i] = CS[i];
        }

        public void Draw()
        {

            RenderControl.glBegin(RenderControl.GL_QUADS);

                RenderControl.glColor3d(Colors[0].R, Colors[0].G, Colors[0].B);
                RenderControl.glVertex3f(X1, Y1 - DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 - DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 - DY, DZ);
                RenderControl.glVertex3f(X1, Y1 - DY, DZ);

                RenderControl.glColor3d(Colors[1].R, Colors[1].G, Colors[1].B);
                RenderControl.glVertex3f(X1, Y1 + DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, DZ);
                RenderControl.glVertex3f(X1, Y1 + DY, DZ);

                RenderControl.glColor3d(Colors[2].R, Colors[2].G, Colors[2].B);
                RenderControl.glVertex3f(X1, Y1 - DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 - DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, -DZ);
                RenderControl.glVertex3f(X1, Y1 + DY, -DZ);

                RenderControl.glColor3d(Colors[3].R, Colors[3].G, Colors[3].B);
                RenderControl.glVertex3f(X1, Y1 - DY, DZ);
                RenderControl.glVertex3f(X2, Y2 - DY, DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, DZ);
                RenderControl.glVertex3f(X1, Y1 + DY, DZ);

                RenderControl.glColor3d(Colors[4].R, Colors[4].G, Colors[4].B);
                RenderControl.glVertex3f(X1, Y1 - DY, -DZ);
                RenderControl.glVertex3f(X1, Y1 + DY, -DZ);
                RenderControl.glVertex3f(X1, Y1 + DY, DZ);
                RenderControl.glVertex3f(X1, Y1 - DY, DZ);

                RenderControl.glColor3d(Colors[5].R, Colors[5].G, Colors[5].B);
                RenderControl.glVertex3f(X2, Y2 - DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, -DZ);
                RenderControl.glVertex3f(X2, Y2 + DY, DZ);
                RenderControl.glVertex3f(X2, Y2 - DY, DZ);

            RenderControl.glEnd();
        }
    }
}
