using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Appointment : BaseEntity
{
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    public long PatientId { get; set; }
    public long WorkCalendarId { get; set; }

    public Category Category { get; set; }
    public Staff Staff { get; set; }
    public Patient Patient { get; set; }
    public WorkCalendar WorkCalendar { get; set; }
}