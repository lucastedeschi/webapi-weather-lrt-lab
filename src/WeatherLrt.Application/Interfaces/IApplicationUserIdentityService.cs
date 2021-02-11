using System.Threading.Tasks;

namespace WeatherLrt.Application.Interfaces
{
    public interface IApplicationUserIdentityService
    {
        Task<(bool signedIn, string token, string errorMessage)> SignIn(string email, string password);

        Task<(bool signedUp, string applicationUserId, string errorMessage)> SignUp(string userName, string emai, string password);
    }
}
