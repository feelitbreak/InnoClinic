using InnoClinic.Extensions;
using InnoClinic.Domain.Extensions;
using InnoClinic.Services.Extensions;
using InnoClinic.Infrastructure.Extensions;
using InnoClinic.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenConfiguration();

builder.Services.ConfigureApiVersioning();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSqlServerDb(builder.Configuration);
builder.Services.AddUnitOfWork();
builder.Services.AddProjectServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddProjectOptions();
builder.Services.AddMiddleware();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
