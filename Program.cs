using FoodDelivery.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 🔧 SERVICES
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// 🔥 PORTA
app.Urls.Clear();
app.Urls.Add("http://localhost:5283");

// 🔥 STATIC FILES
app.UseDefaultFiles();
app.UseStaticFiles();

// 🔥 CORS
app.UseCors("AllowAll");

// 🔥 SWAGGER
app.UseSwagger();
app.UseSwaggerUI();

// 🔥 AUTH
app.UseAuthorization();

app.MapControllers();


// 🔥 SEED AUTOMÁTICO (AQUI ESTÁ O OURO)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(context);
}


// 🔥 ABRIR NAVEGADOR
try
{
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = "http://localhost:5283/swagger",
        UseShellExecute = true
    });
}
catch { }

app.Run();