using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xstorage.Extentions
{
    public static class LongExtentions
    {
        public static string BytesLengthToString(this long bytesLength)
        {
            string[] units = { "bytes", "KB", "MB", "GB", "TB" };
            int unitIndex = 0;

            double length = bytesLength;
            while (length >= 1024 && unitIndex < units.Length - 1)
            {
                length /= 1024;
                unitIndex++;
            }

            if (unitIndex == 0)
            {
                return $"{length:0} {units[unitIndex]}";
            }
            else
            {
                return $"{length:0.00} {units[unitIndex]}";
            }
        }
    }
}
