using Microsoft.EntityFrameworkCore;
using Maxula_project.Components;
using Maxula_project.Services.ActivityService;
using Maxula_project.Services.UserService;
using Maxula_project.Utils.ConsoleLogger;
using Syncfusion.Blazor;
using BlazorDownloadFile;
using Maxula_project.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Configure app settings based on environment prep
var env = builder.Environment;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

if (env.IsProduction())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins("https://maxula.azurewebsites.net/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

// read this
// https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

builder.Services.AddDbContextFactory<DataContext>((options) =>
{
    string connectionString;

    if (env.IsProduction())
    {
        connectionString = builder.Configuration.GetConnectionString("NeonConnection");
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    }

    options.UseNpgsql(connectionString);
});

builder.Services.AddQuickGridEntityFrameworkAdapter();

// adding custom js console log for browser console
builder.Services.AddScoped<IConsoleLoggerService, ConsoleLoggerService>();

// adding mapper then services

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IActivityService, ActivityService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<DateService>();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddBlazorDownloadFile();


var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SYNCFUSION_LICENSE_KEY"]);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// redirect bad routes to a 404 page
app.UseStatusCodePagesWithRedirects("/404");

app.UseHttpsRedirection();

app.UseStaticFiles();

// Add this line for routing asp net ( the places are important, routing then anti forgery then controllers endpoints)
app.UseRouting();

// Use CORS policy in the HTTP request pipeline, the order matters os google it xD
app.UseCors();

app.UseAntiforgery();

// Ensure that your API controllers are routed correctly
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();