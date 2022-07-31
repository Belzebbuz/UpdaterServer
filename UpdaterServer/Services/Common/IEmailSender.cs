using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.Common
{
	public interface IEmailSender
	{
		void Send(string email, string message, string subject);
	}

	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;
		private readonly string _emailSender;
		private readonly string _password;
		public EmailSender(IConfiguration configuration)
		{
			_emailSender = configuration["EmailSender:Email"];
			_password = configuration["EmailSender:Password"];
		}
		public void Send(string email, string message, string subject)
		{
			var fromAddress = new MailAddress(_emailSender, "Launcher email sender");
			var smtp = new SmtpClient
			{
				Host = "smtp.yandex.ru",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_emailSender, _password)
			};

			using (var reportMessage = new MailMessage(fromAddress, new MailAddress(email))
			{
				Subject = subject,
				Body = message,
				IsBodyHtml = true
			})
			{
				smtp.Send(reportMessage);
			}
		}
	}
}
