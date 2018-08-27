using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            var connectionString = @"Data Source=WIN-3GOMQNE45BK\MYSERVER;Initial Catalog=CityInfoDb;Integrated Security=False;User Id=sa;Password=Pa$$w0rd;MultipleActiveResultSets=True";
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            //loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration(@"C:\dev\Spikes\CityInfo\CityInfo\nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
