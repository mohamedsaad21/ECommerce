namespace ECommerce.Services
{
    public static class ShippingService
    {
        public static double ShipItems(List<IShippingService> products)
        {
            // ASSUMPTION: we will calculate shipping fees based on weight of each product
            if(products != null)
            {
                var ratePerKg = 10;
                var totalWeitgh = products.Sum(p => p.getWeight());
                return ratePerKg * totalWeitgh;
            }
            return 0;
        }
    }
}
