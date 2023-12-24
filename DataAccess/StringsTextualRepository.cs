namespace CookBook.DataAccess
{
    public class StringsTextualRepository : StringsRepository
    {
        private static readonly string Seperator = Environment.NewLine;
        protected override List<string> TextToStrings(string fileContents)
            => fileContents.Split(Seperator).ToList();
        protected override string StringsToText(List<string> strings)
            => string.Join(Seperator, strings);
    }
}
