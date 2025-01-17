using BiblioAPI.Models;

namespace BiblioAPI.Interfaces
{
    public interface IMembreService
    {
        Task<IEnumerable<GetMembreDTO>> GetAllMembersAsync();
        Task<GetMembreDTO> GetMemberByIdAsync(int id);
        Task<PostMembreDTO> AddMemberAsync(PostMembreDTO membre);
        void UpdateMemberAsync(int Id, PostMembreDTO membre);
        Task<bool> DeleteMemberAsync(int id);
    }
}
