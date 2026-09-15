using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Api.Configuration.Cors;
using RacingLeagueHub.Api.Middleware;
using RacingLeagueHub.Application;
using RacingLeagueHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddEntityHandlers(typeof(Program).Assembly);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddQueriesAndCommands();
builder.Services.AddApplicationServices();
builder.Services.AddAwsStorage(builder.Configuration); 

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredCors(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors(CorsPolicies.AngularApp);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
