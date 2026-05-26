using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Api.Configuration.Binders;
using RacingLeagueHub.Api.Configuration.Serialization;
using RacingLeagueHub.Api.Middleware;
using RacingLeagueHub.Api.Startup;
using RacingLeagueHub.Application.Services;
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
builder.Services.AddInfrastructureServices();
builder.Services.RegisterAppLayerServices();
builder.Services.RegisterAWSServices(builder.Configuration); 

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AdventureContext>(options => 
                    options.UseNpgsql(builder.Configuration
                            .GetConnectionString("DefaultConnection"))
                            .UseSnakeCaseNamingConvention());

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors("AngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
