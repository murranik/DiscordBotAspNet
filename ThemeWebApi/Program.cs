using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ThemeWebApi.Database;
using ThemeWebApi.Interfaces;
using ThemeWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

builder.Services.AddDbContext<ThemeDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ThemesDatabase")));
builder.Services.AddTransient<IThemesService, ThemesSevrice>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                      });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Themes API",
        Description = "",
    });
});


var app = builder.Build();

app.UseRouting();
app.UseCors("_myAllowSpecificOrigins");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
