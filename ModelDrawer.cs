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
        static int height = 0;
        static ModelDrawer()
        {
            //Инициализация массива
            for (int i = 0; i < gressCoord.GetLength(0); i++)
            {
                for (int j = 0; j < gressCoord.GetLength(1); j += 3)
                { 
                    var coord = rnd.Next(-92, 92);
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
            Gl.glVertex3d(-100, 100, -15);
            Gl.glVertex3d(100, 100, -15);
            Gl.glVertex3d(100, -100, -15);
            Gl.glVertex3d(-100, -100, -15);
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
        public static void DrawLiftingCrane()
        {
            DrawCraneFundament();
            DrawFootingCrane(7);
            DrawCraneBoom(15);
            DrawCabine();
            DrawCabineFundament();
        }

        //Отрисовка стрелы крана
        private static void DrawCraneBoom(int count)
        {
            for (double i = 60; i > -20 * count + 81; i -= 20)
            {
                //Нижний квадрат
                Gl.glPushMatrix();
                Gl.glColor3f(0.91f, 0.81f, 0.12f);
                Gl.glTranslated(5, i + 10, 213);
                Gl.glScaled(0.1, 2, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(25, i + 10, 213);
                Gl.glScaled(0.1, 2, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, i, 213);
                Gl.glScaled(2, 0.1, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(15, i + 20, 213);
                Gl.glScaled(2, 0.1, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                //
                //верхняя балка
                //

                Gl.glPushMatrix();
                Gl.glTranslated(15, i + 15, 228);
                Gl.glScaled(0.1, 1, 0.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                if (i > -20 * count + 81 + 20)
                {
                    Gl.glPushMatrix();
                    Gl.glTranslated(15, i + 5, 228);
                    Gl.glScaled(0.1, 1, 0.1);
                    Glut.glutSolidCube(10);
                    Gl.glPopMatrix();
                }
                

                //
                //Опоры
                //

                Gl.glPushMatrix();
                Gl.glTranslated(10, i + 14.5, 220.5);
                Gl.glRotated(35, 0, 1, 0);
                Gl.glRotated(28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(10, i + 5.5, 220.5);
                Gl.glRotated(35, 0, 1, 0);
                Gl.glRotated(-28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(20, i + 14.5, 220.5);
                Gl.glRotated(-35, 0, 1, 0);
                Gl.glRotated(28, 1, 0, 0);
                Gl.glScaled(0.1, 0.1, 2.1);
                Glut.glutSolidCube(10);
                Gl.glPopMatrix();

                Gl.glPushMatrix();
                Gl.glTranslated(20, i + 5.5, 220.5);
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

        private static void DrawFootingCrane(int count)
        {
            for (var i = 5; i < 30 * count + 6; i += 30)
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


            
        }

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
