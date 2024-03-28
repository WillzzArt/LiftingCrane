using System;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace LiftingCrane
{
    internal static class ModelDrawer
    {
        //Массив содержащий координаты травы
        static double[,] gressCoord = new double[2, 252];
        static Random rnd = new Random();
        static ModelDrawer()
        {
            //Инициализация массива
            for (int i = 0; i < gressCoord.GetLength(0); i++)
            {
                for (int j = 0; j < gressCoord.GetLength(1); j += 3)
                { 
                    var coord = rnd.Next(-192, 192);
                    gressCoord[i, j] = coord;
                    gressCoord[i, j + 1] = coord - rnd.Next(-7, -1);
                    gressCoord[i, j + 2] = coord - rnd.Next(1, 7);
                }
            }
        }

        public static void DrawEarth()
        {

            //Отрисовка земли
            Gl.glPushMatrix();
            Gl.glColor3f(0.12f, 0.97f, 0.13f);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-200, 200, -15);
            Gl.glVertex3d(200, 200, -15);
            Gl.glVertex3d(200, -200, -15);
            Gl.glVertex3d(-200, -200, -15);
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

        //Отрисовка крана
        public static void DrawLiftingCrane(double angle)
        {
            double centerX = 15, centerY = 90, centerZ = -255;

            DrawCraneFundament();

            DrawFootingCrane(156, 2);
            Gl.glPushMatrix();
            Gl.glTranslated(centerX, centerY, centerZ);
            Gl.glRotated(angle, 0, 0, 1);
            Gl.glTranslated(-centerX, -centerY, -centerZ);
            Gl.glPushMatrix();
            DrawCraneBoom(15);
            DrawCraneTown();
            DrawCraneCounterweight();
            DrawCabine();
            Gl.glPopMatrix();
            Gl.glPopMatrix();

            DrawFootingCrane(5, 5);
            DrawCabineFundament();

        }

        //Отрисовка противовеса для крана
        private static void DrawCraneCounterweight()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(0.91f, 0.81f, 0.12f);
            //Gl.glColor3f(0, 0, 0);

            
            //Основа
            Gl.glPushMatrix();
            Gl.glTranslated(15, 155, 230); // z = 213
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
            Gl.glTranslated(-13, 85, 191);
            Gl.glScaled(3.5, 3.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Задняя стена
            Gl.glPushMatrix();
            Gl.glTranslated(-13, 102, 208);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glScaled(3.5, 3.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Боковые стены
            Gl.glPushMatrix();
            Gl.glColor3f(0, 0, 0);
            Gl.glTranslated(-29.5, 85, 199);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScaled(3.2, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(3.5, 85, 199);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScaled(3.2, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Передняя стена
            Gl.glPushMatrix();
            Gl.glTranslated(-13, 68, 199);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glScaled(3.55, 1.5, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Крыша
            Gl.glPushMatrix();
            Gl.glColor3f(0.81f, 0.81f, 0.81f);
            Gl.glTranslated(-13, 85, 226);
            Gl.glScaled(3.6, 3.6, 0.25);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            //Опоры
            Gl.glPushMatrix();
            Gl.glColor3f(0, 0, 0);
            Gl.glTranslated(-29.5, 68, 216);
            Gl.glScaled(0.25, 0.25, 1.9);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(3.5, 68, 216);
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

        

        //Отрисовка пирамиды
        /*private static void DrawPiramide()
        {
            Gl.glPushMatrix();

            // Рисуем усеченную пирамиду
            Gl.glBegin(Gl.GL_QUADS);

            // Нижнее основание
            Gl.glVertex3d(-2, -2, -2);
            Gl.glVertex3d(2, -2, -2);
            Gl.glVertex3d(2, 2, -2);
            Gl.glVertex3d(-2, 2, -2);

            // Верхнее основание
            Gl.glVertex3d(-1, -1, 2);
            Gl.glVertex3d(1, -1, 2);
            Gl.glVertex3d(1, 1, 2);
            Gl.glVertex3d(-1, 1, 2);

            // Боковые грани
            Gl.glVertex3d(-2, -2, -2);
            Gl.glVertex3d(-1, -1, 2);
            Gl.glVertex3d(1, -1, 2);
            Gl.glVertex3d(2, -2, -2);

            Gl.glVertex3d(2, -2, -2);
            Gl.glVertex3d(1, -1, 2);
            Gl.glVertex3d(1, 1, 2);
            Gl.glVertex3d(2, 2, -2);

            Gl.glVertex3d(2, 2, -2);
            Gl.glVertex3d(1, 1, 2);
            Gl.glVertex3d(-1, 1, 2);
            Gl.glVertex3d(-2, 2, -2);

            Gl.glVertex3d(-2, 2, -2);
            Gl.glVertex3d(-1, 1, 2);
            Gl.glVertex3d(-1, -1, 2);
            Gl.glVertex3d(-2, -2, -2);

            Gl.glEnd();
            Gl.glPopMatrix();
        }*/


        
    }
}
