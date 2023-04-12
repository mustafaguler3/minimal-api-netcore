using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Concrete
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(VtContext db) : base(db)
        {
        }

        public async Task<Course> GetStudentList(int courseId)
        {
            var course = await _db.Courses.Include(i => i.Enrollments).ThenInclude(i => i.Student).FirstOrDefaultAsync(i=>i.Id == courseId);

            return course;
        }
    }
}
