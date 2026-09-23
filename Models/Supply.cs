using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Net.Http.Headers;

namespace Lager.Models
{
    public enum SupplyCategory
    {
        Embalasje, // flasker, korker, etiketter, kartonger
        Ingredienser, // gjær, næringssalter, humle
        Rengjøreing, // vaskemidler for tanker og utstyr
    }

    // Innsatsvarer: det som går INN i produksjonen.
    public class Supply
    {
        // Vare ID
        public int SupplyId { get; set; }

        // Vare navn
        [Required(ErrorMessage = "Innsatsvaren må ha et navn")]
        [StringLength(100)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Vare kategori
        [Display(Name = "Kategori")]
        public SupplyCategory Category { get; set; }

        // Vare enhet ("stk", "kg", "liter" osv.) Gjør at 500 stk korker og 2,5 kg gjær kan ligge i samme tabell.
        [Required(ErrorMessage = "Oppgi enhet, f.eks. stk elle kg")]
        [StringLength(20)]
        [Display(Name = "Enhet")]
        public string Unit { get; set; } = "stk";

        // Vare volum
        [Range(0, 1000000, ErrorMessage = "Lagerbeholdning kan ikke være negativ")]
        [Display(Name = "På lager")]
        public decimal QuantityInStock { get; set; }

        // Varsling når beholdning er et viss antall
        [Range(0, 1000000)]
        [Display(Name = "Varsle under")]
        public decimal MinimumStock { get; set; }
 
        // Vare lerandør
        [StringLength(100)]
        [Display(Name = "Leverandør")]
        public string? Supplier { get; set; }
 
        // Hvor varen ligger
        [StringLength(100)]
        [Display(Name = "Plassering")]
        public string? Location { get; set; }
 
        // Beregnes, lagres ikke i databasen. Brukes til varsler.
        [NotMapped]
        public bool IsLowStock => QuantityInStock <= MinimumStock;
    }
}