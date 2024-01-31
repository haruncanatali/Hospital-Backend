using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class WorkCalendar : BaseEntity
{
    public DateTime Date { get; set; }

    public long CategoryId { get; set; }
    public long StaffId { get; set; }

    public Category Category { get; set; }
    public Staff Staff { get; set; }
    public List<Appointment> Appointments { get; set; }
}