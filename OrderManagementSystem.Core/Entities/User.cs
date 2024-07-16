using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Entities
{
    public enum Roles
    {
		Admin = 1,
		Customer = 2,
	}
    public class User : BaseEntity
	{
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public Roles Role { get; set; }
	}
}
