using OrderManagementSystem.Application.AuthService;
using OrderManagementSystem.Application.CustomerService;
using OrderManagementSystem.Application.InvoiceService;
using OrderManagementSystem.Application.OrderService;
using OrderManagementSystem.Application.ProductService;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Repositories.Contract;
using OrderManagementSystem.Core.Services.Contract;
using OrderManagementSystem.Infrastructure;

namespace OrderManagementSystem.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			services.AddScoped(typeof(ICustomerService), typeof(CustomerService));

			services.AddScoped(typeof(IInvoiceService), typeof(InvoiceService));

			services.AddScoped(typeof(IOrderService), typeof(OrderService));

			services.AddScoped(typeof(IProductService), typeof(ProductService));

			services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
			services.AddScoped(typeof(IUserRepositories), typeof(UserRepositories));


			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));




			return services;
		}

	}
}
