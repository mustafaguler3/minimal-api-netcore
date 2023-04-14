using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Abstract;
using AutoMapper;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Api.Services;
using FluentValidation;

namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (IStudentRepository studentRepository, IMapper mapper) =>
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

        group.MapPut("/{id}", async (int id, StudentDto studentDto, IStudentRepository studentRepository,IMapper mapper, IValidator<StudentDto> validator, IFileUpload fileUpload) =>
        {
            var validationResult = await validator.ValidateAsync(studentDto);
            var errors = new List<ErrorResponseDto>();

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var foundModel = await studentRepository.GetAsync(id);
            if (foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(studentDto, foundModel);

            if (studentDto.Picture != null)
            {
                foundModel.Picture = fileUpload.UploadFile(studentDto.Picture, studentDto.OriginalFileName);
            }

            await studentRepository.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        group.MapPost("/", async (CreateStudentDto studentDto, IStudentRepository studentRepository, IMapper mapper,IValidator<CreateStudentDto> validator,IFileUpload fileUpload) =>
        {
            var validationResult = await validator.ValidateAsync(studentDto);
            var errors = new List<ErrorResponseDto>();

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            var student = mapper.Map<Student>(studentDto);

            student.Picture = fileUpload.UploadFile(studentDto.Picture, studentDto.OriginalFileName);

            await studentRepository.AddAsync(student);
            return TypedResults.Created($"/api/Student/{student.Id}", student);
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
