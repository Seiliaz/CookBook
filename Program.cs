var cookiesRecipesApp = new CookieRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction()
);
cookiesRecipesApp.Run();

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

    public void Run()
    {
        var allRecipes = _recipesRepository.Read("filePath");
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesUserInteraction.PromptToCreateRecipe();
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();
        if (ingredients.Count > 0)
        {
            var recipes = new Recipe(ingredients);
            allRecipes.Add(recipe);
            _recipesRepository.Write("filePath", allRecipes);
            _recipesUserInteraction.ShowMessage("Recipe added:");
            _recipesUserInteraction.ShowMessage(recipe.toString());
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

public interface IRecipesRepository
{
    string Read(string filePath);
    void Write(string filePath, string text);
}

public class RecipesRepository : IRecipesRepository
{
    public string Read(string filePath) => filePath;
    public void Write(string filePath, string text)
    {
        //
    }
}

public interface IRecipesUserInteraction
{
    void PrintExistingRecipes(string data);
    void PromptToCreateRecipe();
    List<string> ReadIngredientsFromUser();
    void ShowMessage(string message);
    void Exit();
}

public class RecipesConsoleUserInteraction : IRecipesUserInteraction
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
