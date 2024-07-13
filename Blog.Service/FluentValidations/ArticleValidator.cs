using Blog.Entity.Entities;
using FluentValidation;

namespace Blog.Service.FluentValidations;


public class ArticleValidator : AbstractValidator<Article>
{
    /*
     * Validator, bir nesnenin belirli kurallara ve kısıtlamalara uygun olup olmadığını kontrol eden bir bileşendir.
     * 
     * Validatorlar genellikle girdi verilerini doğrulamak için kullanılır ve
      veritabanına veya iş mantığına geçmeden önce verilerin geçerliliğini kontrol ederler.
       
     *  Örneğin, bir kullanıcı kayıt formunda kullanıcının girdiği e-posta adresinin geçerli bir e-posta 
       formatında olup olmadığını veya bir parolanın belirli uzunluk ve karakter kurallarına uygun olup olmadığını 
       doğrulamak için validator kullanabilirsiniz.
     */
    public ArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(150)
            .WithName("Başlık");

        RuleFor(x => x.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(150)
            .WithName("İçerik");
    }
}