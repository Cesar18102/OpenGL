using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System;

namespace OPGL13
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        private IntPtr DC; 
        private IntPtr HWND;
        private IntPtr WGL_CTX;

        private static float a = 0.8f;
        private static float b = 0.6f;

        private static float gm = 35; //gama
        private static float om = (float)(Math.Asin(b / a) * 180 / Math.PI) + gm; //omega

        private static float A_COS = (float)(a * Math.Cos((om - gm) * Math.PI / 180));
        private static float s = (float)(A_COS - Math.Sqrt(Math.Abs(A_COS * A_COS - a * a + b * b)));
        private static float maxs = s; // upper border for s

        private static float H = 400f; //model standard height
        private static float W = 400f; //model standard width

        private static float DH = 450f; //model top margin
        private static float DW = 400f; //model left margin

        private static float OX = 0f; //system center x
        private static float OY = 0f; //system center y

        private static float BX = W * s;
        private static float BY = 0f;

        private static float CX = W * a;
        private static float CY = 0f;

        private static float al = 30; //rotation around oX angle
        private static float bt = 10; //rotation around oY angle
        private static float sc = 1; //scale

        public RenderControl()
        {
            InitializeComponent();
            DC = GetDC(hWnd);
            HWND = hWnd;

            WGL_CTX = wglCreateContext(DC);
            wglMakeCurrent(DC, WGL_CTX);
        }

        public override void OnRender()
        {   
            glClearColor(BackColor);
            glColor(ForeColor);

            glClearDepth(2000f * sc);   
            glEnable(GL_DEPTH_TEST);
            glDepthFunc(GL_LEQUAL);
            glShadeModel(GL_SMOOTH);
            glHint(GL_PERSPECTIVE_CORRECTION_Hint, GL_NICEST);

            glEnable(GL_LIGHTING);
            glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);
            glEnable(GL_NORMALIZE);
            glEnable(GL_COLOR_MATERIAL);

            glEnable(GL_BLEND);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            glMatrixMode(GL_MODELVIEW);
            glLoadIdentity();

            glViewport(0, 0, Width, Height);
            glOrtho(0, Width + 1, Height + 1, 0, -1000f * sc, 1000f * sc);

            glColor3d(1, 0, 0);
            glLineWidth(1);

            /*LIGHTS*/

            float[] material_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material_diffuse);

            float[] light2_diffuse = { 0.4f, 0.7f, 0.2f };
            float[] light2_position = { 250.0f, 250.0f, 1000.0f, 1.0f };

            glEnable(GL_LIGHT2);
            glLightfv(GL_LIGHT2, GL_DIFFUSE, light2_diffuse);
            glLightfv(GL_LIGHT2, GL_POSITION, light2_position);
            glLightf(GL_LIGHT2, GL_CONSTANT_ATTENUATION, 0.0f);
            glLightf(GL_LIGHT2, GL_LINEAR_ATTENUATION, 1.0f);
            glLightf(GL_LIGHT2, GL_QUADRATIC_ATTENUATION, 1.0f);

            glColorMaterial(GL_FRONT, GL_DIFFUSE);
            glColorMaterial(GL_FRONT, GL_SPECULAR);

            glColorMaterial(GL_FRONT_AND_BACK, GL_EMISSION);

            /*LIGHTS*/

            /*STICKS*/

            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(gm, 0, 0, -1);

                new LineShell(OX * sc, OY * sc, CX * sc, CY * sc, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                                          Color.Yellow, Color.Violet, Color.Maroon }, 1).Draw();

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef((float)(Math.Acos((a * a + s * s - b * b) / (2 * a * s)) * 180 / Math.PI) + gm, 0, 0, -1);

                new LineShell(OX * sc, OY * sc, (BX + 100) * sc, BY * sc, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                                                  Color.Yellow, Color.Violet, Color.Maroon }, 1).Draw();

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(gm, 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0);

                glRotatef(-(float)(Math.Asin(s / a) * 180 / Math.PI) , 0, 0, -1);

                new LineShell(-W * b * sc, 0, W * b * sc, 0, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                                     Color.Yellow, Color.Violet, Color.Maroon }, 1).Draw();

            glPopMatrix();

            /*STICKS*/

            /*BALLS*/

            glPushMatrix();

                glTranslatef(DW, DH, 0);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                IntPtr SPH = gluNewQuadric();
                gluSphere(SPH, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(gm, 0, 0, -1);

                glTranslatef(CX * sc, CY * sc, 0);

                IntPtr SPH2 = gluNewQuadric();
                gluSphere(SPH2, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(gm, 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0);

                glRotatef(-(float)(Math.Asin(s / a) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(W * b * sc, 0, 0);

                IntPtr SPH3 = gluNewQuadric();
                gluSphere(SPH3, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef((float)(Math.Acos((a * a + s * s - b * b) / (2 * a * s)) * 180 / Math.PI) + gm, 0, 0, -1);

                glTranslatef((BX + 100) * sc, BY * sc, 0);

                IntPtr SPH4 = gluNewQuadric();
                gluSphere(SPH4, 17 * sc, 100, 100);

            glPopMatrix();

            /*BALLS*/

            /*PISTON*/

            glPushMatrix();

            glTranslatef(DW, DH, 0);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef((float)(Math.Acos((a * a + s * s - b * b) / (2 * a * s)) * 180 / Math.PI) + gm, 0, 0, -1);

                new LineShell((s * W - 50) * sc, 0, (s * W + 50) * sc, 0, 20 * sc, 20 * sc, new List<Color>() { Color.Black, Color.Black, Color.Black, 
                                                                                                                Color.Black, Color.Black, Color.Black }, 0.8f).Draw();

            glPopMatrix();

            /*PISTON*/

            wglSwapBuffers(DC);
            wglMakeCurrent(DC, WGL_CTX);
        }

        public void Conrolled(CNTRL_ID CID, bool inc)
        {
            switch (CID.ID)
            {
                case 1:
                    gm += inc? 1 : -1;
                    om += inc? 1 : -1;
                    break;
                case 2:

                    float ds = inc ? 0.01f : -0.01f;
                    if (s + ds >= 0.3f && s + ds <= maxs)
                    {
                        s += ds;
                        om = (float)(Math.Acos((a * a + s * s - b * b) / (2 * a * s)) * 180 / Math.PI) + gm;
                    }
                    break;
                case 3:
                    bt += inc ? 1 : -1;
                    break;
                case 4:
                    al += inc ? 1 : -1;
                    break;
                case 5:
                    sc += inc ? 0.01f : -0.01f;
                    break;
            }
        }
    }
}
