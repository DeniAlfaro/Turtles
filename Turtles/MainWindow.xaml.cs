﻿using System;
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
        int score = 0;
        List<Moneda> monedas = new List<Moneda>();
        int contadormonedas = 0;
       
        enum EstadoJuego {Menu, Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Menu;

        enum Direccion { Arriba, Abajo, Ninguna };
        Direccion direccionTurtle = Direccion.Abajo;

        double velocidadTurtle = 70;

        double brincoPixeles = 50;

        double velocidadSalto = 5;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;


            popotes.Add(new Popotes(popote1));
            popotes.Add(new Popotes(popote2));
            popotes.Add(new Popotes(popote3));
            popotes.Add(new Popotes(popote4));
            popotes.Add(new Popotes(popote5));
            popotes.Add(new Popotes(popote6));
            monedas.Add(new Moneda(Moneda1));
            monedas.Add(new Moneda(Moneda2));
            monedas.Add(new Moneda(Moneda3));

            Pacoyo pacoyo = new Pacoyo(trampa);
            lblPacoyo.Text = pacoyo.Name;
            lblCancionP.Text = pacoyo.Cancion;
            lblComidap.Text = pacoyo.Comida;

            Mazapan mazapan = new Mazapan(trampa);
            lblMazapan.Text = mazapan.Name;
            lblCancionM.Text = mazapan.Cancion;
            lblComidaM.Text = mazapan.Comida;

            Quesadilla quesadilla = new Quesadilla(trampa);
            lblQuesadilla.Text = quesadilla.Name;
            lblCancionQ.Text = quesadilla.Cancion;
            lblComidaQ.Text = quesadilla.Comida;

            

            // 1. establecer instrucciones
            ThreadStart threadStart = new ThreadStart(actualizar);
            // 2. inicializar el Thread
            Thread threadMover = new Thread(threadStart);
            // 3. ejecutar el Thread
            threadMover.Start();

        }

        void moverTurtle(TimeSpan deltaTime)
        {
            if (estadoActual == EstadoJuego.Gameplay)
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
                        }
                        else
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
                     
                        miCanvas.Focus();
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

                        if (score >= 200)
                        {
                            lblNivel1.Visibility = Visibility.Collapsed;
                            lblNivel2.Visibility = Visibility.Visible;
                            imgFondo2.Visibility = Visibility.Collapsed;
                            imgFondo1.Visibility = Visibility.Visible;
                        }

                        if (score >= 350)
                        {
                            lblNivel2.Visibility = Visibility.Collapsed;
                            lblNivel3.Visibility = Visibility.Visible;
                            imgFondo1.Visibility = Visibility.Collapsed;
                            imgFondo3.Visibility = Visibility.Visible;
                        }

                        tiempoAnterior = tiempoActual;
                    }
                });
            
            }
        }

        void MovimientoPopote (TimeSpan deltaTime)
        {
            //AQUI ESTA EL MOVIMIENTO DE LOS POPOTES
            if (estadoActual == EstadoJuego.Gameplay)
            {

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

                double leftMoneda1Actual = Canvas.GetLeft(Moneda1);
                Canvas.SetLeft(Moneda1, leftMoneda1Actual - (100 * deltaTime.TotalSeconds));
                double leftMoneda2Actual = Canvas.GetLeft(Moneda2);
                Canvas.SetLeft(Moneda2, leftMoneda2Actual - (100 * deltaTime.TotalSeconds));
                double leftMoneda3Actual = Canvas.GetLeft(Moneda3);
                Canvas.SetLeft(Moneda3, leftMoneda3Actual - (100 * deltaTime.TotalSeconds));

                foreach (Moneda moneda in monedas)
                {
                    if (Canvas.GetLeft(moneda.Imagen) <= -100)
                    {
                        Canvas.SetLeft(moneda.Imagen, 1000);
                    }
                }


                foreach (Popotes popote in popotes)
                {
                    if (Canvas.GetLeft(popote.Imagen) <= 170 && Canvas.GetLeft(popote.Imagen) >= 169.99)
                    {
                        score = score + 20;
                        lblscore.Text = score.ToString();
                        lblscore2.Text = score.ToString();
                    }

                }

                foreach (Moneda moneda in monedas)
                {
                    double xTurtle = Canvas.GetLeft(imgTurtle);
                    double xMonedas = Canvas.GetLeft(moneda.Imagen);
                    double yTurtle = Canvas.GetTop(imgTurtle);
                    double yMonedas = Canvas.GetTop(moneda.Imagen);

                    if (xMonedas + moneda.Imagen.Width >= xTurtle && xMonedas <= xTurtle + imgTurtle.Width && yMonedas + moneda.Imagen.Height >= yTurtle && yMonedas <= yTurtle + imgTurtle.Height)
                    {
                        moneda.Imagen.Visibility = Visibility.Collapsed;
                        contadormonedas = contadormonedas + 1;
                        lblmonedas.Text = contadormonedas.ToString();
                    }

                    if (xMonedas >= 799)
                    {
                        moneda.Imagen.Visibility = Visibility.Visible;
                    }
                }
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
                    tortuga.CambiarDireccion(Tortuga.Direccion.Arriba);
                    
                }
            }
           
        }

     
        double cantidadSalto = 0;
        private void miCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && direccionTurtle == Direccion.Arriba)
            {
             
                tortuga.CambiarDireccion(Tortuga.Direccion.Abajo);
            }
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            
            canvasStart.Visibility = Visibility.Collapsed;
            canvasChoosePlayer.Visibility = Visibility.Visible;
            estadoActual = EstadoJuego.Menu;
        }

        private void btnPacoyo_Click(object sender, RoutedEventArgs e)
        {
            tortuga = new Pacoyo(imgTurtle);
            canvasChoosePlayer.Visibility = Visibility.Collapsed;
            miCanvas.Visibility = Visibility.Visible;
            estadoActual = EstadoJuego.Gameplay;

        }

        private void btnMazapan_Click(object sender, RoutedEventArgs e)
        {
            tortuga = new Mazapan(imgTurtle);
            canvasChoosePlayer.Visibility = Visibility.Collapsed;
            miCanvas.Visibility = Visibility.Visible;
            estadoActual = EstadoJuego.Gameplay;
        }

        private void btnQuesadilla_Click(object sender, RoutedEventArgs e)
        {
            tortuga = new Quesadilla(imgTurtle);
            canvasChoosePlayer.Visibility = Visibility.Collapsed;
            miCanvas.Visibility = Visibility.Visible;
            estadoActual = EstadoJuego.Gameplay;
        }
       
    }
}
