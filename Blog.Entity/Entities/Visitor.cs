using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class Visitor : IEntityBase
{
    //Parametresiz bir yapıcı metodun istenmesinin temel nedeni, generic repository pattern'inde ve
    //özellikle dependency injection (bağımlılık enjeksiyonu) kullanılan ortamlarda nesnelerin kolayca oluşturulabilmesi ve
    //yönetilebilmesidir.Yani aslında getRepository derken unitOfWorkde parametresiz constructora göre nesneleri oluşturup getirebilmesi için.
    public Visitor()
    {
        
    }

    public Visitor(string ıpAddress, string userAgent)
    {
        IpAddress = ıpAddress;
        UserAgent = userAgent;
    }

    public int Id { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }   //Kullanıcıların sisteme hangi tarayıcıdan girdiğini anlamak için kullanacaz
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
}