var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<LoginService>();

var app = builder.Build();

app.MapControllers();

app.MapGet("/" , () => "Hello world 🚀");

app.Run("http://localhost:5000");
