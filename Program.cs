using Microsoft.AspNetCore.Components.Server;
using RecipeShopper.Components;
using RecipeShopper.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<RecipeService>((serviceProvider, client) =>
{
    var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext?.Request;

    if (request != null)
    {
        var baseUri = $"{request.Scheme}://{request.Host}/";
        client.BaseAddress = new Uri(baseUri);
    }
});
builder.Services.AddSingleton<Auth>();
builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
