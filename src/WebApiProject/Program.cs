using appDefinitions;
using DbLayer;
using Microsoft.EntityFrameworkCore;
using WeavrAccounts;

namespace WebApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Tracking is disabled for simplicity.
            // It helps when updating records repeatedly.
            builder.Services.AddDbContext<AppDbContext>(o => 
            o.UseNpgsql(builder.Configuration["DBConnection"])
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            builder.Services.AddScoped<IUserManagement, UserAccountManagement>();
            builder.Services.AddScoped<IDbService, DbService>();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "allowLocalHost",
            //                      policy =>
            //                      {
            //                          policy.WithOrigins(
            //                              "http://localhost:3000",
            //                              "https://localhost");
            //                      });
            //});
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            if (!String.IsNullOrEmpty(builder.Configuration["ApplicationInsightsConn"]))
            {
                builder.Logging.AddApplicationInsights(
                  configureTelemetryConfiguration: (config) =>
                config.ConnectionString = builder.Configuration["ApplicationInsightsConn"],
                configureApplicationInsightsLoggerOptions: (options) => { });
            }

            builder.Services.AddOptions<WeavrDetails>().Bind(builder.Configuration.GetSection("WeavrSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Got annoyed at cors
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin because it's a pain to get same origin localhost working.
                .AllowCredentials()); 

                app.UseSwagger();
                app.MapControllers();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.Run();
        }
    }
}
