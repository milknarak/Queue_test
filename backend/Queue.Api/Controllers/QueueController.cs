
using Microsoft.AspNetCore.Mvc;
using Queue.Api.Data;

namespace Queue.Api.Controllers;

[ApiController]
[Route("api/queue")]
public class QueueController : ControllerBase
{
    private readonly QueueDbContext _db;
    private static readonly object _lock = new();

    public QueueController(QueueDbContext db) { _db = db; }

    [HttpPost("next")]
    public IActionResult Next()
    {
        lock (_lock)
        {
            var q = _db.QueueCounters.First();
            if (q.Number < 9) q.Number++;
            else { q.Number = 0; q.Prefix = q.Prefix < 'Z' ? (char)(q.Prefix + 1) : 'A'; }
            _db.SaveChanges();
            return Ok($"{q.Prefix}{q.Number}");
        }
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        var q = _db.QueueCounters.First();
        q.Prefix = 'A';
        q.Number = 0;
        _db.SaveChanges();
        return Ok("00");
    }
}
