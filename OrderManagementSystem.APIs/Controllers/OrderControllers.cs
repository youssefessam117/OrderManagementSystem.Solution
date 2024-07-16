using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Application.OrderService;
using OrderManagementSystem.Core.Entities;

namespace OrderManagementSystem.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderControllers : ControllerBase
	{
		private readonly OrderService orderService;

		public OrderControllers(OrderService orderService)
		{
			this.orderService = orderService;
		}

		[HttpPost("orders")]
		public async Task<ActionResult<Order>> CreateOrder(Order order)
		{
			var newOrder = await orderService.CreateOrderAsync(order);

			return Ok(newOrder);
		}

		[HttpGet("{id}")]
		public ActionResult<Order> GetOrder(int id)
		{
			var order = orderService.GetOrderByIdAsync(id);

			if (order == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(order);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult<Order> GetOrders()
		{
			var orders = orderService.GetAllOrders();

			return Ok(orders);
		}

		[HttpPut("/{orderId}/status")]
		[Authorize(Roles = "Admin")]
		public ActionResult<Order> UpdateOrderStatus(int orderId, string status)
		{
			var updatedOrder = orderService.UpdateOrderStatusAsync(orderId,status);

			return Ok(updatedOrder);
		}

	}
}
