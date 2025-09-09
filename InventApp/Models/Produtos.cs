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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Categoria { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "ValorUnitario deve ser ≥ 0.")]
        public decimal ValorUnitario {  get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser ≥ 0.")]
        public decimal Quantidade {  get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DataCadastro {  get; set; } = DateTime.UtcNow;

        public bool Disponivel { get; } 

    }
}
