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

        Rectangle[,] grid;
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
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(1 / slider_Interval.Value);
            matriz.ActualizarMatriz(param);
            matriz.AvanzarIteracion();
            temperaturaResultadoLabel.Content = matriz.GetCelda(4, 4).GetTemperaturaActual();


            // Creamos un panel y lo coloreamos en funcion de los valores de temp y fase de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = matriz.GetCelda(i, j).GetTemperaturaActual();
                    double F = matriz.GetCelda(i, j).GetFaseActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }
                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    if(i==5 && j == 5)
                    {

                    }

                    ////byte redvalue = Convert.ToByte(Math.Floor(0.5 * (1 - F) * 255 + 0.5 * (1 + T) * 255));
                    //byte redvalue = Convert.ToByte(Math.Floor((1 + T) * 255));
                    //SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    //mySolidColorBrush.Color = Color.FromArgb(redvalue, 255, 0, 0);


                    //grid[i, j].Fill = mySolidColorBrush;

                    TextBlock TB = new TextBlock();
                    TB.Text = matriz.GetCelda(i, j).GetTemperaturaActual().ToString();
                    //The next two magical lines create a special brush that contains a bitmap rendering of the UI element that can then be used like any other brush and its in hardware and is almost the text book example for utilizing all hardware rending performances in WPF unleashed 4.5
                    BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                    grid[i, j].Fill = bcb;
                }
            }

        }

        private void bt_LoadPanel_Click(object sender, RoutedEventArgs e)
        {
            rows = Convert.ToInt32(tb_Rows.Text);
            columns = Convert.ToInt32(tb_Columns.Text);

            deltax = Convert.ToDouble(tb_deltax.Text.Replace(".",","));
            deltay = Convert.ToDouble(tb_deltay.Text.Replace(".", ","));
            deltat = Convert.ToDouble(tb_deltat.Text.Replace(".", ","));
            delta = Convert.ToDouble(tb_delta.Text.Replace(".", ","));
            epsilon = Convert.ToDouble(tb_epsilon.Text.Replace(".", ","));
            alpha = Convert.ToDouble(tb_alpha.Text.Replace(".", ","));
            M = Convert.ToDouble(tb_M.Text.Replace(".", ","));

            param = new Parametros();
            grid = new Rectangle[rows, columns];
            matriz = new Matriz(columns, rows, param);
            celdasolida = new Celda(0, 0, param);
            matriz.SetCelda(5, 5, celdasolida);

            params1 = new Parametros(M, delta, alpha, deltat, deltax, deltay, epsilon);


            //inicializamos la parte grafica
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);

                    Rectangle r = new Rectangle();
                    r.Width = panelGame.ActualWidth / columns - 0.75;
                    r.Height = panelGame.ActualHeight / rows - 0.75;
                    r.Fill = mySolidColorBrush;
                    r.StrokeThickness = 0.5;
                    r.Stroke = Brushes.Black;

                    panelGame.Children.Add(r);
                    Canvas.SetLeft(r, j * panelGame.ActualWidth / columns);
                    Canvas.SetTop(r, i * panelGame.ActualHeight / rows);
                    r.MouseDown += R_MouseDown;

                    grid[i, j] = r;
                }
            }

            // Creamos un objeto matriz inicial:
            Matriz matriz1 = new Matriz(rows, columns, params1);
            listaMatrices.Add(matriz1);

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
            matriz.ActualizarMatriz(param);
            matriz.AvanzarIteracion();
            temperaturaResultadoLabel.Content = matriz.GetCelda(4, 4).GetTemperaturaActual();


            // Creamos un panel y lo coloreamos en funcion de los valores de temp y fase de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = matriz.GetCelda(i, j).GetTemperaturaActual();
                    double F = matriz.GetCelda(i, j).GetFaseActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }
                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    if (i == 5 && j == 5)
                    {

                    }

                    //byte redvalue = Convert.ToByte(Math.Floor(0.5 * (1 - F) * 255 + 0.5 * (1 + T) * 255));

                    byte redvalue = Convert.ToByte(Math.Floor((1 + T) * 255));

                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(redvalue, 255, 0, 0);


                    grid[i, j].Fill = mySolidColorBrush;

                    TextBlock TB = new TextBlock();
                    TB.Text = Math.Round(matriz.GetCelda(i, j).GetTemperaturaActual(), 2).ToString();
                    //The next two magical lines create a special brush that contains a bitmap rendering of the UI element that can then be used like any other brush and its in hardware and is almost the text book example for utilizing all hardware rending performances in WPF unleashed 4.5
                    BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
                    grid[i, j].Fill = bcb;
                }
            }
        }
    }
}
