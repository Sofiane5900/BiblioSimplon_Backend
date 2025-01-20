using BiblioAPI.Models;

namespace BiblioAPI.Interfaces
{
    public interface IEmpruntService // Une interface est un contrat que un classe devra utilisé pour son implémentation
    {
        Task<List<GetEmpruntDTO>> AfficherEmpruntsActif();

        Task<List<GetEmpruntDTO>> AfficherEmpruntsInactif();
        Task<GetEmpruntDTO?> AfficherEmpruntId(int Id);

        Task<PostEmpruntDTO?> AjouterEmprunt(int membreId, int livreId);

<<<<<<< HEAD
        Task<GetEmpruntDTO?> RendreEmprunt(int Id);
=======
        //Task<bool> ModifierEmprunt(int Id, PostEmpruntDTO emprunt);

>>>>>>> develop
        Task<List<GetEmpruntDTO>> ConsulterEmpruntsParMembre(int membreId);
    }
}
