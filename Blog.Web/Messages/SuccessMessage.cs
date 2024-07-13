namespace Blog.Web.Messages;

public static class SuccessMessage
{
    public static class ArticleMessage
    {
        public static string Add(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla oluşturuldu :)";
        }
        public static string Update(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla güncellendi :)";
        }
        public static string Delete(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarıyla silindi :)";
        }
    }
    public static class CategoryMessage
    {
        public static string Add(string categoryTitle)
        {
            return $"{categoryTitle} başlıklı kategori başarıyla oluşturuldu :)";
        }
        public static string Update(string categoryTitle)
        {
            return $"{categoryTitle} başlıklı kategori başarıyla güncellendi :)";
        }
        public static string Delete(string categoryTitle)
        {
            return $"{categoryTitle} başlıklı kategori başarıyla silindi :)";
        }
    }
    
}