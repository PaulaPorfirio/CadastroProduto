using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventApp.Models;

namespace InventApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosControllercs : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok" });
    }
}
