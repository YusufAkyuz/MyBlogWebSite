
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.Articles;

public class ArticleListDto
{
    public IList<Article> Articles { get; set; }
    public Guid? CategoryId { get; set; }
    public virtual int CurrentPage { get; set; } = 1;
    public virtual int PageSize { get; set; } = 3;
    public virtual int TotalCount { get; set; }
    public virtual int TotalPageNumber => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPageNumber;
    public virtual bool IsAscending { get; set; } = false;
    public int StartPage => Math.Max(1, CurrentPage - 1);
    public int EndPage => Math.Min(TotalPageNumber, StartPage + 2);
}