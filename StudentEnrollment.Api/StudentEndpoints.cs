using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Abstract;
using AutoMapper;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (IStudentRepository studentRepository,IMapper mapper) =>
        {
            var students = studentRepository.GetAllAsync();
            var data = mapper.Map<List<StudentDto>>(students);
            return data;
        })
        .WithName("GetAllStudents")
        .WithOpenApi();

        group.MapGet("/GetDetails/{id}", async (int id, IStudentRepository studentRepository, IMapper mapper) =>
        {
            return await studentRepository.GetStudentDetails(id) is Student model ? Results.Ok(mapper.Map<StudentDetailsDto>(model)) : Results.NotFound();
        })
        .WithName("GetAllStudents")
        .Produces<StudentDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Student>, NotFound>> (int id, VtContext db) =>
        {
            return await db.Students.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Student student, VtContext db) =>
        {
            var affected = await db.Students
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.FirstName, student.FirstName)
                  .SetProperty(m => m.LastName, student.LastName)
                  .SetProperty(m => m.DateOfBirth, student.DateOfBirth)
                  .SetProperty(m => m.IdNumber, student.IdNumber)
                  .SetProperty(m => m.Picture, student.Picture)
                  .SetProperty(m => m.Id, student.Id)
                  .SetProperty(m => m.CreatedDate, student.CreatedDate)
                  .SetProperty(m => m.CreatedBy, student.CreatedBy)
                  .SetProperty(m => m.ModifiedDate, student.ModifiedDate)
                  .SetProperty(m => m.ModifiedBy, student.ModifiedBy)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        group.MapPost("/", async (CreateStudentDto studentDto, IStudentRepository studentRepository,IMapper mapper) =>
        {
            var student = mapper.Map<Student>(studentDto);
            await studentRepository.AddAsync(student);
            return TypedResults.Created($"/api/Student/{student.Id}",student);
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, VtContext db) =>
        {
            var affected = await db.Students
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}
