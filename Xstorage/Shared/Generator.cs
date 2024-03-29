namespace Xstorage.Shared
{
    public static class Generator
    {
        public static string RandomASCIIString(int length)
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                str += Consts.ASCIIChars[random.Next(0, Consts.ASCIIChars.Length)];
            }

            return str;
        }


        public static string RandomString(int length, string charSet = Consts.ASCIILetters)
        {
            string str = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                str += charSet[random.Next(0, charSet.Length)];
            }

            return str;
        }
    }
}
