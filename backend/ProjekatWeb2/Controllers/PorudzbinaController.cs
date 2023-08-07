using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjekatWeb2.Dto;
using ProjekatWeb2.Infrastructure;
using ProjekatWeb2.Interfaces;
using ProjekatWeb2.Models;
using ProjekatWeb2.Services;

namespace ProjekatWeb2.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaService _porudzbinaService;

        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
        }

        //izbaciti ovde add, jer je ovo crud operacija
        [HttpPost("addPorudzbina")]
        //[Authorize(Roles = "kupac")]
        public async Task<IActionResult> CreatePorudzbina([FromBody] PorudzbinaDto porudzbina)
        {
            PorudzbinaDto newPorudzbinaDto = await _porudzbinaService.AddPorudzbina(porudzbina);
            if (newPorudzbinaDto == null)
            {
                return BadRequest("Postoji neki problem prilikom dodavanja porudzbine");
            }
            return Ok(newPorudzbinaDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePorudzbina(long id)
        {
            await _porudzbinaService.DeletePorudzbina(id);

            return Ok($"Porudzbina id {id} je uspesno obrisana");
        }
    }
}
