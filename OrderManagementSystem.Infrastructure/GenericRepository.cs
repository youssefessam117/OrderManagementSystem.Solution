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
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private protected readonly OrderManagementDbContext dbContext;

		public GenericRepository(OrderManagementDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Add(T entity)
			=> dbContext.Set<T>().Add(entity);

		public void Delete(T entity)
			=> dbContext.Set<T>().Remove(entity);

		public async Task<IReadOnlyList<T>> GetAllAsync()
			=> await dbContext.Set<T>().AsNoTracking().ToListAsync();

		public async Task<T?> GetByIdAsync(int id)
			=> await dbContext.Set<T>().FindAsync(id);

		public void Update(T entity)
			=> dbContext.Set<T>().Update(entity);
	}
}
