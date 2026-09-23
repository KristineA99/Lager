using System.ComponentModel.DataAnnotations;   

namespace Lager.Models
{
    // Én pastoriseringsrunde for en batch
    public class Pasteurization
    {
        // ID (primærnøkkel, lages automatisk)
        public int PasteurizationId { get; set; }

        // Hvilken batch som ble pastorisert (fremmednøkkel til Batch)
        public int BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

        // Når pastoriseringen startet
        [Display(Name = "Startet")]
        public DateTime StartedAt { get; set; } = DateTime.Now;   // standard: akkurat nå

        // Hvor mange PU som kreves for godkjenning.
        // Her er det satt 50
        [Display(Name = "Krav (PU)")]
        public decimal RequiredPu { get; set; } = 50;

        // Hvor mange PU som ble oppnådd
        // Regnes ut fra temperaturmålingene av PasteurizationCalculator, og lagres her
        [Display(Name = "Oppnådd (PU)")]
        public decimal CalculatedPu { get; set; }

        // true hvis oppnådd PU er minst like høy som kravet
        // Beregnes hver gang, lagres ikke (settes til Ignore i LagerDbContext)
        [Display(Name = "Godkjent")]
        public bool IsApproved => CalculatedPu >= RequiredPu;

        // Alle temperaturmålingene under denne runden
        public virtual List<PasteurizationReading>? Readings { get; set; }
    }

    // Én temperaturmåling på et bestemt tidspunkt under pastoriseringen.
    // PU regnes ut fra avstanden i tid mellom målingene og temperaturen.
    public class PasteurizationReading
    {
        // ID (primærnøkkel, lages automatisk)
        public int PasteurizationReadingId { get; set; }

        // Hvilken pastoriseringsrunde målingen hører til (fremmednøkkel)
        public int PasteurizationId { get; set; }

        // Navigasjonsegenskap: hele pastoriseringsrunden
        public virtual Pasteurization? Pasteurization { get; set; }

        // Tidspunktet for målingen
        [Display(Name = "Tidspunkt")]
        public DateTime Time { get; set; }

        // Temperaturen i grader Celsius
        [Display(Name = "Temperatur (°C)")]
        public decimal TemperatureCelsius { get; set; }
    }
}