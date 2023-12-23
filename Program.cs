var cookiesRecipesApp = new CookieRecipesApp();
cookiesRecipesApp.Run();

public class CookieRecipesApp
{
    private readonly RecipesRepository _recipesRepository = new();
    private readonly RecipesConsoleUserInteraction _recipesConsoleUserInteraction = new();

    // public CookieRecipesApp(
    //     RecipesRepository recipesRepository,
    //     RecipesConsoleUserInteraction recipesConsoleUserInteraction
    // )
    // {
    //     _recipesRepository = recipesRepository;
    //     _recipesConsoleUserInteraction = recipesConsoleUserInteraction;
    // }

    public void Run()
    {
        var allRecipes = _recipesRepository.Read("filePath");
        _recipesConsoleUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesConsoleUserInteraction.PromptToCreateRecipe();
        var ingredients = _recipesConsoleUserInteraction.ReadIngredientsFromUser();
        if (ingredients.Count > 0)
        {
            var recipes = new Recipe(ingredients);
            allRecipes.Add(recipe);
            _recipesRepository.Write("filePath", allRecipes);
            _recipesConsoleUserInteraction.ShowMessage("Recipe added:");
            _recipesConsoleUserInteraction.ShowMessage(recipe.toString());
            _recipesConsoleUserInteraction.Exit();
        }
        else
        {
            _recipesConsoleUserInteraction.ShowMessage(
                "No ingredients have been selected." +
                "Recipe will not be saved!"
            );
        }
    }
}

public class RecipesRepository
{
    public string Read(string filePath) => filePath;
    public void Write(string filePath, string text)
    {
        //
    }
}

public class RecipesConsoleUserInteraction
{
    public void PrintExistingRecipes(string data)
    {
        //
    }

    public void PromptToCreateRecipe()
    {
        //
    }

    public List<string> ReadIngredientsFromUser()
    {
        return new List<string>();
    }

    public void ShowMessage(string message)
    {
        //
    }

    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
    }
}

public class Recipe
{
    public Recipe(List<string> ingredients)
    {
        //
    }
}
