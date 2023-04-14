using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Dtos;
using AutoMapper;
using StudentEnrollment.Data.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/",async (ICourseRepository courseRepository, IMapper mapper) =>
        {
            var courses = await courseRepository.GetAllAsync();
            var data = mapper.Map<List<CourseDto>>(courses);

            return data;
        }).AllowAnonymous()
        .WithName("GetAllCourses")
        .WithOpenApi();

        group.MapGet("/{id}", async (int id, ICourseRepository courseRepository, IMapper mapper) =>
        {
            return await courseRepository.GetAsync(id) is Course course ? Results.Ok(mapper.Map<CourseDto>(course)) : Results.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi();


        group.MapGet("/GetStudents/{id}", async (int id, ICourseRepository courseRepository, IMapper mapper) =>
        {
            return await courseRepository.GetStudentList(id) is Course model ? Results.Ok(mapper.Map<CourseDetailsDto>(model)) : Results.NotFound();
        })
        .WithName("GetCourseStudentsById")
        .Produces<CourseDetailsDto>(StatusCodes.Status200OK)
        .Produces<CourseDetailsDto>(StatusCodes.Status404NotFound)
        .WithOpenApi();

        group.MapPut("/{id}", async (int id, CourseDto courseDto, VtContext db, IMapper mapper) =>
        {
            var foundModel = await db.Courses.FindAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            mapper.Map(courseDto, foundModel);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", [Authorize] async (CreateCourseDto courseDto, VtContext db, IMapper mapper) =>
        {
            var course = mapper.Map<Course>(courseDto);

            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Course/{course.Id}", course);
        })
        .WithName("CreateCourse")
        .WithOpenApi();

        group.MapDelete("/{id}",[Authorize(Roles = "Administrator")] async Task<Results<Ok, NotFound>> (int id, VtContext db) =>
        {
            var affected = await db.Courses
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
