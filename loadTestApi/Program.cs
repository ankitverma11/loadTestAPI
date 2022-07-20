using loadTestApi.DataAccess;
using loadTestApi.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<PostgreDBContext>(options =>
//options.UseNpgsql(connString));

builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

