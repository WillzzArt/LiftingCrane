using System;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace LiftingCrane
{
    public partial class Form1 : Form
    {
        // вспомогательные переменные - в них будут хранится обработанные значения,
        // полученные при перетаскивании ползунков пользователем
        double _translateX = 0, _translateY = 0, _translateZ = -20, zoom = 1;

        // оси вращения
        int _rotateX = -80, _rotateY = 0, _rotateZ = 0;

        

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                _translateY -= 2;

            if (e.KeyCode == Keys.S)
                _translateY += 2;

            if (e.KeyCode == Keys.A)
                _translateX += 2;

            if (e.KeyCode == Keys.D)
                _translateX -= 2;

            if (e.KeyCode == Keys.Space)
                _translateZ -= 2;

            if (e.KeyCode == Keys.ShiftKey)
                _translateZ += 2;

            if (e.KeyCode == Keys.E)
                _rotateZ += 2;

            if (e.KeyCode == Keys.Q)
                _rotateZ -= 2;

            if (e.KeyCode == Keys.Z)
                _rotateX += 2;

            if (e.KeyCode == Keys.X)
                _rotateX -= 2;

            if (_translateX < -3 && _translateX > -27 && _translateY < -78 && _translateY > -102)
                button1.Visible = true;
            else
                button1.Visible = false;
        }

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация бибилиотеки glut
            Glut.glutInit();
            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы
            Gl.glLoadIdentity();

            // установка перспективы
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();


            RenderTimer.Start();
        }



        private void Draw()
        {
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы
            Gl.glLoadIdentity();

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            //Gl.glTranslated(_transleteX, _transleteY, _transleteZ);
            // поворот по установленной оси
            Gl.glRotated(_rotateX, 1, 0, 0);
            Gl.glRotated(_rotateY, 0, 1, 0);
            Gl.glRotated(_rotateZ, 0, 0, 1);
            // производим перемещение в зависимости от значений, полученных при перемещении ползунков
            Gl.glTranslated(_translateX, _translateY, _translateZ);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);

            Gl.glPushMatrix();

            ModelDrawer.DrawEarth();
            ModelDrawer.DrawLiftingCrane();


            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();

        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
