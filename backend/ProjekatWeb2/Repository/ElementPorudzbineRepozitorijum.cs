using ProjekatWeb2.Infrastructure;
using ProjekatWeb2.Models;
using ProjekatWeb2.Repository.Interfaces;

namespace ProjekatWeb2.Repository
{
    public class ElementPorudzbineRepozitorijum : IElementPorudzbineRepozitorijum
    {
        private readonly OnlineProdavnicaDbContext _dbContext;

        public ElementPorudzbineRepozitorijum(OnlineProdavnicaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddElementPorudzbine(ElementPorudzbine elementPorudzbine)
        {
            if (elementPorudzbine == null)
            {
                throw new ArgumentNullException(nameof(elementPorudzbine), "Element porudzbine ne smije biti null.");
            }
            _dbContext.ElementPorudzbine.Add(elementPorudzbine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
