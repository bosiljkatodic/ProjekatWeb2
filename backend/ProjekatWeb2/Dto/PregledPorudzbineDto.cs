using ProjekatWeb2.Enumerations;

namespace ProjekatWeb2.Dto
{
    public class PregledPorudzbineDto
    {
        public long Id { get; set; }
        public string AdresaDostave { get; set; }
        public string Komentar { get; set; }
        public DateTime VrijemePorucivanja { get; set; }
        public DateTime VrijemeDostave { get; set; }
        public double Cijena { get; set; }
        public List<string> ImenaArtikala { get; set; }
        public StatusPorudzbine StatusPorudzbine { get; set; }
    }
}
