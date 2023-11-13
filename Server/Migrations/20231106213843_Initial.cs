using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cookbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookbooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepTime = table.Column<int>(type: "int", nullable: true),
                    CookTime = table.Column<int>(type: "int", nullable: true),
                    AdditionalTime = table.Column<int>(type: "int", nullable: true),
                    TotalTime = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    Yield = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookbookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Cookbooks_CookbookId",
                        column: x => x.CookbookId,
                        principalTable: "Cookbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientNotes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionFacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    Fat = table.Column<float>(type: "real", nullable: false),
                    Carbs = table.Column<float>(type: "real", nullable: false),
                    Protein = table.Column<float>(type: "real", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionFacts_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngredientNoteId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientNotes_IngredientNoteId",
                        column: x => x.IngredientNoteId,
                        principalTable: "IngredientNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cookbooks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fall Recipes" },
                    { 2, "Summer Recipes" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "AdditionalTime", "CookTime", "CookbookId", "Description", "Image", "Note", "PrepTime", "Servings", "Title", "TotalTime", "Yield" },
                values: new object[,]
                {
                    { 1, 40, 45, 1, "This apple cranberry pie is one of our favorite pies to make for Thanksgiving or the holidays! Flaky buttery crust, spiced apples mixed with dried cranberries and brandy.", "https://www.simplyrecipes.com/thmb/ld2o-VISLJKTwF8PN_6Q7x4DkOw=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2005__11__apple-cranberry-pie-vertical-b-1800-64e0a7c2cef64f89b30490f5b25679ca.jpg", "As you peel, core, and slice the apples, put the apple slices in a bowl and sprinkle with a little lemon juice or apple cider vinegar to prevent them from discoloring.", 30, 8, "Apple Pie", 115, "1 pie" },
                    { 2, null, 15, 2, "This Cold Rice Noodle Salad tosses cool, slick rice noodles with crisp shredded vegetables and a savory sesame dressing for the perfect summer salad. ", "https://www.simplyrecipes.com/thmb/nokA3G2XT3G6t3QN79T3-pbdzQI=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Simply-Redipes-Cold-Noodle-Salad-LEAD-6-c95c84f15e6947219ca26f40a49ee691.jpg", null, 40, 2, "Cold Rice Noodle Salad", 55, "1 salad" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientNotes_RecipeId",
                table: "IngredientNotes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientNoteId",
                table: "Ingredients",
                column: "IngredientNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFacts_RecipeId",
                table: "NutritionFacts",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CookbookId",
                table: "Recipes",
                column: "CookbookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "NutritionFacts");

            migrationBuilder.DropTable(
                name: "IngredientNotes");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Cookbooks");
        }
    }
}
