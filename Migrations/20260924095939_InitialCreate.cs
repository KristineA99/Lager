using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeMl = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    QuantityInStock = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimumStock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "StorageLocations",
                columns: table => new
                {
                    StorageLocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    WidthMeters = table.Column<decimal>(type: "TEXT", nullable: false),
                    LengthMeters = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocations", x => x.StorageLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    SupplyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    QuantityInStock = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinimumStock = table.Column<decimal>(type: "TEXT", nullable: false),
                    Supplier = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StorageLocationId = table.Column<int>(type: "INTEGER", nullable: true),
                    Shelf = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.SupplyId);
                    table.ForeignKey(
                        name: "FK_Supplies_StorageLocations_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "StorageLocationId");
                });

            migrationBuilder.CreateTable(
                name: "Tanks",
                columns: table => new
                {
                    TankId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RfidTag = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CapacityLiters = table.Column<decimal>(type: "TEXT", nullable: false),
                    StorageLocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionX = table.Column<decimal>(type: "TEXT", nullable: false),
                    PositionY = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiameterMeters = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanks", x => x.TankId);
                    table.ForeignKey(
                        name: "FK_Tanks_StorageLocations_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "StorageLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeLines",
                columns: table => new
                {
                    RecipeLineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplyId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountPerLiter = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeLines", x => x.RecipeLineId);
                    table.ForeignKey(
                        name: "FK_RecipeLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeLines_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "SupplyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchCode = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentTankId = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrentVolumeLiters = table.Column<decimal>(type: "TEXT", nullable: false),
                    QualityRating = table.Column<int>(type: "INTEGER", nullable: true),
                    IsFlaggedBad = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_Batches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Batches_Tanks_CurrentTankId",
                        column: x => x.CurrentTankId,
                        principalTable: "Tanks",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AppleInputs",
                columns: table => new
                {
                    AppleInputId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Variety = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Kilograms = table.Column<decimal>(type: "TEXT", nullable: false),
                    Field = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    HarvestDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleInputs", x => x.AppleInputId);
                    table.ForeignKey(
                        name: "FK_AppleInputs_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchAdditions",
                columns: table => new
                {
                    BatchAdditionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Purpose = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BatchVolumeLiters = table.Column<decimal>(type: "TEXT", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RegisteredBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchAdditions", x => x.BatchAdditionId);
                    table.ForeignKey(
                        name: "FK_BatchAdditions_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchAdditions_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "SupplyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchParents",
                columns: table => new
                {
                    ChildBatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentBatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Liters = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchParents", x => new { x.ChildBatchId, x.ParentBatchId });
                    table.ForeignKey(
                        name: "FK_BatchParents_Batches_ChildBatchId",
                        column: x => x.ChildBatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchParents_Batches_ParentBatchId",
                        column: x => x.ParentBatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bottlings",
                columns: table => new
                {
                    BottlingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    LotNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BottledAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BottleCount = table.Column<int>(type: "INTEGER", nullable: false),
                    BestBefore = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottlings", x => x.BottlingId);
                    table.ForeignKey(
                        name: "FK_Bottlings_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bottlings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    TankId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Source = table.Column<int>(type: "INTEGER", nullable: false),
                    RegisteredBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_Measurements_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Measurements_Tanks_TankId",
                        column: x => x.TankId,
                        principalTable: "Tanks",
                        principalColumn: "TankId");
                });

            migrationBuilder.CreateTable(
                name: "Pasteurizations",
                columns: table => new
                {
                    PasteurizationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequiredPu = table.Column<decimal>(type: "TEXT", nullable: false),
                    CalculatedPu = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasteurizations", x => x.PasteurizationId);
                    table.ForeignKey(
                        name: "FK_Pasteurizations_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TankTransfers",
                columns: table => new
                {
                    TankTransferId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromTankId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToTankId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Liters = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransferredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RegisteredBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankTransfers", x => x.TankTransferId);
                    table.ForeignKey(
                        name: "FK_TankTransfers_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TankTransfers_Tanks_FromTankId",
                        column: x => x.FromTankId,
                        principalTable: "Tanks",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TankTransfers_Tanks_ToTankId",
                        column: x => x.ToTankId,
                        principalTable: "Tanks",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TastingNotes",
                columns: table => new
                {
                    TastingNoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: true),
                    Author = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TastingNotes", x => x.TastingNoteId);
                    table.ForeignKey(
                        name: "FK_TastingNotes_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasteurizationReadings",
                columns: table => new
                {
                    PasteurizationReadingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PasteurizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TemperatureCelsius = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasteurizationReadings", x => x.PasteurizationReadingId);
                    table.ForeignKey(
                        name: "FK_PasteurizationReadings_Pasteurizations_PasteurizationId",
                        column: x => x.PasteurizationId,
                        principalTable: "Pasteurizations",
                        principalColumn: "PasteurizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppleInputs_BatchId",
                table: "AppleInputs",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchAdditions_BatchId",
                table: "BatchAdditions",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchAdditions_SupplyId",
                table: "BatchAdditions",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BatchCode",
                table: "Batches",
                column: "BatchCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CurrentTankId",
                table: "Batches",
                column: "CurrentTankId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchParents_ParentBatchId",
                table: "BatchParents",
                column: "ParentBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bottlings_BatchId",
                table: "Bottlings",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bottlings_LotNumber",
                table: "Bottlings",
                column: "LotNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bottlings_ProductId",
                table: "Bottlings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_BatchId",
                table: "Measurements",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_TankId",
                table: "Measurements",
                column: "TankId");

            migrationBuilder.CreateIndex(
                name: "IX_PasteurizationReadings_PasteurizationId",
                table: "PasteurizationReadings",
                column: "PasteurizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasteurizations_BatchId",
                table: "Pasteurizations",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLines_ProductId",
                table: "RecipeLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLines_SupplyId",
                table: "RecipeLines",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_StorageLocationId",
                table: "Supplies",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_RfidTag",
                table: "Tanks",
                column: "RfidTag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_StorageLocationId",
                table: "Tanks",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TankTransfers_BatchId",
                table: "TankTransfers",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TankTransfers_FromTankId",
                table: "TankTransfers",
                column: "FromTankId");

            migrationBuilder.CreateIndex(
                name: "IX_TankTransfers_ToTankId",
                table: "TankTransfers",
                column: "ToTankId");

            migrationBuilder.CreateIndex(
                name: "IX_TastingNotes_BatchId",
                table: "TastingNotes",
                column: "BatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppleInputs");

            migrationBuilder.DropTable(
                name: "BatchAdditions");

            migrationBuilder.DropTable(
                name: "BatchParents");

            migrationBuilder.DropTable(
                name: "Bottlings");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "PasteurizationReadings");

            migrationBuilder.DropTable(
                name: "RecipeLines");

            migrationBuilder.DropTable(
                name: "TankTransfers");

            migrationBuilder.DropTable(
                name: "TastingNotes");

            migrationBuilder.DropTable(
                name: "Pasteurizations");

            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tanks");

            migrationBuilder.DropTable(
                name: "StorageLocations");
        }
    }
}
