using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiledTest.Data;
using FiledTest.Repositories.PaymentRepo.RepositoryImplementation;
using FiledTest.Repositories.PaymentRepo.RepositoryInterface;
using FiledTest.Services.GateWay.ServiceImplemntation;
using FiledTest.Services.GateWay.ServiceInterface;
using FiledTest.Services.ServiceImplementation;
using FiledTest.Services.ServiceInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;

namespace FiledTest.Api
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
            var configurationSection = Configuration.GetSection("ConnectionStrings:PaymentDb");
            services.AddDbContext<PaymentContext>(options => options.UseSqlServer(configurationSection.Value));


            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPaymentRequestService, PaymentRequestService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentStateRepository, PaymentStateRepository>();
            services.AddAutoMapper(c => { c.AddProfile<Services.Profile.PaymentProfile>(); c.AddProfile<Services.Profile.PaymentStateProfile>(); }, typeof(Startup));
            services.AddSwaggerGen(c => {
                
            });
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sheroze Payment Api");
                
            });
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
