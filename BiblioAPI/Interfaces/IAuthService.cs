using BiblioAPI.Models;

namespace BiblioAPI.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterEmployeDTO?> InscrireEmploye(RegisterEmployeDTO employe);
        Task<LoginEmployeDTO?> LoginEmploye(LoginEmployeDTO employe);
    }
}
