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
using System.Windows.Shapes;

namespace Snackautomaten_Verwaltungsapp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class VendingMachineUC : UserControl
    {
        SnackEmDBEntities db = new SnackEmDBEntities();
        public VendingMachineUC(object o)
        {
            InitializeComponent();
   
               try{  AUTOMATEN autoInput = (AUTOMATEN)o;

                List<POSITION> posList =
            (
            from pos in autoInput.POSITIONs
            orderby pos.POS_A_ID
            select pos 
            ).ToList();

                foreach (POSITION pos in posList)
            {
                addPosButton(pos);
            }
            }
            catch (Exception)
            {
                textblock.Foreground = Brushes.Red;
                textblock.Background = Brushes.Blue;
                textblock.Inlines.Add("Da ist ein Fehler aufgetretten! Kein gültiger Automat ausgewählt!");
            }
           
        }
        public void addPosButton(POSITION posIN)
        {


            
            
            BitmapImage bi3 = new BitmapImage(new Uri("C:/Users/Justyna/source/repos/Snackautomaten Verwaltungsapp/cheetosSnack.png", UriKind.Absolute));
            //img.Stretch = Stretch.UniformToFill;
            Image img = new Image { Source = bi3 };
            img.Width = 25;
            img.Height = 45;
            img.Margin = new Thickness(1, 1, 1, 5);
            textblock.Inlines.Add(img);


            /* photo von datenbank
            Image img = new Image();
            img.Width = 25;
            img.Height = 45;
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(posIN.PRODUKT.P_URI_PHOTO);
            bi3.EndInit();
            //img.Stretch = Stretch.UniformToFill;
            img.Source = bi3;
            img.Margin = new Thickness(1, 1, 1, 1);
            textblock.Inlines.Add(img);
            */



            /*              einfache buttons

                             Button but = new Button();
                             but.Width = 15;
                             but.Opacity = 0.5;
                             but.Width = 25;
                             but.Height = 45;
                             but.Tag = posIN.POS_ID;
                             but.Margin = new Thickness(1, 1, 1, 1);
                             textblock.Inlines.Add(but);
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
