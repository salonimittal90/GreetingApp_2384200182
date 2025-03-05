using BusinessLayer.Interface;
using BusinessLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;

var builder = WebApplication.CreateBuilder(args);

//Register BL services or IGreetingBL ka refference inject krdiya
//isko depedency injection
builder.Services.AddScoped<IGreetingBL, GreetingBL>();
builder.Services.AddScoped<IGreetingRL, GreetingRL>();

// Add services to the container.

builder.Services.AddControllers();

//logger refference Add kiya and GetCurrentClassLogger isse current class ka logger set kiya 
var logger = LogManager.Setup() 
    .LoadConfigurationFromFile(Path.Combine(Directory.GetCurrentDirectory(), "NLog.config"))
    .GetCurrentClassLogger();


// sql connection string 
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<HelloGreetingContext>(options =>
    options.UseSqlServer(connectionString));



// NLog is a.Net logging Library h jo  logs generate karne ke liye use hoti hai.
// Configure NLog
builder.Host.UseNLog();



//Add swagger to container
builder.Services.AddEndpointsApiExplorer();

// swagger ki configuration add ki 
builder.Services.AddSwaggerGen();


var app = builder.Build();

//Swagger JSON generate karne ke liye.
app.UseSwagger();

//Swagger UI ko enable kiya jisse browser me API test kar sakein.
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
