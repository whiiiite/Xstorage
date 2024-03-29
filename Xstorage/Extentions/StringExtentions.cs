namespace Xstorage.Extentions
{
    public static class StringExtentions
    {
        public static string ReplaceFirstIfStartWith(this string original, string startWith)
        {
            if(original.StartsWith(startWith))
            {
                return original.Substring(startWith.Length);
            }
            return original;
        }
    }
}
