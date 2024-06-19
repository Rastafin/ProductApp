using ProductApp.Models;

namespace ProductApp.Services.Interfaces
{
    public interface IAuthService
    {
        void Register(User user);
        string Login(string username, string password);
    }
}
