using CBF.Domain;
using CBF.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.EntityConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(x => x.TeamId);
            builder.Property(x => x.Name).IsRequired().HasVarchar(100);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Country).IsRequired().HasVarchar(100);
            builder.Property(x => x.MarketValue).IsRequired().HasDecimalPrecision();

            builder.HasIndex(x => x.TeamId);
        }
    }
}
