using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Jikan;
using TestMvcProject.Jikan.Interfaces;
using TestMvcProject.Jikan.Libs;
using TestMvcProject.Repository;
using TestMvcProject.Repository.Interfaces;
using TestMvcProject.ViewHelperLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnectionString")
    ));


builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();



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
    pattern: "{controller=Anime}/{action=IndexUneditable}/{id?}");

app.Run();
