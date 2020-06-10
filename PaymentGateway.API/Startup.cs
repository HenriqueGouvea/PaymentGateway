using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaymentGateway.Data;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Services;
using PaymentGateway.Domain.Settings;
using PaymentGateway.Services.PaymentProcessing;
using System.Collections.Generic;
using System.Text;

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
      services.AddCors();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      SetDependencies(services);

      services.AddSwaggerGen(
        c =>
      {
        c.SwaggerDoc(ApplicationVersion, new OpenApiInfo { Title = ApplicationTitle, Version = ApplicationVersion });

        c.AddSecurityDefinition(
          "Bearer", new OpenApiSecurityScheme
          {
            Description = @"JWT Authorization header using the Bearer scheme. <br/> 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br/>Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
          });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                 {
                                   {
                                     new OpenApiSecurityScheme
                                     {
                                       Reference = new OpenApiReference
                                                   {
                                                     Type = ReferenceType.SecurityScheme,
                                                     Id = "Bearer"
                                                   },
                                       Scheme = "oauth2",
                                       Name = "Bearer",
                                       In = ParameterLocation.Header,

                                     },
                                     new List<string>()
                                   }
                                 });
      });

      var key = Encoding.ASCII.GetBytes(Security.Secret);
      services.AddAuthentication(
        a =>
        {
          a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(
          j =>
            {
              j.RequireHttpsMetadata = false;
              j.SaveToken = true;
              j.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
              };
            }
          );
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      app.UseSwagger();

      app.UseSwaggerUI(
        c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApplicationTitle} {ApplicationVersion}");

#if DEBUG
        c.RoutePrefix = string.Empty;
#endif
      });

      loggerFactory.AddFile("Logs/PaymentGateway-{Date}.txt");

      app.UseCors(
        c =>
          c.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseAuthentication();
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
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IPaymentRequestService, PaymentRequestService>();
      services.AddScoped<IUserService, UserService>();
    }
  }
}
