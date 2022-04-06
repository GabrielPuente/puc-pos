using CBF.Domain;
using CBF.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.EntityConfigurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Match");

            builder.AddAuditProperties();
            builder.AddSoftDeleteProperties();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(x => x.Reference).IsRequired();
            builder.HasOne(x => x.Tournament);
            builder.HasOne(x => x.HomeTeam);
            builder.HasOne(x => x.AwayTeam);

            var nav = builder.Metadata.FindNavigation(nameof(Match.Tournament));
            nav.SetPropertyAccessMode(PropertyAccessMode.Field);

            var nav2 = builder.Metadata.FindNavigation(nameof(Match.HomeTeam));
            nav2.SetPropertyAccessMode(PropertyAccessMode.Field);

            var nav3 = builder.Metadata.FindNavigation(nameof(Match.AwayTeam));
            nav3.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
