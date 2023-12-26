using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProiectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Proiect")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUtilizatorRepository, UtilizatorRepository>();
builder.Services.AddScoped<IArticolRepository, ArticolRepository>();
builder.Services.AddScoped<IProfilRepository, ProfilRepository>();


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
