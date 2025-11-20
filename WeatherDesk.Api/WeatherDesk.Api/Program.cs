using Microsoft.OpenApi.Models;
using WeatherDesk.Api.Options;
using WeatherDesk.Api.Services;

var builder = WebApplication.CreateBuilder(args);



// טעינת הגדרות
builder.Services.Configure<OpenWeatherMapOptions>(
    builder.Configuration.GetSection("OpenWeatherMap")
);
builder.Services.Configure<GoogleMapsOptions>(
    builder.Configuration.GetSection("GoogleMaps")
);




//
//builder.Services.Configure<OpenWeatherMapOptions>(builder.Configuration.GetSection("OpenWeatherMap"));
//builder.Services.Configure<GoogleMapsOptions>(builder.Configuration.GetSection("GoogleMaps"));



builder.Services.AddScoped<ILocationWeatherService, LocationWeatherService>();

//

// Add services
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<GoogleMapsService>();
builder.Services.AddScoped<WeatherService>();

builder.Services.AddScoped<ILocationWeatherService, LocationWeatherService>();

builder.Services.AddHttpClient<WeatherService>();

// Enable Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WeatherDesk API",
        Version = "v1",
        Description = "API for WeatherDesk application"
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy => policy.WithOrigins("http://localhost:5000") // כתובת ה־WPF שלך
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherDesk API v1");
        c.RoutePrefix = string.Empty; // Swagger UI will be at https://localhost:{port}/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowClient");

app.MapControllers();



app.Run();
