using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using CityInfo.Extensions;
using CityInfo.Services;

namespace CityInfo
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            //.AddJsonOptions(o => //This section is just an example of overriding the default Json set up
                //{
                //    if (o.SerializerSettings != null)
                //    {
                //        // The default sets the first character of Json part to lower case.
                //        // The below code just prevents the default behaviour. The exact class names will be used in the Json
                //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;                    
                //        castedResolver.NamingStrategy = null;
                //    }
                //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            loggerFactory.AddNLog();
            //loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration(@"C:\dev\Spikes\CityInfo\CityInfo\nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
