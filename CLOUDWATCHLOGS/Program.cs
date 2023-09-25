var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var config = builder.Configuration.GetAWSLoggingConfigSection();
var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddAWSProvider(config);

var _logger = loggerFactory!.CreateLogger("Program");
_logger.LogWarning("Hello from configure");

//using (var serviceScope = app.Services.CreateScope())
//{
//    var services = serviceScope.ServiceProvider;

//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    loggerFactory.AddAWSProvider(config);

//    var _logger = loggerFactory.CreateLogger("Startup");
//    _logger.LogWarning("warning from configure");

//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
