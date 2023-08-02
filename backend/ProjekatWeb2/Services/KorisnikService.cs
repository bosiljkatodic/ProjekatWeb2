using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjekatWeb2.Dto;
using ProjekatWeb2.Enumerations;
using ProjekatWeb2.Infrastructure;
using ProjekatWeb2.Interfaces;
using ProjekatWeb2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjekatWeb2.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IMapper _mapper;
        private readonly OnlineProdavnicaDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;

        public KorisnikService(IMapper mapper, OnlineProdavnicaDbContext dbContext, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = configuration.GetSection("SecretKey");
        }

        public async Task<KorisnikDto> AddKorisnik(KorisnikDto newKorisnikDto)
        {
            Korisnik newKorisnik = _mapper.Map<Korisnik>(newKorisnikDto);
            newKorisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(newKorisnik.Lozinka);
            _dbContext.Korisnici.Add(newKorisnik);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<KorisnikDto>(newKorisnik);
        }
        
        public async Task<IspisDto> Login(LoginDto loginKorisnikDto)
        {
            Korisnik loginKorisnik = new Korisnik();
            if (string.IsNullOrEmpty(loginKorisnikDto.Email) && string.IsNullOrEmpty(loginKorisnikDto.Lozinka))
            {
                return new IspisDto("Niste uneli email ili lozinku.");
            }


            loginKorisnik = await _dbContext.Korisnici.FirstOrDefaultAsync(x => x.Email == loginKorisnikDto.Email);

            if (loginKorisnik == null)
                return new IspisDto($"Korisnik sa emailom {loginKorisnikDto.Email} ne postoji");



            if (BCrypt.Net.BCrypt.Verify(loginKorisnikDto.Lozinka, loginKorisnik.Lozinka))
            {
                List<Claim> claims = new List<Claim>();
                if (loginKorisnik.TipKorisnika == TipKorisnika.Administator)
                    claims.Add(new Claim(ClaimTypes.Role, "administrator"));
                if (loginKorisnik.TipKorisnika == TipKorisnika.Kupac)
                    claims.Add(new Claim(ClaimTypes.Role, "kupac"));
                if (loginKorisnik.TipKorisnika == TipKorisnika.Prodavac)
                    claims.Add(new Claim(ClaimTypes.Role, "prodavac"));


                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:7273",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(50),
                    signingCredentials: signInCredentials
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                KorisnikDto korisnikDto = _mapper.Map<KorisnikDto>(loginKorisnik);

                IspisDto ispis = new IspisDto(token, korisnikDto, "Uspesno ste se logovali na sistem");
                return ispis;
            }
            else
            {
                return new IspisDto("Lozinka nije ispravno uneta");
            }
        }

        public async Task<IspisDto> Registration(KorisnikDto registerKorisnik)
        {
            if (string.IsNullOrEmpty(registerKorisnik.Email)) //ako nije unet email, baci gresku
                return new IspisDto("Niste uneli email");
            
            if(_dbContext.Korisnici != null) {
                foreach (Korisnik k in _dbContext.Korisnici)
                {
                    if (k.Email == registerKorisnik.Email)
                        return new IspisDto("Email vec postoji");
                }
            }
           

            if (registerKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                registerKorisnik.VerifikacijaProdavca = VerifikacijaProdavca.UProcesu;
            }

            if (registerKorisnik.TipKorisnika != TipKorisnika.Prodavac)
            {
                registerKorisnik.CijenaDostave = 0;
            }


            if (!IsKorisnikFieldsValid(registerKorisnik)) //ako nisu validna polja onda nista
                return new IspisDto("Ostala polja moraju biti validna");

            KorisnikDto registeredKorisnik = await AddKorisnik(registerKorisnik);

            if (registeredKorisnik == null)
                return null;

            //nema provere za password, pa odmah vracamo token
            List<Claim> claims = new List<Claim>();
            if (registerKorisnik.TipKorisnika == TipKorisnika.Administator)
                claims.Add(new Claim(ClaimTypes.Role, "administrator"));
            if (registerKorisnik.TipKorisnika == TipKorisnika.Kupac)
                claims.Add(new Claim(ClaimTypes.Role, "kupac"));
            if (registerKorisnik.TipKorisnika == TipKorisnika.Prodavac)
                claims.Add(new Claim(ClaimTypes.Role, "prodavac"));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:7273",
                claims: claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signInCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            IspisDto ispis = new IspisDto(token, registeredKorisnik, "Uspesno ste se registrovali");
            return ispis;
        }

        public static bool IsKorisnikFieldsValid(KorisnikDto korisnikDto)
        {
            if (string.IsNullOrEmpty(korisnikDto.KorisnickoIme))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Email))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Lozinka))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Ime))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Prezime))
                return false;
            if (korisnikDto.DatumRodjenja > DateTime.Now)
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Adresa))
                return false;
            if (korisnikDto.CijenaDostave == 0 && korisnikDto.TipKorisnika == TipKorisnika.Prodavac)
                return false;

            return true;
        }

        public async Task<KorisnikDto> GetKorisnik(long id)
        {
            Korisnik korisnik = new Korisnik();
            korisnik = await _dbContext.Korisnici.FindAsync(id);
            KorisnikDto korisnikDto = _mapper.Map<KorisnikDto>(korisnik);

            return korisnikDto;
        }
    }
}
