using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.CustomerService
{
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly ICustomerRepository customerRepository;

		public CustomerService(IUnitOfWork unitOfWork , ICustomerRepository customerRepository)
		{
			this.unitOfWork = unitOfWork;
			this.customerRepository = customerRepository;
		}

		public async Task<Customer> AddCustomerAsync(Customer customer)
		{
			var newCustomer = new Customer()
			{
				Email = customer.Email,
				Id = customer.Id,
				Name = customer.Name,
				Orders = customer.Orders
			};
			unitOfWork.Repository<Customer>().Add(newCustomer);
			await unitOfWork.CompleteAsync();

			return newCustomer;
		}

		public async Task<IEnumerable<Order>?> GetOrdersByCustomerIdAsync(int customerId)
		{
			var orders = await customerRepository.GetAllOrdersForCustomerAsync(o => o.Id == customerId);

			return orders as IEnumerable<Order>;
		}


	}
}
