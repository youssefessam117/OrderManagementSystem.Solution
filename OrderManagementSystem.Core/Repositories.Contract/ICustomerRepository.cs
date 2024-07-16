using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories.Contract
{
	public interface ICustomerRepository : IGenericRepository<Customer>
	{
		Task<IReadOnlyList<Customer>> GetAllOrdersForCustomerAsync(Expression<Func<Customer, bool>> expression);
	}
}
