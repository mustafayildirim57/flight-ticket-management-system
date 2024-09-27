using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lam�n� DI sistemine ekliyoruz
builder.Services.AddDbContext<FlightProjectAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightProjectAdminPortalDb")));

// Servisleri Dependency Injection'a ekliyoruz
builder.Services.AddScoped<AuthService>();       // AuthService i�in DI
builder.Services.AddScoped<UserService>();       // UserService i�in DI
builder.Services.AddScoped<FlightService>();     // FlightService i�in DI
builder.Services.AddScoped<ReservationService>(); // ReservationService i�in DI

// CORS yap�land�rmas� ekleniyor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Di�er servisler ve swagger yap�land�rmas�
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS middleware'ini ekliyoruz
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
