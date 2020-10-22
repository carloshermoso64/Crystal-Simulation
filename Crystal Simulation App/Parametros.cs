using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Simulation_App
{
    class Parametros
    {
        double M;
        double delta;
        double alpha;
        double deltaT;
        double deltaX;
        double deltaY;
        double epsilon;
    

    //Constructores

    public Parametros()
    {
            this.M = 20;
            this.delta = 0.5;
            this.alpha = 400;
            this.deltaT = 0.00001;
            this.deltaX = 0.005;
            this.deltaY = 0.005;
            this.epsilon = 0.005;
    }

        public Parametros(double m, double delta, double alpha, double deltaT, double deltaX, double deltaY, double epsilon)
        {
            M = m;
            this.delta = delta;
            this.alpha = alpha;
            this.deltaT = deltaT;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
            this.epsilon = epsilon;
        }

        //geters y seters

        public void SetM(double M)
        {
            this.M = M;
        }

        public double GetM()
        {
            return this.M;
        }

        public void SetDelta(double delta)
        {
            this.delta = delta;
        }

        public double GetDelta()
        {
            return this.delta;
        }

        public void SetAlpha(double alpha)
        {
            this.alpha = alpha;
        }

        public double GetAlpha()
        {
            return this.alpha;
        }

        public void SetDeltaT(double deltaT)
        {
            this.deltaT = deltaT;
        }

        public double GetDeltaT()
        {
            return this.deltaT;
        }

        public void SetDeltaX(double deltaX)
        {
            this.deltaX = deltaX;
        }

        public double GetDeltaX()
        {
            return this.deltaX;
        }

        public void SetDeltaY(double deltaY)
        {
            this.deltaY = deltaY;
        }

        public double GetDeltaY()
        {
            return deltaY;
        }
        public void SetEpsilon(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public double GetEpsilon()
        {
            return this.epsilon;
        }


    }
}
