using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            this.panel = new Celda[filas, columnas];
            this.param = param;


            int i =0;
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
            //ContornoReflector();
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
            //ContornoReflector();
            Boolean matrizsolida = ComprobarMatrizSolida();
            if (matrizsolida == true)
            {
                MessageBox.Show("La matriz se ha solidificado");
            }
            else
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
                        if (i == 5 && j == 5){
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

        public Boolean ComprobarMatrizSolida()
        {
            int i = 1;
            Boolean matrizsolida = true;
                while (i < filas - 1 && matrizsolida)
            {
                int j = 1;
                while (j < columnas - 1 && matrizsolida)
                {
                    if (this.panel[i,j].GetTemperaturaActual() != 0 && this.panel[i,j].GetFaseActual() != 0)
                    {
                        matrizsolida = false;
                    }
                }
            }
            return matrizsolida;
        }


        public void ContornoReflector()
        {
            int f = 0;
            while (f <= this.filas + 1)
            {
                int c = 0;
                while (c <= this.columnas + 1)
                {
                    //fila superior
                    if (f == 0 && c >= 1 && c <= this.columnas)
                        this.SetCelda(f, c, this.GetCelda(f + 1, c));
                    //fila inferior
                    if (f == this.filas + 1 && c >= 1 && c <= this.columnas)
                        this.SetCelda(f, c, this.GetCelda(f - 1, c));
                    //columna izquierda
                    if (c == 0 && f >= 1 && f <= this.filas)
                        this.SetCelda(f, c, this.GetCelda(f, c + 1));
                    //columna derecha
                    if (c == this.columnas + 1 && f >= 1 && f <= this.filas)
                        this.SetCelda(f, c, this.GetCelda(f, c - 1));
                    //esquina superior izquierda
                    if (c == 0 && f == 0)
                        this.SetCelda(f, c, this.GetCelda(f + 1, c + 1));
                    //esquina inferior izquierda
                    if (c == 0 && f == this.filas + 1)
                        this.SetCelda(f, c, this.GetCelda(f - 1, c + 1));
                    //esquina  superior derecha
                    if (c == this.columnas + 1 && f == 0)
                        this.SetCelda(f, c, this.GetCelda(f + 1, c - 1));
                    //esquina inferior derecha
                    if (c == this.columnas + 1 && f == this.filas + 1)
                        this.SetCelda(f, c, this.GetCelda(f - 1, c - 1));
                    c++;
                }
                f++;
            }
        }

    }
}
