namespace ProjekatWeb2.Models
{
    public class Artikal
    {
        public Artikal() { }

        public long Id { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set;}
        public string Opis { get; set; }
        public string Fotografija { get; set; }
        public double CijenaDostave { get; set; }
        public long ProdavacId { get; set; }
        public Korisnik Prodavac { get; set; }


    }
}
