using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patient.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Persistence.Extentsions
{
    public static class EntityBuilderExtentions
    {
        public static EntityTypeBuilder<TEntity> MapBase<TEntity>(this EntityTypeBuilder<TEntity> builder, string tableName, string? schemaName = null)
        where TEntity : BaseEntity
        {
            builder.ToTable(tableName, schemaName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(x => x.HasActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Ignore(x => x.DomainEvents);

            return builder;
        }
    }
}
