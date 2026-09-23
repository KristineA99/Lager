using System.ComponentModel.DataAnnotations; 

namespace Lager.Models
{
    // Hva slags flytting det er. Blir nedtrekksliste i skjemaet.
    public enum TransferType
    {
        Fylling,      // inn i tank for første gang (fra presse)
        Flytting,     // fra en tank til en annen
        Filtrering,   // filtrert over i ny tank
        Blanding,     // inn i en blandingsbatch
        Tapping       // ut av tank til flasker
    }

    // Én rad for hver gang en batch flyttes.
    // Denne tabellen gjør at historikken aldri forsvinner ved filtrering,
    // og lar oss finne alle batcher som har vært i en bestemt tank.
    public class TankTransfer
    {
        // ID (primærnøkkel, lages automatisk)
        public int TankTransferId { get; set; }

        // Hvilken batch som ble flyttet (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Tanken den ble flyttet FRA (fremmednøkkel til Tank)
        // Tom (null) ved Fylling, fordi sideren da kom fra pressa
        [Display(Name = "Fra tank")]
        public int? FromTankId { get; set; }
        public virtual Tank? FromTank { get; set; }

        // Tanken den ble flyttet TIL (fremmednøkkel til Tank)
        // Tom (null) ved Tapping, fordi sideren da gikk på flasker
        [Display(Name = "Til tank")]
        public int? ToTankId { get; set; }
        public virtual Tank? ToTank { get; set; }

        // Hva slags flytting
        [Display(Name = "Type")]
        public TransferType Type { get; set; }

        // Hvor mange liter som ble flyttet
        [Range(0.1, 100000, ErrorMessage = "Liter må være større enn 0")]
        [Display(Name = "Liter")]
        public decimal Liters { get; set; }

        // Når flyttingen skjedde
        [Display(Name = "Tidspunkt")]
        public DateTime TransferredAt { get; set; } = DateTime.Now;   // standard: akkurat nå

        // Kommentar, f.eks. hvilket filter som ble brukt
        [StringLength(500)]
        [Display(Name = "Kommentar")]
        public string? Comment { get; set; }

        // Hvem som registrerte flyttingen
        [StringLength(100)]
        [Display(Name = "Registrert av")]
        public string? RegisteredBy { get; set; }
    }
}