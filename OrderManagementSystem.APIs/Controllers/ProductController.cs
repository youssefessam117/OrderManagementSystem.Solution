using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Application.ProductService;
using OrderManagementSystem.Core.Entities;

namespace OrderManagementSystem.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ProductService productService;

		public ProductController(ProductService productService)
		{
			this.productService = productService;
		}


		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
		{
			var products = await productService.GetAllProductAsync();
			return Ok(products);
		}

		[HttpGet("{productId}")]
		public async Task<ActionResult> GetProductById(int productId)
		{
			var product = await productService.GetProductById(productId);
			if (product == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult> AddProduct([FromBody]Product product)
		{
			var createdProduct = await productService.AddProductAsync(product);
			return Ok(createdProduct);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{productId}")]
		public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product product)
		{
			await productService.UpdateProductAsync(productId, product);
			return Ok();
		}
	}
}
