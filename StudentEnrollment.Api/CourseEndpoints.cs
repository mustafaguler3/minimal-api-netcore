using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (VtContext db) =>
        {
            var data = new List<CourseDto>();
            var courses = await db.Courses.ToListAsync();

            foreach (var course in courses)
            {
                data.Add(new CourseDto
                {
                    Title = course.Title,
                    Credits = course.Credits,
                    Id = course.Id,
                });
            }

            return data;
        })
        .WithName("GetAllCourses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Course>, NotFound>> (int id, VtContext db) =>
        {
            return await db.Courses.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Course model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Course course, VtContext db) =>
        {
            var affected = await db.Courses
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Title, course.Title)
                  .SetProperty(m => m.Credits, course.Credits)
                  .SetProperty(m => m.Id, course.Id)
                  .SetProperty(m => m.CreatedDate, course.CreatedDate)
                  .SetProperty(m => m.CreatedBy, course.CreatedBy)
                  .SetProperty(m => m.ModifiedDate, course.ModifiedDate)
                  .SetProperty(m => m.ModifiedBy, course.ModifiedBy)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", async (CourseDto courseDto, VtContext db) =>
        {
            var course = new Course()
            {
                Title = courseDto.Title,
                Credits= courseDto.Credits,
            };

            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Course/{course.Id}",course);
        })
        .WithName("CreateCourse")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VtContext db) =>
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
