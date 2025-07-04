using ECommerce.Models;

namespace ECommerce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Notes:
            // All Processes of checkout and shipment is implemented in static order class
            // Managing expires and not expires products,  shippable and unshippable using inheritance
            // using try .. catch to handle all validations
            // Entities: Customer, Product, ExpirableProduct, ShippableProduct, Order, Cart, CartItem
            // Make shippable class implement Ishippable Interface
            // Services: ShippingService contains a method that takes IShippableProduct to calculate shipping fees
            // using LINQ In C#
            // using delegate and events 
            Customer c = new Customer { Id = 1, Name = "Mohamed", Balance = 12000 };
            var products = new List<Product>
            {
                new ExpirableProduct{Id = 1,  Name = "Cheese",  Price = 20, Quantity = 150, ExpireDate = DateTimeOffset.Now.AddDays(20)},
                new ExpirableProduct{Id = 2,  Name = "Pepsi", Price = 7, Quantity = 150, ExpireDate = DateTimeOffset.Now.AddDays(10)},
                new ExpirableProduct{Id = 3,  Name = "Ketchup", Price = 20, Quantity = 5, ExpireDate = DateTimeOffset.Now.AddDays(5)},
                new ShippableProduct{Id = 4,  Name = "Apples", Price = 10, Quantity = 6, Weight = 10},
                new ShippableProduct{Id = 5,  Name = "Redmi", Price = 30, Quantity = 3, Weight = 12}
            };
            var cart = new Cart { Id = 1, CustomerId = c.Id };
            var cartItems = new List<CartItem>
            {
                new CartItem {Id = 1, CartId = cart.Id, ProductId = 2, Quantity = 4},
                new CartItem {Id = 2, CartId = cart.Id, ProductId = 3, Quantity = 2},
                new CartItem {Id = 3, CartId = cart.Id, ProductId = 5, Quantity = 3},
                new CartItem {Id = 4, CartId = cart.Id, ProductId = 4, Quantity = 3},
            };
            //var cartItems = new List<CartItem>();
            Order.products = products.IntersectBy(cartItems.Select(p => p.ProductId), p => p.Id).ToList();

            // using events checkout started if shipment and cart is already done
            Order.Checkout += Order.CheckoutProcess;
            Order.ShipmentProcess(cartItems, c.Balance);
        }        
    }
}
