using BusinessLayer.Interface;
using BusinessLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using GreetingApp.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);



// ? JWT Authentication Setup

/*var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing in configuration");
}

var key = Encoding.UTF8.GetBytes(jwtKey);
*/

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//jwt lock 
    builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
});

c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


//Register BL services or IGreetingBL ka refference inject krdiya
//isko depedency injection
builder.Services.AddScoped<IGreetingBL, GreetingBL>();
builder.Services.AddScoped<IGreetingRL, GreetingRL>();
builder.Services.AddScoped<IUserBL, UserBL>();  //  Business Layer
builder.Services.AddScoped<IUserRL, UserRL>();  //  Repository Layer

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<JwtService>(); // ? JWT Service Register Karo



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
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication(); // ? JWT Authentication Middleware
app.UseAuthorization();


//Swagger JSON generate karne ke liye.
app.UseSwagger();

//Swagger UI ko enable kiya jisse browser me API test kar sakein.
app.UseSwaggerUI();

app.MapControllers();

app.Run();


