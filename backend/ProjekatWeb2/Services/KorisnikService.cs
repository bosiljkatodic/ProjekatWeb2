using ProjekatWeb2.Dto;
using ProjekatWeb2.Interfaces;

namespace ProjekatWeb2.Services
{
    public class KorisnikService : IKorisnikService
    {
        public Task<IspisDto> Login(LoginDto loginKorisnikDto)
        {
            throw new NotImplementedException();
        }

        public Task<IspisDto> Registration(KorisnikDto registerKorisnik)
        {
            throw new NotImplementedException();
        }
    }
}
