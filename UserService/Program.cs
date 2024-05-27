using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

#region Add DbContext

var dbConnectionString = builder.Configuration.GetConnectionString("UserDb");

builder.Services.AddDbContext<UserDbContext>(opt => 
    opt.UseNpgsql(dbConnectionString, o => 
        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

#endregion

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