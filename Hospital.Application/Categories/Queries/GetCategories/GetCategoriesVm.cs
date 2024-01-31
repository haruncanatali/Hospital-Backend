using Hospital.Application.Categories.Queries.Dtos;

namespace Hospital.Application.Categories.Queries.GetCategories;

public class GetCategoriesVm
{
    public List<CategoryDto> Categories { get; set; }
    public int Count { get; set; }
}