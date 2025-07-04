using ECommerce.Services;
namespace ECommerce.Models
{
    public class ShippableProduct : Product, IShippingService
    {
        public double Weight { get; set; }
        public string getName() => this.Name;
        public double getWeight() => this.Weight;
    }
}
