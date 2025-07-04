namespace ECommerce.Models
{
    public class ExpirableProduct : Product
    {
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow > ExpireDate;
    }
}
