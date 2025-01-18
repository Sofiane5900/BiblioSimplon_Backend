using BiblioAPI.Models;

namespace BiblioAPI.Interfaces
{
    public interface IEmpruntService // Une interface est un contrat que un classe devra utilisé pour son implémentation
    {
        Task<List<GetEmpruntDTO>> AfficherEmprunts();

        Task<GetEmpruntDTO?> AfficherEmpruntId(int Id);

        Task<PostEmpruntDTO?> AjouterEmprunt(int membreId, int livreId);

        //Task<bool> ModifierEmprunt(int Id, PostEmpruntDTO emprunt);

        Task<List<GetEmpruntDTO>> ConsulterEmpruntsParMembre(int membreId);
    }
}
