using Microsoft.EntityFrameworkCore;
using MoviesProject.Application;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;
using MoviesProject.Infrastructure.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieDBContext>(); 
builder.Services.AddScoped<IMovieRepository,MovieRepository>();
builder.Services.AddScoped<IMovieCommandHandler, MovieCommandHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
