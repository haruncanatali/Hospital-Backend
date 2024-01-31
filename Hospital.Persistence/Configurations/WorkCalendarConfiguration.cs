using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Configurations;

public class WorkCalendarConfiguration : IEntityTypeConfiguration<WorkCalendar>
{
    public void Configure(EntityTypeBuilder<WorkCalendar> builder)
    {
        builder.Property(c => c.StaffId).IsRequired();
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.Date).IsRequired();
    }
}