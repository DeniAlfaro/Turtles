using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace Turtles
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;*/

        enum Direccion { Arriba, Abajo, Ninguna };
        Direccion direccionTurtle = Direccion.Abajo;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();
        }

        void moverTurtle(TimeSpan deltaTime)
        {
            double topTurtleActual = Canvas.GetTop(imgTurtle);
            switch (direccionTurtle)
            {
                case Direccion.Arriba:
                    Canvas.SetTop(imgTurtle, topTurtleActual - 20);
                    break;
                case Direccion.Abajo:
                    Canvas.SetTop(imgTurtle, topTurtleActual + 20);
                    break;
                default:
                    break;
            }
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                direccionTurtle = Direccion.Arriba;
            }
        }

        private void miCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && direccionTurtle == Direccion.Arriba)
            {
                direccionTurtle = Direccion.Abajo;
            }
        }
    }
}
