namespace CookBook.Recipes.Ingredients
{

    public class IngredientsRegister : IIngredientsRegister
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

}
