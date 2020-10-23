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
                }
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
    }
}
