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


        //Constructores 

        public Matriz()
        {

        }

        public Matriz(int columnas, int filas)
        {
            this.columnas = columnas;
            this.filas = filas;
            this.panel = new Celda[filas + 2, columnas + 2];

            int i = 0;
            while (i < filas + 2)
            {
                int j = 0;
                while (j < columnas + 2)
                {
                    this.panel[i, j] = new Celda(-1, 1); //Celda en estado liquido 
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

        public void ActualizarMatriz()
        {
            int i = 1;
            while(i < filas + 1)
            {
                int j = 1;
                while(j < columnas + 1)
                {
                    // Estado de la Celda a investigar
                    double faseActual = this.panel[i, j].GetFaseActual();
                    double temperaturaActual = this.panel[i, j].GetTemperaturaActual();

                    //Estado de la celda superior
                    double faseSuperior = this.panel[i + 1, j].GetFaseActual();
                    double temperaturaSuperior = this.panel[i + 1, j].GetTemperaturaActual();

                  //  this.panel[i,j].CalcularEstadosFuturos(new Parametros(), faseSuperior,)

                    j++;
                }
                i++;
            }
        }
    }
}
