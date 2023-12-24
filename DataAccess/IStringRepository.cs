using CookBook.Recipes;

namespace CookBook.DataAccess
{
    public interface IRecipesRepository
    {
        List<Recipe> Read(string filePath);
        void Write(string filePath, List<Recipe> recipes);
    }
}
