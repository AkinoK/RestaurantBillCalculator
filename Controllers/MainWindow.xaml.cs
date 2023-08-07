//Akino Kashima
//Coding Sample

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using RestaurantBillCalculator.Models;
using RestaurantBillCalculator.Views;

namespace RestaurantBillCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _beverages = new ObservableCollection<Product>();
        private ObservableCollection<Product> _appetizer = new ObservableCollection<Product>();
        private ObservableCollection<Product> _maincourse = new ObservableCollection<Product>();
        private ObservableCollection<Product> _dessert = new ObservableCollection<Product>();

        private ObservableCollection<Product> _cart = new ObservableCollection<Product>();

        double _subtotal = 0.0;
        double _tax = 0.0;
        double _total = 0.0;

        public MainWindow()
        {
            InitializeComponent();
            //showProgressBar();

            DockPanel.Visibility = Visibility.Visible;
            StatusBar.Visibility = System.Windows.Visibility.Collapsed;
            StatusBar.Visibility = Visibility.Visible;

            DataContext = this;

            _beverages = new ObservableCollection<Product>(Inventory.GetItemsByCategory("Beverage"));
            _appetizer = new ObservableCollection<Product>(Inventory.GetItemsByCategory("Appetizer"));
            _maincourse = new ObservableCollection<Product>(Inventory.GetItemsByCategory("Main Course"));
            _dessert = new ObservableCollection<Product>(Inventory.GetItemsByCategory("Dessert"));

            _cart = new ObservableCollection<Product>(Inventory.GetAll());

            BeverageCbx.ItemsSource = _beverages;
            AppetizerCbx.ItemsSource = _appetizer;
            MainCourseCbx.ItemsSource = _maincourse;
            DessertCbx.ItemsSource = _dessert;

            subtotal.Content = " Subtotal: " + _subtotal;
            tax.Content = " Tax: " + _tax;
            total.Content = " Total: " + _total;



        }

        private void showProgressBar()
        {
            this.Dispatcher.Invoke((Action)(() => {
                StatusBar.Visibility = Visibility.Visible;
            }));
        }

        // Method invoked when an item is selected in Combobox
        private void BeverageCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _name = BeverageCbx.SelectedItem as Product;
            AddShoppingCart(_name.Name.ToString());
        }
        private void AppetizerCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var _name = AppetizerCbx.SelectedItem as Product;
                AddShoppingCart(_name.Name.ToString());
        }
        private void MainCourseCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var _name = MainCourseCbx.SelectedItem as Product;
                AddShoppingCart(_name.Name.ToString());
        }

        private void DessertCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var _name = DessertCbx.SelectedItem as Product;
                AddShoppingCart(_name.Name.ToString());
        }

        // Add an item to the shopping cart
      
        private ObservableCollection<Product> orderitem = new ObservableCollection<Product>();
        private void AddShoppingCart(string _name)
        {
            double _price = 0.00;
            string _category;

            foreach (Product p in _cart.ToList().Where(p => p.Name == _name))
            {
                _price = p.Price;
                _category = p.Category;
                //_quantity = p.Quantity;

                //ShoppingCartGrid.ItemsSource = orderitem;
                //orderitem.Add(new Product() { Name = _name, Category = _category, Price = _price });
               
                    if(!ShoppingCartGrid.Items.Contains(p))
                    {
                        //ShoppingCartGrid.ItemsSource = orderitem;
                        ShoppingCartGrid.Items.Add(p);
                        orderitem.Add(p);
                        p.Quantity = 1;
                }
                    else 
                    {
                        p.Quantity++;
                    }        
            }

            CalculateBill();
        }

        // Prevent user input into the textbox but only typing numeric input.
        void TypeNumericValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        // prevent pasting of incorrect data 
        void PasteNumericValidation(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String input = (String)e.DataObject.GetData(typeof(String));
                if (new Regex("[^0-9]+").IsMatch(input))
                {
                    e.CancelCommand();
                }
            }
            else e.CancelCommand();
        }

        // Event when the quantity changes manually 
        private void textChangedEventHandler(object sender, RoutedEventArgs e)
        {
            CalculateBill();
        }

        // Calculate the bill
        void CalculateBill()
        {
            double _subtotal = 0.0;
            double _tax = 0.0;
            double _total = 0.0;

            foreach (var item in orderitem)
            {
                _subtotal += item.Price * item.Quantity;
            }
            _tax = _subtotal * 0.13;
            _total = _subtotal + _tax;

            subtotal.Content = " Subtotal: " + Math.Round(_subtotal,2);
            tax.Content = " Tax: " + Math.Round(_tax,2);
            total.Content = " Total: " + Math.Round(_total,2);
        }

        // When clicked Clear button
        void OnClickClear(object sender, RoutedEventArgs e)
        {
            double _subtotal = 0.0;
            double _tax = 0.0;
            double _total = 0.0;

            subtotal.Content = " Subtotal: " + _subtotal;
            tax.Content = " Tax: " + _tax;
            total.Content = " Total: " + _total;

            ShoppingCartGrid.Items.Clear();
            orderitem.Clear();
        }

        // Delete the selected row in the shpping cart with delete key.
        private void ShoppingCartGrid_KeyDown(object sender, KeyEventArgs e)
        {
            var selectedItem = ShoppingCartGrid.SelectedItem;
            Product? product = selectedItem as Product;
            
            if (e.Key == Key.Delete)
            {

                ShoppingCartGrid.Items.Remove(selectedItem);
                orderitem.Remove(product);            
                CalculateBill();
                e.Handled = true;
            }
        }




        //private DataTable DT { get; set; }
        //public DataView GI { get; set; }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    window.Owner = this;
        //}

        private ObservableCollection<Product> invoice = new ObservableCollection<Product>();

        // Get shpooing cart items for invoice
        public void GetShoppingCartItems()
        {
            var selectedItem = ShoppingCartGrid.SelectedItem;
            Product? product = selectedItem as Product;

            foreach (Product p in orderitem)
            {
                if (p != null)
                    invoice.Add(p);
            }


        }

        // Open Invoice view when clicked Generate Invoice button
        void OnClickGenerateInvoice(object sender, RoutedEventArgs e)
        {

            //DataRowView item = ShoppingCartGrid.ChosenItem;
            Invoice invoiceWindow = new Invoice(orderitem);
            invoiceWindow.ShowDialog();
            






            //ShoppingCartGrid.Items.Add(orderitem);
            //DT = new DataTable();
            //DT.Columns.Add("Name", typeof(String));
            //DT.Columns.Add("Category", typeof(String));
            //DT.Columns.Add("Price", typeof(double));
            //DT.Columns.Add("Quantity", typeof(int));




            //GI = DT.DefaultView;
            //DataContext = this;

            //Invoice invoiceWindow = new Invoice();
            //invoiceWindow.DataContext = GI;

            // invoiceWindow.DataContext = this;




        }

        // Open the link when clicked the image
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var ps = new ProcessStartInfo("https://akino-kashima-portfolio.web.app//")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);

           
        }

    }
}
