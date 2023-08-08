﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjekatWeb2.Dto;
using ProjekatWeb2.Interfaces;
using ProjekatWeb2.Models;

namespace ProjekatWeb2.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;
        private readonly IEmailService _emailService;
        public KorisnikController(IKorisnikService korisnikService, IEmailService emailService)
        {
            _korisnikService = korisnikService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKorisnik([FromBody] KorisnikDto korisnik)
        {
            return Ok(await _korisnikService.AddKorisnik(korisnik));
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetKorisnik(long id)
        {
            KorisnikDto korisnik = await _korisnikService.GetKorisnik(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginKorisnikDto)
        {
            IspisDto ispis = await _korisnikService.Login(loginKorisnikDto);
            if (ispis.KorisnikDto == null)
            {
                return BadRequest(ispis.Result);
            }

            ispis.KorisnikDto.Lozinka = loginKorisnikDto.Lozinka;
            return Ok(ispis);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] KorisnikDto registerKorisnikDto)
        {
            IspisDto ispis = await _korisnikService.Registration(registerKorisnikDto);
            if (ispis.KorisnikDto == null)
                return BadRequest(ispis.Result);

            ispis.KorisnikDto.Lozinka = registerKorisnikDto.Lozinka;
            return Ok(ispis);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AzuriranjeKorisnika(int id, [FromForm] UpdateKorisnikDto korisnikDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnikDto.Id)
            {
                return BadRequest("Id korisnika se ne poklapa sa Id vrijednoscu u rutiranju.");
            }

            try
            {
                await _korisnikService.UpdateKorisnik(korisnikDto);
                return Ok("Korisnik je azuriran.");
            }
            catch (Exception ex)
            {

                return BadRequest($"Greska prilikom azuriranja: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKorisnik(long id)
        {
            await _korisnikService.DeleteKorisnik(id);
            return Ok($"Korisnik sa id = {id} je uspesno obrisan.");
        }

        [HttpGet("getProdavci")]
        //[Authorize(Roles = "administrator")]
        public async Task<IActionResult> GetProdavci()
        {
            return Ok(await _korisnikService.GetProdavci());
        }

        [HttpPut("verifyProdavca/{id}")]
        //[Authorize(Roles = "administrator")]
        public async Task<IActionResult> VerifyProdavca(long id, [FromBody] string statusVerifikacije)
        {
            List<KorisnikDto> verifiedProdavci = await _korisnikService.VerifyProdavac(id, statusVerifikacije);
            if (verifiedProdavci == null)
            {
                return BadRequest("Ne postoji prodavac");
            }

            KorisnikDto prodavac = await _korisnikService.GetKorisnik(id);
            _emailService.SendEmail(prodavac.Email, statusVerifikacije);

            return Ok(verifiedProdavci);
        }

    }
}
