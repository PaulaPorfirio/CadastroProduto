using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventApp.Models;
using InventApp.Data;

namespace InventApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly DataContext Db;
        public ProdutosController(DataContext _Db)
        {
            Db = _Db;
        }

        [HttpGet("GetProdutos")]
        public async Task<IEnumerable<Produtos>> GetProdutos()
        {
            return await Db.Produtos.ToListAsync();
        }

        [HttpGet("GetProdutoPorId")]
        public async Task<ActionResult<Produtos>> GetProdutoPorId(int Id)
        {
            var Produto = await Db.Produtos.FirstOrDefaultAsync(p => p.Codigo == Id);

            if (Produto is null)
            {
                return NotFound(new { error = "Produto inválido." });
            }

            return Ok(Produto);
        }
        
        [HttpPost("AdicionaProduto")]
        public async Task<ActionResult<Produtos>> AdicionaProduto(Produtos produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome)) return BadRequest("Nome é obrigatório.");
            if (produto.ValorUnitario < 0m) return BadRequest("ValorUnitario deve ser ≥ 0.");
            if (produto.Quantidade < 0) return BadRequest("Quantidade deve ser ≥ 0.");

            produto.DataCadastro = DateTime.UtcNow;

            try
            {
                Db.Produtos.Add(produto);
                await Db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProdutoPorId), new { id = produto.Codigo }, produto);
            }
            catch (Exception)
            {
                return Problem(title: "Erro ao adicionar Produto {0}." + Convert.ToString(produto.Codigo), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("AtualizaProduto")]
        public async Task<IActionResult> AtualizaProduto(int Codigo, Produtos produto)
        {
            if (Codigo != produto.Codigo) return BadRequest("Código inválido.");
            if (produto.ValorUnitario < 0m) return BadRequest("ValorUnitario deve ser ≥ 0.");
            if (produto.Quantidade < 0) return BadRequest("Quantidade deve ser ≥ 0.");

            try
            {
                var Cadastro = await Db.Produtos.FindAsync(new object[] { produto.Codigo });
                if (Cadastro is null) return NotFound();

                Cadastro.Nome = produto.Nome;
                Cadastro.Categoria = produto.Categoria;
                Cadastro.DataCadastro = DateTime.UtcNow;
                Cadastro.ValorUnitario = produto.ValorUnitario;
                Cadastro.Quantidade = produto.Quantidade;

                await Db.SaveChangesAsync(true);

                return NoContent();
            }

            catch (Exception)
            {
                return Problem(title: "Erro ao cadastrar Produto {0}." + Convert.ToString(produto.Codigo), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveProduto")]
        public async Task<IActionResult> RemoveProduto(int Id)
        {
            var Produto = await Db.Produtos.FindAsync(new object?[] { Id });

            if (Produto is null) return NotFound();

            Db.Produtos.Remove(Produto);
            await Db.SaveChangesAsync();

            return NoContent();
        }
    }
}
