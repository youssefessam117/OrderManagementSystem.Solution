using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public Task<IReadOnlyList<Product>> GetAllProductAsync()
			=> unitOfWork.Repository<Product>().GetAllAsync();

		public Task<Product?> GetProductById(int id )
			=> unitOfWork.Repository<Product>().GetByIdAsync(id);

		public async Task<Product> AddProductAsync(Product product)
		{
			unitOfWork.Repository<Product>().Add(product);
			await unitOfWork.CompleteAsync();

			return product;
		}

		public async Task UpdateProductAsync(int productId, Product product)
		{
			var existProduct = await unitOfWork.Repository<Product>().GetByIdAsync(productId);
			if (existProduct != null)
			{
				existProduct.Name = product.Name;
				existProduct.Price = product.Price;
				existProduct.Stock = product.Stock;

				unitOfWork.Repository<Product>().Update(existProduct);
				await unitOfWork.CompleteAsync();
			}

			
		}

		public async Task UpdateStockAsync(int productId, int SoldProducts)
		{
			var product = await unitOfWork.Repository<Product>().GetByIdAsync(productId);
			if (product != null)
			{
				product.Stock -= SoldProducts;
				unitOfWork.Repository<Product>().Update(product);
				//await unitOfWork.CompleteAsync();
			}
		}
	}
}
