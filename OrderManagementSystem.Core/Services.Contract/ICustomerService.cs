﻿using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface ICustomerService
	{
		Task<Customer> AddCustomerAsync(Customer customer);

		Task<IEnumerable<Order>?> GetOrdersByCustomerIdAsync(int customerId);
	}
}
