using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.Property(c => c.Resume).IsRequired();
        builder.Property(c => c.Score).HasDefaultValue(0.0);
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
    }
}