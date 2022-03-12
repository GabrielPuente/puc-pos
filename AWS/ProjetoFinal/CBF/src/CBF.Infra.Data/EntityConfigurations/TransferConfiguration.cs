using CBF.Domain;
using CBF.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.EntityConfigurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfer");

            builder.AddAuditProperties();
            builder.AddSoftDeleteProperties();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedNever()
                   .IsRequired();

            builder.Property(x => x.TransferDate).IsRequired();
            builder.Property(x => x.Value).IsRequired().HasDecimalPrecision();

            builder.HasOne(x => x.TeamOrigin);
            builder.HasOne(x => x.TeamDestiny);
            builder.HasOne(x => x.Player);

            var nav = builder.Metadata.FindNavigation(nameof(Transfer.TeamOrigin));
            nav.SetPropertyAccessMode(PropertyAccessMode.Field);

            var nav2 = builder.Metadata.FindNavigation(nameof(Transfer.TeamDestiny));
            nav2.SetPropertyAccessMode(PropertyAccessMode.Field);

            var nav3 = builder.Metadata.FindNavigation(nameof(Transfer.Player));
            nav3.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
