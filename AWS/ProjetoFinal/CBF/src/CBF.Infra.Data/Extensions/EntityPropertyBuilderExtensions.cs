using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBF.Infra.Data.Extensions
{
    public static class EntityPropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> HasDecimalPrecision<TProperty>(this PropertyBuilder<TProperty> property)
        {
            return property.HasColumnType("decimal(18,2)");
        }
        public static PropertyBuilder<TProperty> HasVarchar<TProperty>(this PropertyBuilder<TProperty> property, int maxLenght)
        {
            return property.HasColumnType($"varchar({maxLenght})");
        }
    }
}
