using API.Data;
using API.Entities;
using API.Extension;
using API.RequestHelper;
using API.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    public class ProductController(StoreContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Entities.Product>>> GetProducts([FromQuery] ProductParams productParams)
        {
            var query = context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            var products =
            await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);

            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await context.Products.Select(p => p.Type).Distinct().ToListAsync();

            return Ok(new { brands, types });
        }

        // [HttpPost]
        // public async Task<ActionResult<Product>> CreateProduct(Product product)
        // {
        //     context.Products.Add(product);
        //     await context.SaveChangesAsync();
        //     return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateProduct(int id, Product product)
        // {
        //     if (id != product.Id) return BadRequest();
        //     context.Entry(product).State = EntityState.Modified;
        //     await context.SaveChangesAsync();
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteProduct(int id)
        // {
        //     var product = await context.Products.FindAsync(id);
        //     if (product == null) return NotFound();
        //     context.Products.Remove(product);
        //     await context.SaveChangesAsync();
        //     return NoContent();
        // }
    }
}
