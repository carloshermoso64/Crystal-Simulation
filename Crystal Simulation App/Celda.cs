using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Simulation_App
{
    class Celda
    {
        double temperaturaActual;
        double temperaturaFutura;
        double faseActual;
        double faseFutura;

        //Constructores
        public Celda()
        {
            this.temperaturaActual = 0;
            this.faseActual = 0;
        }

        public Celda(double t, double f)
        {
            this.temperaturaActual = t;
            this.faseActual = f;
        }

        //Getters y setters
        public void SetTemperaturaActual(double tA)
        {
            this.temperaturaActual = tA;
        }

        public double GetTemperaturaActual()
        {
            return this.temperaturaActual;
        }

        public void SetTemperaturaFutura(double tF)
        {
            this.temperaturaFutura = tF;
        }

        public double GetTemperaturaFutura()
        {
            return this.temperaturaFutura;
        }

        public void SetFaseActual(double fA)
        {
            this.faseActual = fA;
        }

        public double GetFaseActual()
        {
            return this.faseActual;
        }
        public void SetFaseFutura(double fF)
        {
            this.faseActual = fF;
        }

        public double GetFaseFutura()
        {
            return this.faseFutura;
        }

        public double C

    }
}
