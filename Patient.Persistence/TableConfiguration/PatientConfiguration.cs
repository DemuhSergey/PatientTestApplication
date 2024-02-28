using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patient.Persistence.Extentsions;

namespace Patient.Persistence.TableConfiguration
{
    public class PatientConfiguration
        : IEntityTypeConfiguration<Domain.Entities.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Patient> builder)
        {
            builder.MapBase("Patients");

            builder.Property(x => x.Use)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.Family)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Gender)
                .IsRequired(false);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.Given)
                .HasConversion(x => string.Join(',', x) ,
                                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
