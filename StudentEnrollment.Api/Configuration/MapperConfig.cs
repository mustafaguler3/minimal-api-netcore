using AutoMapper;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        { 
            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Course,CreateCourseDto>().ReverseMap();
            CreateMap<Course, CourseDetailsDto>().ForMember(i => i.Students, i => i.MapFrom(course => course.Enrollments.Select(stu => stu.Student)));

            CreateMap<Student, StudentDetailsDto>().ForMember(i => i.Courses, i => i.MapFrom(student => student.Enrollments.Select(course => course.Course)));
        }
    }
}
