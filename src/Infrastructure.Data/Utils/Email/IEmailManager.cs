using Domain.Entities;

namespace Infrastruture.Utils.Email
{
    public interface IEmailManager
    {
        string GetConfirmEmailTemplate(string emailLink, User user);
        string GetResetPasswordEmailTemplate(string emailLink, string email);
    }
}
