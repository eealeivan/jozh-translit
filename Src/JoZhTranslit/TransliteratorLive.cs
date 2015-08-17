namespace JoZhTranslit
{
    public sealed class TransliteratorLive
    {
        private readonly TranslitProcessor _translitProcessor;

        public TransliteratorLive(string mapJson)
        {
            var translitData = new TranslitData(mapJson);
            _translitProcessor = new TranslitProcessor(translitData);
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