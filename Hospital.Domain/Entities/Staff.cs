using Hospital.Domain.Common;
using Hospital.Domain.Enums;
using Hospital.Domain.Identity;

namespace Hospital.Domain.Entities;

public class Staff : BaseEntity
{
    public string Resume { get; set; }
    public double Score { get; set; }
    public Title Title { get; set; }
    
    public long CategoryId { get; set; }
    public long UserId { get; set; }

    public Category Category { get; set; }
    public User User { get; set; }

    public List<Appointment> Appointments { get; set; }
}