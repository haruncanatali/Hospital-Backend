using System.ComponentModel;

namespace Hospital.Domain.Enums;

public enum Gender
{
    [Description("Kadın")]
    Female = 1,
    [Description("Erkek")]
    Male,

}