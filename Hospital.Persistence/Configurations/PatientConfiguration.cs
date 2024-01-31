using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(c => c.Nationality).IsRequired();
        builder.Property(c => c.Address).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Discontinuity).HasDefaultValue(0);
    }
}