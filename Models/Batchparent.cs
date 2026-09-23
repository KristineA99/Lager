using System.ComponentModel.DataAnnotations; 

namespace Lager.Models
{
    // Kobling for blandingssider (slektstre).
    // Hvis rosé-batch C er laget av batch A og B, finnes det to rader:
    //   (barn: C, forelder: A, liter)
    //   (barn: C, forelder: B, liter)
    // Denne klassen har ingen egen ID. Nøkkelen er kombinasjonen av
    // ChildBatchId og ParentBatchId, og den settes opp i LagerDbContext.
    public class BatchParent
    {
        // Den nye blandingen (barnet), fremmednøkkel til Batch
        public int ChildBatchId { get; set; }
        public virtual Batch? ChildBatch { get; set; }

        // Batchen som ble brukt i blandingen (forelderen), fremmednøkkel til Batch
        public int ParentBatchId { get; set; }
        public virtual Batch? ParentBatch { get; set; }

        // Hvor mange liter fra forelderen som gikk inn i blandingen
        [Range(0.1, 100000, ErrorMessage = "Liter må være større enn 0")]
        [Display(Name = "Liter brukt")]
        public decimal Liters { get; set; }
    }
}