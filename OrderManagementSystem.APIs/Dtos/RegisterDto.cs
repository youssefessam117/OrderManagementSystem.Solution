using OrderManagementSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.APIs.Dtos
{
	public class RegisterDto
	{

		[Required]
		public string Username { get; set; }

		[Required]
		[RegularExpression("(?=^.{6,10}$)((?=.*\\d))(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;,])(?!.*\\s).*$",
			ErrorMessage = "Password must have 1 Uppercase 1 lowercase 1 number, 1 non alphanumeric and at least 6 characters")]
		public string Password { get; set; }

		[Required]
		public Roles Role { get; set; }
	}
}
