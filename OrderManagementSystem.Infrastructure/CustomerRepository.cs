using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure
{
	public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
	{

		public CustomerRepository(OrderManagementDbContext dbContext)
			:base(dbContext)
		{
		}
		public async Task<IReadOnlyList<Customer>> GetAllOrdersForCustomerAsync(Expression<Func<Customer, bool>> expression)
		{
			
			return await dbContext.Set<Customer>().Where(expression).AsNoTracking().ToListAsync();
		}
	}
}
