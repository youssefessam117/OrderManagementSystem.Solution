using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OrderManagementDbContext dbContext;
		private Hashtable _repositories;

		public UnitOfWork(OrderManagementDbContext dbContext)
		{
			this.dbContext = dbContext;
			_repositories = new Hashtable();
		}

		public Task<int> CompleteAsync()
			=> dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync()
			=> dbContext.DisposeAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(key))
			{
				var repository = new GenericRepository<TEntity>(dbContext);
				_repositories.Add(key, repository);
			}

			return _repositories[key] as IGenericRepository<TEntity>;
		}
	}
}
