using AuthService.Consumers.UserCreated;
using AuthService.Data;
using Common.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

#region Add DbContext

var dbConnectionString = builder.Configuration.GetConnectionString("AuthDb");

builder.Services.AddDbContext<AuthDbContext>(opt => 
    opt.UseNpgsql(dbConnectionString, o => 
        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

#endregion

#region Add RedisSettings

builder.Services.AddSingleton(builder.Configuration.GetSection("Redis").Get<RedisSettings>()!);

#endregion

builder.Services.AddHostedService<UserCreatedEventConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();