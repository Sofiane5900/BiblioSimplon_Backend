using System.Collections.Generic;
using BiblioAPI.Models;

namespace BiblioAPI.Services.Interfaces
{
    public interface ILivreService
    {
        //Ici, on définit le crud que tout service pour les livres doit pouvoir faire :
        //Récupérer tous les livres.
        IEnumerable<LivreModel> GetAllLivres();
        // chercher un livre avec son ID
        LivreModel GetLivreById(int id);
        // créer un livre 
        void AddLivre(LivreModel livre);
        // modifier un livre 
        void UpdateLivre(int id, LivreModel livre);
        // supprimer un livre 
        void DeleteLivre(int id);
    }
    // les classes qui vont hériter de IlivreService ne se soucient pas de comment ces actions sont réalisées.
}