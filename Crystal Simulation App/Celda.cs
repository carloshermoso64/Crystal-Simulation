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

        Parametros param;

        //Constructores
        public Celda()
        {
            this.temperaturaActual = -1;
            this.faseActual = 1;
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
            this.faseFutura = fF;
        }

        public double GetFaseFutura()
        {
            return this.faseFutura;
        }

        public void CalcularEstadosFuturos(Parametros param, double faseSuperior, double faseInferior,
            double faseDerecha, double faseIzquierda, double temperaturaSuperior, double temperaturaInferior,
            double temperaturaDerecha, double temperaturaIzquierda)
        {
            this.param = param;
            double M = param.GetM();
            double delta = param.GetM();
            double alpha = param.GetAlpha();
            double deltaX = param.GetDeltaX();
            double deltaY = param.GetDeltaY();
            double deltaT = param.GetDeltaT();
            double epsilon = param.GetEpsilon();

            // Segundas derivadas
            double derivadadosfasex = (faseDerecha - 2 * faseActual + faseIzquierda) / (deltaX * deltaX);
            double derivadadosfasey = (faseSuperior - 2 * faseActual + faseInferior) / (deltaY * deltaY);

            double derivadadostemperaturax = (temperaturaDerecha - 2 * temperaturaActual + temperaturaIzquierda) / (deltaX * deltaX);
            double derivadadostemperaturay = (temperaturaSuperior - 2 * temperaturaActual + temperaturaInferior) / (deltaY * deltaY);

            // Laplaciano

            double laplacianoFase = derivadadosfasex + derivadadosfasey;
            double laplacianoTemperatura = derivadadostemperaturax + derivadadostemperaturay;

            // Derivadas temporales

            double derivadafaset = (1 / (epsilon * epsilon * M)) * (faseActual*(1 - faseActual)*(faseActual - 0.5 + 30*epsilon*alpha*delta*faseActual*(1-faseActual))) + (epsilon * epsilon * laplacianoFase);
            double derivadatemperaturat = laplacianoTemperatura - (1 / delta) * (30 * Math.Pow(faseActual, 2) - 60 * Math.Pow(faseActual,3) + 30 * Math.Pow(faseActual,4))*derivadafaset;

            // Set fase y temperatura futura 

            this.faseFutura = Math.Min(faseActual + (derivadafaset * deltaT), 1);
            this.temperaturaFutura = Math.Max(temperaturaActual + (derivadatemperaturat * deltaT), -1);
        }


    }
}
