using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class UserMap : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

        // Maps to the AspNetUsers table
        builder.ToTable("AspNetUsers");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        var SuperAdmin = new AppUser()
        {
            Id = Guid.Parse("C1E1C41C-2F28-436A-9FC7-3EAA4567C792"),
            UserName = "superadmin@gmail.com",
            NormalizedUserName = "superadmin@gmail.com".ToUpper(),
            Email = "superadmin@gmail.com",
            NormalizedEmail = "superadmin@gmail.com".ToUpper(),
            PhoneNumber = "+903829481902830",
            FirstName = "Super Admin",
            LastName = "Akyüz",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()

        };
        SuperAdmin.PasswordHash = CreatePasswordHash(SuperAdmin, "123456");
        
        var NormalUser = new AppUser()
        {
            Id = Guid.Parse("6EEE8F25-7132-4211-A212-0CF4E83900F7"),
            UserName = "normaluser@gmail.com",
            NormalizedUserName = "normaluser@gmail.com".ToUpper(),
            Email = "normaluser@gmail.com",
            NormalizedEmail = "normaluser@gmail.com".ToUpper(),
            PhoneNumber = "+9053728293938",
            FirstName = "Normal User",
            LastName = "Akyüz",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        NormalUser.PasswordHash = CreatePasswordHash(NormalUser, "111111");

        var Admin = new AppUser()
        {
            Id = Guid.Parse("10560B88-983E-4D8F-8D55-803D7A1AD87E"),
            UserName = "admin@gmail.com",
            NormalizedUserName = "admin@gmail.com".ToUpper(),
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com".ToUpper(),
            PhoneNumber = "+90219837912742",
            FirstName = "Admin User",
            LastName = "Akyüz",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        Admin.PasswordHash = CreatePasswordHash(Admin, "987654");

        builder.HasData(SuperAdmin, NormalUser, Admin);
    }

    private string CreatePasswordHash(AppUser user, string password)
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        return passwordHasher.HashPassword(user, password);
    }
}