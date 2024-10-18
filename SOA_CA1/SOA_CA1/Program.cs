using Blazored.LocalStorage;
using Blazored.SessionStorage;
using SOA_CA1;
using SOA_CA1.Clients.Models;
using SOA_CA1.Components;
using SOA_CA1.Services;
using SOA_CA1.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(builder.Configuration);

// Add services to the container.
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();


builder.Services.AddScoped<UserSession>();

builder.Services.AddScoped<IGoogleBooksService, GoogleBooksService>();
builder.Services.AddScoped<INewsAPIService, NewsAPIService>();
builder.Services.AddScoped<IUserService, UserService>();
var googleApiKey = builder.Configuration["GoogleBookAPI:APIKey"];
var googleBaseUrl= builder.Configuration["GoogleBookAPI:Base_Url"];

var newsApiKey = builder.Configuration["NewsAPI:APIKey"];
var newsBaseUrl= builder.Configuration["NewsAPI:Base_Url"];

//builder.Services.AddSingleton

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
