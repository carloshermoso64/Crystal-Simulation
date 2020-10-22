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
        public void setTemperaturaActual(double tA)
        {
            this.temperaturaActual = tA;
        }

        public double getTemperaturaActual()
        {
            return this.temperaturaActual;
        }

        public void setTemperaturaFutura(double tF)
        {
            this.temperaturaFutura = tF;
        }

        public double getTemperaturaFutura()
        {
            return this.temperaturaFutura;
        }

        public void setFaseActual(double fA)
        {
            this.faseActual = fA;
        }

        public double getFaseActual()
        {
            return this.faseActual;
        }
        public void setFaseFutura(double fF)
        {
            this.faseActual = fF;
        }

        public double getFaseFutura()
        {
            return this.faseFutura;
        }

    }
}
