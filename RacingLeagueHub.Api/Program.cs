using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Api.Configuration.Binders;
using RacingLeagueHub.Api.Configuration.Serialization;
using RacingLeagueHub.Api.Startup;
using RacingLeagueHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.ModelBinderProviders.Insert(0, new EncryptedIdModelBinderProvider());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new EncryptedIdJsonConverter());
    });

builder.Services.AddRepositories(typeof(Program).Assembly);
builder.Services.AddEntityHandlers(typeof(Program).Assembly);

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

builder.Services.RegisterAWSServices(builder.Configuration);

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
