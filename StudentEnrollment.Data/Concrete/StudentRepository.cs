using StudentEnrollment.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data.Concrete
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(VtContext db) : base(db)
        {
        }

        public Task<Student> GetStudentDetails(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
