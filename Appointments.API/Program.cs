using Appointments.Data.DbConnection;
using Appointments.Data.Repositories;
using Appointments.Data.Repositories.Interfaces;
using Appointments.Domain.Services;
using Appointments.Domain.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppointmentsConnection");

builder.Services.AddSingleton<ConnectionProvider>(sp => 
    new ConnectionProvider(connectionString, sp.GetRequiredService<ILogger<ConnectionProvider>>()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServiceTypesRepository, ServiceTypesRepository>();
builder.Services.AddScoped<IServiceTypesService, ServiceTypesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();