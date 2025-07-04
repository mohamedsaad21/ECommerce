using ECommerce.Services;

namespace ECommerce.Models
{
    public static class Order
    {
        public delegate void checkoutHandler(List<CartItem> cart, double Balance);
        public static event checkoutHandler Checkout;
        public static List<Product> products;
        public static List<IShippingService> shippableProducts;
        public static void ShipmentProcess(List<CartItem> cart, double Balance)
        {
            try
            {
                if (cart is null || cart.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }
                double TotalWeight = 0;
                Console.WriteLine("** shipment notice **");
                foreach (var item in cart)
                {
                    var product = products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                    if (product is null) throw new Exception("Product Not Found!!"); 
                    if (item.Quantity > product.Quantity)
                    {
                        throw new Exception($"The Quantity isn't available of {product.Name}");
                    }
                    if (product is ExpirableProduct exProduct && exProduct.IsExpired)
                    {
                        throw new Exception($"{product.Name} is expired!!");
                    }
                    Console.WriteLine($"{item.Quantity}x{product.Name} \t\t{product.Quantity}");
                    TotalWeight += product.Quantity; 
                }
                
                Console.WriteLine($"Total package weight {TotalWeight}kg");
                Console.WriteLine("==================================================");

                if (Checkout != null)
                {
                    Checkout.Invoke(cart, Balance);
                }
            }catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void CheckoutProcess(List<CartItem> cart, double Balance)
        {
            try
            {
                double SubTotal = 0;
                Console.WriteLine("** Checkout receipt **");
                foreach (var item in cart)
                {
                    var product = products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                    Console.WriteLine($"{item.Quantity}x{product.Name} \t\t{product.Price}");
                    SubTotal += product.Price * item.Quantity;
                }
                var shipping = CalculateShippingFees();
                
                Console.WriteLine("==================================================");
                Console.WriteLine($"SubTotal\t\t{SubTotal}");
                if (Balance < SubTotal + shipping)
                {
                    throw new Exception("You balance is insufficient");
                }
                Balance -= SubTotal;
                Console.WriteLine($"Balance\t\t\t{Balance}");
                Console.WriteLine($"Shipping\t\t{shipping}");
                SubTotal += shipping;
                Console.WriteLine($"Amount\t\t\t{SubTotal}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static double CalculateShippingFees()
        {
            // Applying Polymorphism
            var shippableProducts = products.OfType<IShippingService>().ToList();
            return ShippingService.ShipItems(shippableProducts);
        }
    }
}
