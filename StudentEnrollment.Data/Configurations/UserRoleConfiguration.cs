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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "dfd93910-e198-4b0f-b36c-9b1e34a22134",
                    UserId = "17b5d3bf-0bbc-4f74-969d-7f5dded7b8e8"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "1c2c2a8f-863e-4d7c-9d6b-2459591fe23a",
                    UserId = "1745ef0e-1ada-4a40-8e32-3059020176a1"
                }
                );
        }
    }
}
