using Hospital.Domain.Common;
using Hospital.Domain.Identity;

namespace Hospital.Domain.Entities;

public class Patient : BaseEntity
{
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public int Discontinuity { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
    public List<Appointment> Appointments { get; set; }
}