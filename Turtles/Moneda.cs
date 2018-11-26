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
    public class Moneda
    {
        public Image Imagen { get; set; }

        public Moneda(Image imagen)
        {
            Imagen = imagen;
        }

        public void desaparecer ()
        {
            Imagen.Visibility = Visibility.Collapsed;
        }


    }
}
