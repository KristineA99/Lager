using Microsoft.EntityFrameworkCore;  
using Lager.Models;                  

namespace Lager.DAL
{
    // Fyller databasen med startdata slik at vi har noe å teste med.
    // "static" betyr at vi ikke trenger å lage et objekt av klassen,
    // vi kan bare kalle DBInit.Seed(app) direkte fra Program.cs.
    public static class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            // Lager et "scope" og henter en LagerDbContext fra dependency injection.
            // "using" gjør at den ryddes bort automatisk når metoden er ferdig.
            using var serviceScope = app.ApplicationServices.CreateScope();
            LagerDbContext context = serviceScope.ServiceProvider.GetRequiredService<LagerDbContext>();

            // Kjører migrasjoner som ikke er kjørt ennå.
            // I motsetning til demoen sletter vi IKKE databasen,
            // så alt du legger inn via nettleseren blir liggende.
            context.Database.Migrate();

            // ---- Lagerplasser ----
            // Legges bare inn hvis tabellen er tom, så de ikke dobles ved hver oppstart
            if (!context.StorageLocations.Any())
            {
                var locations = new List<StorageLocation>
                {
                    new StorageLocation
                    {
                        Name = "Tankrom",
                        Type = LocationType.Tankrom,
                        Description = "Gjæring og lagring på tank",
                        WidthMeters = 12,     // PLASSHOLDER: mål opp rommet
                        LengthMeters = 8
                    },
                    new StorageLocation
                    {
                        Name = "Kjølelager",
                        Type = LocationType.Kjolelager,
                        Description = "Ferdig tappet sider og friske bær",
                        WidthMeters = 6,
                        LengthMeters = 4
                    },
                    new StorageLocation
                    {
                        Name = "Varelager",
                        Type = LocationType.Varelager,
                        Description = "Flasker, korker, etiketter og kartonger",
                        WidthMeters = 8,
                        LengthMeters = 6
                    }
                };
                context.AddRange(locations);
                context.SaveChanges();   // Lagrer, slik at lagerplassene får ID-er
            }

            // Henter lagerplassene fra databasen så vi kan koble varer og tanker til dem
            var tankrom = context.StorageLocations.First(l => l.Type == LocationType.Tankrom);
            var kjolelager = context.StorageLocations.First(l => l.Type == LocationType.Kjolelager);
            var varelager = context.StorageLocations.First(l => l.Type == LocationType.Varelager);

