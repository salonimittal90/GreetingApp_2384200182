using BusinessLayer.Interface;
using BusinessLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using GreetingApp.Middleware;
var builder = WebApplication.CreateBuilder(args);

//Register BL services or IGreetingBL ka refference inject krdiya
//isko depedency injection
builder.Services.AddScoped<IGreetingBL, GreetingBL>();
builder.Services.AddScoped<IGreetingRL, GreetingRL>();

// Add services to the container.

builder.Services.AddControllers();

//logger refference Add kiya and GetCurrentClassLogger isse current class ka logger set kiya 
// NLog is a.Net logging Library h jo  logs generate karne ke liye use hoti hai.

var logger = LogManager.Setup() 
    .LoadConfigurationFromFile(Path.Combine(Directory.GetCurrentDirectory(), "NLog.config"))
    .GetCurrentClassLogger();
builder.Host.UseNLog(); // Configure NLog



// sql connection string 
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<HelloGreetingContext>(options =>
    options.UseSqlServer(connectionString));

//exception
/*builder.Services.AddControllers(options =>
{ 
options.Filters.Add<ExceptionMiddleware>();
});*/



//swagger
builder.Services.AddEndpointsApiExplorer();//Add swagger to container
builder.Services.AddSwaggerGen();// swagger ki configuration add ki 


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();



// Middleware ko register krna h 
app.UseRouting();
//app.UseMiddleware<GreetingApp.Middleware.ExceptionMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

//Swagger JSON generate karne ke liye.
app.UseSwagger();

//Swagger UI ko enable kiya jisse browser me API test kar sakein.
app.UseSwaggerUI();

app.MapControllers();

app.Run();


