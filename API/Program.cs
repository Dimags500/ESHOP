using API.Errors;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", policy =>
//    {
//        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200/");
//    });

//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IProductRepository, ProducRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

//conection strings
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
var csRedis = builder.Configuration.GetConnectionString("Redis");

// basket reository & Redis config  
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var config = ConfigurationOptions.Parse(csRedis, true);
    return ConnectionMultiplexer.Connect(config); 

});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();


builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(cs));
builder.Services.AddAutoMapper(typeof(MappingProfiles));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy" , policy => 
//    { 
//        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200/");
//    });

//});

builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.InvalidModelStateResponseFactory = ActionContext =>
    {
        var errors = ActionContext.ModelState
        .Where(e => e.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseStatusCodePagesWithReExecute("/errors/{0}"); 
app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();


// Context factory , Load data from files to DB
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context , loggerFactory);
}
catch (System.Exception ex )
{

    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error during migration "); 
}


await app.RunAsync();