            // ---- Produkter (ferdigvarer) ----
            // Navnene er Aga Sideri sine produkter.
            // Volum, pris og antall er PLASSHOLDERE: spør Joar om de riktige tallene.
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Lagmann",          Type = ProductType.Sider,      VolumeMl = 750, Price = 100, QuantityInStock = 240, MinimumStock = 50 },
                    new Product { Name = "Bøddel",           Type = ProductType.Sider,      VolumeMl = 750, Price = 100, QuantityInStock = 30,  MinimumStock = 50 },  // under minimum: tester varsel
                    new Product { Name = "Humlepung",        Type = ProductType.Sider,      VolumeMl = 330, Price = 50,  QuantityInStock = 600, MinimumStock = 100 },
                    new Product { Name = "Humlepung Rosé",   Type = ProductType.Sider,      VolumeMl = 330, Price = 50,  QuantityInStock = 0,   MinimumStock = 100 }, // tomt: tester varsel
                    new Product { Name = "Humlesus",         Type = ProductType.Alkoholfri, VolumeMl = 330, Price = 40,  QuantityInStock = 400, MinimumStock = 100 },
                    new Product { Name = "Eplemost",         Type = ProductType.Most,       VolumeMl = 750, Price = 60,  QuantityInStock = 150, MinimumStock = 40 }
                };
                context.AddRange(products);
                context.SaveChanges();
            }

            // ---- Innsatsvarer ----
            // Mengder er PLASSHOLDERE. Noen er satt under minimum for å teste varsler.
            if (!context.Supplies.Any())
            {
                var supplies = new List<Supply>
                {
                    // Emballasje
                    new Supply { Name = "Flaske 750 ml",    Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 2000, MinimumStock = 500, StorageLocation = varelager, Shelf = "A1" },
                    new Supply { Name = "Flaske 330 ml",    Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 300,  MinimumStock = 1000, StorageLocation = varelager, Shelf = "A2" }, // under minimum
                    new Supply { Name = "Kork",             Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 5000, MinimumStock = 1000, StorageLocation = varelager, Shelf = "B1" },
                    new Supply { Name = "Kapsel",           Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 4000, MinimumStock = 1000, StorageLocation = varelager, Shelf = "B2" },
                    new Supply { Name = "Etikett Lagmann",  Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 800,  MinimumStock = 200, StorageLocation = varelager, Shelf = "C1" },
                    new Supply { Name = "Kartong 6-pakk",   Category = SupplyCategory.Emballasje, Unit = "stk", QuantityInStock = 50,   MinimumStock = 100, StorageLocation = varelager, Shelf = "D1" }, // under minimum

                    // Ingredienser
                    new Supply { Name = "Sidergjær",        Category = SupplyCategory.Ingrediens, Unit = "kg",  QuantityInStock = 2.5m, MinimumStock = 1,   StorageLocation = kjolelager, Supplier = "PLASSHOLDER" },
                    new Supply { Name = "Gjærnæring",       Category = SupplyCategory.Ingrediens, Unit = "kg",  QuantityInStock = 0.5m, MinimumStock = 1,   StorageLocation = varelager },  // under minimum
                    
                    // Smakstilsetning
                    new Supply { Name = "Humle",            Category = SupplyCategory.Smakstilsetning, Unit = "kg",  QuantityInStock = 4,    MinimumStock = 1,   StorageLocation = kjolelager },
                    new Supply { Name = "Bringebær",        Category = SupplyCategory.Smakstilsetning, Unit = "kg",  QuantityInStock = 20,   MinimumStock = 5,   StorageLocation = kjolelager },
                    new Supply { Name = "Rips",             Category = SupplyCategory.Smakstilsetning, Unit = "kg",  QuantityInStock = 10,   MinimumStock = 5,   StorageLocation = kjolelager },
                
                    // Rengjøring
                    new Supply { Name = "Tankvask",         Category = SupplyCategory.Rengjoring, Unit = "liter", QuantityInStock = 25, MinimumStock = 10, StorageLocation = varelager }
                };
                // Merk: vi setter StorageLocation (objektet) i stedet for StorageLocationId.
                // Entity Framework finner ID-en selv.
                context.AddRange(supplies);
                context.SaveChanges();
            }

            // ---- Tanker ----
            // Antall, størrelse, RFID og plassering er PLASSHOLDERE.
            // PositionX/Y er meter fra venstre og øverste vegg i tankrommet.
            if (!context.Tanks.Any())
            {
                var tanks = new List<Tank>
                {
                    new Tank { Name = "Tank 1", RfidTag = "RFID-0001", CapacityLiters = 2000, StorageLocation = tankrom, PositionX = 2,  PositionY = 2, DiameterMeters = 1.5m },
                    new Tank { Name = "Tank 2", RfidTag = "RFID-0002", CapacityLiters = 2000, StorageLocation = tankrom, PositionX = 5,  PositionY = 2, DiameterMeters = 1.5m },
                    new Tank { Name = "Tank 3", RfidTag = "RFID-0003", CapacityLiters = 1000, StorageLocation = tankrom, PositionX = 8,  PositionY = 2, DiameterMeters = 1.2m },
                    new Tank { Name = "Tank 4", RfidTag = "RFID-0004", CapacityLiters = 1000, StorageLocation = tankrom, PositionX = 2,  PositionY = 5, DiameterMeters = 1.2m },
                    new Tank { Name = "Tank 5", RfidTag = "RFID-0005", CapacityLiters = 500,  StorageLocation = tankrom, PositionX = 5,  PositionY = 5, DiameterMeters = 1.0m }
                };
                context.AddRange(tanks);
                context.SaveChanges();
            }
        }
    }
}