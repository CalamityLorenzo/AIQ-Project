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

            builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration["DBConnection"]));

            builder.Services.AddScoped<IUserManagement, UserAccountManagement>();
            builder.Services.AddScoped<IDbService, DbService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "allowLocalHost",
                                  policy =>
                                  {
                                      policy.WithOrigins(
                                          "http://localhost:3000",
                                          "https://localhost");
                                  });
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddOptions<WeavrDetails>().Bind(builder.Configuration.GetSection("WeavrSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Got annoyed at cors
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

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
