using OrderManagementSystem.Application.InvoiceService;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IInvoiceService? invoiceService;
		private readonly IProductService productService;

		public OrderService(IUnitOfWork unitOfWork , IInvoiceService invoiceService, IProductService productService)
		{
			this.unitOfWork = unitOfWork;
			this.invoiceService = invoiceService;
			this.productService = productService;
		}
		public OrderService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<bool> ValidateOrderItemsQuantityAsync(Order order)
		{
			if (order != null)
			{
				foreach (var item in order.OrderItems)
				{
					var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);

					if (product?.Stock < item.Quantity)
					{
						return false;
					}

				}
				return true;
			}
			return false;
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{

			// Validate stock
			var isEnoughQuantity = await ValidateOrderItemsQuantityAsync(order);

			if (isEnoughQuantity)
			{
				order.Status = "done";
				// Save order
				unitOfWork.Repository<Order>().Add(order);
				await unitOfWork.CompleteAsync();

				// Update stock
				foreach (var item in order.OrderItems)
				{
					await productService.UpdateStockAsync(item.ProductId, item.Quantity);
				}
				await unitOfWork.CompleteAsync();

				// Generate invoice
				if (invoiceService != null)
					await invoiceService.GenerateInvoiceAsync(order.Id);

				return order;
			}
			order.Status = "not enough";
			return order;
		}

		public async Task<Order?> GetOrderByIdAsync(int orderId)
			=> await unitOfWork.Repository<Order>().GetByIdAsync(orderId);

		public async Task<IReadOnlyList<Order>> GetAllOrders()
			=> await unitOfWork.Repository<Order>().GetAllAsync();


		public async Task UpdateOrderStatusAsync(int orderId, string status)
		{
			var order = await unitOfWork.Repository<Order>().GetByIdAsync(orderId);
			if (order != null)
			{
				order.Status = status;
				unitOfWork.Repository<Order>().Update(order);
				await unitOfWork.CompleteAsync();
			}
			
		}
	}
}
