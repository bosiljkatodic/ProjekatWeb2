using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatWeb2.Models;

namespace ProjekatWeb2.Infrastructure.Configurations
{
    public class ArtikalConfiguration : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Prodavac)
                   .WithMany(x => x.ArtikliProdavac)
                   .HasForeignKey(x => x.ProdavacId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
