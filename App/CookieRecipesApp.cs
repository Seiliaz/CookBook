using CookBook.Recipes;
using CookBook.UserInteraction;

namespace CookBook.App
{
    public class CookieRecipesApp
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IRecipesUserInteraction _recipesUserInteraction;

        public CookieRecipesApp(
            IRecipesRepository recipesRepository,
            IRecipesUserInteraction recipesConsoleUserInteraction
        )
        {
            _recipesRepository = recipesRepository;
            _recipesUserInteraction = recipesConsoleUserInteraction;
        }

        public void Run(string filePath)
        {
            var allRecipes = _recipesRepository.Read(filePath);
            _recipesUserInteraction.PrintExistingRecipes(allRecipes);
            _recipesUserInteraction.PromptToCreateRecipe();
            var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();
            if (ingredients.Count() > 0)
            {
                var recipe = new Recipe(ingredients);
                allRecipes.Add(recipe);
                _recipesRepository.Write(filePath, allRecipes);
                _recipesUserInteraction.ShowMessage("Recipe added:");
                var test = recipe.ToString();
                _recipesUserInteraction.ShowMessage(test);
                _recipesUserInteraction.Exit();
            }
            else
            {
                _recipesUserInteraction.ShowMessage(
                    "No ingredients have been selected." +
                    "Recipe will not be saved!"
                );
            }
        }
    }

}
