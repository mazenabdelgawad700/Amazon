using Amazon.Core.MiddleWares;
using Amazon.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Amazon.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region AppDbContext Service Register
        builder.Services.AddDbContext<AppDbContext>(
           options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("constr"),
                   b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
               )
       );
        #endregion

        #region Allow Cors
        var CORS = "_cors";
        builder.Services.AddCors(options =>
        {
            //options.AddPolicy(name: "CORS", policy => policy.WithOrigins("https://localhost:5173/"));
            options.AddPolicy(name: CORS, policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });
        #endregion

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        #region Middlewares
        app.UseMiddleware<ErrorHandlerMiddleware>();
        #endregion

        app.UseHttpsRedirection();

        app.UseCors(CORS);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
