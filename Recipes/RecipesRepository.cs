using CookBook.DataAccess;
using CookBook.Recipes.Ingredients;

namespace CookBook.Recipes
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly IStringsRepository _stringsRepository;
        private readonly IIngredientsRegister _ingredientsRegister;
        private const string Seperator = ",";

        public RecipesRepository(
            IStringsRepository stringsRepository,
            IIngredientsRegister ingredientsRegister
            )
        {
            _stringsRepository = stringsRepository;
            _ingredientsRegister = ingredientsRegister;
        }

        public List<Recipe> Read(string filePath)
        {
            List<string> recipesFromFile = _stringsRepository.Read(filePath);
            var recipes = new List<Recipe>();
            foreach (string recipeFromFile in recipesFromFile)
            {
                var recipe = RecipeFromString(recipeFromFile);
                recipes.Add(recipe);
            }
            return recipes;
        }

        public void Write(string filePath, List<Recipe> recipes)
        {
            var recipesAsStrings = new List<string>();
            foreach (Recipe recipe in recipes)
            {
                var allIds = new List<int>();
                foreach (Ingredient ingredient in recipe.Ingredients)
                    allIds.Add(ingredient.Id);
                recipesAsStrings.Add(string.Join(Seperator, allIds));
            }
            _stringsRepository.Write(filePath, recipesAsStrings);
        }

        private Recipe RecipeFromString(string recipeFromFile)
        {
            var textualIds = recipeFromFile.Split(Seperator);
            var ingredients = new List<Ingredient>();
            foreach (var textualId in textualIds)
            {
                var id = int.Parse(textualId);
                ingredients.Add(_ingredientsRegister.GetById(id));
            }
            return new Recipe(ingredients);
        }
    }

}
