using System.ComponentModel;

namespace Hospital.Domain.Enums;

public enum EntityStatus
{
    [Description("Aktif")]
    Active = 1,
    [Description("Pasif")]
    Passive
}