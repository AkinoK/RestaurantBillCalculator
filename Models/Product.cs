//Akino Kashima
//Coding Sample

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBillCalculator.Models
{
    public class Product : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        private int _quantity;

        public event PropertyChangedEventHandler? PropertyChanged;


        public Product(string name, string category, double prince)
        {
            this.Name = name;
            this.Category = category;
            this.Price = prince;
        }

        public Product()
        {

        }

        private void NotifyPropertyChanged(int qty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set 
            { 
                _quantity = value;
                NotifyPropertyChanged(_quantity);
            }
        }
    }
}
