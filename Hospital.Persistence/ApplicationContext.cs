using Hospital.Application.Common.Interfaces;
using Hospital.Domain.Common;
using Hospital.Domain.Entities;
using Hospital.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Persistence
{
    public class ApplicationContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>,
            UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>,
        IApplicationContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        #region Entities

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staves { get; set; }
        public DbSet<WorkCalendar> WorkCalendars { get; set; }

        #endregion

        #region Identity User Tables

        public DbSet<User> Users
        {
            get { return base.Users; }
            set { }
        }

        public DbSet<Role> Roles
        {
            get { return base.Roles; }
            set { }
        }

        public DbSet<UserRole> UserRoles
        {
            get { return base.UserRoles; }
            set { }
        }

        #endregion


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if(_currentUserService!= null)
            {
				foreach (var entry in ChangeTracker.Entries<BaseEntity>())
				{
					switch (entry.State)
					{
						case EntityState.Added:
							entry.Entity.CreatedBy = _currentUserService.UserId;
							entry.Entity.CreatedAt = DateTime.Now;
							break;
						case EntityState.Modified:
							entry.Entity.UpdatedBy = _currentUserService.UserId;
							entry.Entity.UpdatedAt = DateTime.Now;
							break;
						case EntityState.Deleted:
							entry.Entity.DeletedBy = _currentUserService.UserId;
							entry.Entity.DeletedAt = DateTime.Now;
							break;
					}
				}
			}

    

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}