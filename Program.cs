using Microsoft.EntityFrameworkCore; // Gir tilgang til Entity Framework (UseSqlite osv.)
using Lager.DAL; // Gir tilgang til LagerDbContext i DAL-mappa

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrerer databasen i appen (dependency injection).
// Adressen til databasen hentes fra "LagerDbContextConnection" i appsettings.json.
builder.Services.AddDbContext<LagerDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:LagerDbContextConnection"]);
});

var app = builder.Build();
// Fyller databasen med startdata når vi utvikler (ikke i produksjon)
if (app.Environment.IsDevelopment())
{
    DBInit.Seed(app);
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
