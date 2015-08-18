namespace JoZhTranslit
{
    /// <summary>
    /// Transliterator that can be used to perform "live" transliteration. This is done by calling
    /// <see cref="AddChar"/> and <see cref="Reset"/> methods.
    /// </summary>
    public sealed class TransliteratorLive
    {
        private readonly TranslitProcessor _translitProcessor;

        /// <summary>
        /// Creates an instance of <see cref="TransliteratorLive"/> class.
        /// </summary>
        /// <param name="mapJson">
        /// Graphemes map in JSON format. Example: { "б": ["b"], "ё": ["jo", "yo"] }
        /// </param>
        public TransliteratorLive(string mapJson)
        {
            var translitData = new TranslitData(mapJson);
            _translitProcessor = new TranslitProcessor(translitData);
        }

        /// <summary>
        /// Adds a char to inner cache and tries to transliterate it. Depending on previously 
        /// added chars the returned value may differ. 
        /// </summary>
        /// <param name="c">Char to add.</param>
        public AddCharResult AddChar(char c)
        {
            return _translitProcessor.AddChar(c);
        }

        /// <summary>
        /// Resets inner cache of chars.
        /// </summary>
        public void Reset()
        {
            _translitProcessor.Reset();
        }
    }
}