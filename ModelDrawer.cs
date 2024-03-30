using System;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace LiftingCrane
{
    internal static class ModelDrawer
    {
        //Массив содержащий координаты травы
        static double[,] gressCoord = new double[2, 432];
        static Random rnd = new Random();

        static uint walls1 = 0;
        static uint walls2 = 0;
        static uint bricks = 0;
        static uint[] cargos = new uint[3];

        static ModelDrawer()
        {
            //Инициализация массива
            for (int i = 0; i < gressCoord.GetLength(0); i++)
            {
                for (int j = 0; j < gressCoord.GetLength(1); j += 3)
                { 
                    var coord = rnd.Next(-452, 452);
                    gressCoord[i, j] = coord;
                    gressCoord[i, j + 1] = coord - rnd.Next(-7, -1);
                    gressCoord[i, j + 2] = coord - rnd.Next(1, 7);
                }
            }

            walls1 = TextureMaker.LoadTexture("fon1.jpg");
            walls2 = TextureMaker.LoadTexture("fon2.jpg");
            cargos[0] = TextureMaker.LoadTexture("bricks.jpg");
            cargos[1] = TextureMaker.LoadTexture("concrete.jpg");
            cargos[2] = TextureMaker.LoadTexture("bricks.jpg");
        }

        private static void DrawTexture()
        {
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(-1, 1, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(-1, -1, 0);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(1, -1, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(1, 1, 0);
            Gl.glTexCoord2f(0, 1);

            Gl.glEnd();
        }

        public static void DrawEarth()
        {

            //Отрисовка земли
            Gl.glPushMatrix();
            Gl.glColor3f(0.12f, 0.97f, 0.13f);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-460, 460, -15);
            Gl.glVertex3d(460, 460, -15);
            Gl.glVertex3d(460, -460, -15);
            Gl.glVertex3d(-460, -460, -15);
            Gl.glEnd();
            Gl.glPopMatrix();

            //Отрисовка травы
            Gl.glPushMatrix();
            Gl.glColor3f(0.09f, 0.65f, 0.1f);

            for (int i = 0; i < gressCoord.GetLength(1); i += 3)
            {
                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glVertex3d(gressCoord[0, i], gressCoord[1, i], -15);
                Gl.glVertex3d(gressCoord[0, i + 1], gressCoord[1, i + 1], -15);
                Gl.glVertex3d(gressCoord[0, i + 2], gressCoord[1, i + 2], rnd.Next(-13, -11));

                Gl.glEnd();
            }

            Gl.glPopMatrix();


        }

        public static void DrawWalls()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            var angle = 0;
            for (int i = 0; i < 5; i++)
            {
                if (i > 1)
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, walls1);
                else
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, walls2);

                Gl.glPushMatrix();
                Gl.glRotated(angle, 0, 0, 1);
                Gl.glPushMatrix();
                Gl.glTranslated(0, -460, 200);
                Gl.glRotated(90, 1, 0, 0);
                Gl.glScaled(460, 220, 0);
                DrawTexture();
                Gl.glPopMatrix();
                Gl.glPopMatrix();

                angle += 90;
            }
            

            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        //Отрисовка крана
        public static void DrawLiftingCrane(double angle, double translateTralley, double cableHeight, bool isTakedCargo)
        {
            double centerX = 15, centerY = 90, centerZ = -255;
            Gl.glPushMatrix();
            DrawCraneFundament();
            DrawCabineFundament();
            DrawFootingCrane(5, 5);

            Gl.glPushMatrix();
            Gl.glTranslated(centerX, centerY, centerZ);
            Gl.glRotated(angle, 0, 0, 1);
            Gl.glTranslated(-centerX, -centerY, -centerZ);
            Gl.glPushMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0, translateTralley, 0);
            DrawMobileTralleyCrane(cableHeight);
            Gl.glPopMatrix();


            DrawFootingCrane(186, 1);
            DrawCraneBoom(15);
            DrawCraneTown();
            DrawCraneCounterweight();
            DrawCabine();

            if (isTakedCargo)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(15, translateTralley + 20, 218 - cableHeight*10);
                DrawCargo();
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
            Gl.glPopMatrix();
            Gl.glPopMatrix();
        }

        //Отрисовка тележки для крюка крана
        private static void DrawMobileTralleyCrane(double cableHeight)
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0, 0, 0);

            Gl.glPushMatrix();
            Gl.glTranslated(15, 20, 228);
            Gl.glScaled(2.45, 4, 0.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(3.5, 20, 229);
            Gl.glScaled(0.15, 4, 0.3);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(26.5, 20, 229);
            Gl.glScaled(0.15, 4, 0.3);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //трос
            Gl.glPushMatrix();
            Gl.glTranslated(15, 20, 228);
            Gl.glRotated(180, 0, 1, 0);
            Gl.glScaled(0.1, 0.1, cableHeight);
            Glut.glutSolidCylinder(10, 10, 32, 32);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка противовеса для крана
        private static void DrawCraneCounterweight()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            
            //Основа
            Gl.glPushMatrix();
            Gl.glTranslated(15, 155, 230);
            Gl.glScaled(2, 11, 0.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Противовес
            Gl.glPushMatrix();
            Gl.glColor3f(0.55f, 0.55f, 0.55f);
            Gl.glTranslated(15, 165, 232);
            Gl.glScaled(1.5, 4.5, 0.4);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //Перила
            //

            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            Gl.glTranslated(5.65, 100.65, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(24.35, 100.65, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(5.65, 209.35, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(24.35, 209.35, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //
            //

            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            Gl.glTranslated(5.65, 136.88, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(24.35, 136.88, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(5.65, 173.11, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(24.35, 173.11, 238);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //Перекладины
            //

            for (int i = 238; i < 246; i += 7)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(5.65, 155, i);
                Gl.glScaled(0.13, 11, 0.13);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(24.35, 155, i);
                Gl.glScaled(0.13, 11, 0.13);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, 100.65, i);
                Gl.glScaled(2, 0.13, 0.13);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, 209.35, i);
                Gl.glScaled(2, 0.13, 0.13);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();
            }
            

            Gl.glPopMatrix();
        }

        //Отрисовка башни крана
        private static void DrawCraneTown()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            //Gl.glColor3f(0, 0, 0);

            Gl.glPushMatrix();
            Gl.glTranslated(15, 90, 280);
            Glut.glutSolidCube(2);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(10.2, 94.97, 255);
            Gl.glRotated(11.7, 0, 1, 0);
            Gl.glRotated(10.1, 1, 0, 0);
            Gl.glScaled(0.1, 0.1, 5.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(10.2, 84.97, 255);
            Gl.glRotated(11.7, 0, 1, 0);
            Gl.glRotated(-10.1, 1, 0, 0);
            Gl.glScaled(0.1, 0.1, 5.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(19.8, 84.97, 255);
            Gl.glRotated(-11.7, 0, 1, 0);
            Gl.glRotated(-10.1, 1, 0, 0);
            Gl.glScaled(0.1, 0.1, 5.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(19.8, 94.97, 255);
            Gl.glRotated(-11.7, 0, 1, 0);
            Gl.glRotated(10.1, 1, 0, 0);
            Gl.glScaled(0.1, 0.1, 5.2);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //
            //

            Gl.glPushMatrix();
            Gl.glTranslated(15, 90, 230);
            Gl.glScaled(2.5, 2.5, 0.15);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //
            //

            Gl.glPushMatrix();
            Gl.glTranslated(15, 84.4, 252);
            Gl.glRotated(-10.1, 1, 0, 0);
            Gl.glScaled(1.1, 0.1, 0.1);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(15, 95.5, 252);
            Gl.glRotated(10.1, 1, 0, 0);
            Gl.glScaled(1.1, 0.1, 0.1);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(9.6, 90, 252);
            Gl.glRotated(11.7, 0, 1, 0);
            Gl.glScaled(0.1, 1.1, 0.1);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(20.4, 90, 252);
            Gl.glRotated(-11.7, 0, 1, 0);
            Gl.glScaled(0.1, 1.1, 0.1);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            Gl.glPushMatrix();
            Gl.glTranslated(15, 83.2, 245);
            Gl.glRotated(-10.1, 1, 0, 0);
            Gl.glScaled(1.4, 0.1, 0.1);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPopMatrix();

        }

        //Отрисовка стрелы крана
        private static void DrawCraneBoom(int count)
        {
            for (double i = 60; i > -20 * count + 81; i -= 20)
            {
                //Нижний квадрат
                Gl.glPushMatrix();
                Gl.glColor3f(0.91f, 0.81f, 0.12f);
                Gl.glTranslated(5, i + 10, 230);
                Gl.glScaled(0.1, 2, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(25, i + 10, 230);
                Gl.glScaled(0.1, 2, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, i, 230);
                Gl.glScaled(2, 0.1, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, i + 20, 230);
                Gl.glScaled(2, 0.1, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                //
                //верхняя балка
                //

                if (i == 60)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(15, i + 15, 245);
                    Gl.glScaled(0.1, 1.6, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }
                else
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(15, i + 15, 245);
                    Gl.glScaled(0.1, 1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }
                

                if (i > -20 * count + 81 + 20)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(15, i + 5, 245);
                    Gl.glScaled(0.1, 1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }
                

                //
                //Опоры
                //

                Gl.glPushMatrix();
                Gl.glTranslated(10, i + 14.5, 237.5);
                Gl.glRotated(35, 0, 1, 0);
                Gl.glRotated(28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(10, i + 5.5, 237.5);
                Gl.glRotated(35, 0, 1, 0);
                Gl.glRotated(-28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(20, i + 14.5, 237.5);
                Gl.glRotated(-35, 0, 1, 0);
                Gl.glRotated(28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(20, i + 5.5, 237.5);
                Gl.glRotated(-35, 0, 1, 0);
                Gl.glRotated(-28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();
            }
            

        }


        //Отрисовка кабины
        private static void DrawCabine()
        {
            //Пол
            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(-13.5, 85, 191);
            Gl.glScaled(3.5, 3.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Задняя стена
            Gl.glPushMatrix();
            Gl.glTranslated(-13.5, 102, 208);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glScaled(3.5, 3.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Боковые стены
            Gl.glPushMatrix();
            Gl.glColor3f(0, 0, 0);
            Gl.glTranslated(-30, 85.2, 199.7);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScaled(3.1, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(3, 85.2, 199.7);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScaled(3.1, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Передняя стена
            Gl.glPushMatrix();
            Gl.glTranslated(-13.5, 68.5, 199.7);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glScaled(3.55, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Крыша
            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(-13.5, 85, 226);
            Gl.glScaled(3.6, 3.6, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Опоры
            Gl.glPushMatrix();
            Gl.glColor3f(0, 0, 0);
            Gl.glTranslated(-30, 68.5, 216);
            Gl.glScaled(0.25, 0.25, 1.9);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(3, 68.5, 216);
            Gl.glScaled(0.25, 0.25, 1.9);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            
        }

        private static void DrawCraneFundament()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(15, 87, -13);
            Gl.glScaled(7, 7, 0.4);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();
        }

        //Отрисовка основания крана
        private static void DrawFootingCrane(int start, int count)
        {
            Gl.glPushMatrix();
            for (var i = start; i < 30 * count + start + 1; i += 30)
            {
                Gl.glPushMatrix();
                Gl.glColor3f(0.91f, 0.81f, 0.12f);

                //Лестница основания крана
                Gl.glPushMatrix();
                Gl.glTranslated(12, 97, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(18, 97, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                for (double j = i - 13; j < i + 14; j += 5)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(15, 97, j);
                    Gl.glScaled(0.7, 0.1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }


                //
                //
                //

                //Ножки основания крана
                Gl.glPushMatrix();
                Gl.glTranslated(5, 80, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(5, 100, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(25, 100, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(25, 80, i);
                Gl.glScaled(0.1, 0.1, 3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                //
                //
                //

                //Перекладины основания крана
                for (int j = i - 13; j < i + 14; j += 13)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(5, 90, j);
                    Gl.glScaled(0.1, 2, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(25, 90, j);
                    Gl.glScaled(0.1, 2, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();


                    Gl.glPushMatrix();
                    Gl.glTranslated(15, 80, j);
                    Gl.glScaled(2, 0.1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(15, 100, j);
                    Gl.glScaled(2, 0.1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();


                }

                for (double j = i - 6.5; j < i + 14; j += 13)
                {
                    Gl.glPushMatrix();
                    Gl.glColor3f(0.91f, 0.81f, 0.12f);
                    Gl.glTranslated(10, 80, j);
                    Gl.glRotated(39, 0, 1, 0);
                    Gl.glScaled(0.1, 0.1, 1.55);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(20, 80, j);
                    Gl.glRotated(-39, 0, 1, 0);
                    Gl.glScaled(0.1, 0.1, 1.55);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(25, 90, j);
                    Gl.glRotated(-32, 1, 0, 0);
                    Gl.glScaled(0.1, 2.3, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(5, 90, j);
                    Gl.glRotated(-32, 1, 0, 0);
                    Gl.glScaled(0.1, 2.3, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();

                    Gl.glPushMatrix();
                    Gl.glTranslated(15, 100, j);
                    Gl.glRotated(-55, 0, 1, 0);
                    Gl.glScaled(0.1, 0.1, 2.3);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }

                //
                //
                //

                Gl.glPopMatrix();
            }
            Gl.glPopMatrix();
        }

        //Отрисовка фундамента кабины
        private static void DrawCabineFundament()
        {
            //фундамент
            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            Gl.glTranslated(-15, 60, 168);
            Gl.glScaled(10, 10, 3);
            CabineBase();
            Gl.glPopMatrix();

            //Перила
            Gl.glPushMatrix();
            Gl.glTranslated(-4.35, 70.55, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(34.35, 70.55, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(34.35, 109.35, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-4.35, 109.35, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //
            //

            Gl.glPushMatrix();
            Gl.glTranslated(-4.35, 89.85, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(34.35, 89.85, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(15, 109.35, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(15, 70.55, 178.3);
            Gl.glScaled(0.13, 0.13, 1.5);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //
            //
            //

            for (var i = 178.3; i < 186; i += 7)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(-4.35, 89.85, i);
                Gl.glRotated(90, 1, 0, 0);
                Gl.glScaled(0.13, 0.13, 4);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(34.35, 89.85, i);
                Gl.glRotated(90, 1, 0, 0);
                Gl.glScaled(0.13, 0.13, 4);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, 109.35, i);
                Gl.glRotated(90, 0, 1, 0);
                Gl.glScaled(0.13, 0.13, 4);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, 70.55, i);
                Gl.glRotated(90, 0, 1, 0);
                Gl.glScaled(0.13, 0.13, 4);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();
            }

        }

        //Отрисовка фундамента для кабины
        private static void CabineBase()
        {
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(1, 1, 0);
            Gl.glVertex3d(1, 5, 0);
            Gl.glVertex3d(2, 5, 0);
            Gl.glVertex3d(2, 1, 0);

            Gl.glVertex3d(2, 5, 0);
            Gl.glVertex3d(5, 5, 0);
            Gl.glVertex3d(5, 4, 0);
            Gl.glVertex3d(2, 4, 0);

            Gl.glVertex3d(5, 4, 0);
            Gl.glVertex3d(5, 1, 0);
            Gl.glVertex3d(4, 1, 0);
            Gl.glVertex3d(4, 4, 0);

            Gl.glVertex3d(4, 1, 0);
            Gl.glVertex3d(2, 1, 0);
            Gl.glVertex3d(2, 2, 0);
            Gl.glVertex3d(4, 2, 0);

            //
            //
            //

            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(1, 5, 1);
            Gl.glVertex3d(2, 5, 1);
            Gl.glVertex3d(2, 1, 1);

            Gl.glVertex3d(2, 5, 1);
            Gl.glVertex3d(5, 5, 1);
            Gl.glVertex3d(5, 4, 1);
            Gl.glVertex3d(2, 4, 1);

            Gl.glVertex3d(5, 4, 1);
            Gl.glVertex3d(5, 1, 1);
            Gl.glVertex3d(4, 1, 1);
            Gl.glVertex3d(4, 4, 1);

            Gl.glVertex3d(4, 1, 1);
            Gl.glVertex3d(2, 1, 1);
            Gl.glVertex3d(2, 2, 1);
            Gl.glVertex3d(4, 2, 1);

            //
            //
            //

            Gl.glVertex3d(1, 1, 0);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(5, 1, 1);
            Gl.glVertex3d(5, 1, 0);

            Gl.glVertex3d(1, 1, 0);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(1, 5, 1);
            Gl.glVertex3d(1, 5, 0);

            Gl.glVertex3d(1, 5, 0);
            Gl.glVertex3d(1, 5, 1);
            Gl.glVertex3d(5, 5, 1);
            Gl.glVertex3d(5, 5, 0);

            Gl.glVertex3d(5, 5, 0);
            Gl.glVertex3d(5, 5, 1);
            Gl.glVertex3d(5, 1, 1);
            Gl.glVertex3d(5, 1, 0);


            Gl.glEnd();
            Gl.glPopMatrix();
        }

        public static void DrawWallsWithFractal()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.46f, 0.44f, 0.40f);
            Gl.glTranslated(100, 30, 12);
            Gl.glRotated(-45, 0, 0, 1);
            Gl.glScaled(7, 0.4, 5.2);
            
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();
        }

        //Отрисовка груза
        public static void DrawCargo()
        {
            Gl.glPushMatrix();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, cargos[1]);
            var angle = 0;
            for (int i = 0; i < 5; i++)
            {
                Gl.glPushMatrix();
                Gl.glRotated(angle, 0, 0, 1);

                Gl.glPushMatrix();
                Gl.glTranslated(0, 10, 0);
                Gl.glRotated(90, 1, 0, 0);
                Gl.glScaled(10, 10, 0);
                DrawTexture();
                Gl.glPopMatrix();
                Gl.glPopMatrix();

                angle += 90;
            }

            for (int i = -10; i < 11; i += 20)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(0, 0, i);
                Gl.glScaled(10, 10, 0);
                DrawTexture();
                Gl.glPopMatrix();

                angle += 90;
            }

            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glPushMatrix();
            Gl.glTranslated(0, -12, -13);
            DrawPallet();
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка поддона
        private static void DrawPallet()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.78f, 0.51f, 0.17f);

            for (int i = 0; i < 25 ; i += 12)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(0, i, 0);
                Gl.glScaled(4, 0.5, 0.3);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

            }

            for (double i = -17.5; i < 18.5; i += 17.5)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(i, 12, 2);
                Gl.glScaled(0.5, 2.9, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();
            }

            for (double i = -0.1; i < 24; i+= 5.925)
            {
                Gl.glPushMatrix();
                Gl.glColor3f(0.81f, 0.54f, 0.2f);
                Gl.glTranslated(0, i, 3);
                Gl.glScaled(4, 0.4, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();
            }
            
            //крепление
            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(0, -0.1, 3);
            Gl.glScaled(0.1, 0.1, 2.2);
            Glut.glutSolidCylinder(10, 10, 32, 32);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(0, 23.8, 3);
            Gl.glScaled(0.1, 0.1, 2.2);
            Glut.glutSolidCylinder(10, 10, 32, 32);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(0, 23, 24);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glScaled(0.1, 0.1, 2.4);
            Glut.glutSolidCylinder(10, 10, 32, 32);
            Gl.glPopMatrix();


            Gl.glPopMatrix();
        }

        public static void DrawBineryTree(double size)
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.97f, 0.75f, 0.13f);
            Gl.glTranslated(98, 28, -10);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-45, 0, 1, 0);
            Gl.glLineWidth(2);
            Gl.glScaled(0.55, 0.55, 1);
            Gl.glBegin(Gl.GL_LINES);
            CalculateBineryTree(0, 0, 20, 0, size);
            Gl.glEnd();
            Gl.glPopMatrix();

            
        }

        private static void CalculateBineryTree(double x, double y, int length, int angle, double size)
        {
            double x1, y1;
            x1 = x + length * Math.Sin(angle * 2 * Math.PI / 360.0);
            y1 = y + length * Math.Cos(angle * 2 * Math.PI / 360.0);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(x1, y1);

            if (length > 2)
            {
                CalculateBineryTree(x1, y1, (int)(length / size), angle + 40, size);
                CalculateBineryTree(x1, y1, (int)(length / size), angle - 40, size);
                CalculateBineryTree(x1, y1, (int)(length / size), angle + 10, size);
            }


        }

    }
}
