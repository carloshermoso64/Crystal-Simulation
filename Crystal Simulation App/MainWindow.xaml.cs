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
            Matriz newMatriz = listaMatrices.Last().ActualizarMatriz();
            listaMatrices.Add(newMatriz);

            // Creamos un panel y lo coloreamos en funcion de los valores de temp y fase de cada celda
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double T = newMatriz.GetCelda(i, j).GetTemperaturaActual();
                    double F = newMatriz.GetCelda(i, j).GetFaseActual();

                    if (T > 0) { T = 0; }
                    if (T < -1) { T = -1; }
                    if (F < 0) { F = 0; }
                    if (F > 1) { F = 1; }

                    byte redvalue = Convert.ToByte(Math.Floor(0.5 * (1 - F) * 255 + 0.5 * (1 + T) * 255));

                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, redvalue, 0, 0);

                    grid[i, j].Fill = mySolidColorBrush;
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
            grid = new Rectangle[rows, columns];

            params1 = new Parametros(M, delta, alpha, deltat, deltax, deltay, epsilon);



            //inicializamos la parte grafica
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, 0, 0, 0);

                    Rectangle r = new Rectangle();
                    r.Width = panelGame.ActualWidth / columns - 0.75;
                    r.Height = panelGame.ActualHeight / rows - 0.75;
                    r.Fill = mySolidColorBrush;
                    r.StrokeThickness = 0.5;
                    r.Stroke = Brushes.White;
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
            if (timerenabled == true) { timer.Stop(); }
            else if (timerenabled == false) { timer.Start(); }
        }
    }
}
