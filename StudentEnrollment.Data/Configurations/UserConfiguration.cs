using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                new User
                {
                    Id = "17b5d3bf-0bbc-4f74-969d-7f5dded7b8e8",
                    Email = "school@localhost.com",
                    NormalizedEmail = "SCHOOL@LOCALHOST.COM",
                    NormalizedUserName = "SCHOOLADMIN@LOCALHOST.COM",
                    UserName = "schooladmin@localhost.com",
                    FirstName = "School",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null,"123"),
                    EmailConfirmed = true,
                },
                new User
                {
                    Id = "1745ef0e-1ada-4a40-8e32-3059020176a1",
                    Email = "schooluser@localhost.com",
                    NormalizedEmail = "SCHOOLUSER@LOCALHOST.COM",
                    NormalizedUserName = "SCHOOLUSER@LOCALHOST.COM",
                    UserName = "schooluser@localhost.com",
                    FirstName = "School",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "123"),
                    EmailConfirmed = true,
                }
                );
        }
    }
}
