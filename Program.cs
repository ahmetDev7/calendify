var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/" , () => "Hello world 🚀");

app.Run("http://localhost:5000");
