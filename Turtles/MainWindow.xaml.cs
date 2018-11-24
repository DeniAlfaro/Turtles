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

        Tortuga tortuga;
        List<Popotes> popotes = new List<Popotes>();
       
        enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;

        enum Direccion { Arriba, Abajo, Ninguna };
        Direccion direccionTurtle = Direccion.Abajo;

        double velocidadTurtle = 100;

        double brincoPixeles = 100;

        double velocidadSalto = 7;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            tortuga = new Tortuga(imgTurtle);
            popotes.Add(new Popotes(popote1));
            popotes.Add(new Popotes(popote2));
            popotes.Add(new Popotes(popote3));
            popotes.Add(new Popotes(popote4));
            popotes.Add(new Popotes(popote5));
            popotes.Add(new Popotes(popote6));

            // 1. establecer instrucciones
            ThreadStart threadStart = new ThreadStart(actualizar);
            // 2. inicializar el Thread
            Thread threadMover = new Thread(threadStart);
            // 3. ejecutar el Thread
            threadMover.Start();

        }

        void moverTurtle(TimeSpan deltaTime)
        {
            double topTurtleActual = Canvas.GetTop(imgTurtle);
            switch (direccionTurtle)
            {

                case Direccion.Arriba: 
                    double cantidadMovimiento = ((brincoPixeles * deltaTime.TotalSeconds) * velocidadSalto);
                    cantidadSalto += cantidadMovimiento;
                    if (cantidadSalto <= brincoPixeles)
                    {
                        Canvas.SetTop(imgTurtle, topTurtleActual - cantidadMovimiento);
                    } else
                    {
                        direccionTurtle = Direccion.Abajo;
                    }
                    break;
                case Direccion.Abajo:
                    double nuevaPosicion = topTurtleActual + (velocidadTurtle * deltaTime.TotalSeconds);
                    if (nuevaPosicion + imgTurtle.Width <= 450)
                    {
                        Canvas.SetTop(imgTurtle, nuevaPosicion);
                    }
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
                        MovimientoPopote(deltaTime);

                        //colisiones
                        foreach (Popotes popote in popotes)
                        {
                            double xTurtle = Canvas.GetLeft(imgTurtle);
                            double xPopotes = Canvas.GetLeft(popote.Imagen);
                            double yTurtle = Canvas.GetTop(imgTurtle);
                            double yPopotes = Canvas.GetTop(popote.Imagen);

                            if (xPopotes + popote.Imagen.Width >= xTurtle && xPopotes <= xTurtle + imgTurtle.Width &&
                                yPopotes + popote.Imagen.Height >= yTurtle && yPopotes <= yTurtle + imgTurtle.Height)
                            {
                                estadoActual = EstadoJuego.Gameover;
                                miCanvas.Visibility = Visibility.Collapsed;
                                canvasGameOver.Visibility = Visibility.Visible;
                            }
                        }

                        tiempoAnterior = tiempoActual;
                    }
                });
            }
        }

        void MovimientoPopote (TimeSpan deltaTime)
        {
            //AQUI ESTA EL MOVIMIENTO DE LOS POPOTES

            double leftPopote1Actual = Canvas.GetLeft(popote1);
            Canvas.SetLeft(popote1, leftPopote1Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote1) <= -100)
            {
                Canvas.SetLeft(popote1, 800);
            }

            double leftPopote2Actual = Canvas.GetLeft(popote2);
            Canvas.SetLeft(popote2, leftPopote2Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote2) <= -100)
            {
                Canvas.SetLeft(popote2, 800);
            }

            double leftPopote3Actual = Canvas.GetLeft(popote3);
            Canvas.SetLeft(popote3, leftPopote3Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote3) <= -100)
            {
                Canvas.SetLeft(popote3, 800);
            }

            double leftPopote4Actual = Canvas.GetLeft(popote4);
            Canvas.SetLeft(popote4, leftPopote4Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote4) <= -100)
            {
                Canvas.SetLeft(popote4, 800);
            }

            double leftPopote5Actual = Canvas.GetLeft(popote5);
            Canvas.SetLeft(popote5, leftPopote5Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote5) <= -100)
            {
                Canvas.SetLeft(popote5, 800);
            }

            double leftPopote6Actual = Canvas.GetLeft(popote6);
            Canvas.SetLeft(popote6, leftPopote6Actual - (100 * deltaTime.TotalSeconds));
            if (Canvas.GetLeft(popote6) <= -100)
            {
                Canvas.SetLeft(popote6, 800);
            }
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (direccionTurtle != Direccion.Arriba)
                {
                    direccionTurtle = Direccion.Arriba;
                    cantidadSalto = 0;
                }
            }
        }

        double cantidadSalto = 0;
        private void miCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Space && direccionTurtle == Direccion.Arriba)
            {
                direccionTurtle = Direccion.Abajo;

            }*/
        }
    }
}
