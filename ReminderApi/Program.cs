using ReminderApi.BackgroundServices;
using ReminderApi.Interfaces;
using ReminderApi.Repositories;
using ReminderApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IReminderRespository, ReminderRepository>();
builder.Services.AddSingleton<IReminderService, ReminderService>();
builder.Services.AddSingleton<IReminderResponseMapper, ReminderResponseMapper>();
builder.Services.AddHostedService<ReminderWorker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
