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
   public class Pacoyo : Tortuga
    {
        
        public Pacoyo(Image imagen):base (imagen)
        {
            Imagen = imagen;

            arriba.Add(new BitmapImage(new Uri("tortuga2Arriba.png", UriKind.Relative)));
            abajo.Add(new BitmapImage(new Uri("tortuga2.png", UriKind.Relative)));
            Name = "PACOYO";
            Cancion = "TAKI TAKI";
            Comida = "Tacos de la Allende";
        }
    }
}
