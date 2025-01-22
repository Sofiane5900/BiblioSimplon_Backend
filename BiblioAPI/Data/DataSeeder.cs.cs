using BiblioAPI.Models;
using Newtonsoft.Json;

namespace BiblioAPI.Data
{
    public class DataSeeder
    {
        private readonly BiblioDbContext _context;

        public DataSeeder(BiblioDbContext context)
        {
            _context = context;
        }

        public async Task AjouterLivresDepuisJsonAsync(string cheminFichier)
        {
            try
            {
                // Lire le fichier JSON
                var jsonContent = await File.ReadAllTextAsync(cheminFichier);

                // Désérialiser le JSON en une liste d'objets LivreModel
                var livres = JsonConvert.DeserializeObject<List<LivreModel>>(jsonContent);

                // Ajouter les livres dans la base de données
                if (livres != null)
                {
                    _context.Livre.AddRange(livres);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de lecture ou de désérialisation
                Console.WriteLine($"Erreur lors de l'ajout des livres depuis le fichier JSON : {ex.Message}");
            }
        }
        public async Task AjouterMembreDepuisJsonAsync(string cheminFichier1)
        {
            try
            {
                // Lire le fichier JSON
                var jsonContent = await File.ReadAllTextAsync(cheminFichier1);

                // Désérialiser le JSON en une liste d'objets LivreModel
                var membres = JsonConvert.DeserializeObject<List<MembreModel>>(jsonContent);

                // Ajouter les livres dans la base de données
                if (membres != null)
                {
                    _context.Membre.AddRange(membres);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de lecture ou de désérialisation
                Console.WriteLine($"Erreur lors de l'ajout des livres depuis le fichier JSON : {ex.Message}");
            }
        }
    }

}
