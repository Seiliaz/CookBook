using System.Text.Json;
using CookBook.Recipes;
using CookBook.Recipes.Ingredients;

const FileFormat fileFormat = FileFormat.Json;
IStringsRepository stringsRepository =
    fileFormat == FileFormat.Json
        ? new StringsJsonRepository()
        : new StringsTextualRepository();

const string FileName = "recipes";
var fileMetadata = new FileMetadata(FileName, fileFormat);
var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookieRecipesApp(
    new RecipesRepository(
        stringsRepository,
        ingredientsRegister),
    new RecipesConsoleUserInteraction(
        ingredientsRegister)
);
cookiesRecipesApp.Run(fileMetadata.ToPath());

public class FileMetadata
{
    public string Name { get; }
    public FileFormat Format { get; }
    public FileMetadata(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }
    public string ToPath() => $"{Name}.{Format.AsFileExtension()}";
}

public static class FileFormatExtension
{
    public static string AsFileExtension(this FileFormat fileFormat)
    {
        return fileFormat == FileFormat.Json ? "json" : "txt";
    }
}

public enum FileFormat
{
    Json, Txt
}

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

public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientRegister _ingredientsRegister;
    private const string Seperator = ",";

    public RecipesRepository(
        IStringsRepository stringsRepository,
        IIngredientRegister ingredientsRegister
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

public interface IRecipesUserInteraction
{
    void PrintExistingRecipes(IEnumerable<Recipe> recipes);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
    void ShowMessage(string message);
    void Exit();
}

public interface IIngredientRegister
{
    IEnumerable<Ingredient> All { get; }
    Ingredient GetById(int id);
}

public class IngredientsRegister : IIngredientRegister
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
    private readonly IIngredientRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IIngredientRegister ingredientsRegister)
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

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}


public class StringsJsonRepository : StringsRepository
{
    protected override List<string> TextToStrings(string fileContents)
        => JsonSerializer.Deserialize<List<string>>(fileContents) ?? new List<string>();
    protected override string StringsToText(List<string> strings)
        => JsonSerializer.Serialize(strings);
}
