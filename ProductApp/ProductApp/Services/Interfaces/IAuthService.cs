using ProductApp.Models;
using ProductApp.ViewModels;

namespace ProductApp.Services.Interfaces
{
    public interface IAuthService
    {
        void Register(RegisterViewModel model);
        string Login(string username, string password);
    }
}
