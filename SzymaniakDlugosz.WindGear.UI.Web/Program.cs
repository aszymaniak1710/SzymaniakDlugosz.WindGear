using SzymaniakDlugosz.WindGear.BL;
using SzymaniakDlugosz.WindGear.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register BL Service
// BLService implicitly loads DAO from App.config via ConfigurationManager
// Ensure System.Configuration.ConfigurationManager package is referenced and App.config exists
// MUST be Singleton if using DAOMock (in-memory) to persist state across requests
builder.Services.AddSingleton<IBLService, BLService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Manufacturer}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
