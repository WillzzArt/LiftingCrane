﻿using System;
using Tao.OpenGl;

namespace LiftingCrane.SurfaceOfRotation
{
    internal class SurfaceRotation
    {
        private float a = -0.5f, b = 5;
        private int n = 25;

        private double[,] GeometricArray = new double[64, 3];
        private double[,,] ResaultGeometric = new double[64, 64, 3];

        private int count_elements = 25;

        private double Angle = 2 * Math.PI / 64;
        private int Iter = 64;

        public SurfaceRotation(int c, int d, int e, int f)
        {
            // количество элементов последовательности геометрии, на основе которых будет строится тело вращения
            count_elements = 8;

            // непосредственное заполнение точек
            // после изменения данной геометрии мы сразу получим новое тело вращения

            Function(c, d, e, f);

            // построение геометрии тела вращения
            // принцип сводится к двум циклам - на основе первого перебираются
            // вершины в геометрической последовательности
            // второй использует параметр Iter - производит поворот последней линии геометрии вокруг центра тела вращения
            // при этом используется заранее определенный угол angle, который определяется как 2*Pi / количество меридиан объекта
            // за счет выполнения этого алгоритма получается набор вершин, описывающих оболочку тела врещения
            // остается только соединить эти точки в режиме рисования примитивов для получения
            // визуализированного объекта

            // цикл по последовательности точек кривой, на основе которой будет построено тело вращения
            for (int ax = 0; ax < count_elements; ax++)
            {

                // цикл по меридианам объекта, заранее определенным в программе
                for (int bx = 0; bx < Iter; bx++)
                {

                    // для всех (bx > 0) элементов алгоритма используются предыдушая построенная последовательность
                    // для ее поворота на установленный угол
                    if (bx > 0)
                    {

                        double new_x = ResaultGeometric[ax, bx - 1, 0] * Math.Cos(Angle) - ResaultGeometric[ax, bx - 1, 1] * Math.Sin(Angle);
                        double new_y = ResaultGeometric[ax, bx - 1, 0] * Math.Sin(Angle) + ResaultGeometric[ax, bx - 1, 1] * Math.Cos(Angle);
                        ResaultGeometric[ax, bx, 0] = new_x;
                        ResaultGeometric[ax, bx, 1] = new_y;
                        ResaultGeometric[ax, bx, 2] = GeometricArray[ax, 2];

                    }
                    else // для построения первого меридиана мы используем начальную кривую, описывая ее нулевым значением угла поворота
                    {

                        double new_x = GeometricArray[ax, 0] * Math.Cos(0) - GeometricArray[ax, 1] * Math.Sin(0);
                        double new_y = GeometricArray[ax, 1] * Math.Sin(0) + GeometricArray[ax, 1] * Math.Cos(0);
                        ResaultGeometric[ax, bx, 0] = new_x;
                        ResaultGeometric[ax, bx, 1] = new_y;
                        ResaultGeometric[ax, bx, 2] = GeometricArray[ax, 2];

                    }

                }

            }
        }

        private void Function(int c, int d, int e, int f)
        {
            var step = a;
            var h = (b - a) / n;

            for (int i = 0; i <= n; i++)
            {
                GeometricArray[i, 0] = step;
                GeometricArray[i, 1] = Math.Pow(step, 3) * c + Math.Pow(step, 2) * d + step * e + f; //Math.Pow(2, 3) * step + Math.Pow(3, 2) * step + step * 4 + 5; //Math.Pow(step + 2, 2 / 3) - Math.Pow(step - 2, 2);
                GeometricArray[i, 2] = step;

                step = a + i * h;
            }
        }

        public void DrawSurfaceWithPoint()
        {
            // режим вывода геометрии - точки
            Gl.glBegin(Gl.GL_POINTS);

            // выводим всю ранее просчитанную геометрию объекта
            for (int ax = 0; ax < count_elements; ax++)
            {

                for (int bx = 0; bx < Iter; bx++)
                {

                    // отрисовка точки
                    Gl.glVertex3d(ResaultGeometric[ax, bx, 0], ResaultGeometric[ax, bx, 1], ResaultGeometric[ax, bx, 2]);

                }

            }
            // завершаем режим рисования
            Gl.glEnd();
        }

        public void DrawSurfaceWithLine()
        {
            // устанавливаем режим отрисвки линиями (последовательность линий)
            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (int ax = 0; ax < count_elements; ax++)
            {

                for (int bx = 0; bx < Iter; bx++)
                {


                    Gl.glVertex3d(ResaultGeometric[ax, bx, 0], ResaultGeometric[ax, bx, 1], ResaultGeometric[ax, bx, 2]);
                    Gl.glVertex3d(ResaultGeometric[ax + 1, bx, 0], ResaultGeometric[ax + 1, bx, 1], ResaultGeometric[ax + 1, bx, 2]);

                    if (bx + 1 < Iter - 1)
                    {

                        Gl.glVertex3d(ResaultGeometric[ax + 1, bx + 1, 0], ResaultGeometric[ax + 1, bx + 1, 1], ResaultGeometric[ax + 1, bx + 1, 2]);

                    }
                    else
                    {

                        Gl.glVertex3d(ResaultGeometric[ax + 1, 0, 0], ResaultGeometric[ax + 1, 0, 1], ResaultGeometric[ax + 1, 0, 2]);

                    }

                }

            }
            Gl.glEnd();
        }

