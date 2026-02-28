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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
