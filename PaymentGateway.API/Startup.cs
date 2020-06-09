using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentGateway.Data;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Services;
using PaymentGateway.Services.PaymentProcessing;

namespace PaymentGateway.API
{
  public class Startup
  {
    private const string ApplicationTitle = "Checkout Payment Gateway";
    private const string ApplicationVersion = "v1";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      SetDependencies(services);

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc(ApplicationVersion, new OpenApiInfo { Title = ApplicationTitle, Version = ApplicationVersion });
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApplicationTitle} {ApplicationVersion}");

#if DEBUG
        c.RoutePrefix = string.Empty;
#endif
      });

      loggerFactory.AddFile("Logs/PaymentGateway-{Date}.txt");

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }

    private void SetDependencies(IServiceCollection services)
    {
      var useMockedPaymentProcessingService = Configuration.GetValue<bool>("UseMockedPaymentProcessingService");

      if (useMockedPaymentProcessingService)
      {
        services.AddScoped<IPaymentProcessingService, FakePaymentProcessingService>();
      }
      else
      {
        services.AddScoped<IPaymentProcessingService, PaymentProcessingService>();
      }

      services.AddScoped<SQLiteDbContext>();
      services.AddScoped<IPaymentRequestRepository, PaymentRequestRepository>();
      services.AddScoped<IPaymentRequestService, PaymentRequestService>();
    }
  }
}
