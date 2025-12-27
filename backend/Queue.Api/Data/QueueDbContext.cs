
using Microsoft.EntityFrameworkCore;
using Queue.Api.Models;

namespace Queue.Api.Data;
public class QueueDbContext : DbContext
{
    public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options) {}
    public DbSet<QueueCounter> QueueCounters => Set<QueueCounter>();
}
