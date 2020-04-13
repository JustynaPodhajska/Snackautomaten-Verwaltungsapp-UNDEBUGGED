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
    public partial class VendingMachineUI : Window
    {
     //   SnackEmDBEntities db = new SnackEmDBEntities();
     //   POSITION sendPOS;
        public VendingMachineUI(object o)
        {
            //InitializeComponent();
            //Liste liauto = (ListeAutomatenCTL)o;
            //AUTOMATEN autoInput = liauto.selAUT;
            ///* List<posID> posList =
            //   (
            //   from pos in autoInput.POSITIONs
            //   orderby pos.POS_A_ID
            //   select new posID
            //   {
            //       ID = pos.POS_ID
            //   }

            //   ).ToList();
            //   */

            //List<POSITION> posList =
            // (
            // from pos in autoInput.POSITIONs
            // orderby pos.POS_A_ID
            // select pos
            // ).ToList();

            //foreach (POSITION pos in posList)
            //{
            //    addPosButton(pos);
            //}
        }
        public void addPosButton(POSITION posIN)
        {
       
            Button but = new Button();
            but.Width = 15;
            but.Opacity = 0.5;
            but.Width = 25;
            but.Height = 45;
            but.Margin = new Thickness(1, 8, 1, 8);
            textblock.Inlines.Add(but);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class posID
    {
        public int ID { get; set; }
    }
}
