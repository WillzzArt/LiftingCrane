using System;
using Tao.OpenGl;

namespace LiftingCrane.Animation
{
    internal class CargoAnimation
    {
        // экземпляра класса Explosion
        //private Explosion BOOOOM_1 = new Explosion(_translateCargoX, 10, 1, 300, 500);
        private double _translateCargoX, _translateCargoY, _translateCargoZ;
        private float lastTime = 0;

        // вектор гравитации
        private float[] Grav = new float[3];
        // ускорение груза
        private float[] power = new float[3];
        // коэфицент увеличение силы
        private float amplification;

        bool _isBroken = false;

        double _angle, _radiusTralley, _height = 0;

        // набранная скорость
        private float[] speed = new float[3];

        public double GetTranslateCargoX
        {
            get { return _translateCargoX; }
        }

        public double GetTranslateCargoY
        {
            get { return _translateCargoY; }
        }

        public double GetTranslateCargoZ
        {
            get { return _translateCargoZ; }
        }
        public double GetAngle
        {
            get { return _angle; }
        }

        public void StartFalling(double angle, double radiusTralley, double height, float startTime)
        {
            Random rnd = new Random();
            lastTime = startTime;

            speed[0] = 0;
            speed[1] = 0;
            speed[2] = 0;

            Grav[0] = 0;
            Grav[1] = 9.8f;
            Grav[2] = 0;

            amplification = 53.33f;

            float _power_rnd = rnd.Next(46 / 20, 46);

            power[0] = _power_rnd * ((float)rnd.Next(100, 1000) / 1000.0f) * 5;
            power[1] = _power_rnd * ((float)rnd.Next(100, 1000) / 1000.0f) * 5;
            power[2] = _power_rnd * ((float)rnd.Next(100, 1000) / 1000.0f) * 5;

            _angle = angle;
            _radiusTralley = radiusTralley;
            _height = height;

            if (218 - _height * 10 > 100)
            {
                _isBroken = true;
            }

            
            
        }

        public void DrawFallingCargo(float timeNow, out CargoStatus cargoStatus)
        {
            /*_translateCargoX = 15 + (radiusTralley) * Math.Cos(angle * 2 * Math.PI / 360.0);
            _translateCargoY = 90 + (radiusTralley) * Math.Sin(angle * 2 * Math.PI / 360.0);*/

            _translateCargoX = 15;
            _translateCargoY = _radiusTralley + 20;
            _translateCargoZ = 218 - _height * 10;

            UpdatePositionCargo(timeNow, out cargoStatus);

            Gl.glPushMatrix();
            Gl.glTranslated(15, 90, _translateCargoZ);
            Gl.glRotated(_angle, 0, 0, 1);
            Gl.glTranslated(-15, -90, -_translateCargoZ);
            Gl.glTranslated(_translateCargoX, _translateCargoY, _translateCargoZ);
            ModelDrawer.DrawCargo();
            Gl.glPopMatrix();

        }

        private void UpdatePositionCargo(float timeNow, out CargoStatus cargoStatus)
        {
            float dTime = timeNow - lastTime;
            lastTime = timeNow;
            cargoStatus = CargoStatus.Abondoned;

            for (int a = 0; a < 3; a++)
            {
                if (power[a] < 2000)
                {
                    power[a] += amplification * dTime * 100;
                }

                if (_translateCargoZ > 70)
                {
                    _translateCargoZ -= (speed[a] * dTime + (Grav[a] + power[a]) * dTime * dTime);
                }
                else
                {
                    if (_isBroken)
                    {
                        cargoStatus = CargoStatus.Broken;
                    }
                    else
                    {
                        _translateCargoZ = 10;
                        cargoStatus = CargoStatus.Falled;
                    }
                    
                }

                speed[a] += (Grav[a] + power[a]) * dTime;
            }
        }

        /*public void Boom(float globalTime)
        {
            BOOOOM_1.Calculate(globalTime);
        }*/
    }
}
