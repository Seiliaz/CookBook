using CookBook.Recipes;
using CookBook.Recipes.Ingredients;

namespace CookBook.UserInteraction
{
    public interface IRecipesUserInteraction
    {
        void PrintExistingRecipes(IEnumerable<Recipe> recipes);
        void PromptToCreateRecipe();
        IEnumerable<Ingredient> ReadIngredientsFromUser();
        void ShowMessage(string message);
        void Exit();
    }
}
