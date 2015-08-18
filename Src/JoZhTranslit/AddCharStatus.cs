namespace JoZhTranslit
{
    /// <summary>
    /// Status of adding a new char to transliterator.
    /// </summary>
    public enum AddCharStatus
    {
        /// <summary>
        /// No graphemes were found for given char.
        /// </summary>
        NoGraphemeFound,

        /// <summary>
        /// New grapheme was found by given char value.
        /// </summary>
        NewGrapheme,

        /// <summary>
        /// New grapheme was found when search was performed on added char combined with prevoiusly 
        /// saved chars. Previously returned grapheme should be replaced with found one. 
        /// </summary>
        SubstitutePreviousGrapheme
    }
}