using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Text;
using System.Configuration;
using webdd;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 添加服務到容器
builder.Services.AddHttpClient(); // 註冊IHttpClientFactory
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();


// Configure your DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);




app.Run();

