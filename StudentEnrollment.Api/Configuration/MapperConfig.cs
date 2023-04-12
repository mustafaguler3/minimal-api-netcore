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
        }
    }
}
