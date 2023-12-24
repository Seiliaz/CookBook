using System.Text.Json;

namespace CookBook.DataAccess
{
    public class StringsJsonRepository : StringsRepository
    {
        protected override List<string> TextToStrings(string fileContents)
            => JsonSerializer.Deserialize<List<string>>(fileContents) ?? new List<string>();
        protected override string StringsToText(List<string> strings)
            => JsonSerializer.Serialize(strings);
    }
}
