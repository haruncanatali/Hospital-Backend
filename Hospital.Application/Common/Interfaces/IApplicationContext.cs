using Hospital.Domain.Entities;
using Hospital.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Common.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Staff> Staves { get; set; }
    public DbSet<WorkCalendar> WorkCalendars { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}