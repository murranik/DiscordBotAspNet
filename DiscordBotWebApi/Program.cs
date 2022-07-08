using DiscordBotWebApi.Bot;
using DiscordBotWebApi.Options;
using DTOModels;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Services;
using Infrastructure.Services.Administration;
using Infrastructure.Services.BackgroundServices;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Options;
using Options.Shikimory;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<DanbooruOptions>(builder.Configuration.GetSection(DanbooruOptions.Title))
    .Configure<ShikimoryOptions>(builder.Configuration.GetSection(ShikimoryOptions.Title))
    .Configure<DiscordOptions>(builder.Configuration.GetSection(DiscordOptions.Title))
    .Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Title))
    .Configure<ShikimoryClientOptions>(builder.Configuration.GetSection(ShikimoryClientOptions.Title));

var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

builder.Services.AddDbContext<DiscordBotContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DiscordDatabase")));
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<CommandsHandler>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ShikimoryService>();
builder.Services.AddTransient<GetService>();
builder.Services.AddTransient<CommandService>();
builder.Services.AddTransient<AdministrationService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddTransient<SmtpService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddHostedService<UpdateUserStatisticHostedServices>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddClient(configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

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
        Title = "Discord bot API",
        Description = "",
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();
app.UseCors("_myAllowSpecificOrigins");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
