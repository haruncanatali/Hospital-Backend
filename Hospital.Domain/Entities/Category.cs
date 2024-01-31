using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public List<Staff> Staves { get; set; }
}