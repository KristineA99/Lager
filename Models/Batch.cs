using System.ComponentModel.DataAnnotations; 

namespace Lager.Models
{
    // Hvor langt batchen har kommet. Blir nedtrekksliste i skjemaet.
    public enum BatchStatus
    {
        Gjaering,   // gjærer
        Modning,    // modner/lagres
        Klar,       // klar til tapping
        Tappet,     // ferdig tappet på flasker
        Kassert     // kastet
    }

    // Selve sideren. Beholder identiteten sin uansett hvor mange ganger den flyttes.
    // All historikk (epler, gjær, målinger, flyttinger) henger på batchen, ikke på tanken.
    public class Batch
    {
        // Batch ID (primærnøkkel, lages automatisk)
        public int BatchId { get; set; }

        // Batch-nummer f.eks. "2026-014"
        // Settes som unik i LagerDbContext
        [Required(ErrorMessage = "Batchen må ha en kode")]
        [StringLength(30)]
        [Display(Name = "Batchkode")]
        public string BatchCode { get; set; } = string.Empty;

        // Valgfritt kallenavn, f.eks. "Tidlig Gravenstein"
        [StringLength(100)]
        [Display(Name = "Arbeidsnavn")]
        public string? Name { get; set; }

        // Hvilket produkt batchen skal bli (fremmednøkkel til Product)
        // int? gjør det valgfritt å fylle inn. Fordi man ikke alltid vet hvilker sidertype den skal bli. (?)
        [Display(Name = "Produkt-id")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        // Loggfør når batchen ble startet
        [DataType(DataType.Date)]
        [Display(Name = "Startdato")]
        public DateTime StartDate { get; set; } = DateTime.Today;   // standard: i dag

        // Nåværende status
        [Display(Name = "Status")]
        public BatchStatus Status { get; set; } = BatchStatus.Gjaering;   // standard: gjærer

        // Hvilken tank batchen ligger i nå (fremmednøkkel til Tank)
        // Oppdateres ved hver flytting. Tom når batchen er tappet.
        [Display(Name = "Tank nå")]
        public int? CurrentTankId { get; set; }
        public virtual Tank? CurrentTank { get; set; }

        // Hvor mange liter som er igjen i batchen nå
        [Display(Name = "Volum nå (liter)")]
        public decimal CurrentVolumeLiters { get; set; }

        // Kvalitetsvurdering 1-5, for å finne de beste batchene
        // int? = kan stå tom til noen har vurdert den
        [Range(1, 5, ErrorMessage = "Kvalitet må være mellom 1 og 5")]
        [Display(Name = "Kvalitet (1-5)")]
        public int? QualityRating { get; set; }

        // Merkes true hvis sideren ble dårlig. Brukes til å finne tanker som går igjen.
        [Display(Name = "Merket som dårlig")]
        public bool IsFlaggedBad { get; set; }

        // Frie notater
        [StringLength(1000)]
        [Display(Name = "Notater")]
        public string? Notes { get; set; }

        // ---- Historikk: lister med alt som har skjedd med batchen ----
        // Hver liste er en en-til-mange-kobling: én batch har mange av hver

        // Hvilke epler som gikk inn
        public virtual List<AppleInput>? AppleInputs { get; set; }

        // Gjær, næringsstoffer og annet som er tilsatt
        public virtual List<BatchAddition>? Additions { get; set; }

        // Alle målinger (sukker, pH, temperatur osv.)
        public virtual List<Measurement>? Measurements { get; set; }

        // Smaksnotater
        public virtual List<TastingNote>? TastingNotes { get; set; }

        // Alle flyttinger mellom tanker
        public virtual List<TankTransfer>? Transfers { get; set; }

        // Pastoriseringsrunder
        public virtual List<Pasteurization>? Pasteurizations { get; set; }

        // Tappinger på flasker
        public virtual List<Bottling>? Bottlings { get; set; }

        // ---- Blanding: "slektstreet" ----

        // Batchene denne batchen er blandet AV (foreldrene)
        public virtual List<BatchParent>? Parents { get; set; }

        // Blandingene denne batchen har gått INN I (barna)
        public virtual List<BatchParent>? Children { get; set; }
    }
}