﻿namespace LiftingCrane.Animation
{
    internal class Partilce
    {
        // позиция частицы
        private double[] position = new double[3];
        // размер
        private float _size;

        //угол поворота;
        private double _angle;

        // время жизни
        private double _lifeTime;

        // вектор гравитации
        private float[] Grav = new float[3];
        // ускорение частицы
        private double[] power = new double[3];
        // коэфицент затухания силы
        private double attenuation;

        // набранная скорость
        private double[] speed = new double[3];

        // временной интервал активации частицы
        private float LastTime = 0;

        // конструктор класса
        public Partilce(double x, double y, double z, float size, float lifeTime, float start_time)
        {
            // записываем все начальные настройки частицы, устанавливаем начальный коэфицент затухания
            // и обнуляем скорость и силу, приложенную к частице
            _size = size;
            _lifeTime = lifeTime;

            position[0] = x;
            position[1] = y;
            position[1] = z;

            speed[0] = 0;
            speed[1] = 0;
            speed[2] = 0;

            Grav[0] = 0;
            Grav[1] = -9.8f;
            Grav[2] = 0;

            attenuation = 3.33f;

            power[0] = 0;
            power[0] = 0;
            power[0] = 0;

            LastTime = start_time;

        }

        // функция установка ускорения, действующего на частицу
        public void SetPower(float x, float y, float z)
        {
            power[0] = x;
            power[1] = y;
            power[2] = z;
        }

        // инвертирование скорости частицы по заданной оси с указанным затуханием
        // удобно использовать для простой демонстрации столкновений, например с землей
        public void InvertSpeed(int os, float attenuation)
        {
            speed[os] *= -1 * attenuation;
        }

        // получение размера частицы
        public float GetSize()
        {
            return _size;
        }

        public double GetAngle()
        {
            return _angle;
        }

        // установка нового значения затухания
        public void setAttenuation(double new_value)
        {
            attenuation = new_value;
        }

        // обновление позиции частицы
        public void UpdatePosition(float timeNow)
        {
            // орпределяем разницу во времени, прошедшую с последнего обновления
            // позиции частицы (ведь таймер может быть не фиксированный)
            double dTime = timeNow - LastTime;
            _lifeTime -= dTime;

            // обновляем последнюю отметку временного интервала
            LastTime = timeNow;

            // перерасчитываем ускорение, движущее частицу, с учетом затухания
            for (int a = 0; a < 3; a++)
            {
                if (power[a] > 0)
                {
                    power[a] -= attenuation * dTime;

                    if (power[a] <= 0)
                        power[a] = 0;
                }

                // перерасчитываем позицию частицы с учетом гравитации, вектора ускорения и прощедшего промежутка времени
                position[a] += (speed[a] * dTime + (Grav[a] + power[a]) * dTime * dTime);
                _angle += 5;
                // обновляем скорость частицы
                speed[a] += (Grav[a] + power[a]) * dTime;
            }
        }

        // проверка, не закончилось ли время жизни частицы
        public bool isLife()
        {
            if (_lifeTime > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // получение координат частицы
        public double GetPositionX()
        {
            return position[0];
        }
        public double GetPositionY()
        {
            return position[1];
        }
        public double GetPositionZ()
        {
            return position[2];
        }

    }
}
