using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;

namespace OrderManagementSystem.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly IInvoiceService invoiceService;

		public InvoiceController(IInvoiceService invoiceService)
		{
			this.invoiceService = invoiceService;
		}


		[Authorize(Roles = "Admin")]
		[HttpGet("invoiceId")] /*Task<ActionResult<IReadOnlyList<Product>>>*/
		public async Task<ActionResult<Invoice>> GetSpecificInvoice(int invoiceId)
		{
			var invoice = await invoiceService.GetInvoiceByIdAsync(invoiceId);

			if (invoice == null)
			{
				return NotFound(new ApiResponse(404));
			}

			return Ok(invoice);
		}


		[Authorize(Roles = "Admin")]
		[HttpGet] 
		public async Task<ActionResult<IReadOnlyList<Invoice>>> GetAllInvoices()
		{
			var invoices = await invoiceService.GetAllInvoicesAsync();

			if (invoices == null)
			{
				return NotFound(new ApiResponse(404));
			}

			return Ok(invoices);
		}
	}
}
