using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.InvoiceService
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IUnitOfWork unitOfWork;

		public InvoiceService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<Invoice> ApplyTieredDiscounts(Invoice invoice)
		{
			var order = await unitOfWork.Repository<Order>().GetByIdAsync(invoice.OrderId);

			if (order?.TotalAmount > 200)
			{
				invoice.TotalAmount = invoice.TotalAmount - (invoice.TotalAmount * (10 / 100));
			}
			else if (order?.TotalAmount > 100)
			{
				invoice.TotalAmount = invoice.TotalAmount - (invoice.TotalAmount * (5 / 100));

			}
			return invoice;
		}

		public async Task GenerateInvoiceAsync(int orderId)
		{
			var order = await unitOfWork.Repository<Order>().GetByIdAsync(orderId);

			if (order is not null)
			{
				var invoice = new Invoice
				{
					OrderId = orderId,
					InvoiceDate = DateTime.Now,
					TotalAmount = order.TotalAmount
				};
				var invoiceWithDis = await ApplyTieredDiscounts(invoice);
				unitOfWork.Repository<Invoice>().Add(invoiceWithDis);
				await unitOfWork.CompleteAsync();
			}

		}
		public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
			=> await unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);

		public async Task<IReadOnlyList<Invoice?>> GetAllInvoicesAsync()
			=> await unitOfWork.Repository<Invoice>().GetAllAsync();
	}
}
