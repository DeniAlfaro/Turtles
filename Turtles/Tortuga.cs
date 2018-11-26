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

namespace Turtles
{
    public class Tortuga
    {
        Image Imagen { get; set; }
        List<BitmapImage> arriba = new List<BitmapImage>();
        List<BitmapImage> abajo = new List<BitmapImage>();

        public enum Direccion { Izquierda, Derecha, Arriba, Abajo };
        Direccion DireccionActual { get; set; }

        public Tortuga(Image imagen)
        {
            Imagen = imagen;

            arriba.Add(new BitmapImage(new Uri("tortugaArriba.png", UriKind.Relative)));
            abajo.Add(new BitmapImage(new Uri("turtle.png", UriKind.Relative)));
        }

        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            DireccionActual = nuevaDireccion;
            switch (DireccionActual)
            {
                case Direccion.Abajo:
                    Imagen.Source = abajo[0];
                    break;
                case Direccion.Arriba:
                    Imagen.Source = arriba[0];
                    break;
            
            }
        }

    }
}
