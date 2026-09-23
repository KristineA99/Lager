using System.ComponentModel.DataAnnotations; 

namespace Lager.Models
{
    // Hvilke epler som gikk inn i en batch. Én batch kan ha flere sorter.
    public class AppleInput
    {
        // ID (primærnøkkel, lages automatisk)
        public int AppleInputId { get; set; }

        // Hvilken batch eplene gikk inn i (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Eplesort, f.eks. "Gravenstein" eller "Aroma"
        [Required(ErrorMessage = "Oppgi eplesort")]
        [StringLength(100)]
        [Display(Name = "Eplesort")]
        public string Variety { get; set; } = string.Empty;

        // Mengde i kilo
        [Range(0.1, 1000000, ErrorMessage = "Mengden må være større enn 0")]
        [Display(Name = "Mengde (kg)")]
        public decimal Kilograms { get; set; }

        // Hvilket felt/hage eplene kom fra (valgfritt). Kan bli egen tabell senere.
        [StringLength(100)]
        [Display(Name = "Felt")]
        public string? Field { get; set; }

        // Når eplene ble høstet (valgfritt)
        // DateTime? = dato som kan stå tom
        [DataType(DataType.Date)]
        [Display(Name = "Høstet")]
        public DateTime? HarvestDate { get; set; }
    }
}