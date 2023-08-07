namespace ProjekatWeb2.Dto
{
    public class PregledPorudzbineDto
    {
        public long Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDostave { get; set; }
        public double CijenaPorudzbine { get; set; }
        public List<string> ImenaArtikala { get; set; }
    }
}
