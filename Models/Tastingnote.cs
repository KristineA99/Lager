using System.ComponentModel.DataAnnotations;   

namespace Lager.Models
{
    // Et smaksnotat for en batch. En batch kan smakes mange ganger underveis.
    public class TastingNote
    {
        // ID (primærnøkkel, lages automatisk)
        public int TastingNoteId { get; set; }

        // Hvilken batch som ble smakt (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Når den ble smakt
        [DataType(DataType.Date)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; } = DateTime.Today;   // standard: i dag

        // Selve smaksnotatet
        [Required(ErrorMessage = "Skriv et smaksnotat")]
        [StringLength(2000)]
        [Display(Name = "Smaksnotat")]
        public string Text { get; set; } = string.Empty;

        // Vurdering 1-5 (valgfritt)
        [Range(1, 5, ErrorMessage = "Vurdering må være mellom 1 og 5")]
        [Display(Name = "Vurdering (1-5)")]
        public int? Rating { get; set; }

        // Hvem som smakte
        [StringLength(100)]
        [Display(Name = "Smakt av")]
        public string? Author { get; set; }
    }
}