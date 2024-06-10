using AuthService.Consumers.UserCreated;
using AuthService.Data;
using AuthService.Services;
using Common.Events;
using Common.Publisher;
using Common.Settings;
using Microsoft.EntityFrameworkCore;

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

#region Jwt

builder.Services.AddSingleton(builder.Configuration.GetSection("Jwt").Get<JwtCredentials>()!);

#endregion

builder.Services.AddHostedService<UserCreatedEventConsumer>();

builder.Services.AddTransient<IEventPublisher<AuthInfoCreatedEvent>, RedisEventPublisher<AuthInfoCreatedEvent>>();

builder.Services.AddTransient<JwtAuthService>();

builder.Services.AddTransient<IClaimService, ClaimService>();

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