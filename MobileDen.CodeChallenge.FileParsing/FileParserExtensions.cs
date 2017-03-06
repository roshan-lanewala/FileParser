using System.Collections.Generic;
using System.Linq;

namespace MobileDen.CodeChallenge.FileParsing
{
    public static class FileParserExtensions
    {
        /// <summary>
        /// Discard any empty lines
        /// </summary>
        public static IEnumerable<string> FilterEmptyLines(this IEnumerable<string> theLines)
        {
            return theLines.Where(w => !string.IsNullOrEmpty(w.Trim()));
        }

        /// <summary>
        /// Replaces the TabSpaces in enumeration with new string
        /// </summary>
        public static IEnumerable<string> ReplaceTabSpaces(this IEnumerable<string> theLines, string newString)
        {
            return theLines.Select(s => s.Replace("\t", newString));
        }
    }
}
