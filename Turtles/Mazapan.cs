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
     public class Mazapan : Tortuga
    {
        public Mazapan(Image imagen) : base(imagen)
        {
            Imagen = imagen;

            arriba.Add(new BitmapImage(new Uri("tortuga3Arriba.png", UriKind.Relative)));
            abajo.Add(new BitmapImage(new Uri("tortuga3.png", UriKind.Relative)));
            Name = "MAZAPAN";
            Cancion = "AFRICA-TOTO";
            Comida = "California Empanizado sin Alga";
        }

    }
}
