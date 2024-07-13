using AutoMapper;
using EmployeeApi;
using EmployeeApi.DTO;
using EmployeeApi.Models;
using EmployeeApi.Repository;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTSetting:JWTOptions"));
builder.Services.AddDbContext<EmployeeDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("First")));
builder.Services.AddScoped<IAuthoRepository, AuthService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeServices>();
builder.Services.AddScoped<IJwtTokenGenerator,JwtTokenService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EmployeeDbContext>().AddDefaultTokenProviders();
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
