using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesProject.Application;
using MoviesProject.Config;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.DomainLayer.Models;
using MoviesProject.Extensions;
using MoviesProject.Infrastructure.DBContext;
using MoviesProject.Infrastructure.Repos;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddDbContext<AuthDBContext>();
//builder.Services.AddAuthorization();
//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<AuthDBContext>();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<AuthDBContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthDBContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<MovieDBContext>(); 
builder.Services.AddScoped<IMovieRepository,MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
builder.Services.AddScoped<IMovieCommandHandler, MovieCommandHandler>();
builder.Services.AddScoped<IActorCommandHandler, ActorCommandHandler>();
builder.Services.AddScoped<IMovieQueryHandler, MovieQueryHandler>();
builder.Services.AddScoped<IActorQueryHandler, ActorQueryHandler>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials(); ;
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew=TimeSpan.Zero,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("AllowAngular");
app.ConfigureCustomExceptionMiddleware(); //global exception handling
//app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
