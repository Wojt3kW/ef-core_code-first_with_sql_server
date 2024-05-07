using EFCoreDemoApp.DataAccess;
using EFCoreDemoApp.Integration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly ShopDbContext _dbContext;

        public OrdersController(ILogger<OrdersController> logger, ShopDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public Task<List<OrderDto>> Get()
        {
            _logger.LogDebug($"get orders");

            return _dbContext.Orders
                .Include(i => i.Customer)
                .Select(s => new OrderDto(s))
                .ToListAsync();
        }
    }
}
