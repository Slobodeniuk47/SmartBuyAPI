using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
//using Microsoft.OpenApi.Models;
using SmartBuyAPI.Mapper;
//using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No such a connection string");
builder.Services.AddDbContext<SmartBuyAPI.Data.ApplicationDbContext>(options =>
options.UseSqlServer(connectionString)); //��� ������ ������� �������� Microsoft.EntityFrameworkCore.SqlServer
//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));//FLuentAssertions packet
builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(conf =>
    conf.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3000"));
//.AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
var dir = Path.Combine(Directory.GetCurrentDirectory(), "Images");
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/Images"
});
//app.UseDefaultFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();