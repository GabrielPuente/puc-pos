using CBF.Domain;
using CBF.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.EntityConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team");

            builder.AddAuditProperties();
            builder.AddSoftDeleteProperties();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(x => x.Name).IsRequired().HasVarchar(100);
            builder.Property(x => x.Locality).IsRequired().HasVarchar(100);

            builder.HasMany(x => x.Players)
                    .WithOne()
                    .HasForeignKey(x => x.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);

            var nav = builder.Metadata.FindNavigation(nameof(Team.Players));
            nav.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