        public void DrawWithQuads()
        {
            Gl.glBegin(Gl.GL_QUADS); // режим отрисовки полигонов, состоящих из 4 вершин
            for (int ax = 0; ax < count_elements; ax++)
            {

                for (int bx = 0; bx < Iter; bx++)
                {

                    // вспомогательные переменные для более наглядного использования кода при расчете нормалей
                    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, z1 = 0, z2 = 0, z3 = 0, z4 = 0;

                    // первая вершина
                    x1 = ResaultGeometric[ax, bx, 0];
                    y1 = ResaultGeometric[ax, bx, 1];
                    z1 = ResaultGeometric[ax, bx, 2];

                    if (ax + 1 < count_elements) // если текущий ax не последний
                    {

                        // берем следующую точку последовательности
                        x2 = ResaultGeometric[ax + 1, bx, 0];
                        y2 = ResaultGeometric[ax + 1, bx, 1];
                        z2 = ResaultGeometric[ax + 1, bx, 2];

                        if (bx + 1 < Iter - 1) // если текущий bx не последний
                        {

                            // берем следующую точку последовательности и следующий меридиан
                            x3 = ResaultGeometric[ax + 1, bx + 1, 0];
                            y3 = ResaultGeometric[ax + 1, bx + 1, 1];
                            z3 = ResaultGeometric[ax + 1, bx + 1, 2];

                            // точка, соотвествующуя по номеру только на соседнем меридиане
                            x4 = ResaultGeometric[ax, bx + 1, 0];
                            y4 = ResaultGeometric[ax, bx + 1, 1];
                            z4 = ResaultGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            // если это последний меридиан, то в качестве следующего мы берем начальный (замыкаем геометрию фигуры)
                            x3 = ResaultGeometric[ax + 1, 0, 0];
                            y3 = ResaultGeometric[ax + 1, 0, 1];
                            z3 = ResaultGeometric[ax + 1, 0, 2];

                            x4 = ResaultGeometric[ax, 0, 0];
                            y4 = ResaultGeometric[ax, 0, 1];
                            z4 = ResaultGeometric[ax, 0, 2];

                        }

                    }
                    else // данный элемент ax последний, следовательно мы будем использовать начальный (нулевой) вместо данного ax
                    {

                        // слудуещей точкой будет нулевая ax
                        x2 = ResaultGeometric[0, bx, 0];
                        y2 = ResaultGeometric[0, bx, 1];
                        z2 = ResaultGeometric[0, bx, 2];


                        if (bx + 1 < Iter - 1)
                        {

                            x3 = ResaultGeometric[0, bx + 1, 0];
                            y3 = ResaultGeometric[0, bx + 1, 1];
                            z3 = ResaultGeometric[0, bx + 1, 2];

                            x4 = ResaultGeometric[ax, bx + 1, 0];
                            y4 = ResaultGeometric[ax, bx + 1, 1];
                            z4 = ResaultGeometric[ax, bx + 1, 2];

                        }
                        else
                        {

                            x3 = ResaultGeometric[0, 0, 0];
                            y3 = ResaultGeometric[0, 0, 1];
                            z3 = ResaultGeometric[0, 0, 2];

                            x4 = ResaultGeometric[ax, 0, 0];
                            y4 = ResaultGeometric[ax, 0, 1];
                            z4 = ResaultGeometric[ax, 0, 2];

                        }

                    }


                    // переменные для расчета нормали
                    double n1 = 0, n2 = 0, n3 = 0;

                    // нормаль будем расчитывать как векторное произведение граней полигона
                    // для нулевого элемента нормаль мы будем считать немного по-другому

                    // на самом деле разница в расчете нормали актуальна только для последнего и первого полигона на меридиане

                    if (ax == 0) // при расчете нормали для ax мы будем использовать точки 1,2,3
                    {

                        n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                        n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                        n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    }
                    else // для остальных - 1,3,4
                    {

                        n1 = (y4 - y3) * (z1 - z3) - (y1 - y3) * (z4 - z3);
                        n2 = (z4 - z3) * (x1 - x3) - (z1 - z3) * (x4 - x3);
                        n3 = (x4 - x3) * (y1 - y3) - (x1 - x3) * (y4 - y3);

                    }


                    // если не включен режим GL_NORMILIZE, то мы должны в обязательном порядке
                    // произвести нормализацию вектора нормали, перед тем как передать информацию о нормали
                    double n5 = (double)Math.Sqrt(n1 * n1 + n2 * n2 + n3 * n3);
                    n1 /= (n5 + 0.01);
                    n2 /= (n5 + 0.01);
                    n3 /= (n5 + 0.01);

                    // передаем информацию о нормали
                    Gl.glNormal3d(-n1, -n2, -n3);

                    // передаем 4 вершины для отрисовки полигона
                    Gl.glVertex3d(x1, y1, z1);
                    Gl.glVertex3d(x2, y2, z2);
                    Gl.glVertex3d(x3, y3, z3);
                    Gl.glVertex3d(x4, y4, z4);

                }

            }

            // завершаем выбранный режим рисования полигонов
            Gl.glEnd();
        }
    }
}
