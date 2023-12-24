using CookBook.Recipes;
using CookBook.Recipes.Ingredients;

var cookiesRecipesApp = new CookieRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction(new IngredientsRegister())
);
cookiesRecipesApp.Run("recipes.txt");

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
        var allRecipes = _recipesRepository.Read("filePath");
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesUserInteraction.PromptToCreateRecipe();
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();
        // if (ingredients.Count > 0)
        // {
        //     var recipes = new Recipe(ingredients);
        //     allRecipes.Add(recipe);
        //     _recipesRepository.Write("filePath", allRecipes);
        //     _recipesUserInteraction.ShowMessage("Recipe added:");
        //     _recipesUserInteraction.ShowMessage(recipe.toString());
        //     _recipesUserInteraction.Exit();
        // }
        // else
        // {
        //     _recipesUserInteraction.ShowMessage(
        //         "No ingredients have been selected." +
        //         "Recipe will not be saved!"
        //     );
        // }
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
    void Write(string filePath, string text);
}

public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>
        {
            new Recipe(new List<Ingredient> {
                new WheatFlour(),
                new Butter(),
                new Sugar(),
            }),
            new Recipe(new List<Ingredient> {
                new CocoaPowder(),
                new SpeltFlour(),
                new Cinnamon(),
            }),
        };
    }

    public void Write(string filePath, string text)
    {
        //
    }
}

public interface IRecipesUserInteraction
{
    void PrintExistingRecipes(IEnumerable<Recipe> recipes);
    void PromptToCreateRecipe();
    List<string> ReadIngredientsFromUser();
    void ShowMessage(string message);
    void Exit();
}

public class IngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new SpeltFlour(),
        new Butter(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder(),
        new Sugar(),
        new Chocolate()
    };
}

public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IngredientsRegister ingredientsRegister)
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
