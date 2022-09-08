using DiscordBotWebApi.Bot;
using DiscordBotWebApi.Options;
using DiscordBotWebApi.Options.Shikimory;
using DTOModels;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Services;
using Infrastructure.Services.Administration;
using Infrastructure.Services.BackgroundServices;
using Interfaces;
using Interfaces.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddTransient<IAuditService, AuditService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IShikimoryService, ShikimoryService>();
builder.Services.AddTransient<IGetService, GetService>();
builder.Services.AddTransient<IUpdateService, UpdateService>();
builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<IAdministrationService, AdministrationService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
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
