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
        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
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


    }
}
