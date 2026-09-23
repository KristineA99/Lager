using System.ComponentModel.DataAnnotations;   

namespace Lager.Models
{
    // Den fysiske tanken. Den står fast, men innholdet (batchen) kan flyttes.
    public class Tank
    {
        // Tank ID (primærnøkkel, lages automatisk)
        public int TankId { get; set; }

        // Navn på tanken, f.eks. "Tank 3"
        [Required(ErrorMessage = "Tanken må ha et navn")]
        [StringLength(50)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Koden fra RFID-brikken på tanken
        // Settes som unik i LagerDbContext, så to tanker ikke kan ha samme kode
        [StringLength(100)]
        [Display(Name = "RFID")]
        public string? RfidTag { get; set; }

        // Hvor mange liter tanken rommer
        [Range(1, 100000, ErrorMessage = "Kapasitet må være mellom 1 og 100 000 liter")]
        [Display(Name = "Kapasitet (liter)")]
        public decimal CapacityLiters { get; set; }

        // Hvilket rom tanken står i (fremmednøkkel til StorageLocation)
        [Display(Name = "Lagerplass")]
        public int StorageLocationId { get; set; }
        public virtual StorageLocation? StorageLocation { get; set; }

        // Plassering i plantegningen: meter fra venstre vegg
        [Display(Name = "Posisjon X (m)")]
        public decimal PositionX { get; set; }

        // Plassering i plantegningen: meter fra øverste vegg
        [Display(Name = "Posisjon Y (m)")]
        public decimal PositionY { get; set; }

        // Hvor stor tanken tegnes i plantegningen
        // = 1.5m gir standardverdi 1,5 meter (m-en betyr at tallet er decimal)
        [Display(Name = "Diameter (m)")]
        public decimal DiameterMeters { get; set; } = 1.5m;

        // Om tanken er i bruk. Gamle tanker kan skjules i stedet for å slettes,
        // slik at historikken deres blir liggende.
        [Display(Name = "I bruk")]
        public bool IsActive { get; set; } = true;

        // Frie notater, f.eks. om vedlikehold
        [StringLength(500)]
        [Display(Name = "Notater")]
        public string? Notes { get; set; }

        // Batchen(e) som ligger i tanken akkurat nå
        public virtual List<Batch>? CurrentBatches { get; set; }
    }
}