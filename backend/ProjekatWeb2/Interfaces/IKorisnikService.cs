﻿using Microsoft.Build.Framework;
using ProjekatWeb2.Dto;
using ProjekatWeb2.Models;

namespace ProjekatWeb2.Interfaces
{
    public interface IKorisnikService
    {
        Task<KorisnikDto> AddKorisnik(KorisnikDto korisnikDto);
        Task<IspisDto> Login(LoginDto loginKorisnikDto);
        Task<IspisDto> Registration(KorisnikDto registerKorisnik);
        Task<KorisnikDto> GetKorisnik(long id);
        Task<IEnumerable<KorisnikDto>> GetAllKorisnici();
        Task UpdateKorisnik(UpdateKorisnikDto updateKorisnikDto);
        Task DeleteKorisnik(long id);
        Task<List<KorisnikDto>> GetProdavci();
        Task<List<KorisnikDto>> VerifyProdavac(long id, string statusVerifikacije);



    }
}
