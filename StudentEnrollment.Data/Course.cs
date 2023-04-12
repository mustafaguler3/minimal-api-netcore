using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment.Data
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
