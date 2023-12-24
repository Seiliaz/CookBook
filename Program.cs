using CookBook.App;
using CookBook.Recipes;
using CookBook.FileAccess;
using CookBook.DataAccess;
using CookBook.UserInteraction;
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
