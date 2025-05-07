using Blazored.LocalStorage;
using IndoorMappingWebsite.Components;
using IndoorMappingWebsite.Services;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;  // Habilita erros detalhados
    });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserLocService, UserLocService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBeaconService, BeaconService>();
builder.Services.AddScoped<IPathService, PathService>();
builder.Services.AddScoped<IPathMapService, PathMapService>();
builder.Services.AddScoped<IAccessibilityService, AccessibilityService>();
builder.Services.AddScoped<IInfraestruturaService, InfraestruturaService>();
builder.Services.AddScoped<ITipoInfraestruturaService, TipoInfraestruturaService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri("https://isepindoornavigationapi-vgq7.onrender.com") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = new FileExtensionContentTypeProvider
    {
        Mappings = { [".geojson"] = "application/geo+json" }
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
