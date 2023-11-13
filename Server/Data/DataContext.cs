using Microsoft.EntityFrameworkCore;
using RecipeApp.Shared;
using System.Reflection.Emit;

namespace RecipeApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        public DbSet<Cookbook> Cookbooks { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<IngredientNote> IngredientNotes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<NutritionFacts> NutritionFacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasOne(x => x.Recipe)
                .WithMany()
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ingredient>()
                .HasOne(x => x.IngredientNote)
                .WithMany()
                .HasForeignKey(x => x.IngredientNoteId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Cookbook>().HasData(
                new Cookbook { Id = 1, Name = "Fall Recipes"},
                new Cookbook { Id = 2, Name = "Summer Recipes"}
                );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Title = "Apple Pie", Description = "This apple cranberry pie is one of our favorite pies to make for Thanksgiving or the holidays! Flaky buttery crust, spiced apples mixed with dried cranberries and brandy.", Image = "https://www.simplyrecipes.com/thmb/ld2o-VISLJKTwF8PN_6Q7x4DkOw=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2005__11__apple-cranberry-pie-vertical-b-1800-64e0a7c2cef64f89b30490f5b25679ca.jpg", CookTime = 45, PrepTime = 30, AdditionalTime = 40, TotalTime = 115, Note = "As you peel, core, and slice the apples, put the apple slices in a bowl and sprinkle with a little lemon juice or apple cider vinegar to prevent them from discoloring.", Servings = 8, Yield = "1 pie", CookbookId = 1 },
                new Recipe { Id = 2, Title = "Cold Rice Noodle Salad", Description = "This Cold Rice Noodle Salad tosses cool, slick rice noodles with crisp shredded vegetables and a savory sesame dressing for the perfect summer salad. ", Image = "https://www.simplyrecipes.com/thmb/nokA3G2XT3G6t3QN79T3-pbdzQI=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Simply-Redipes-Cold-Noodle-Salad-LEAD-6-c95c84f15e6947219ca26f40a49ee691.jpg", CookTime = 15, PrepTime = 40, TotalTime = 55, Servings = 2, Yield = "1 salad", CookbookId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}
