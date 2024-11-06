using Microsoft.EntityFrameworkCore;
using TodoApp.AppDataContext;
using TodoApp.Interface;
using TodoApp.Middleware;
using TodoApp.Models;
using TodoApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Bind the configuration section to the settings class
// builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Get the connection string from appsettings.json
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = "Server=localhost; User ID=emmanuel; Password=EmmanuelMuuo3!@#$; Database=todo";

// Register the DbContext with MySQL
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddLogging();

builder.Services.AddScoped<ITodoServices, TodoServices>();

var app = builder.Build();
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();