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
        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;

        enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;

        enum Direccion { Arriba, Abajo, Ninguna };
        Direccion direccionTurtle = Direccion.Abajo;

        double velocidadTurtle = 50;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;
            
            // 1. establecer instrucciones
            ThreadStart threadStart = new ThreadStart(actualizar);
            // 2. inicializar el Thread
            Thread threadMoverEnemigos = new Thread(threadStart);
            // 3. ejecutar el Thread
            threadMoverEnemigos.Start();

        }

        void moverTurtle(TimeSpan deltaTime)
        {
            double topTurtleActual = Canvas.GetTop(imgTurtle);
            switch (direccionTurtle)
            {
                case Direccion.Arriba:
                    Canvas.SetTop(imgTurtle,  topTurtleActual - (velocidadTurtle * deltaTime.TotalSeconds));
                    break;
                case Direccion.Abajo:
                    Canvas.SetTop(imgTurtle, topTurtleActual + (velocidadTurtle * deltaTime.TotalSeconds));
                    break;
                default:
                    break;
            }
        }

        void actualizar()
        {
            while (true)
            {
                Dispatcher.Invoke(
                () =>
                {
                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime = tiempoActual - tiempoAnterior;

                    if (estadoActual == EstadoJuego.Gameplay)
                    {
                        moverTurtle(deltaTime);
                    }

                    tiempoAnterior = tiempoActual;
                });
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
