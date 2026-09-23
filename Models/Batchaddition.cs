using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;   

namespace Lager.Models
{
    // Tilsetninger
    public enum AdditionPurpose
    {
        Gjaering,     // gjær
        Naering,      // næringsstoffer
        Smak,         // humle, bringebær, rips osv.
        Sukker,
        Annet
    }

    // Alt som tilsettes en batch: gjær, næringsstoffer, sukker, smak osv.
    // Peker til Supply (innsatsvaren), slik at lageret kan trekkes ned automatisk.
    public class BatchAddition
    {
        // ID (primærnøkkel, lages automatisk)
        public int BatchAdditionId { get; set; }

        // Hvilken batch det ble tilsatt (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Hva som ble tilsatt (fremmednøkkel til Supply, f.eks. en bestemt humle)
        [Display(Name = "Innsatsvare")]
        public int SupplyId { get; set; }
        public virtual Supply? Supply { get; set; }

        // Hva tilsetningen er til (gjæring, smak osv.)
        [Display(Name = "Formål")]
        public AdditionPurpose Purpose { get; set; }

        // Hvor mye som ble tilsatt, i samme enhet som innsatsvaren (kg, g, stk)
        [Range(0.001, 100000, ErrorMessage = "Mengden må være større enn 0")]
        [Display(Name = "Mengde")]
        public decimal Amount { get; set; }

        // Hvor mange liter batchen var da det ble tilsatt.
        // Trengs for å vite hvor sterkt det ble: 2 kg bær i 500 liter er noe
        // helt annet enn 2 kg i 2000 liter.
        [Range(0.1, 100000, ErrorMessage = "Volumet må være større enn 0")]
        [Display(Name = "Batchvolum (liter)")]
        public decimal BatchVolumeLiters { get; set; }

        // Når det ble tilsatt (dato og klokkeslett)
        [Display(Name = "Lagt i")]
        public DateTime AddedAt { get; set; } = DateTime.Now;   // standard: akkurat nå

        // Når det ble tatt ut igjen.
        // DateTime? = kan stå tom, fordi den fortsatt ligger i, eller fordi
        // noe som gjær og sukker aldri tas ut.
        [Display(Name = "Tatt ut")]
        public DateTime? RemovedAt { get; set; }

        // Frie notater, f.eks. "i nettingpose" eller "rørt daglig"
        [StringLength(500)]
        [Display(Name = "Notater")]
        public string? Notes { get; set; }

        // Hvem som registrerte det. Fylles automatisk når innlogging er på plass.
        [StringLength(100)]
        [Display(Name = "Registrert av")]
        public string? RegisteredBy { get; set; }

        // Hvor lenge det lå i. Beregnes, lagres ikke.
        // Er null hvis den ikke er tatt ut ennå.
        // TimeSpan er en tidslengde, f.eks. "3 dager og 4 timer".
        [NotMapped]
        public TimeSpan? Duration => RemovedAt.HasValue ? RemovedAt.Value - AddedAt : null;

        // Hvor mye per liter. Beregnes, lagres ikke.
        // Eks: 2 kg bringebær i 500 liter = 0,004 kg per liter (4 gram per liter).
        [NotMapped]
        public decimal AmountPerLiter => BatchVolumeLiters > 0 ? Amount / BatchVolumeLiters : 0;
    }
}