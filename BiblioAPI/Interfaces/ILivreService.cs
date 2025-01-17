using System.Collections.Generic;
using BiblioAPI.Models;

namespace BiblioAPI.Services.Interfaces
{
    public interface ILivreService
    {
        //Ici, on définit les méthodes que tout service pour les livres doit pouvoir faire :

        //Récupérer tous les livres.
        IEnumerable<GetLivreDTO> GetAllLivres();

        // chercher un livre avec son ID
        GetLivreDTO GetLivreById(int Id);

        // créer un livre
        void AddLivre(PostLivreDTO livre);

        // modifier un livre
        void UpdateLivre(int Id,PostLivreDTO livre);

        // supprimer un livre
        void DeleteLivre(int Id);
    }
    // les classes qui vont hériter de IlivreService ne se soucient pas de comment ces actions sont réalisées.
}
