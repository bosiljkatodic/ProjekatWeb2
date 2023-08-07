using ProjekatWeb2.Enumerations;
using ProjekatWeb2.Models;

namespace ProjekatWeb2.Dto
{
    public class PorudzbinaDto
    {
        public long Id { get; set; }
        public string Komentar { get; set; }
        public string AdresaDostave { get; set; }
        public double Cijena { get; set; }
        public DateTime VrijemeDostave { get; set; }
        public DateTime VrijemePorucivanja { get; set; }
        public StatusPorudzbine StatusPorudzbine { get; set; }
        public long KorisnikId { get; set; }
        public List<ElementPorudzbineDto> ElementiPorudzbine { get; set; }

    }
}
