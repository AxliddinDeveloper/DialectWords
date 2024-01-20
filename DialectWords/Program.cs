//=============
// For Client
//=============

using DialectWords.Brokers.Storages;
using DialectWords.Services.Foundations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StorageBroker>();
builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<IWordService, WordService>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();