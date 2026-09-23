using Lager.Models;

namespace Lager.Services
{
    public static class PasteurizationCalculator
    {
        // Standardformel: 1 PU = 1 minutt ved 60 °C.
        // PU = minutter * 1,393^(T - 60), summert over hele forløpet.
        // Referansetemperatur og faktor bør bekreftes med Joar.
        private const double ReferenceTemp = 60.0;
        private const double Factor = 1.393;

        public static decimal CalculatePu(IEnumerable<PasteurizationReading> readings)
        {
            var sorted = readings.OrderBy(r => r.Time).ToList();
            double totalPu = 0;

            // Går gjennom hvert intervall mellom to målinger
            for (int i = 1; i < sorted.Count; i++)
            {
                double minutes = (sorted[i].Time - sorted[i - 1].Time).TotalMinutes;
                double avgTemp = (double)(sorted[i].TemperatureCelsius + sorted[i - 1].TemperatureCelsius) / 2;
                totalPu += minutes * Math.Pow(Factor, avgTemp - ReferenceTemp);
            }

            return Math.Round((decimal)totalPu, 1);
        }
    }
}