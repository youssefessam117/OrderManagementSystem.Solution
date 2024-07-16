namespace OrderManagementSystem.Core.Entities
{
	public class Order : BaseEntity
	{
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
		public string PaymentMethod { get; set; }
		public string Status { get; set; }
	}
}