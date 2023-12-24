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
        if (ingredients.Count() > 0)
        {
            var recipe = new Recipe(ingredients);
            allRecipes.Add(recipe);
            // _recipesRepository.Write("filePath", allRecipes);
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
    IEnumerable<Ingredient> ReadIngredientsFromUser();
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
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder(),
        new Chocolate()
    };

    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
            if (ingredient.Id == id)
                return ingredient;
        return null;
    }
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
