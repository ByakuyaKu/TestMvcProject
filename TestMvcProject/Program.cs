using JikanDotNet;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestMvcProject.Data;
using TestMvcProject.Jikan;
using TestMvcProject.Jikan.Interfaces;
using TestMvcProject.Jikan.Libs;
using TestMvcProject.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnectionString")
    ));

//builder.Services.AddSingleton<IJikan, Jikan>()
//    .BuildServiceProvider();
builder.Services.AddScoped<IViewHelper, ViewHelper>();
builder.Services.AddScoped<IJikan, Jikan>();

builder.Services.AddScoped<IMangaLib, MangaLib>();

builder.Services.AddScoped<IAnimeLib, AnimeLib>();

builder.Services.AddHostedService<JikanService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
