using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure.Data
{
	public class OrderManagementDbContext : DbContext
	{
		private readonly DbContextOptions<OrderManagementDbContext> options;

		public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) 
			:base(options)
		{
			this.options = options;
		}

        public DbSet<Customer> Customers { get; set; }
		public DbSet<Invoice>  Invoices { get; set; }
		public DbSet<Order>  Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }

	}
}
