using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Web.API;
public static class MigrationExtensions
{
public static void ApplyMigrations(this WebApplication app)
{
    using var scope=app.Services.CreateScope();
    var dbcontext=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbcontext.Database.Migrate();

}
}