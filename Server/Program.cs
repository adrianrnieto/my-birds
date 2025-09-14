using Microsoft.Extensions.FileProviders;
using MyBirds.Domain.Shared;
using MyBirds.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IBirdsService, BirdsService>();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureHostedServices();
builder.Services.ConfigureApplicationHandlers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IDomainEvent).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.MapStaticAssets();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"C:\Users\Adrian\Pictures\FX82\Birds"),
    RequestPath = "/photos"
});

app.MapFallbackToFile("index.html");

app.UseEntityFrameworkMigrations();

app.Run();
