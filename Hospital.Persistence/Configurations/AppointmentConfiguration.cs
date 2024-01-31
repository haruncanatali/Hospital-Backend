using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(c => c.PatientId).IsRequired();
        builder.Property(c => c.StaffId).IsRequired();
        builder.Property(c => c.WorkCalendarId).IsRequired();
        builder.Property(c => c.CategoryId).IsRequired();
    }
}