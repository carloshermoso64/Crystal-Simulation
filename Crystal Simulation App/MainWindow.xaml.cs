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
        public double B;
        public double delta;
        public double M;
        public double deltat;



        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void timer_Tick(object sender, EventArgs e)
        {
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
            B = Convert.ToDouble(tb_B.Text.Replace(".", ","));
            M = Convert.ToDouble(tb_M.Text.Replace(".", ","));

            grid = new Rectangle[rows, columns];

            //inicializamos la parte grafica
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = panelGame.ActualWidth / columns - 0.75;
                    r.Height = panelGame.ActualHeight / rows - 0.75;
                    r.Fill = Brushes.Black;
                    r.StrokeThickness = 0.5;
                    r.Stroke = Brushes.White;
                    panelGame.Children.Add(r);
                    Canvas.SetLeft(r, j * panelGame.ActualWidth / columns);
                    Canvas.SetTop(r, i * panelGame.ActualHeight / rows);
                    r.MouseDown += R_MouseDown;

                    grid[i, j] = r;
                }
            }
        }
    }
}
