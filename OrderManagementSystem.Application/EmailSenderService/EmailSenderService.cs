using Microsoft.Extensions.Configuration;
using OrderManagementSystem.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.EmailSenderService
{
	public class EmailSenderService : IEmailSenderService
	{

		private readonly IConfiguration _configuration;

		public EmailSenderService(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		public async Task SendAsync(string from, string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];
			var senderPassword = _configuration["EmailSettings:SenderPassword"];

			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(from);
			emailMessage.To.Add(recipients);
			emailMessage.Subject = subject;
			emailMessage.Body = $"<html><body>{body}</body></html>";
			emailMessage.IsBodyHtml = true;



			var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpClientServer"], int.Parse(_configuration["EmailSettings:SmtpClientPort"] ?? ""))

			{
				Credentials = new NetworkCredential(senderEmail, senderPassword),
				EnableSsl = true
			};

			await smtpClient.SendMailAsync(emailMessage);
		}
	}
}
