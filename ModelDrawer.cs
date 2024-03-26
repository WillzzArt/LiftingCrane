using System;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace LiftingCrane
{
    internal static class ModelDrawer
    {
        static double[,] coordTravi = new double[2, 252];
        static Random rnd = new Random();

        static ModelDrawer()
        {
            for (int i = 0; i < coordTravi.GetLength(0); i++)
            {
                for (int j = 0; j < coordTravi.GetLength(1); j += 3)
                { 
                    var coord = rnd.Next(-92, 92);
                    coordTravi[i, j] = coord;
                    coordTravi[i, j + 1] = coord - rnd.Next(-7, -1);
                    coordTravi[i, j + 2] = coord - rnd.Next(1, 7);
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

            for (int i = 0; i < coordTravi.GetLength(1); i += 3)
            {
                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glVertex3d(coordTravi[0, i], coordTravi[1, i], -15);
                Gl.glVertex3d(coordTravi[0, i + 1], coordTravi[1, i + 1], -15);
                Gl.glVertex3d(coordTravi[0, i + 2], coordTravi[1, i + 2], rnd.Next(-13, -11));

                Gl.glEnd();
            }

            Gl.glPopMatrix();


        }

        //Отрисовка крана
        public static void DrawLiftingCrane()
        {
            for (var i = 0; i < 151; i+=30)
            {
                Gl.glPushMatrix();
                Gl.glColor3f(0.91f, 0.81f, 0.12f);

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
    }
}
