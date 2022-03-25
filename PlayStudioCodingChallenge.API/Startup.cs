using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlayStudioCodingChallenge.API.BsonMapper;
using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Application.Services;
using PlayStudioCodingChallenge.Application.Services.QuestService;
using PlayStudioCodingChallenge.Persistence.DbConfiguration;
using PlayStudioCodingChallenge.Persistence.Repositories;

namespace PlayStudioCodingChallenge.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BsonMapperConfig.AddBsonMapperConfig();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlayStudio Coding Challenge", Version = "v1" });
            });
            services.Configure<MongoConfiguration>(Configuration.GetSection("MongoDatabase"));
            // Register services
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<ISeedingDataService, SeedingDataService>();
            // Register repositories
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            // Seed data
            var provider = services.BuildServiceProvider();
            var seedDataService = provider.GetRequiredService<ISeedingDataService>();
            seedDataService.InitializeData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayStudioCodingChallenge.API v1"));
            }
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
