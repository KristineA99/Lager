using System.ComponentModel.DataAnnotations; 

namespace Lager.Models
{
    // Hva slags rom det er. Blir nedtrekksliste i skjemaet.
    public enum LocationType
    {
        Tankrom,
        Kjolelager,
        Varelager,
        Annet
    }

    // Et rom eller område på gården: tankrommet, kjølelageret, varelageret osv.
    public class StorageLocation
    {
        // Lagerplass ID (primærnøkkel, lages automatisk)
        public int StorageLocationId { get; set; }

        // Navn på rommet, f.eks. "Tankrom 1"
        [Required(ErrorMessage = "Lagerplassen må ha et navn")]
        [StringLength(100)]
        [Display(Name = "Navn")]
        public string Name { get; set; } = string.Empty;

        // Type rom
        [Display(Name = "Type")]
        public LocationType Type { get; set; }

        // Beskrivelse (valgfritt)
        [StringLength(500)]
        [Display(Name = "Beskrivelse")]
        public string? Description { get; set; }

        // Bredde på rommet i meter. Brukes til å tegne plantegningen.
        [Display(Name = "Bredde (m)")]
        public decimal WidthMeters { get; set; }

        // Lengde på rommet i meter. Brukes til å tegne plantegningen.
        [Display(Name = "Lengde (m)")]
        public decimal LengthMeters { get; set; }

        // Alle tankene som står i dette rommet
        // List = en liste med mange. Ett rom har mange tanker (en-til-mange)
        public virtual List<Tank>? Tanks { get; set; }

        // Alle innsatsvarene som ligger i dette rommet
        public virtual List<Supply>? Supplies { get; set; }
    }
}