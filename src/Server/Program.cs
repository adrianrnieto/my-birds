using FastEndpoints;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MyBirds.Application.Options;
using MyBirds.Domain.Shared;
using MyBirds.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IBirdsService, BirdsService>();
builder.Services.Configure<PhotoStorageOptions>(builder.Configuration.GetSection(PhotoStorageOptions.SectionName));
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureHostedServices();
builder.Services.ConfigureApplicationHandlers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IDomainEvent).Assembly));

var app = builder.Build();
var photoStorageOptions = app.Services.GetRequiredService<IOptions<PhotoStorageOptions>>().Value;

var photosRootPath = Path.GetFullPath(photoStorageOptions.PhotoRootPath);
var thumbnailsRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, photoStorageOptions.ThumbnailOutputFolderName));
Directory.CreateDirectory(thumbnailsRootPath);

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
app.MapFastEndpoints();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(photosRootPath),
    RequestPath = "/photos",
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers[HeaderNames.CacheControl] =
            $"public,max-age={TimeSpan.FromDays(photoStorageOptions.PhotosCacheMaxAgeDays).TotalSeconds:F0}";
    }
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(thumbnailsRootPath),
    RequestPath = "/thumbnails",
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers[HeaderNames.CacheControl] =
            $"public,max-age={TimeSpan.FromDays(photoStorageOptions.ThumbnailsCacheMaxAgeDays).TotalSeconds:F0}";
    }
});

app.MapFallbackToFile("index.html");

app.UseEntityFrameworkMigrations();

await app.RunAsync();
