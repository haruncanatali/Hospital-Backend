using Hospital.Domain.Enums;

namespace Hospital.Application.Common.Models;

public class BaseDto
{
    public EntityStatus Status { get; set; }
    public string StatusText { get; set; }
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public long CreatedBy { get; set; }
    public long? DeletedBy { get; set; }
    public long? UpdatedBy { get; set; }
}