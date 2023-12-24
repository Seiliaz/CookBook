using CookBook.Recipes;
using CookBook.Recipes.Ingredients;

namespace CookBook.UserInteraction
{
    public class RecipesConsoleUserInteraction : IRecipesUserInteraction
    {
        private readonly IIngredientsRegister _ingredientsRegister;

        public RecipesConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
        {
            _ingredientsRegister = ingredientsRegister;
        }

        public void PrintExistingRecipes(IEnumerable<Recipe> recipes)
        {
            if (recipes.Count() > 0)
            {
                Console.WriteLine($"Existing recipes are: {Environment.NewLine}");
                int counter = 1;
                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine($"***** {counter} *****");
                    Console.WriteLine(recipe);
                    Console.WriteLine();
                    counter++;
                }
            }
        }

        public void PromptToCreateRecipe()
        {
            Console.WriteLine("Create a new cookie recipe! "
                + "Available ingredients are: ");
            foreach (Ingredient ingredient in _ingredientsRegister.All)
            {
                Console.WriteLine(ingredient);
            }
        }

        public IEnumerable<Ingredient> ReadIngredientsFromUser()
        {
            bool shallStop = false;
            var ingredients = new List<Ingredient>();
            while (!shallStop)
            {
                Console.WriteLine("Add an ingredient by its ID, "
                    + "or type anything else if finished");
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int id))
                {
                    Ingredient selectedIngredient = _ingredientsRegister.GetById(id);
                    if (selectedIngredient is not null)
                        ingredients.Add(selectedIngredient);
                }
                else shallStop = true;
            }
            return ingredients;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Exit()
        {
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }

}
