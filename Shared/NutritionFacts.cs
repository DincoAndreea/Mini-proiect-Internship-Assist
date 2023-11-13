using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Shared
{
    public class NutritionFacts
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Calories { get; set; }
        public float Fat { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }
        public Recipe? Recipe { get; set; }
        public int RecipeId { get; set; }

    }
}
