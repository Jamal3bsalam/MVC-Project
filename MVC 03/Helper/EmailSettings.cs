using Company.G05.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_03.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email) 
		{
			// Mail Server : gmail.com

			// Smtp
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;

			// hoouqfpjesrbkrkg
			client.Credentials = new NetworkCredential("jamalwork11@gmail.com", "hoouqfpjesrbkrkg");

			client.Send("jamalwork11@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
