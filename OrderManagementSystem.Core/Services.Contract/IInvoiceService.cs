using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IInvoiceService
	{
		Task<Invoice> ApplyTieredDiscounts(Invoice invoice);

		Task GenerateInvoiceAsync(int orderId);

		Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);

		Task<IReadOnlyList<Invoice?>> GetAllInvoicesAsync();


	}
}
