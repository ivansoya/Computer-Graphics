using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Laboratory2CP
{
    public partial class Form1 : Form
    {

        private double a = 1, b = 0, c = 0;
        private double rotate = 0.01;

        public Form1()
        {
            InitializeComponent();
            OGL.InitializeContexts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, OGL.Height, OGL.Width);

            // Режим матрицы проекций
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // Единичная матрица
            Gl.glLoadIdentity();
            // Задаем перспективную матрицу (угол охвата, отношение w/h, ближняя, дальняя плоскость)
            Glu.gluPerspective(45, (float)OGL.Width / OGL.Height, 0.1 , 1000);
            Gl.glOrtho(0.0, (double)OGL.Height, 0.0, (double)OGL.Width, 0, 1000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            a = (double)trackBar1.Value / 100;
            label1.Text = a.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            b = (double)trackBar2.Value / 100;
            label2.Text = b.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            c = (double)trackBar3.Value / 100;
            label3.Text = c.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawFrames();
        }

        private void DrawFrames()
        {

            //Очистка кадра (очищаем буферы цвета и глубины)
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            // Перемещает матрицу в стек;
            Gl.glPushMatrix();
            // Двигает матрицу по Z
            Gl.glTranslated(225, 275, 0);
            // Осуществляет поворот по двум осям на 45 градусов
            rotate += 0.1;
            if (rotate > 360)
                rotate = 0.01;
            Gl.glRotated(rotate, 50, 50, 50);

            //Режим рисования квадратов
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(0.0, 0.0, 0.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(0.0, 0.0, 100.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(0.0, 100.0, 100.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(0.0, 100.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(0.0, 100.0, 0.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(100.0, 0.0, 100.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(100.0, 100.0, 100.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(100.0, 100.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(100.0, 100.0, 0.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(100.0, 100.0, 100.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(100.0, 0.0, 100.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(100.0, 0.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(100.0, 0.0, 0.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(100.0, 0.0, 100.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(0.0, 0.0, 100.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(0.0, 0.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(0.0, 0.0, 0.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(100.0, 0.0, 0.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(100.0, 100.0, 0.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(0.0, 100.0, 0.0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3d(a, b, c);
            Gl.glVertex3d(0.0, 0.0, 100.0);
            Gl.glColor3d(b, c, a);
            Gl.glVertex3d(100.0, 0.0, 100.0);
            Gl.glColor3d(a, c, b);
            Gl.glVertex3d(100.0, 100.0, 100.0);
            Gl.glColor3d(c, b, a);
            Gl.glVertex3d(0.0, 100.0, 100.0);
            Gl.glEnd();

            Gl.glFlush();
            OGL.Invalidate();
        }

        private void Display()
        {

        }
    }
}


