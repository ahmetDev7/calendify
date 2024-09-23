using calendify.Data;
using Microsoft.EntityFrameworkCore; // Ensure you add this
using Npgsql.EntityFrameworkCore.PostgreSQL; // Ensure Npgsql is installed

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<LoginService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.MapControllers();

app.MapGet("/" , () => "Hello world 🚀");

app.Run("http://localhost:5000");
