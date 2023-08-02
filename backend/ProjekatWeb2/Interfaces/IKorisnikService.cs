using ProjekatWeb2.Dto;

namespace ProjekatWeb2.Interfaces
{
    public interface IKorisnikService
    {
        Task<IspisDto> Login(LoginDto loginKorisnikDto);
        Task<IspisDto> Registration(KorisnikDto registerKorisnik);
    }
}
