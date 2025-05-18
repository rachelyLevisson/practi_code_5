using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Service;
using WebAPI.Entity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// קרא את ה-token מהקונפיגורציה
var token = builder.Configuration["TOKEN_GITHUB"];

// הוסף את השירותים לקונטיינר
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IGitHubService>(provider => new GitHubService(token));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin() // מאפשר לכל המקורות
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// הוסף את השורה הזו כדי להשתמש במדיניות CORS
app.UseCors("MyPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
