using System;

namespace JoZhTranslit
{
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