using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Timers;
using System.Xaml.Schema;

namespace Crystal_Simulation_App
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int rows;
        public int columns;

        Rectangle[,] grid_temp;
        Rectangle[,] grid_phase;
        Matriz matriz;
        Parametros param;
        Celda celdasolida;

        public double deltax;
        public double deltay;
        public double epsilon;
        public double alpha;
        public double delta;
        public double M;
        public double deltat;

        DispatcherTimer timer;

        bool timerenabled = false;
        List<Matriz> listaMatrices = new List<Matriz>();
        Parametros params1;



        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            bt_StartSimulation.Visibility = Visibility.Hidden;
            bt_PauseSimulation.Visibility = Visibility.Hidden;
            bt_NextStep.Visibility = Visibility.Hidden;
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //leemos la celda clicada
            Rectangle reg = (Rectangle)sender; // Obtenemos el rectangulo sobre el que se clicó
            Point p = (Point)reg.Tag; // recuperamos el tag, que contiene las coordenadas de la casilla clicada

            //posicion en el panel
            int fil = Convert.ToInt32(p.X + 1);
            int col = Convert.ToInt32(p.Y + 1);

            lb_phasevalue.Content = matriz.GetCelda(rows,col).GetFaseActual().ToString();
            lb_tempvalue.Content = matriz.GetCelda(rows, col).GetFaseActual().ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Actualizamos la velocidad de la simulación
            timer.Interval = TimeSpan.FromSeconds(1 / slider_Interval.Value);

            // Actualizamos la matriz, calculamos la nueva iteración
            matriz.ActualizarMatriz(param);
            matriz.AvanzarIteracion();

            int centralcell_i = Convert.ToInt32(Math.Floor(Convert.ToDouble(rows) / 2));
            int centralcell_j = Convert.ToInt32(Math.Floor(Convert.ToDouble(columns) / 2));


            lb_tempvalue.Content = matriz.GetCelda(centralcell_i, centralcell_j - 1).GetTemperaturaActual();
            lb_phasevalue.Content = matriz.GetCelda(centralcell_i, centralcell_j - 1).GetFaseActual();




            // Creamos un panel y lo coloreamos en funcion de los valores de TEMPERATURA de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = matriz.GetCelda(i, j).GetTemperaturaActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }

                    if (rb_Color.IsChecked==true)
                    {
                        byte redvalue = Convert.ToByte(Math.Floor((1 + T) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(redvalue, 255, 0, 0);
                        grid_temp[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetTemperaturaActual(),2).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_temp[i, j].Fill = bcb;
                    }
                }
            }

            // Creamos un panel y lo coloreamos en funcion de los valores de FASE de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double F = matriz.GetCelda(i, j).GetFaseActual();

                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    if (rb_Color.IsChecked == true)
                    {
                        byte bluevalue = Convert.ToByte(Math.Floor((1 - F) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(bluevalue, 0, 0, 255);
                        grid_phase[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetFaseActual(),2).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_phase[i, j].Fill = bcb;
                    }
                }
            }
        }

        private void bt_LoadPanel_Click(object sender, RoutedEventArgs e)
        {
            //Leemos los valores introducidos por el usuario
            rows = Convert.ToInt32(tb_Rows.Text);
            columns = Convert.ToInt32(tb_Columns.Text);

            deltax = Convert.ToDouble(tb_deltax.Text.Replace(".",","));
            deltay = Convert.ToDouble(tb_deltay.Text.Replace(".", ","));
            deltat = Convert.ToDouble(tb_deltat.Text.Replace(".", ","));
            delta = Convert.ToDouble(tb_delta.Text.Replace(".", ","));
            epsilon = Convert.ToDouble(tb_epsilon.Text.Replace(".", ","));
            alpha = Convert.ToDouble(tb_alpha.Text.Replace(".", ","));
            M = Convert.ToDouble(tb_M.Text.Replace(".", ","));

            // Cremoas Grids, matriz inicial, etc...
            grid_temp = new Rectangle[rows, columns];
            grid_phase = new Rectangle[rows, columns];
            matriz = new Matriz(columns, rows, param);
            celdasolida = new Celda(0, 0, param);

            int centralcell_i = Convert.ToInt32(Math.Floor(Convert.ToDouble(rows) / 2));
            int centralcell_j = Convert.ToInt32(Math.Floor(Convert.ToDouble(columns) / 2));
            matriz.SetCelda( centralcell_i, centralcell_j, celdasolida);

            //Guardamos los parametros introducidos por el usuario en un objeto
            param = new Parametros(M, delta, alpha, deltat, deltax, deltay, epsilon);


            //inicializamos la parte grafica

            // 1. Grafica Temp
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);

                    Rectangle r = new Rectangle();
                    r.Width = panelGame_Temp.ActualWidth / columns - 0.75;
                    r.Height = panelGame_Temp.ActualHeight / rows - 0.75;
                    r.Fill = mySolidColorBrush;
                    r.StrokeThickness = 0.5;
                    r.Stroke = Brushes.Black;

                    panelGame_Temp.Children.Add(r);
                    Canvas.SetLeft(r, j * panelGame_Temp.ActualWidth / columns);
                    Canvas.SetTop(r, i * panelGame_Temp.ActualHeight / rows);
                    r.MouseDown += R_MouseDown;

                    grid_temp[i, j] = r;
                }
            }

            // 2. Grafica Phase
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);

                    Rectangle r = new Rectangle();
                    r.Width = panelGame_Phase.ActualWidth / columns - 0.75;
                    r.Height = panelGame_Phase.ActualHeight / rows - 0.75;
                    r.Fill = mySolidColorBrush;
                    r.StrokeThickness = 0.5;
                    r.Stroke = Brushes.Black;

                    panelGame_Phase.Children.Add(r);
                    Canvas.SetLeft(r, j * panelGame_Phase.ActualWidth / columns);
                    Canvas.SetTop(r, i * panelGame_Phase.ActualHeight / rows);
                    r.MouseDown += R_MouseDown;

                    grid_phase[i, j] = r;
                }
            }



            // Luego coloreamos Matrices

            // Creamos un panel y lo coloreamos en funcion de los valores de TEMPERATURA de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = matriz.GetCelda(i, j).GetTemperaturaActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }

                    if (rb_Color.IsChecked == true)
                    {
                        byte redvalue = Convert.ToByte(Math.Floor((1 + T) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(redvalue, 255, 0, 0);
                        grid_temp[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetTemperaturaActual(), 2).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_temp[i, j].Fill = bcb;
                    }
                }
            }

            // Creamos un panel y lo coloreamos en funcion de los valores de FASE de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double F = matriz.GetCelda(i, j).GetFaseActual();

                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    if (rb_Color.IsChecked == true)
                    {
                        byte bluevalue = Convert.ToByte(Math.Floor((1 - F) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(bluevalue, 0, 0, 255);
                        grid_phase[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetFaseActual(), 2).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_phase[i, j].Fill = bcb;
                    }
                }
            }

            bt_StartSimulation.Visibility = Visibility.Visible;
            bt_PauseSimulation.Visibility = Visibility.Visible;
            bt_NextStep.Visibility = Visibility.Visible;


        }

        private void bt_StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            timerenabled = true;
        }

        private void bt_PauseSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (timerenabled == true) { timer.Stop(); timerenabled = false; }
            else if (timerenabled == false) { timer.Start(); timerenabled = true; }
        }

        private void bt_NextStep_Click(object sender, RoutedEventArgs e)
        {
            // Actualizamos la matriz, calculamos la nueva iteración
            matriz.ActualizarMatriz(param);
            matriz.AvanzarIteracion();
            int centralcell_i = Convert.ToInt32(Math.Floor(Convert.ToDouble(rows) / 2));
            int centralcell_j = Convert.ToInt32(Math.Floor(Convert.ToDouble(columns) / 2));


            lb_tempvalue.Content = matriz.GetCelda(centralcell_i, centralcell_j - 1).GetTemperaturaActual();
            lb_phasevalue.Content = matriz.GetCelda(centralcell_i, centralcell_j - 1).GetFaseActual();



            // Creamos un panel y lo coloreamos en funcion de los valores de TEMPERATURA de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = matriz.GetCelda(i, j).GetTemperaturaActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }

                    if (rb_Color.IsChecked == true)
                    {
                        byte redvalue = Convert.ToByte(Math.Floor((1 + T) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(redvalue, 255, 0, 0);
                        grid_temp[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetTemperaturaActual(), 3).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_temp[i, j].Fill = bcb;
                    }
                }
            }

            // Creamos un panel y lo coloreamos en funcion de los valores de FASE de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double F = matriz.GetCelda(i, j).GetFaseActual();

                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    if (rb_Color.IsChecked == true)
                    {
                        byte bluevalue = Convert.ToByte(Math.Floor((1 - F) * 255));
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(bluevalue, 0, 0, 255);
                        grid_phase[i, j].Fill = mySolidColorBrush;
                    }
                    else
                    {
                        TextBlock TB = new TextBlock();
                        TB.Text = Math.Round(matriz.GetCelda(i, j).GetFaseActual(), 3).ToString();
                        BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                        grid_phase[i, j].Fill = bcb;
                    }
                }
            }
        }

        private void bt_ParameterSet1_Click(object sender, RoutedEventArgs e)
        {
            tb_deltax.Text = 0.005.ToString();
            tb_deltay.Text = 0.005.ToString();
            tb_deltat.Text = 0.000005.ToString();
            tb_delta.Text = 0.5.ToString();
            tb_epsilon.Text = 0.005.ToString();
            tb_alpha.Text = 400.ToString();
            tb_M.Text = 20.ToString();
        }

        private void bt_ParameterSet2_Click(object sender, RoutedEventArgs e)
        {
            tb_deltax.Text = 0.005.ToString();
            tb_deltay.Text = 0.005.ToString();
            tb_deltat.Text = 0.000005.ToString();
            tb_delta.Text = 0.7.ToString();
            tb_epsilon.Text = 0.005.ToString();
            tb_alpha.Text = 300.ToString();
            tb_M.Text = 30.ToString();
        }
    }
}
