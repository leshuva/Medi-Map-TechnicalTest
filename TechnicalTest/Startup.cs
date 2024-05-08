using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechnicalTest.ApplicationServices;
using TechnicalTest.Domain_Services;

namespace TechnicalTest;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped<IErrorLogger, ErrorLogger>();
        // Application Services
        services.AddScoped<IPatientApplicationService, PatientApplicationService>();
        services.AddScoped<IMedicationApplicationService, MedicationApplicationService>();
        
        // Domain Services
        services.AddScoped<IPatientDomainService, PatientDomainService>();
        services.AddScoped<IMedicationDomainService, MedicationDomainService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}