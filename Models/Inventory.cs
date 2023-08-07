//Akino Kashima
//Coding Sample
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBillCalculator.Models
{
    public class Inventory {
        public static IEnumerable<Product> GetAll()
        {
            yield return new Product("Soda", "Beverage", 1.95);
            yield return new Product("Tea", "Beverage", 1.50);
            yield return new Product("Coffe", "Beverage", 1.25);
            yield return new Product("Mineral Water", "Beverage", 2.95);
            yield return new Product("Juice", "Beverage", 2.50);
            yield return new Product("Milk", "Beverage", 1.50);

            yield return new Product("Buffalo Wings", "Appetizer", 5.95);
            yield return new Product("Buffalo Fingers", "Appetizer", 6.95);
            yield return new Product("Potato Skins", "Appetizer", 8.95);
            yield return new Product("Nachos", "Appetizer", 8.95);
            yield return new Product("Mushroom Caps", "Appetizer", 10.95);
            yield return new Product("Shrimp Cocktail", "Appetizer", 12.95);
            yield return new Product("Chips and Salsa", "Appetizer", 6.95);

            yield return new Product("Seafood Alfredo", "Main Course", 15.95);
            yield return new Product("Chicken Alfredo", "Main Course", 13.95);
            yield return new Product("Chicken Picatta", "Main Course", 13.95);
            yield return new Product("Turkey Club", "Main Course", 11.95);
            yield return new Product("Lobster Pie", "Main Course", 19.95);
            yield return new Product("Prime Rib", "Main Course", 20.95);
            yield return new Product("Shrimp Scampi", "Main Course", 18.95);
            yield return new Product("Turkey Dinner", "Main Course", 13.95);
            yield return new Product("Stuffed Chicken", "Main Course", 14.95);

            yield return new Product("Apple Pie", "Dessert", 5.95);
            yield return new Product("Sundae", "Dessert", 3.95);
            yield return new Product("Carrot Cake", "Dessert", 5.95);
            yield return new Product("Mud Pie", "Dessert", 4.95);
            yield return new Product("Apple Crisp", "Dessert", 5.95);
        }

        //public string? SelectItemValue { get; set; }

        // Retuns one only item selected by Cod of the item.
        public static Product? GetProduct()
        {
            return GetAll().FirstOrDefault<Product>();
        }

        // Returns a list of products
        public static IEnumerable<Product> GetItemsByCategory(string cat)
        {
            return GetAll().Where<Product>(p => p.Category == cat);
        }

    }

}
