using ProjekatWeb2.Models;

namespace ProjekatWeb2.Repository.Interfaces
{
    public interface IPorudzbinaRepozitorijum
    {
        Task AddPorudzbina(Porudzbina porudzbina);
        Task DeletePorudzbina(Porudzbina porudzbina);
        Task<Porudzbina> GetPorudzbinaById(long id);
    }
}
