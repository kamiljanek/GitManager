using System.Reflection;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using GitIssueBridge;
using GitIssueBridge.Options;
using GitIssueBridge.Services;
using GitManager.Application.Configuration;
using GitManager.Infrastructure.Middlewares;
using GitManagerApi.Filters;
using GitManagerApi.Swagger;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GitManagerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        builder.Services.AddControllers(opt => { opt.Filters.Add<ErrorHandlingFilterAttribute>(); })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(AssembliesHook).Assembly, Assembly.GetExecutingAssembly()); });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("GitManagerApi", policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        builder.Services.AddGitIssueManager(options =>
        {
            options.AddGitHubConfig(new GitHubOptions {Owner = "kamiljanek", Repo = "GitManager", AccessToken = builder.Configuration["Git:AccessToken"]});
        });
        
        builder.Services.AddScoped<IGitIssueService>(sp =>
        {
            var factory = sp.GetRequiredService<GitIssueServiceFactory>();
            return factory.Create(EGitServiceType.GitHub);
        });

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptionsProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in descriptionsProvider.ApiVersionDescriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
                options.RoutePrefix = "swagger";
            }
        });
        
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();
        app.UseCors("GitManagerApi");

        app.MapControllers();

        app.Run();
    }
}