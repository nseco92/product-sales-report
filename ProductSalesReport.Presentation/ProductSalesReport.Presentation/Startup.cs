using ProductSalesReport.Application.Services;
using ProductSalesReport.Domain.Interfaces.Repositories;
using ProductSalesReport.Infrastructure.Repositories;
using ProductSalesReport.Domain.Interfaces.Services;
using ProductSalesReport.Domain.Services;
using Microsoft.OpenApi.Models;
using ProductSalesReport.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductSalesReport.DistributedServices.ExternalServices;
using ProductSalesReport.Application.Interfaces.Services;
using ProductSalesReport.Presentation.Middlewares;

namespace ProductSalesReport.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<ICurrencyConverterService, CurrencyConverterService>();
            services.AddScoped<IProductTransactionService, ProductTransactionService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddLogging();
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));

            services.AddHttpClient<IRateExternalService, RateExternalService>(x =>
            {
                x.BaseAddress = new Uri(Configuration["Endpoints:Rates"]);
                x.DefaultRequestHeaders.Clear();
            });

            services.AddHttpClient<ITransactionExternalService, TransactionExternalService>(x =>
            {
                x.BaseAddress = new Uri(Configuration["Endpoints:Transactions"]);
                x.DefaultRequestHeaders.Clear();
            });

            services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(
                Configuration.GetConnectionString("DbConnection"),
                b => b.MigrationsAssembly("ProductSalesReport.Presentation")
             ));

            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            services.AddControllers();

            services.AddAutoMapper(typeof(Application.Mapper.ProfileMapper));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "API ProductSalesReport",
                    Description = "An ASP.NET Core Web API that displays a list of products and the total sum of their sales",
                    Version = "v1" 
                });
                options.CustomSchemaIds(r => r.FullName);

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories);
                foreach (var xmlFile in xmlFiles)
                {
                    options.IncludeXmlComments(xmlFile);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("ProductSalesReport-Log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "API ProductSalesReport");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
