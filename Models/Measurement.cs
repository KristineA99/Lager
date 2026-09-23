using System.ComponentModel.DataAnnotations;  

namespace Lager.Models
{
    // Hva som ble målt. Blir nedtrekksliste i skjemaet.
    public enum MeasurementType
    {
        Sukker,       // f.eks. Brix fra Anton Paar
        Egenvekt,     // SG / tetthet
        pH,
        Alkohol,
        Temperatur,
        Annet
    }

    // Hvor målingen kom fra
    public enum MeasurementSource
    {
        Manuell,      // skrevet inn for hånd
        AntonPaar,    // fra Anton Paar-instrumentet
        Import        // hentet fra temperaturprogrammet
    }

    // Én felles tabell for alle målinger.
    // Ny type måling = ny verdi i enumen over, ikke nye kolonner i databasen.
    public class Measurement
    {
        // Måling ID (primærnøkkel, lages automatisk)
        public int MeasurementId { get; set; }

        // Hvilken batch som ble målt (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Hvilken tank batchen lå i da målingen ble tatt (fremmednøkkel til Tank)
        // Nyttig for å se om én tank gir avvikende målinger
        [Display(Name = "Tank")]
        public int? TankId { get; set; }
        public virtual Tank? Tank { get; set; }

        // Hva som ble målt
        [Display(Name = "Type")]
        public MeasurementType Type { get; set; }

        // Selve måleverdien
        [Display(Name = "Verdi")]
        public decimal Value { get; set; }

        // Enheten til verdien, f.eks. "°Brix", "°C", "%", "g/cm³"
        [Required(ErrorMessage = "Oppgi enhet")]
        [StringLength(20)]
        [Display(Name = "Enhet")]
        public string Unit { get; set; } = string.Empty;

        // Når målingen ble tatt
        [Display(Name = "Målt")]
        public DateTime MeasuredAt { get; set; } = DateTime.Now;   // standard: akkurat nå

        // Hvor målingen kom fra
        [Display(Name = "Kilde")]
        public MeasurementSource Source { get; set; } = MeasurementSource.Manuell;

        // Hvem som registrerte målingen
        [StringLength(100)]
        [Display(Name = "Registrert av")]
        public string? RegisteredBy { get; set; }
    }
}