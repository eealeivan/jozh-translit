namespace JoZhTranslit
{
    /// <summary>
    /// Result of adding a new char to transliterator.
    /// </summary>
    public struct AddCharResult
    {
        internal AddCharResult(AddCharStatus status, string grapheme) : this()
        {
            Status = status;
            Grapheme = grapheme;
        }

        /// <summary>
        /// Status that indicates how transliteration off added char went.
        /// </summary>
        public AddCharStatus Status { get; private set; }

        /// <summary>
        /// Found grapheme. Will be null if <see cref="Status"/> is equal  to 
        /// <see cref="AddCharStatus.NoGraphemeFound"/>.
        /// </summary>
        public string Grapheme { get; private set; } 
    }
}