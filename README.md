Kişisel Blog Sitesi
Bu proje, .NET Core MVC çerçevesi kullanılarak geliştirilmiş bir kişisel blog sitesidir. Proje, modern yazılım geliştirme pratiklerini ve çeşitli teknolojileri kullanarak esnek, güvenli ve sürdürülebilir bir yapıda tasarlanmıştır.

Özellikler
Kullanıcı Kimlik Doğrulama ve Yetkilendirme: ASP.NET Core Identity kullanılarak kullanıcı girişi ve rol bazlı yetkilendirme sağlanır.
Veritabanı Yönetimi: Entity Framework Core ve PostgreSQL kullanılarak veritabanı işlemleri gerçekleştirilir.
Birim İş Deseni (Unit of Work): Veritabanı işlemlerini daha düzenli ve kontrol edilebilir hale getirmek için kullanılır.
Nesne Dönüşümleri: AutoMapper kütüphanesi ile DTO ve Model dönüşümleri kolaylaştırılır.
Doğrulama: Fluent Validation kütüphanesi kullanılarak veri doğrulama işlemleri yapılır.
Kullanıcı Arayüzü: Razor ve View Components ile dinamik ve modüler bir kullanıcı arayüzü tasarlanmıştır.
Kullanılan Teknolojiler
.NET Core MVC
ASP.NET Core Identity
Entity Framework Core
PostgreSQL
AutoMapper
Fluent Validation
Razor
View Components
Kurulum ve Çalıştırma
Gereksinimler
.NET Core SDK (v6.0 veya üstü)
PostgreSQL
Visual Studio veya Visual Studio Code
Kurulum Adımları
Depoyu Klonlayın:

git clone https://github.com/kullanici_adi/proje_adi.git
cd proje_adi
Bağımlılıkları Yükleyin:

Proje dizininde, terminal veya komut istemcisinde aşağıdaki komutu çalıştırın:

dotnet restore
Veritabanı Yapılandırması:

appsettings.json dosyasındaki bağlantı dizesini (connection string) kendi PostgreSQL sunucunuza göre güncelleyin:

json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=blogDb;Username=postgres;Password=your_password"
}
Veritabanı Migrasyonları ve Güncellemeleri:

Veritabanı migrasyonlarını ve güncellemelerini uygulamak için aşağıdaki komutları çalıştırın:

dotnet ef database update
Uygulamayı Çalıştırın:

Uygulamayı çalıştırmak için:

dotnet run
Projeye Erişim:

Web tarayıcınızda http://localhost:5000 adresine giderek projeye erişebilirsiniz.

Katkıda Bulunma
Katkıda bulunmak için lütfen bir çekme isteği (pull request) gönderin. Büyük değişiklikler için, önce neyi değiştirmek istediğinizi tartışmak üzere bir konu açınız.
