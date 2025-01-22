using BiblioAPI.Models;

namespace BiblioAPI.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterEmployeDTO?> RegisterEmploye(RegisterEmployeDTO employe);
        Task<EmployeModel?> LoginEmploye(LoginEmployeDTO employe);
        Task<ResetPasswordDTO> ResetUserPasswordAsync(string username, string newPassword);
        Task<EmployeModel> GetUserByIdAsync(string username);
        
    }
}
