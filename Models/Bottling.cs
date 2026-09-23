using System.ComponentModel.DataAnnotations;  

namespace Lager.Models
{
    // En tapping: en batch blir til flasker med et lotnummer.
    // Lotnummeret blir strekkoden på etikett/kartong, så en skanning
    // fører rett tilbake til batchen og hele historikken.
    public class Bottling
    {
        // ID (primærnøkkel, lages automatisk)
        public int BottlingId { get; set; }

        // Hvilken batch som ble tappet (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Hvilket produkt det ble tappet som (fremmednøkkel til Product)
        // Lagerbeholdningen til dette produktet økes med antall flasker
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        // Lotnummeret som blir strekkode. Settes som unik i LagerDbContext.
        [Required(ErrorMessage = "Tappingen må ha et lotnummer")]
        [StringLength(50)]
        [Display(Name = "Lotnummer")]
        public string LotNumber { get; set; } = string.Empty;

        // Når det ble tappet
        [DataType(DataType.Date)]
        [Display(Name = "Tappedato")]
        public DateTime BottledAt { get; set; } = DateTime.Today;   // standard: i dag

        // Hvor mange flasker som ble tappet
        [Range(1, 1000000, ErrorMessage = "Antall flasker må være minst 1")]
        [Display(Name = "Antall flasker")]
        public int BottleCount { get; set; }

        // Best før-dato (valgfritt)
        [DataType(DataType.Date)]
        [Display(Name = "Best før")]
        public DateTime? BestBefore { get; set; }
    }
}