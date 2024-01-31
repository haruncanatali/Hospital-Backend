namespace Hospital.Application.Common.Models.Queue;

public class AddAppointmentRequestModel
{
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    public long PatientId { get; set; }
    public long WorkCalendarId { get; set; }
}