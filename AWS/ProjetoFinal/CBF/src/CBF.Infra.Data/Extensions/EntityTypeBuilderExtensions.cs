using CBF.Domain.DefaultEntity;
using CBF.Infra.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CBF.Infra.Data.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> AddAuditProperties<T>(this EntityTypeBuilder<T> modelBuilder)
          where T : Entity
        {
            modelBuilder.Property<DateTime>(Columns.CREATED_DATE);
            modelBuilder.Property<DateTime?>(Columns.LAST_UPDATED_DATE);

            return modelBuilder;
        }

        public static EntityTypeBuilder<T> AddSoftDeleteProperties<T>(this EntityTypeBuilder<T> modelBuilder)
            where T : Entity
        {
            modelBuilder.Property<bool>(Columns.IS_DELETED);
            modelBuilder.Property<DateTime?>(Columns.DELETED_DATE);

            return modelBuilder;
        }
    }
}
