using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.ApplicationServices.Dtos;
using WebApiProject.ApplicationServices.Services.Contracts;
using WebApiProject.Models.DomainModels.ProductAggregates;
using WebApiProject.Models.Services.Contracts;
using WebApiProject.Models.Services.Repositories;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;

        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }


        #region [- Post() -]
        // POST /api/product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostProductDto postProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var serviceResponse = await _productApplicationService.Post(postProductDto);

            if (!serviceResponse.IsSuccessful)
                return BadRequest(new { message = serviceResponse.ErrorMessage });

            return Ok(new { id = serviceResponse.Result });
        }
        #endregion

        #region [- Get() -]
        // GET /api/product/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var getResponse = await _productApplicationService.GetByIdProduct(new GetByIdProductDto { Id = id });
            if (getResponse.Result is null)
                return NotFound();

            return Ok(getResponse.Result);
        }
        #endregion


        #region [- GetAll() -]
        // GET /api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllResponse = await _productApplicationService.GetAllProduct();
            return Ok(getAllResponse.Result.GetByIdProductDto);
        }
        #endregion

        #region [- Put() -]
        // PUT /api/product/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PutProductDto putProductDto)
        {
            if (id == Guid.Empty || id != putProductDto.Id)
                return BadRequest(new { message = "Product ID is required and must match." });

            var serviceResponse = await _productApplicationService.Put(putProductDto);

            if (!serviceResponse.IsSuccessful)
                return NotFound(new { message = serviceResponse.ErrorMessage });

            return NoContent(); // standard for successful PUT
        }
        #endregion


        #region [- Delete() -]
        // DELETE /api/product/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Product ID is required." });

            var serviceResponse = await _productApplicationService.Delete(new DeleteProductDto { Id = id });

            if (!serviceResponse.IsSuccessful)
                return NotFound(new { message = serviceResponse.ErrorMessage });

            return NoContent(); // standard for successful DELETE
        } 
        #endregion
    }
}
