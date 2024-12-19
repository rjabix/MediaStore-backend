using Microsoft.EntityFrameworkCore;
using OrderMService.Models;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRebus(rebus =>
    rebus.Routing(r => 
            r.TypeBased().MapAssemblyOf<Program>("store-queue"))
        .Transport(t => 
            t.UseRabbitMq(builder.Configuration.GetConnectionString("MessageBroker"), "store-queue"))
        .Sagas(s =>
            s.StoreInPostgres(builder.Configuration.GetConnectionString("SagasDb"), "Sagas", "Sagas_indexes"))
        .Timeouts(t =>
           // t.StoreInPostgres(builder.Configuration.GetConnectionString("SagasDb"), "Timeouts"))
           t.StoreInMemory())
);

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();