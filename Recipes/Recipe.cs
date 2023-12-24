using CookBook.Recipes.Ingredients;
namespace CookBook.Recipes
{
    public class Recipe
    {
        public IEnumerable<Ingredient> Ingredients { get; }

        public Recipe(IEnumerable<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }

        public override string ToString()
        {
            List<string> steps = new List<string>();
            foreach (Ingredient ingredient in Ingredients)
            {
                steps.Add($"{ingredient.Name}. {ingredient.PreparationInstructions}");
            }
            return string.Join(Environment.NewLine, steps);
        }
    }
}
