using LiftingCrane.Animation;
using LiftingCrane.Filter;
using System;
using System.Media;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace LiftingCrane
{

    public partial class MainForm : Form
    {
        // вспомогательные переменные - в них будут хранится обработанные значения,
        // полученные при перетаскивании ползунков пользователем
        double _translateX = 0, _translateY = 0, _translateZ = -20, zoom = 1;
        float _globalTime = 0;
        // оси вращения
        double _rotateX = -80, _rotateY = 0, _rotateZ = 0;

        double angle = 0;
        double _translateTralley = 0;
        double _angleCam = 0.0;
        double _camSpeed = 0.0175;
        double _sizeFractal = 1.2;
        double _cableHeight = 1;

        bool _isBoom = false;
        bool _isInCrane = false;
        bool _isCargoRight = false;
        bool _isCargoLeft = false;

        CargoStatus cargoStatus;

        /*bool _isTakedCargo = false;
        bool _isAbondonedCargo = false;
        bool _isFalledCargo = false;*/
        bool _isAboveTheBuilding = false;

        CargoAnimation cargo;

        private Explosion BOOOOM_1 = new Explosion(1, 10, 1, 300, 500);

        private AnEngine ProgrammDrawingEngine;

        SoundPlayer soundPlayer;

        public MainForm()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInCrane)
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
            _isInCrane = true;
            button2.Visible = true;
            button6.Visible = true;
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
            _sizeFractal = (double)trackBar1.Value / 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProgrammDrawingEngine.Filter_4();
        }

        private void button6_Click(object sender, EventArgs e)
        {;
            cargoStatus = CargoStatus.Taked;
            button6.Visible = false;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cargo = new CargoAnimation();
            cargoStatus = CargoStatus.Abondoned;
            button6.Visible = true;
            button7.Visible = false;
            cargo.StartFalling(-angle, _translateTralley, _cableHeight, _globalTime, _isAboveTheBuilding);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isInCrane = false;
            _translateX = 0;
            _translateY = 0;
            _translateZ = -20;
            _rotateZ = 0;
            _rotateY = 0;
            comboBox1.Visible = false;
            button2.Visible = false;
            button6.Visible = false;
            button7.Visible = false;

        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new AboutForm();
            about.ShowDialog();
        }

        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new AboutControlForm();
            about.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cargoStatus = CargoStatus.None;
            button8.Visible = false;
            label16.Visible = false;
            button6.Visible = true;
            _isBoom = false;
        }

        

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            label5.Text = _translateY.ToString();
            label7.Text = _rotateZ.ToString();
            label6.Text = _translateX.ToString();
            label4.Text = _translateZ.ToString();
            label9.Text = _rotateX.ToString();
            label14.Text = angle.ToString();
            label18.Text = _translateTralley.ToString();
            label17.Text = _cableHeight.ToString();


            if (!_isInCrane)
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
                if (angle >= 319 && angle <= 340 && _translateTralley <= -160)
                    _isAboveTheBuilding = true;
                else
                    _isAboveTheBuilding = false;

                if (e.KeyCode == Keys.A)
                {
                    if (!_isAboveTheBuilding && angle == 341 && _cableHeight >= 7 && !_isCargoLeft && _translateTralley <= -170)
                    {
                        _isCargoRight = true;
                        angle = 341;
                    }
                    else
                    {
                        _isCargoRight = false;
                        angle -= 1;
                        _angleCam += _camSpeed;

                        if (comboBox1.SelectedIndex != 2)
                            _rotateZ -= 1;
                    }

                    if (angle < 0) angle = 360;
                    
                }


                if (e.KeyCode == Keys.D)
                {
                    if (!_isAboveTheBuilding && angle == 318 && _cableHeight >= 7 && !_isCargoRight && _translateTralley <= -170)
                    {
                        angle = 318;
                        _isCargoLeft = true;
                    }
                    else
                    {
                        _isCargoLeft = false;
                        angle += 1;
                        _angleCam -= _camSpeed;

                        if (comboBox1.SelectedIndex != 2)
                            _rotateZ += 1;
                    }

                    if (angle > 360) angle = 0;
                }

                if (e.KeyCode == Keys.W)
                {
                    if (_isAboveTheBuilding && _cableHeight >= 7)
                        _translateTralley = -160;
                    else
                    {
                        if (_translateTralley <= -200)
                            _translateTralley = -200;
                        else
                            _translateTralley -= 2;
                    }
                }

                if (e.KeyCode == Keys.S)
                {
                    if (_translateTralley >= 0)
                        _translateTralley = 0;
                    else
                        _translateTralley += 2;
                }

                if (e.KeyCode == Keys.ShiftKey)
                {
                    if (_isAboveTheBuilding && _cableHeight >= 6)
                        _cableHeight = 6;
                    if (!_isAboveTheBuilding && _cableHeight >= 22)
                        _cableHeight = 22;
                    else
                        _cableHeight += 0.5;
                }

                if (e.KeyCode == Keys.Space)
                {
                    if (_cableHeight <= 1)
                        _cableHeight = 1;
                    else
                        _cableHeight -= 0.5;
                }

                if (comboBox1.SelectedIndex == 0)
                {
                    double centerX = -15;
                    double centerY = -90;

                    double radius = 28;

                    _translateX = centerX + radius * Math.Cos(_angleCam);
                    _translateY = centerY + radius * Math.Sin(_angleCam);
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
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.6, 2000);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            ModelDrawer.InitModelDrawer();
            
            cargoStatus = CargoStatus.None;

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

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            // поворот по оси
            Gl.glRotated(_rotateX, 1, 0, 0);
            Gl.glRotated(_rotateY, 0, 1, 0);
            Gl.glRotated(_rotateZ, 0, 0, 1);
            // производим перемещение в зависимости от значений, полученных при перемещении ползунков
            Gl.glTranslated(_translateX, _translateY, _translateZ);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);

            //ProgrammDrawingEngine.SwapImage();

            Gl.glPushMatrix();

            ModelDrawer.DrawEarth();
            ModelDrawer.DrawLiftingCrane(-angle, _translateTralley, _cableHeight, cargoStatus);
            ModelDrawer.DrawWalls();
            ModelDrawer.DrawWallsWithFractal();
            ModelDrawer.DrawBineryTree(_sizeFractal);
            ModelDrawer.DrawSand();
            ModelDrawer.DrawBuilding();

            switch (cargoStatus)
            {
                case CargoStatus.None:
                    {
                        ModelDrawer.DrawCargo();
                        break;
                    }
                case CargoStatus.Abondoned:
                    {
                        cargo.DrawFallingCargo(_globalTime, out cargoStatus);
                        break;
                    }
                case CargoStatus.Falled:
                    {
                        Gl.glPushMatrix();
                        Gl.glTranslated(15, 90, cargo.GetTranslateCargoZ);
                        Gl.glRotated(cargo.GetAngle, 0, 0, 1);
                        Gl.glTranslated(-15, -90, -cargo.GetTranslateCargoZ);
                        Gl.glTranslated(cargo.GetTranslateCargoX, cargo.GetTranslateCargoY, cargo.GetTranslateCargoZ);
                        ModelDrawer.DrawCargo();
                        Gl.glPopMatrix();
                        break;
                    }
                case CargoStatus.Broken:
                    {
                        if (!_isBoom)
                        {
                            Random rnd = new Random();

                            var x = 15 + (-cargo.GetTranslateCargoY) * Math.Sin(cargo.GetAngle * 2 * Math.PI / 360.0);
                            var y = 90 + (-cargo.GetTranslateCargoY) * Math.Cos(cargo.GetAngle * 2 * Math.PI / 360.0);

                            BOOOOM_1.SetNewPosition(x, cargo.GetTranslateCargoZ, y);

                            BOOOOM_1.SetNewPower(rnd.Next(20, 80));
                            BOOOOM_1.Boooom(_globalTime);
                            Gl.glPopMatrix();
                            _isBoom = true;
                            button6.Visible = false;
                            button8.Visible = true;
                            label16.Visible = true;
                            soundPlayer = new SoundPlayer(@"falled.wav");
                            //soundPlayer.Play();
                        }

                        break;
                    }
            }

            BOOOOM_1.Calculate(_globalTime);

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();

        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            _globalTime += (float)RenderTimer.Interval / 1000;
            Draw();
        }
    }
}
