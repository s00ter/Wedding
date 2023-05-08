namespace Wedding.AdminPanel.Models.WareCategory;

public class UpdateWareCategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IFormFile File { get; set; }
}