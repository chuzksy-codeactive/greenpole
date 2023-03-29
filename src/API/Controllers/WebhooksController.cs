using Application.DTOs;
using Infrastructure.Data.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhooksController : ControllerBase
{
    private readonly ILogger<WebhooksController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IScheduler _scheduler;

    public WebhooksController(ILogger<WebhooksController> logger, ApplicationDbContext dbContext,
        IScheduler scheduler)
    {
        _logger = logger;
        _dbContext = dbContext;
        _scheduler = scheduler;
    }

    [HttpPost("schedule_update")]
    public async Task<IActionResult> CreatePayout(ScheduleUpdateViewModel model)
    {
        await Task.FromResult(2);
        return Ok("It works!");
        // var payout = new DividendPayout {
        //     Title = model.Title,
        //     CreatedOn = DateTime.UtcNow,
        //     PayoutDate = DateOnly.FromDateTime(model.PayoutDate),
        //     CreatedBy = "System",
        // };

        // _dbContext.DividendPayouts.Add(payout);
        // await _dbContext.SaveChangesAsync();
        // // _scheduler.ScheduleJob(new DividendPayoutJob(payout.DividendPayoutId));

        // // TODO: send email on new payout created
        // return Created(Request.GetDisplayUrl(), payout);
    }
}
