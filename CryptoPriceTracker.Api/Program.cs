using CryptoPriceTracker.Api.Contract.Repository;
using CryptoPriceTracker.Api.Contract.Service;
using CryptoPriceTracker.Api.Entity.Data;
using CryptoPriceTracker.Api.Repository;
using CryptoPriceTracker.Api.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=crypto.db"));
builder.Services.AddScoped<CryptoPriceService>();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Build Services
builder.Services.AddTransient<ICryptoPriceService, CryptoPriceService>();

//Build Repositories
builder.Services.AddTransient<ICryptoAssetRepository, CryptoAssetRepository>();
builder.Services.AddTransient<ICryptoPriceHistoryRepository, CryptoPriceHistoryRepository>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllers();
app.Run();