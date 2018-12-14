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

        private static float a = 0.46f;
        private static float b = 0.82f;
        private static float c = 0.6f;

        private static float gm = -60; //gama
        private static float om = 60; //omega
        private static float ksi = 40; //ksi

        private static float s = (float)(Math.Sqrt(Math.Abs(a * a - 2 * a * c * Math.Cos((180 + gm - om) * Math.PI / 180))));
        private static float maxs = s; //upper border for s

        private static float H = 400f; //model standard height
        private static float W = 400f; //model standard width

        private static float DH = 300f; //model top margin
        private static float DW = 350f; //model left margin

        private static float OX = 0f; //system center x
        private static float OY = 0f; //system center y

        private static float AX = W * b;
        private static float AY = 0f;

        private static float BX = W * c;
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

            /*LIGHTS*/

            /*STICKS*/

            glPushMatrix();

                glColorMaterial(GL_FRONT_AND_BACK, GL_EMISSION);
                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);

                new LineShell(OX * sc, OY * sc, CX * sc, CY * sc, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                                          Color.Yellow, Color.Violet, Color.Maroon }).Draw();

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0f);

                glRotatef((float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI) + om, 0, 0, -1);

                new LineShell(0, 0, BX * sc, BY * sc, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                              Color.Yellow, Color.Violet, Color.Maroon }).Draw();

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0f);

                glRotatef((float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI) + om - ksi, 0, 0, -1);

                new LineShell(0, 0, AX * sc, AY * sc, 10f * sc, 10f * sc, new List<Color>() { Color.Red, Color.Green, Color.Blue, 
                                                                                              Color.Yellow, Color.Violet, Color.Maroon }).Draw();

            glPopMatrix();

            ///*STICKS*/

            ///*BALLS*/

            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0f);

                IntPtr SPH = gluNewQuadric();
                gluSphere(SPH, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                IntPtr SPH2 = gluNewQuadric();
                gluSphere(SPH2, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0f);

                glRotatef((float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI) + om - ksi, 0, 0, -1);
                glTranslatef(AX * sc, AY * sc, 0f);

                IntPtr SPH3 = gluNewQuadric();
                gluSphere(SPH3, 17 * sc, 100, 100);

            glPopMatrix();



            glPushMatrix();

                glTranslatef(DW, DH, 0f);

                glRotatef(al, -1, 0, 0);//PERSPECTIVE
                glRotatef(bt, 0, -1, 0);//PERSPECTIVE

                glRotatef(-(float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI), 0, 0, -1);
                glTranslatef(CX * sc, CY * sc, 0f);

                glRotatef((float)(Math.Acos((a * a + s * s) / (2 * a * Math.Sqrt(s * s + c * c))) * 180 / Math.PI) + om, 0, 0, -1);
                glTranslatef(BX * sc, BY * sc, 0f);

                IntPtr SPH4 = gluNewQuadric();
                gluSphere(SPH4, 17 * sc, 100, 100);

            glPopMatrix();

            ///*BALLS*/

            wglSwapBuffers(DC);
            wglMakeCurrent(DC, WGL_CTX);
        }

        public void Conrolled(CNTRL_ID CID, bool inc)
        {
            switch (CID.ID)
            {
                case 1:
                    float ksin = ksi + (inc ? -1 : 1);
                    if (ksin >= 20 && ksin <= 90)
                        ksi = ksin;
                    break;
                case 2:

                    float ns = s + (inc ? 0.01f : -0.01f);
                    if (ns >= 0 && ns <= 0.7f)
                        s = ns;
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
