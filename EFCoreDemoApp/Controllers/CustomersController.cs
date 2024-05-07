using EFCoreDemoApp.DataAccess;
using EFCoreDemoApp.Integration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ShopDbContext _dbContext;

        public CustomersController(ILogger<CustomersController> logger, ShopDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public Task<List<CustomerDto>> Get()
        {
            _logger.LogDebug($"get customers");

            return _dbContext.Customers
                .Select(s => new CustomerDto(s))
                .ToListAsync();
        }
    }
}
