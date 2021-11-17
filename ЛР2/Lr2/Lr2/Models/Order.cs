namespace Lr2.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}