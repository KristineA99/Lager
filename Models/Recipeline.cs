using System.ComponentModel.DataAnnotations;   

namespace Lager.Models
{
    // Én linje i resepten til et produkt: hvor mye av en innsatsvare
    // som trengs per liter ferdig sider. Brukes til å regne ut varebehov.
    // Eksempel for 0,75 l-flasker: 1,33 flasker per liter, 1,33 korker per liter.
    // Skal man lage 2000 liter, ganges alle linjene med 2000.
    public class RecipeLine
    {
        // ID (primærnøkkel, lages automatisk)
        public int RecipeLineId { get; set; }

        // Hvilket produkt resepten gjelder (fremmednøkkel til Product)
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        // Hvilken innsatsvare som trengs (fremmednøkkel til Supply)
        [Display(Name = "Innsatsvare")]
        public int SupplyId { get; set; }
        public virtual Supply? Supply { get; set; }

        // Mengde per liter ferdig produkt, i innsatsvarens enhet
        [Range(0.000001, 100000, ErrorMessage = "Mengden må være større enn 0")]
        [Display(Name = "Mengde per liter")]
        public decimal AmountPerLiter { get; set; }
    }
}