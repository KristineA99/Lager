using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lager.Models
{
     // Kategori for innsatsvarer. Blir nedtrekksliste i skjemaet.
    public enum SupplyCategory
    {
        Emballasje, // flasker, korker, etiketter, kartonger
        Ingrediens, // gjær, næringssalter
        Smakstilsetning, // humle, rips, bringebær osv.
        Rengjoring, // vaskemidler for tanker og utstyr
    }

    // Innsatsvarer: det som går INN i produksjonen.
    public class Supply
    {
        // Innsatsvare ID (primærnøkkel, lages automatisk)
        public int SupplyId { get; set; }

        // Navn på innsatsvaren, f.eks. "Kork 29 mm"
        [Required(ErrorMessage = "Innsatsvaren må ha et navn")]
        [StringLength(100)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Kategori (emballasje, ingrediens osv.)
        [Display(Name = "Kategori")]
        public SupplyCategory Category { get; set; }

        // Enhet varen telles i: "stk", "kg", "liter"
        // Gjør at korker (stk) og gjær (kg) kan ligge i samme tabell
        [Required(ErrorMessage = "Oppgi enhet, f.eks. stk eller kg")]
        [StringLength(20)]
        [Display(Name = "Enhet")]
        public string Unit { get; set; } = "stk"; // "stk" er standardverdi

        // Varebeholdning i enheten over
        // decimal fordi noe måles med desimaler (f.eks. 2,5 kg gjær)
        [Range(0, 1000000, ErrorMessage = "Lagerbeholdning kan ikke være negativ")]
        [Display(Name = "På lager")]
        public decimal QuantityInStock { get; set; }

        // Varsle når beholdningen er på eller under dette
        [Range(0, 1000000)]
        [Display(Name = "Varsle under")]
        public decimal MinimumStock { get; set; }
 
        // Hvem man kjøper varen fra (valgfritt)
        [StringLength(100)]
        [Display(Name = "Leverandør")]
        public string? Supplier { get; set; }
 
        // Hvilket lager/rom varen ligger i
        // int? = kan være tom hvis plassering ikke er satt
        [Display(Name = "Lagerplass")]
        public int? StorageLocationId { get; set; }
        public virtual StorageLocation? StorageLocation { get; set; }

        // Mer nøyaktig plassering inne i rommet, f.eks. "Hylle B3"
        [StringLength(100)]
        [Display(Name = "Hylle/plass")]
        public string? Shelf { get; set; }
 
        // Beregnes, lagres ikke i databasen. true når lageret er lavt.
        [NotMapped]
        public bool IsLowStock => QuantityInStock <= MinimumStock;
    }
}