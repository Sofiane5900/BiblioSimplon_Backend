using System.Security.Claims;
using System.Text;
using BiblioAPI.Data;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. // test
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BiblioDbContext>(opt => opt.UseSqlite("Data Source=BiblioDb.db"));
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // Seulement en d�veloppement (https non requis)
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
            ValidAudience = builder.Configuration["JWTConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Secret"]!)
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RoleClaimType = ClaimTypes.Role,
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "BiblioSimplon API",
            Version = "v1",
            Description = "Une API pour g�rer une bibliothéque de manniere numérique",
            Contact = new OpenApiContact
            {
                Name = "BiblioSimplon Support",
                Email = "BiblioSimplon@example.com",
                Url = new Uri("https://www.BiblioSimplon.com"),
            },
        }
    );

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Veillez entrer votre token JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
        }
    );
    c.DocumentFilter<SecurityRequirementsDocumentFilter>();
});

// Injection de d�pendances pour le service Emprunt, Scoped cr�e une instance qui a une dur�e de vie de la requ�te
builder.Services.AddScoped<EmpruntServices>();
builder.Services.AddScoped<LivreServices>();
builder.Services.AddScoped<MembreService>();
builder.Services.AddScoped<AuthServices>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
