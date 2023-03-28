using Domain.Entities;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Infrastruture.Utils.Email
{
    public class EmailManger : IEmailManager
    {
        private readonly SendGridClient _clientKey;
        private readonly IConfiguration _config;
        private readonly EmailAddress _from;
        public EmailManger()
        {

        }

        public string GetConfirmEmailTemplate(string emailLink, User user)
        {
            string body;
            var folderName = Path.Combine("wwwroot", "Templates", "ConfirmEmail.html");
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (File.Exists(filepath))
                body = File.ReadAllText(filepath);
            else
                return null;

            string msgBody = body.Replace("{{action_url}}", emailLink)
                .Replace("{{org_name}}", "EZ Cash 9-5")
                .Replace("{{_name}}", $"{user.FirstName} {user.LastName}")
                .Replace("{email}", user.Email);

            return msgBody;
        }

        public string GetResetPasswordEmailTemplate(string emailLink, string email)
        {
            string body;
            var folderName = Path.Combine("wwwroot", "Templates", "ResetPassword.html");
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (File.Exists(filepath))
                body = File.ReadAllText(filepath);
            else
                return null;

            string msgBody = body.Replace("{email_link}", emailLink).
                Replace("{email}", email);

            return msgBody;
        }
    }
}
