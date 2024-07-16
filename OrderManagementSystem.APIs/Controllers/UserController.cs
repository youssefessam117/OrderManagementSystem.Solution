using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Dtos;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Entities;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;

namespace OrderManagementSystem.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepositories userRepositorie;
		private readonly IUnitOfWork unitOfWork;
		private readonly IAuthService authService;

		public UserController(IUserRepositories userRepositorie, IUnitOfWork unitOfWork , IAuthService authService)
		{
			this.userRepositorie = userRepositorie;
			this.unitOfWork = unitOfWork;
			this.authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto registerDto)
		{
			var user = await userRepositorie.GetUserByUsernameAsync(registerDto.Username);
			if (user != null)
			{
				return BadRequest(new ApiResponse(400, "User already exists")); 
			}

			var newUser = new User
			{
				Username = registerDto.Username,
				Role = registerDto.Role,
				PasswordHash = registerDto.Password
			};
			userRepositorie.Add(newUser);
			await unitOfWork.CompleteAsync();

			return Ok(newUser);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			var user = await userRepositorie.GetUserByUsernameAsync(loginDto.Username);
			if (user == null || loginDto.Password != user.PasswordHash)
			{
				return Unauthorized(new ApiResponse(401));
			}

			var token = authService.GenerateJwtToken(user);
			return Ok(token);
		}

	}
}
