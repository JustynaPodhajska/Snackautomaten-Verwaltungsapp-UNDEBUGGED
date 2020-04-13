using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class MenuVMODEL : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;

            if (parameter?.ToString() != "WindowInfo")
                mw.contentctrl.Children.Clear();

            switch (parameter.ToString())
            {
                case "StandortClick":
                    mw.contentctrl.Children.Add(new ListeStandortCTL());
                    break;
                case "AutomatenClick":
                    mw.contentctrl.Children.Add(new ListeAutomatenCTL());
                    break;
                case "KategorieClick":
                    mw.contentctrl.Children.Add(new ListeKategorienCTL());
                    break;
                case "ProdukteClick":
                    mw.contentctrl.Children.Add(new ListeProdukteCTL());
                    break;
                case "KontrollenClick":
                    mw.contentctrl.Children.Add(new ListeKontrollenCTL());
                    break;
                case "PositionenClick":
                    mw.contentctrl.Children.Add(new ListePositionen());
                    break;
                case "StatistikClick":
                    mw.contentctrl.Children.Add(new AutomatenStatCTL());
                    break;
                case "ExitClick":
                    Application.Current.Shutdown();
                    break;
                default:
                    MessageBox.Show(parameter.ToString() + " - keine Namensübereinstimmung zw. Menüeintrag und Code!");
                    break;
            }
        }
    }
}
