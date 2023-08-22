using InnoClinic.Extensions;
using InnoClinic.Domain.Extensions;
using InnoClinic.Services.Extensions;
using InnoClinic.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSqlServerDb(builder.Configuration);
builder.Services.AddUnitOfWork();
builder.Services.AddProjectServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddProjectOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
