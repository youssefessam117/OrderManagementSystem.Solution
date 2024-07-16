﻿namespace OrderManagementSystem.Core.Entities
{
	public class OrderItem : BaseEntity
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount { get; set; }
	}
}