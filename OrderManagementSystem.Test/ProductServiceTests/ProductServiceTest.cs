using Moq;
using OrderManagementSystem.Application.ProductService;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.Core.Services.Contract;
using FluentAssertions;

namespace OrderManagementSystem.Test.ProductServiceTests
{
	public class ProductServiceTest
	{

		private readonly Mock<IUnitOfWork> unitOfWorkMock;
		private readonly IProductService productService;

		public ProductServiceTest()
		{
			unitOfWorkMock = new Mock<IUnitOfWork>();
			productService = new ProductService(unitOfWorkMock.Object);
		}

		[Fact]
		public async Task UpdateStock_ShouldUpdateStockSuccessfully()
		{
			// Arrange
			var product = new Product
			{
				Id = 1,
				Name = "Product 1",
				Stock = 10
			};

			unitOfWorkMock.Setup(u => u.Repository<Product>().GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
			unitOfWorkMock.Setup(u => u.Repository<Product>().Update(It.IsAny<Product>()));
			unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

			// Act
			await productService.UpdateStockAsync(1, 5);

			// Assert
			product.Stock.Should().Be(5);
			unitOfWorkMock.Verify(u => u.Repository<Product>().Update(It.IsAny<Product>()), Times.Once);
			unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
		}
	}
}
