using BiblioAPI.Data;
using BiblioAPI.Models;
using BiblioAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. // test
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BiblioDbContext>(opt => opt.UseSqlite("Data Source=BiblioDb.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "BiblioSimplon API",
            Version = "v1",
            Description = "Une API pour gérer une bibliothéque de manniére numérique",
            Contact = new OpenApiContact
            {
                Name = "BiblioSimplon Support",
                Email = "BiblioSimplon@example.com",
                Url = new Uri("https://www.BiblioSimplon.com"),
            },
        }
    );
});

// Injection de dépendances pour le service Emprunt, Scoped crée une instance qui a une durée de vie de la requête
builder.Services.AddScoped<EmpruntServices>();

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BiblioSimplon API v1");
    c.RoutePrefix = "swagger";
});

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

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
