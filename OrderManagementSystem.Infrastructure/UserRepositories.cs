using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure
{
	public class UserRepositories : GenericRepository<User>, IUserRepositories
	{

		public UserRepositories(OrderManagementDbContext dbContext) :base(dbContext)
		{

		}

		public async Task<User?> GetUserByUsernameAsync(string username)
			=> await dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
	}
}
