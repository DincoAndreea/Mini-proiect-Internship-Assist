using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Shared
{
    public class Recipe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
        public int? AdditionalTime { get; set; }
        public int TotalTime { get; set; }
        public int Servings { get; set; }
        public string Yield { get; set; }
        public string? Note { get; set; }
        public string? Image { get; set; }
        public Cookbook? Cookbook { get; set; }
        public int CookbookId { get; set; }
    }
}
