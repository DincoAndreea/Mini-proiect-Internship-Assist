using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Shared
{
    public class Ingredient
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public Recipe? Recipe { get; set; }
        public IngredientNote? IngredientNote { get; set; }
        public int IngredientNoteId { get; set; }
        public int RecipeId { get; set; }
    }
}
