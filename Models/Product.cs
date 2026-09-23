using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lager.Models
{
    // Enum = en fast liste med lovlige verdier.
    // I skjemaet blir dette en nedtrekksliste. I databasen lagres det som et tall (0, 1, 2).
    public enum ProductType
    {
        Sider,
        Alkoholfri,
        Most
    }

    // Ferdigvarer som selges: Lagmann, Bøddel, Humlesus, most osv.
    public class Product
    {

        // Produkt ID
        public int ProductId { get; set; }

        // Produkt Navn (Lagman, Bøddel, Rose osv.)
        [Required(ErrorMessage = "Produktet må ha et navn")]
        [StringLength(100)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Produkt Type (sider, alkoholfri eller most)
        [Display(Name ="Type")]
        public ProductType Type {get; set; }

        // Volum per flaske i milliliter (f.eks. 330 eller 750), IKKE totalt på lager
        [Range(1, 10000, ErrorMessage = "Volum må være mellom 1 og 10 000 ml")]
        [Display(Name = "Volum (ml)")]
        public int VolumeMl { get; set; }

        // Produkt pris per flaske
        // decimal brukes for penger fordi den regner nøyaktig med desimaler
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

        // Varebeholdning (antall flasker på lager)
        [Range(0, int.MaxValue, ErrorMessage = "Lagerbeholdning kan ikke være negativ")]
        [Display(Name = "På lager (flasker)")]
        public int QuantityInStock { get; set; }

        // Varsling når beholdning er et viss antall
        [Range(0, int.MaxValue)]
        [Display (Name = "Varsle under")]
        public int MinimumStock { get; set; }

        // Beregnes hver gang, lagres ikke i databasen. Brukes til varsler.
        // => betyr "regn ut og returner dette". Blir true når lageret er lavt.
        [NotMapped]
        public bool IsLowStock => QuantityInStock <= MinimumStock;
    }
}