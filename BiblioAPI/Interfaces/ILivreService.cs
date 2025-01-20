using System.Collections.Generic;
using BiblioAPI.Models;

namespace BiblioAPI.Services.Interfaces
{
    public interface ILivreService
    {
        //Ici, on définit les méthodes que tout service pour les livres doit pouvoir faire :

        //Récupérer tous les livres.
        Task<IEnumerable<GetLivreDTO>> AfficherLivreAsync();

        // chercher un livre avec son ID
        Task<GetLivreDTO> GetLivreByIdAsync(int id);

        // créer un livre
        Task<PostLivreDTO> AddLivreAsync(PostLivreDTO livre);


        // modifier un livre
        Task UpdateLivreAsync(int id, PostLivreDTO livre);
        // supprimer un livre
        Task DeleteLivreAsync(int id);
    }
    // les classes qui vont hériter de IlivreService ne se soucient pas de comment ces actions sont réalisées.
}
