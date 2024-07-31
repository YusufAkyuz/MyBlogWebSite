using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class ArticleVisitor : IEntityBase
{
    //Many to many ilişkileri kurmak için oluşturduk bu tabloyu
    
    //Parametresiz bir yapıcı metodun istenmesinin temel nedeni, generic repository pattern'inde ve
    //özellikle dependency injection (bağımlılık enjeksiyonu) kullanılan ortamlarda nesnelerin kolayca oluşturulabilmesi ve
    //yönetilebilmesidir. Yani aslında getRepository derken unitOfWorkde parametresiz constructora göre nesneleri oluşturup getirebilmesi için.
    
    public ArticleVisitor()
    {
        
    }
    public ArticleVisitor(Guid articleId, int visitorId)
    {
        ArticleId = articleId;
        VisitorId = visitorId;
    }
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }
    public int VisitorId { get; set; }
    public Visitor Visitor { get; set; }
}