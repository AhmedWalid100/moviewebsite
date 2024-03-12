using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Application;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Extensions;
using MoviesProject.Infrastructure.DBContext;
using MoviesProject.Infrastructure.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuthDBContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDBContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
                    .AllowAnyHeader();
        });
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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
