using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPGL13
{
    public partial class Form1 : Form
    {
        private RenderControl RC;

        public Form1()
        {
            RC = new RenderControl();
            RC.Dock = DockStyle.Fill;

            Label L = new Label();
            L.Location = new Point(20, 700 - L.Height * 5);
            L.AutoSize = true;
            L.Text = "Управление: W, S - управление поршнем\nA, D - поворот установки вокруг oZ\nQ, E - поворот установки вокруг oY\nNum8, Num2 - поворот установки вокруг oX\n+, - для изменения масштаба";
            L.BackColor = RC.BackColor;
            this.Controls.Add(L);

            this.Controls.Add(RC);

            KeyPreview = true;

            InitializeComponent();

            RenderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            RC.OnRender();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    RC.Conrolled(CNTRL_ID.GetFirstId(), true);
                    break;
                case Keys.D:
                    RC.Conrolled(CNTRL_ID.GetFirstId(), false);
                    break;

                case Keys.W:
                    RC.Conrolled(CNTRL_ID.GetSecondId(), true);
                    break;
                case Keys.S:
                    RC.Conrolled(CNTRL_ID.GetSecondId(), false);
                    break;

                case Keys.Q:
                    RC.Conrolled(CNTRL_ID.GetThirdId(), true);
                    break;
                case Keys.E:
                    RC.Conrolled(CNTRL_ID.GetThirdId(), false);
                    break;

                case Keys.NumPad2:
                    RC.Conrolled(CNTRL_ID.GetFourthId(), true);
                    break;
                case Keys.NumPad8:
                    RC.Conrolled(CNTRL_ID.GetFourthId(), false);
                    break;

                case Keys.Add:
                    RC.Conrolled(CNTRL_ID.GetFifthId(), true);
                    break;
                case Keys.Subtract:
                    RC.Conrolled(CNTRL_ID.GetFifthId(), false);
                    break;
            }
        }
    }
}
