using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Threading.Tasks;
using TechBuddy.Middlewares.RequestResponse;

namespace RequestResponseLibraryTest.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RequestResponseLibraryTest.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RequestResponseLibraryTest.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.AddTBRequestResponseMiddleware(opt => 
            {
                //opt.UseHandler(async context => 
                //{
                //    System.Console.WriteLine("Handler Worked");
                //    System.Console.WriteLine(JsonSerializer.Serialize(context));

                //    await Task.CompletedTask;
                //});

                opt.UseLogger(app.ApplicationServices.GetService<ILoggerFactory>(), opt =>
                {
                    opt.LoggerCategoryName = "MyCustomCategoryName";
                    opt.LoggingLevel = LogLevel.Error;

                    opt.UseSeparateContext = true;

                    opt.LoggingFields.Add(LogFields.Path);
                    opt.LoggingFields.Add(LogFields.QueryString);
                    opt.LoggingFields.Add(LogFields.Request);
                    opt.LoggingFields.Add(LogFields.Response);
                    opt.LoggingFields.Add(LogFields.ResponseTiming);
                    opt.LoggingFields.Add(LogFields.RequestLength);
                    opt.LoggingFields.Add(LogFields.ResponseLength);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
