using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;

namespace OrderManagementSystem.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService customerService;

		public CustomerController(ICustomerService customerService)
		{
			this.customerService = customerService;
		}


		[HttpPost("customers")]
		public  ActionResult<Customer> CreateCustomer(Customer customer)
		{
			var newCustomer = customerService.AddCustomerAsync(customer);
			return Ok(newCustomer);
		}

		[HttpGet("{customerId}/orders")]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersByCustomerId(int customerId)
		{
			var orders = await customerService.GetOrdersByCustomerIdAsync(customerId);
			if (orders == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(orders);
		}

	}
}
