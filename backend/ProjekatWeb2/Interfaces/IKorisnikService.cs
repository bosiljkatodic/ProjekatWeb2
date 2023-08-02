using Microsoft.Build.Framework;
using ProjekatWeb2.Dto;

namespace ProjekatWeb2.Interfaces
{
    public interface IKorisnikService
    {
        Task<KorisnikDto> AddKorisnik(KorisnikDto korisnikDto);
        Task<IspisDto> Login(LoginDto loginKorisnikDto);
        Task<IspisDto> Registration(KorisnikDto registerKorisnik);
        Task<KorisnikDto> GetKorisnik(long id);


    }
}
