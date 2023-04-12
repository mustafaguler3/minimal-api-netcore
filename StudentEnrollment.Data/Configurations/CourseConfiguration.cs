using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course()
                {
                    Id = 1,
                    Title = "Angular",
                    Credits=3
                },
                new Course()
                {
                    Id = 2,
                    Title = "React",
                    Credits = 3
                },
                new Course()
                {
                    Id = 3,
                    Title = "C#",
                    Credits = 4
                });
        }
    }
}
