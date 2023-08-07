using Microsoft.EntityFrameworkCore;
using ProjekatWeb2.Infrastructure;
using ProjekatWeb2.Models;
using ProjekatWeb2.Repository.Interfaces;

namespace ProjekatWeb2.Repository
{
    public class PorudzbinaRepozitorijum : IPorudzbinaRepozitorijum
    {
        private readonly OnlineProdavnicaDbContext _dbContext;

        public PorudzbinaRepozitorijum(OnlineProdavnicaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPorudzbina(Porudzbina porudzbina)
        {

            if (porudzbina == null)
            {
                throw new ArgumentNullException(nameof(porudzbina), "Porudzbina ne smije biti null.");
            }
            _dbContext.Porudzbine.Add(porudzbina);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePorudzbina(Porudzbina porudzbina)
        {
            _dbContext.Porudzbine.Remove(porudzbina);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Porudzbina> GetPorudzbinaById(long id)
        {
            return await _dbContext.Porudzbine.FindAsync(id);
        }
    }
}
