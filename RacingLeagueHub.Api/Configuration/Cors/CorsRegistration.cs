namespace RacingLeagueHub.Api.Configuration.Cors;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddConfiguredCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration
            .GetSection($"Cors:{CorsPolicies.AngularApp}")
            .Get<CorsOptions>();

        if (corsOptions is null)
        {
            throw new InvalidOperationException($"Missing CORS configuration section: Cors:{CorsPolicies.AngularApp}");
        }

        if (corsOptions.AllowedOrigins.Length == 0)
        {
            throw new InvalidOperationException("CORS requires at least one allowed origin.");
        }

        if (corsOptions.AllowCredentials 
            && corsOptions.AllowedOrigins.Any(origin => origin == "*"))
        {
            throw new InvalidOperationException("CORS cannot safely use wildcard origins together with credentials.");
        }

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicies.AngularApp, policy =>
            {
                policy.WithOrigins(corsOptions.AllowedOrigins);

                if (corsOptions.AllowedMethods.Length > 0)
                {
                    policy.WithMethods(corsOptions.AllowedMethods);
                }
                else
                {
                    policy.AllowAnyMethod();
                }

                if (corsOptions.AllowedHeaders.Length > 0)
                {
                    policy.WithHeaders(corsOptions.AllowedHeaders);
                }
                else
                {
                    policy.AllowAnyHeader();
                }

                if (corsOptions.AllowCredentials)
                {
                    policy.AllowCredentials();
                }
                else
                {
                    policy.DisallowCredentials();
                }
            });
        });

        return services;
    }
}