using BakerySystem.Api;
using BakerySystem.Api.Filters;
using BakerySystem.Core.Extensions;
using BakerySystem.DataAccess;
using BakerySystem.Handlers.SignalR;
using BakerySystem.Repositories.Implementation;
using BakerySystem.Services.Implementation;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        builder.Configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            SchemaName = "dbo",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(
            builder.Configuration["Logging:LogLevel:Default"] ??
            throw new Exception("Cannot find 'Logging:LogLevel:Default'")))
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Services.AddControllers();
builder.Services.AddInstallersFromAssemblies(builder.Configuration,
    typeof(ApplicationDbContext), typeof(ApiAssemblyMarker),
    typeof(RepositoryManager), typeof(ServiceAssemblyMarker));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton(Log.Logger);
builder.Logging.AddSerilog();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseCors("AllowOrigin");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader()
    .WithOrigins(builder.Configuration.GetSection("Cors:Origins").Get<string[]>()!));

app.MapHub<OrderHub>("/hubs/order");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
