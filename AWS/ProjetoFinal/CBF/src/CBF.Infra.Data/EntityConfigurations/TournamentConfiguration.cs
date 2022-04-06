using CBF.Domain;
using CBF.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.EntityConfigurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournament");

            builder.AddAuditProperties();
            builder.AddSoftDeleteProperties();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(x => x.Name).IsRequired().HasVarchar(150);
            builder.Property(x => x.Reference).IsRequired();

            builder.OwnsMany(
                m => m.Matches,
                m =>
                {
                    m.ToTable("Match");

                    m.HasKey(x => x.Id);
                    m.Property(x => x.Id)
                           .ValueGeneratedNever()
                           .IsRequired();

                    m.WithOwner().HasForeignKey("TournamentId");

                    m.Property(x => x.Reference).IsRequired();
                    m.HasOne(x => x.HomeTeam);
                    m.HasOne(x => x.AwayTeam);

                    m.OwnsMany(
                        e => e.Events,
                        e =>
                        {
                            e.ToTable("Event");

                            e.HasKey(x => x.Id);
                            e.Property(x => x.Id)
                                   .ValueGeneratedNever()
                                   .IsRequired();

                            e.WithOwner().HasForeignKey("MatchId");

                            e.Property(x => x.Reference).IsRequired().HasVarchar(300);

                        });

                });
        }
    }
}
