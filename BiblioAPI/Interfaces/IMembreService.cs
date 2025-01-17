using BiblioAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiblioAPI.Interfaces
{
    public interface IMembreService
    {
        Task<IEnumerable<GetMembreDTO>> GetAllMembersAsync();
        Task<PostMembreDTO> GetMemberByIdAsync(int id);
        Task<PostMembreDTO> AddMemberAsync(PostMembreDTO membre);
        Task<PostMembreDTO> UpdateMemberAsync(int id, PostMembreDTO membre);
        Task<bool> DeleteMemberAsync(int id);
    }
}
