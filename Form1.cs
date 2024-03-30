﻿using LiftingCrane.Filter;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.DevIl;
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
        double _rotateX = -80, _rotateY = 0, _rotateZ = 0;

        double angle = 0;
        double translateTralley = 0;
        bool isInCrane = false;
        double angleCam = 0.0;
        double camSpeed = 0.0175;
        double sizeFractal = 1.2;
        double cableHeight = 1;
        bool isTakedCargo = false;

        private AnEngine ProgrammDrawingEngine;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInCrane)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            _translateX = 14;
                            _translateY = -86;
                            _translateZ = -218;
                            _rotateX = -74;
                            _rotateZ = angle - 180;
                            break;
                        }
                    case 1:
                        {
                            _translateX = -16;
                            _translateY = -90;
                            _translateZ = -288;
                            _rotateX = -58;
                            _rotateZ = angle - 180;
                            break;
                        }
                    case 2:
                        {
                            _translateX = 96;
                            _translateY = -222;
                            _translateZ = -288;
                            _rotateX = -62;
                            _rotateZ = 150;
                            break;
                        }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isInCrane = true;
            button2.Visible = true;
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            button1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            trackBar1.Visible = true;
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            trackBar1.Visible = false;
            button4.Visible = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            sizeFractal = (double)trackBar1.Value / 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.Filter_4();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            isTakedCargo = true;
            button6.Visible = false;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            isTakedCargo = false;
            button6.Visible = true;
            button7.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isInCrane = false;
            _translateX = 0;
            _translateY = 0;
            _translateZ = -20;
            _rotateZ = 0;
            _rotateY = 0;
            comboBox1.Visible = false;
            button2.Visible = false;

        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            label5.Text = _translateY.ToString();
            label7.Text = _rotateZ.ToString();
            label6.Text = _translateX.ToString();
            label4.Text = _translateZ.ToString();
            label9.Text = _rotateX.ToString();
            label14.Text = angle.ToString();
            

            if (!isInCrane)
            {
                button1.Visible = false;

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

                if (_translateX < -3 && _translateX > -27 && _translateY < -78 && _translateY > -102)
                    button1.Visible = true;
                else
                    button1.Visible = false;
            }
            else
            {
                if (e.KeyCode == Keys.A)
                {
                    angle -= 1;
                    angleCam += camSpeed;

                    if (comboBox1.SelectedIndex != 2)
                        _rotateZ -= 1;
                }


                if (e.KeyCode == Keys.D)
                {
                    angle += 1;
                    angleCam -= camSpeed;

                    if (comboBox1.SelectedIndex != 2)
                        _rotateZ += 1;
                }

                if (e.KeyCode == Keys.W)
                {
                    if (translateTralley <= -200)
                        translateTralley = -200;
                    else
                        translateTralley -= 2;
                }

                if (e.KeyCode == Keys.S)
                {
                    if (translateTralley >= 22)
                        translateTralley = 22;
                    else
                        translateTralley += 2;
                }

                if (e.KeyCode == Keys.ShiftKey)
                {
                    if (cableHeight >= 22)
                        cableHeight = 22;
                    else
                        cableHeight += 0.5;
                }

                if (e.KeyCode == Keys.Space)
                {
                    if (cableHeight <= 1)
                        cableHeight = 1;
                    else
                        cableHeight -= 0.5;
                } 

                if (comboBox1.SelectedIndex == 0)
                {
                    double centerX = -15;
                    double centerY = -90;

                    double radius = 28;

                    _translateX = centerX + radius * Math.Cos(angleCam);
                    _translateY = centerY + radius * Math.Sin(angleCam);
                }

                if (comboBox1.SelectedIndex == 2)
                {
                    if (e.KeyCode == Keys.E)
                        _rotateZ += 2;

                    if (e.KeyCode == Keys.Q)
                        _rotateZ -= 2;
                }
            }

            if (e.KeyCode == Keys.Z)
                _rotateX += 2;

            if (e.KeyCode == Keys.X)
                _rotateX -= 2;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация бибилиотеки glut
            Glut.glutInit();
            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // инициализация библиотеки OpenIL
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            // очистка матрицы
            Gl.glLoadIdentity();

            // установка перспективы
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.6, 1000);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            ProgrammDrawingEngine = new AnEngine(AnT.Width, AnT.Height, AnT.Width, AnT.Height);

            RenderTimer.Start();
        }



        private void Draw()
        {
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы
            Gl.glLoadIdentity();
            ProgrammDrawingEngine.SwapImage();
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
            ModelDrawer.DrawLiftingCrane(-angle, translateTralley, cableHeight, isTakedCargo);
            ModelDrawer.DrawWalls();
            ModelDrawer.DrawWallsWithFractal();
            ModelDrawer.DrawBineryTree(sizeFractal);

            if (!isTakedCargo)
            {
                ModelDrawer.DrawCargo();
            }
            



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
