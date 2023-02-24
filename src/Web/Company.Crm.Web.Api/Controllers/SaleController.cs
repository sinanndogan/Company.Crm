using Company.Crm.Application.Dtos.Sale;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _saleService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _saleService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrUpdateSaleDto entity)
        {
            var data = _saleService.Insert(entity);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateOrUpdateSaleDto entity)
        {
            var data = _saleService.Update(entity);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _saleService.DeleteById(id);
            return Ok(data);
        }

        [HttpPost("deleteByEntity")]
        public IActionResult Delete([FromBody] SaleDetailDto entity)
        {
            var data = _saleService.Delete(entity);
            return Ok(data);
        }

        // api/sale/chartData
        [HttpGet("chartData")]
        public IActionResult GetChartData()
        {
            var response = _saleService.GetChartData();

            return Ok(response);
        }
    }
}
