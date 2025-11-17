using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationParamters specParams)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(specParams);
            if (result == null) return BadRequest(); // 400
            return Ok(result); //200

        }

        [HttpGet  ("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); // 400
            return Ok(result); // 200
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();
            if (result == null) return BadRequest(); // 400
            return Ok(result); //200

        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result == null) return BadRequest(); // 400
            return Ok(result); //200

        }


    }
}
