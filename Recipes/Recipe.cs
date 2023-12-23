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
    }
}
