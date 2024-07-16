using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.APIs.Dtos
{
	public class LoginDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
