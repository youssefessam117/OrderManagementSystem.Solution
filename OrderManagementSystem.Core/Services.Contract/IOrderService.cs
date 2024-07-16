using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IOrderService
	{
		Task<bool> ValidateOrderItemsQuantityAsync(Order order);
		Task<Order> CreateOrderAsync(Order order);
		Task<Order?> GetOrderByIdAsync(int orderId);
	}
}
