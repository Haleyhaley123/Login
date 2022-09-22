using Domain.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reponsitory.Interface;
using Reponsitory.Reponsitory;
using System.Configuration;
using System.Text;
using static Domain.DBContext.DBContext;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:7035")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
//Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWWT:ValidAudience"],
            ValidIssuer = configuration["JWWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
        });
                

//adding Jwt Bearer




#region Hacode
//khai báo DI
builder.Services.AddScoped<IUserReponsitory, UserReponsitory>();
//Name title
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "1" });
 });
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
