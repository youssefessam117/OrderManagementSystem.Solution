﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IEmailSenderService
	{

		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
