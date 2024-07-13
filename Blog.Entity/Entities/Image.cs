using System.Collections;
using Blog.Core.Entities;
using Blog.Entity.Enums;

namespace Blog.Entity.Entities;

public class Image : EntityBase
{
    public Image()
    {
        
    }

    public Image(string fileName, string fileType, string createdBy)
    {
        FileName = fileName;
        FileType = fileType;
        CreatedBy = createdBy;
    }
    public string FileName { get; set; }
    public string FileType { get; set; }
    
    // Aşağıdaki prop'u One To Many ilişkisini kurmak adına ekledik. Bu image birden fazla articleda olabilir
    public ICollection<Article> Articles { get; set; }
    
    public ICollection<AppUser> Users { get; set; }
}