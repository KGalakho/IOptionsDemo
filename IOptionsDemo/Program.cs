using IOptionsDemo.Configuration;
using IOptionsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FeatureOptions>(builder.Configuration
    .GetSection(FeatureOptions.SectionName));

builder.Services.AddSingleton<StaticOptionsService>();       // IOptions<T>
builder.Services.AddScoped<SnapshotOptionsService>();        // IOptionsSnapshot<T>
builder.Services.AddSingleton<MonitorOptionsService>();      // IOptionsMonitor<T>

builder.Services.AddControllers();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger and Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
