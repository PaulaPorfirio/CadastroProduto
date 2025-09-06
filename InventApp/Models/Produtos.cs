using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static InventApp.Models.Produtos;

namespace InventApp.Models
{
    public class Produtos 
    {
        public int Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Categoria { get; set; }

        [Required]
        public decimal ValorUnitario {  get; set; }

        [Required]
        public decimal Quantidade {  get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DataCadastro {  get; set; } = DateTime.UtcNow;

        public bool Disponivel { get; } 

    }
}
