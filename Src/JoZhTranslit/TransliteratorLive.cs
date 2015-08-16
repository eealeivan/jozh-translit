namespace JoZhTranslit
{
    public sealed class TransliteratorLive
    {
        private readonly TranslitData _translitData;
        private readonly TranslitProcessor _translitProcessor;

        public TransliteratorLive(string mapJson)
        {
            _translitData = new TranslitData(mapJson);
            _translitProcessor = new TranslitProcessor(
                _translitData.FindGrapheme, _translitData.MaxGraphemeLength);
        }

        public AddCharResult AddChar(char c)
        {
            return _translitProcessor.AddChar(c);
        }

        public void Reset()
        {
            _translitProcessor.Reset();
        }
    }
}