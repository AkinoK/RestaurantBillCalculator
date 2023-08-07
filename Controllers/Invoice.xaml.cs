//Akino Kashima
//Coding Sample

using RestaurantBillCalculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace RestaurantBillCalculator.Views
{
    /// <summary>
    /// Invoice.xaml の相互作用ロジック
    /// </summary>
    public partial class Invoice : Window
    {
       

        public Invoice()
        {
            InitializeComponent();

            this.DataContext = new MainWindow();

        }

        public Invoice(ObservableCollection<Product> orderitem ) : this()
        {
            String ticket = "";
            double _subtotal = 0.0;
            double _tax = 0.0;
            double _total = 0.0;

            foreach (Product o in orderitem)
            {
                ticket += o.Name + " - " + o.Category + " - $" + o.Price + " - " + o.Quantity + "\n";
                _subtotal += o.Price * o.Quantity;

            }

            _tax = _subtotal * 0.13;
            _total = _subtotal + _tax;

            ticket += " \n\n\n\n\nSubtotal: " + Math.Round(_subtotal, 2) +"\n";
            ticket += " Tax: " + Math.Round(_tax, 2) + "\n";
            ticket += " Total: " + Math.Round(_total, 2) + "\n";


            this.name.Content = ticket;
    
        }



        //private ObservableCollection<Product> invoice = new ObservableCollection<Product>();

        //Product selectedItem = ShoppingCartGrid.SelectedItem;
        //Product? product = selectedItem as Product;






    }
}
