using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Database;
using RacingLeagueHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddDbContext<AdventureContext>(options => 
                    options.UseNpgsql(builder.Configuration
                            .GetConnectionString("DefaultConnection"))
                            .UseSnakeCaseNamingConvention());

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AngularApp");

app.MapControllers();

app.Run();
