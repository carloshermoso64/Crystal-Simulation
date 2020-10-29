using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Simulation_App
{
    class Matriz
    {
        Celda[,] panel;
        int columnas;
        int filas;

        Parametros param;


        //Constructores 

        public Matriz()
        {

        }

        public Matriz(int columnas, int filas, Parametros param)
        {
            this.columnas = columnas;
            this.filas = filas;
            this.panel = new Celda[filas + 2, columnas + 2];
            this.param = param;


            int i = 0;
            while (i < filas)
            {
                int j = 0;
                while (j < columnas)
                {
                    this.panel[i, j] = new Celda(-1, 1, param); //Celda en estado liquido 
                    j++;
                }
                i++;
            }
        }

        // Getters y setters

        public void SetColumnas(int col)
        {
            this.columnas = col;
        }

        public int GetColumnas()
        {
            return this.columnas;
        }

        public void SetFilas(int filas)
        {
            this.filas = filas;
        }

        public int GetFilas()
        {
            return this.filas;
        }

        public void SetCelda(int fila, int columna, Celda celda)
        {
            this.panel[fila, columna] = celda;
        }

        public Celda GetCelda(int fila, int columna)
        {
            return this.panel[fila, columna];
        }

        public void ActualizarMatriz(Parametros param)
        {
            int i = 1;
            while (i < filas - 1)
            {
                int j = 1;
                while (j < columnas - 1)
                {
                    // Estado de la Celda a investigar
                    double faseActual = this.panel[i, j].GetFaseActual();
                    double temperaturaActual = this.panel[i, j].GetTemperaturaActual();

                    //Estado de la celda superior
                    double faseSuperior = this.panel[i - 1, j].GetFaseActual();
                    double temperaturaSuperior = this.panel[i - 1, j].GetTemperaturaActual();

                    //Estado de la celda inferior
                    double faseInferior = this.panel[i + 1 , j].GetFaseActual();
                    double temperaturaInferior = this.panel[i + 1, j].GetTemperaturaActual();

                    //Estado de la celda izquierda
                    double faseIzquierda = this.panel[i , j - 1].GetFaseActual();
                    double temperaturaIzquierda = this.panel[i , j - 1].GetTemperaturaActual();

                    //Estado de la celda derecha
                    double faseDerecha = this.panel[i, j + 1].GetFaseActual();
                    double temperaturaDerecha = this.panel[i, j + 1].GetTemperaturaActual();

                    //Calcular Estados futuros
                    if (i == 5 && j == 4){
                        int stop = 1;
                    }
                    if (i == 6 && j == 4)
                    {
                        int stop = 1;
                    }

                    this.panel[i, j].CalcularEstadosFuturos(param, faseSuperior, faseInferior, faseDerecha, faseIzquierda, temperaturaSuperior, temperaturaInferior, temperaturaDerecha, temperaturaIzquierda);

                    j++;
                }
                i++;
            }
        }
        public void AvanzarIteracion()
        {
            int i = 1;
            while (i < filas - 1)
            {
                int j = 1;
                while (j < columnas - 1)
                {
                    this.panel[i, j].SetFaseActual(this.panel[i, j].GetFaseFutura());
                    this.panel[i, j].SetTemperaturaActual(this.panel[i, j].GetTemperaturaFutura());
                    j++;
                }
                i++;
            }
            
        }

    }
}
