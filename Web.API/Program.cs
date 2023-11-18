using Application;
using Infraestructure;
using Web.API;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation()
.AddInfraestructure(builder.Configuration)
.AddApplication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
