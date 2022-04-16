using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> products = new()
        {
            new Product(){ Id = 1, Name = "Apple", IsAvailable = true, Price = 140, Quantity = 100  },
            new Product(){ Id = 2, Name = "Oranges", IsAvailable = true, Price = 160, Quantity = 300  },
            new Product(){ Id = 3, Name = "Banana", IsAvailable = false, Price = 80, Quantity = 500  }
        };

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            products.Add(product);

            return Ok(product);
        }
    }
}

