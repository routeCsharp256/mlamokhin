using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("/api/merchandise")]
    [Produces("application/json")]
    public class MerchandiseController:ControllerBase
    {
        private readonly IMerchandiseService _service;

        public MerchandiseController(IMerchandiseService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var items = await _service.GetAll(token);
            return Ok(items);
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<MerchandiseItem>> GetById(long id, CancellationToken token)
        {
            var item = await _service.GetById(id, token);
            if (item == null)
            {
                NotFound();
            }
            return item;
        }
    }
}