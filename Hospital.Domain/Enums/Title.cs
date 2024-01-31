using System.ComponentModel;

namespace Hospital.Domain.Enums;

public enum Title
{
    [Description("Profesör Doktor")]
    Professor = 1,
    [Description("Doçent Doktor")]
    AssociateProfessor,
    [Description("Doktor")]
    Doctor
}