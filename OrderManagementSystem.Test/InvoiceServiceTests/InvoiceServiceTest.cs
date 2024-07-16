using FluentAssertions;
using Moq;
using OrderManagementSystem.Application.InvoiceService;
using OrderManagementSystem.Application.OrderService;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Test.InvoiceServiceTests
{
	public class InvoiceServiceTest
	{
		private readonly Mock<IUnitOfWork> unitOfWorkMock;
		private readonly IInvoiceService _invoiceService;
		public InvoiceServiceTest()
		{
			unitOfWorkMock = new Mock<IUnitOfWork>();
			_invoiceService = new InvoiceService(unitOfWorkMock.Object);
		}

		[Fact]
		public async Task check10PercentDiscount()
		{
			// Arrange
			var order = new Order
			{
				Id = 1,
				TotalAmount = 250m
			};
			var invoice = new Invoice
			{
				OrderId = 1,
				TotalAmount = 250m
			};

			unitOfWorkMock.Setup(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);

			// Act
			var result = await _invoiceService.ApplyTieredDiscounts(invoice);

			// Assert
			result.TotalAmount.Should().Be(225m); // 250 - 10%
			unitOfWorkMock.Verify(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>()), Times.Once);
		}

		[Fact]
		public async Task check5PercentDiscount()
		{
			// Arrange
			var order = new Order
			{
				Id = 1,
				TotalAmount = 150m
			};
			var invoice = new Invoice
			{
				OrderId = 1,
				TotalAmount = 150m
			};

			unitOfWorkMock.Setup(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);

			// Act
			var result = await _invoiceService.ApplyTieredDiscounts(invoice);

			// Assert
			result.TotalAmount.Should().Be(142.5m); // 150 - 5%
			unitOfWorkMock.Verify(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>()), Times.Once);
		}

		[Fact]
		public async Task ApplyTieredDiscounts_ShouldNotApplyDiscount()
		{
			// Arrange
			var order = new Order
			{
				Id = 1,
				TotalAmount = 100m
			};
			var invoice = new Invoice
			{
				OrderId = 1,
				TotalAmount = 100m
			};

			unitOfWorkMock.Setup(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>())).ReturnsAsync(order);

			// Act
			var result = await _invoiceService.ApplyTieredDiscounts(invoice);

			// Assert
			result.TotalAmount.Should().Be(100m); // No discount
			unitOfWorkMock.Verify(u => u.Repository<Order>().GetByIdAsync(It.IsAny<int>()), Times.Once);
		}
	}
}
