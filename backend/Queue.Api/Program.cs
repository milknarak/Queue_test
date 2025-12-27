
using Microsoft.EntityFrameworkCore;
using Queue.Api.Data;
using Queue.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QueueDbContext>(o => o.UseInMemoryDatabase("QueueDB"));
builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddPolicy("all", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors("all");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QueueDbContext>();
    if (!db.QueueCounters.Any())
    {
        db.QueueCounters.Add(new QueueCounter { Id = 1, Prefix = 'A', Number = 0 });
        db.SaveChanges();
    }
}

app.Run();
