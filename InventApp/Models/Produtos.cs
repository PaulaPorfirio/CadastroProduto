using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static InventApp.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace InventApp.Models
{
    public class Produtos 
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public int CodigoCategoria { get; set; }

        [Required]
        [StringLength(30)]
        public string DescricaoCategoria { get; set; }

        [Required]
        public decimal ValorUnitario {  get; set; }

        [Required]
        public decimal Quantidade {  get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DataCadastro {  get; set; } = DateTime.UtcNow;

        public bool Disponivel { get; } 

    }
}
