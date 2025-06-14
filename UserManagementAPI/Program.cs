using UserManagementAPI.Models;
using UserManagementAPI.Services;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserService>(o =>
{
    // Sample data for demonstration purposes
    List<UserData> _users = new List<UserData>()
    {
        new UserData { Id = 1, Name = "John Doe", Email = "" },
        new UserData { Id = 2, Name = "Jane Smith", Email = "" },
        new UserData { Id = 3, Name = "Alice Johnson", Email = "" }
    };
    return new UserManager(_users);
});

var app = builder.Build();

// Add the exception handling middleware FIRST
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Add the token validation middleware after exception handling, before other middlewares
app.UseMiddleware<TokenValidationMiddleware>();

// Add the request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.MapControllers();

app.Run();