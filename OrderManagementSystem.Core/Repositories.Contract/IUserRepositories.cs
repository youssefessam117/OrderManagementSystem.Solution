using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories.Contract
{
	public interface IUserRepositories :IGenericRepository<User>
	{
		Task<User?> GetUserByUsernameAsync(string username);
		

	}
}
