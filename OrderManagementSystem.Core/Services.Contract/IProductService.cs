using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetAllProductAsync();

		Task<Product?> GetProductById(int id);

		Task<Product> AddProductAsync(Product product);

		Task UpdateProductAsync(int productId, Product product);

		Task UpdateStockAsync(int productId, int SoldProducts);
	}
}
