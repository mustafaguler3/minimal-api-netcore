using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Api;
using StudentEnrollment.Api.Configuration;
using StudentEnrollment.Data.Abstract;
using StudentEnrollment.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VtContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddAutoMapper(typeof(MapperConfig));
// Add services to the container.

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

//app.MapControllers();
app.MapGet("/courses", async (VtContext context) =>
{
    return await context.Courses.ToListAsync();
});

app.MapGet("/courses/{id}", async (VtContext context,int id) =>
{
    return await context.Courses.FindAsync(id) is Course course ? Results.Ok(course) : Results.NotFound();
});

app.MapPost("/courses", async (VtContext context, Course course) =>
{
    await context.AddAsync(course);
    await context.SaveChangesAsync();

    return Results.Created($"/courses/{course.Id}",course);
});

app.MapPut("/courses/{id}", async (VtContext context, Course course,int id) =>
{
    var courseExists = await context.Courses.FindAsync(id);
    if (courseExists == null)
    {
        return Results.NotFound();
    }
    courseExists.Credits = course.Credits;
    courseExists.Title = course.Title;

    context.Update(course);
    await context.SaveChangesAsync();

    return Results.Created($"/courses/{course.Id}", course);
});

app.MapDelete("/courses/{id}", async (VtContext context, int id) =>
{
    var courseExists = await context.Courses.FindAsync(id);
    if (courseExists == null)
    {
        return Results.NotFound();
    }
    context.Remove(courseExists);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapStudentEndpoints();

app.MapCourseEndpoints();


app.Run();
