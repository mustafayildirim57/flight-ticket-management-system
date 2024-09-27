using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlamýný DI sistemine ekliyoruz
builder.Services.AddDbContext<FlightProjectAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightProjectAdminPortalDb")));

// Servisleri Dependency Injection'a ekliyoruz
builder.Services.AddScoped<AuthService>();       // AuthService için DI
builder.Services.AddScoped<UserService>();       // UserService için DI
builder.Services.AddScoped<FlightService>();     // FlightService için DI
builder.Services.AddScoped<ReservationService>(); // ReservationService için DI

// CORS yapýlandýrmasý ekleniyor
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

// Diðer servisler ve swagger yapýlandýrmasý
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
