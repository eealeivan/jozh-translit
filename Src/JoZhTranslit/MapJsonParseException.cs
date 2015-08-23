using System;

namespace JoZhTranslit
{
    /// <summary>
    /// Indicates that provided graphemes map is not in correct format.
    /// Example: { "б": ["b"], "ё": ["jo", "yo"] }
    /// </summary>
    public class MapJsonParseException : Exception
    {
        internal MapJsonParseException()
        {
        }

        internal MapJsonParseException(string message)
            : base(message)
        {
        }

        internal MapJsonParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}