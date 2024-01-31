using Hospital.Domain.Enums;

namespace Hospital.Domain.Common;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public long CreatedBy { get; set; }
    public long? DeletedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public BaseEntity()
    {
        CreatedAt = DateTime.Now;
        Status = EntityStatus.Active;
    }
}