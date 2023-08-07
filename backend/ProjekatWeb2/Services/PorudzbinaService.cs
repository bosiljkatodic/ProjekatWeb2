using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjekatWeb2.Dto;
using ProjekatWeb2.Interfaces;
using ProjekatWeb2.Models;
using ProjekatWeb2.Repository.Interfaces;

namespace ProjekatWeb2.Services
{
    public class PorudzbinaService : IPorudzbinaService
    {

        private readonly IMapper _mapper;
        private readonly IArtikalRepozitorijum _artikalRepozitorijum;
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IPorudzbinaRepozitorijum _porudzbinaRepozitorijum;
        private readonly IElementPorudzbineRepozitorijum _elementPorudzbineRepozitorijum;

        public PorudzbinaService(IMapper mapper, IArtikalRepozitorijum artikalRepozitorijum, IKorisnikRepozitorijum korisnikRepozitorijum, IPorudzbinaRepozitorijum porudzbinaRepozitorijum, IElementPorudzbineRepozitorijum elementPorudzbineRepozitorijum)
        {
            _mapper = mapper;
            _artikalRepozitorijum = artikalRepozitorijum;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _porudzbinaRepozitorijum = porudzbinaRepozitorijum;
            _elementPorudzbineRepozitorijum = elementPorudzbineRepozitorijum;
        }

        public async Task<PorudzbinaDto> AddPorudzbina(PorudzbinaDto newPorudzbinaDto)
        {
            //ako je lista artikala prazna, onda ne moze da se napravi porudzbina
            if (newPorudzbinaDto.ElementiPorudzbine == null)
            {
                return null;
            }
            var korisnik = await _korisnikRepozitorijum.GetKorisnikById(newPorudzbinaDto.KorisnikId);
            if (korisnik == null)
                return null;

            if (string.IsNullOrEmpty(newPorudzbinaDto.AdresaDostave))
            {
                return null;
            }

            var newPorudzbina = _mapper.Map<Porudzbina>(newPorudzbinaDto);
            newPorudzbina.Korisnik = korisnik;

            //ostalo mi vreme dostave 
            //generisem neki broj izmedju 2 i 12, jer vreme dostave mora biti vece od 1h
            Random hoursGenerator = new Random();
            int additionalHours = hoursGenerator.Next(2, 12);
            newPorudzbina.VrijemeDostave = DateTime.Now.AddHours(additionalHours);
            newPorudzbina.ElementiPorudzbine = null;
            newPorudzbina.Cijena = 0;
            await _porudzbinaRepozitorijum.AddPorudzbina(newPorudzbina);

            //sve ovo mora da stavim u try, jer u slucaju da pukne, porudzbina mora da se obrise iz baze
            //mora da napravim transkacionu operaciju

            try
            {
                //ostali mi artikili u porudzbini
                foreach (ElementPorudzbineDto elementPorudzbineDto in newPorudzbinaDto.ElementiPorudzbine)
                {
                    //pronaci artikal koji odgovara artiklu porudzbine
                    Artikal artikal = await _artikalRepozitorijum.GetArtikalById(elementPorudzbineDto.IdArtikal);

                    //ako artikal ne postoji vrati null
                    if (artikal == null)
                    {
                        //stavim da se porudzbina izbaci ako nije uspelo pronalazenje artikla
                        await _porudzbinaRepozitorijum.DeletePorudzbina(newPorudzbina);
                        return null;
                    }

                    if (artikal.Kolicina < elementPorudzbineDto.Kolicina)
                    {
                        //stavim da se porudzbina izvbaci ako se trazi vise nego sto ima
                        await _porudzbinaRepozitorijum.DeletePorudzbina(newPorudzbina);
                        return null;
                    }

                    newPorudzbina.Cijena += elementPorudzbineDto.Kolicina * artikal.Cijena + artikal.CijenaDostave;

                    //skinuti kolicinu artikala kolko je poruceno
                    artikal.Kolicina -= elementPorudzbineDto.Kolicina;

                    //kreiranje elementa porudzbine
                    ElementPorudzbine element = new ElementPorudzbine
                    {
                        IdArtikal = artikal.Id,
                        Kolicina = elementPorudzbineDto.Kolicina,
                        IdPorudzbina = newPorudzbina.Id,
                        Porudzbina = newPorudzbina,
                    };

                    //dodati svaki porudzbinaArtikal u db set
                    //newPorudzbina.ElementiPorudzbine.Add(element);
                    await _elementPorudzbineRepozitorijum.AddElementPorudzbine(element);
                }

                //kad je napravljena porudzbina, onda nju treba dodati u kupcu i na kraju konacno sacuvati svae changes
                //mozda ne treba da se ovo uradi jer je uradjen svae changes, on je vec sacuvao korisniku na osnovu ID porudbinu
                //_dbContext.Korisnici.Find(newPorudzbinaDto.KorisnikId).Porudzbine.Add(newPorudzbina);

                //await _dbContext.SaveChangesAsync();

                PorudzbinaDto returnPorudzbinaDto = _mapper.Map<PorudzbinaDto>(newPorudzbina);
                return returnPorudzbinaDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //obrisati porudzbinu
                ReturnKolicinaArtikalaPorudzbine(newPorudzbinaDto.ElementiPorudzbine);
                
                await _porudzbinaRepozitorijum.DeletePorudzbina(newPorudzbina);
                return null;
            }
        }

        public async void ReturnKolicinaArtikalaPorudzbine(List<ElementPorudzbineDto> elementiPorudzbineDto)
        {
            foreach (ElementPorudzbineDto elementPorudzbineDto in elementiPorudzbineDto)
            {
                var artikalPorudzbine = await _artikalRepozitorijum.GetArtikalById(elementPorudzbineDto.IdArtikal);
                if (artikalPorudzbine != null)
                {
                    artikalPorudzbine.Kolicina += elementPorudzbineDto.Kolicina;
                }
            }
        }

        public async Task DeletePorudzbina(long id)
        {
            var deletePorudzbina = await _porudzbinaRepozitorijum.GetPorudzbinaById(id);
            await _porudzbinaRepozitorijum.DeletePorudzbina(deletePorudzbina);

        }

        public Task<List<PorudzbinaDto>> GetAllPorudzbina()
        {
            throw new NotImplementedException();
        }

        public Task<List<PorudzbinaDto>> GetKupcevePorudzbine(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PregledPorudzbineDto> GetPorudzbinaById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PorudzbinaDto>> GetProdavceveNovePorudzbine(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PorudzbinaDto>> GetProdavcevePrethodnePorudzbine(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PregledPorudzbineDto> OtkaziPorudzbinu(long id, string statusVerifikacije)
        {
            throw new NotImplementedException();
        }

        public Task<PorudzbinaDto> UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto)
        {
            throw new NotImplementedException();
        }
    }
}
