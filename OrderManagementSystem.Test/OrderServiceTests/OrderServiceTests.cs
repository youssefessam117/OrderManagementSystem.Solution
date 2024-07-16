using FluentAssertions;
using Moq;
using OrderManagementSystem.Application.OrderService;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Test.OrderServiceTests
{
	public class OrderServiceTests
	{

		private readonly Mock<IUnitOfWork> unitofworkMock;
		private readonly IOrderService orderservice;
		public OrderServiceTests()
		{
			unitofworkMock = new Mock<IUnitOfWork>();
			orderservice = new OrderService(unitofworkMock.Object);
		}


		[Fact]
		public async Task CreateOrder_ShouldValidateOrderSuccessfully()
		{
			// Arrange
			var order = new Order
			{
				CustomerId = 1,
				OrderItems = new List<OrderItem>
			{
				new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 10 }
			}
			};

			unitofworkMock.Setup(u => u.Repository<Order>().Add(It.IsAny<Order>()));
			unitofworkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

			// Act
			var result = await orderservice.CreateOrderAsync(order);

			// Assert
			result.Should().Be(order);
			unitofworkMock.Verify(u => u.Repository<Order>().Add(It.IsAny<Order>()), Times.Once);
			unitofworkMock.Verify(u => u.CompleteAsync(), Times.Once);
		}
	}
}
