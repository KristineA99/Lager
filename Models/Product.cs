using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lager.Models
{
    public enum ProductType
    {
        Sider,
        Alkoholfri,
        Most
    }

    public class Product
    {

        // Produkt ID
        public int ProductId { get; set; }

        // Produkt Navn
        [Required(ErrorMessage = "Produktet må ha et navn")]
        [StringLength(100)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Produkt Type
        [Display(Name ="Type")]
        public ProductType Type {get; set; }

        // Volum per produkt
        [Range(1, 10000, ErrorMessage = "Volum må være mellom 1 og 10 000 ml")]
        [Display(Name = "Volum (ml)")]
        public int VolumeMl { get; set; }

        // Produkt pris
        [Range(0.01, 100000, ErrorMessage = "Prisen må være større enn 0")]
        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        // Produkt beskrivelse
        [StringLength(500)]
        [Display(Name = "Beskrivelse")]
        public string? Description { get; set; }

        // Bilde av produkt
        [Display(Name = "Bilde-URL")]
        public string? ImageUrl { get; set; }

        // Varebeholdning (antall flasker)
        [Range(0, int.MaxValue, ErrorMessage = "Lagerbeholdning kan ikke være negativ")]
        [Display(Name = "På lager (flasker)")]
        public int QuantityInStock { get; set; }

        // Varsling når beholdning er et viss antall
        [Range(0, int.MaxValue)]
        [Display (Name = "Varsle ved")]
        public int MinimumStock { get; set; }

        // Beregnes, lagres ikke i databasen. Brukes til varsler.
        [NotMapped]
        public bool IsLowStock => QuantityInStock <= MinimumStock;
    }
}