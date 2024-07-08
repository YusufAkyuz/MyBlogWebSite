using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        builder.ToTable("AspNetUserRoles");

        builder.HasData(new AppUserRole()
            {
                UserId = Guid.Parse("C1E1C41C-2F28-436A-9FC7-3EAA4567C792"),
                RoleId = Guid.Parse("84D1C5CD-7D85-49AF-91E0-62D57C3ECE16")
            },
            new AppUserRole()
            {
                UserId = Guid.Parse("10560B88-983E-4D8F-8D55-803D7A1AD87E"),
                RoleId = Guid.Parse("0636CC5D-4297-4E98-AF36-B38C1A01262D"),
            }, new AppUserRole()
            {
                UserId = Guid.Parse("6EEE8F25-7132-4211-A212-0CF4E83900F7"),
                RoleId = Guid.Parse("07F1169A-C805-419A-BF44-B7A2E5BBEAAB")
            });
    }
}