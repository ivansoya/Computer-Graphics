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

namespace Laboratory1CP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OGL.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация glut
            Glut.glutInit(); 
            // Установка RGB, двойной буферизации, буфер глубины
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH); 

            // Цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            //Установка вывода
            Gl.glViewport(0, 0, OGL.Width, OGL.Height); 

            // Режим матрицы проекций
            Gl.glMatrixMode(Gl.GL_PROJECTION); 
            // Единичная матрица
            Gl.glLoadIdentity(); 
            // Задаем перспективную матрицу (угол охвата, отношение w/h, ближняя, дальняя плоскость)
            Glu.gluPerspective(45, (float)OGL.Width / OGL.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW); 
            Gl.glLoadIdentity(); 
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Очистка кадра (очищаем буферы цвета и глубины)
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); 
            Gl.glLoadIdentity(); 
            //Установка цвета
            Gl.glColor3f(1.0f, 0, 0); 
            // Перемещает матрицу в стек;
            Gl.glPushMatrix(); 
            // Двигает матрицу по Z
            Gl.glTranslated(0, 0, -6);
            // Осуществляет поворот по двум осям на 45 градусов
            Gl.glRotated(45, 1, 1, 0);

            // Создание сферы (радиус, количество меридиан и параллелей)
            Glut.glutWireSphere(2, 32, 32); 

            // Убрать из стека матрицу 
            Gl.glPopMatrix(); 

            // Ожидание завершения визуализации
            Gl.glFlush(); 
            // Отрисовка в окне
            OGL.Invalidate();
        }
    }
}
