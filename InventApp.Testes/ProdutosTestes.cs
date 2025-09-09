using System.ComponentModel.DataAnnotations;
using InventApp.Models; 
using Xunit;

namespace InventApp.Testes;

public class ProdutosTestes
{
    private static IList<ValidationResult> Validate(object instance)
    {
        var ctx = new ValidationContext(instance, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(instance, ctx, results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void VerificaProdutoValido()
    {
        var p = new Produtos
        {
            Nome = "Mouse Gamer",
            Categoria = "Periféricos",
            ValorUnitario = 199.90m,
            Quantidade = 10,
            DataCadastro = DateTime.UtcNow
        };

        var results = Validate(p);

        Assert.Empty(results);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void VerificaNomeObrigatorio(string? nome)
    {
        var p = new Produtos
        {
            Nome = nome ?? "",
            Categoria = "Periféricos",
            ValorUnitario = 100,
            Quantidade = 1
        };

        var results = Validate(p);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Produtos.Nome)));
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-10)]
    public void ValidaValorUnitario(double valor)
    {
        var p = new Produtos
        {
            Nome = "Teclado",
            Categoria = "Periféricos",
            ValorUnitario = (decimal)valor,
            Quantidade = 1
        };

        var results = Validate(p);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Produtos.ValorUnitario)));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ValidaQuantidade(int qtd)
    {
        var p = new Produtos
        {
            Nome = "Headset",
            Categoria = "Periféricos",
            ValorUnitario = 50,
            Quantidade = qtd
        };

        var results = Validate(p);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(Produtos.Quantidade)));
    }
}
