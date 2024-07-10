using Blog.Entity.DTOs.Categories;

namespace Blog.Entity.DTOs.Articles;

public class ArticleUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string  Content { get; set; }
    public Guid CategoryId { get; set; }
    public IList<CategoryDto> Categories { get; set; }
}