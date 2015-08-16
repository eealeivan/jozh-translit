namespace JoZhTranslit
{
    public struct AddCharResult
    {
        internal AddCharResult(AddCharStatus status, string grapheme) : this()
        {
            Status = status;
            Grapheme = grapheme;
        }

        public AddCharStatus Status { get; private set; }
        public string Grapheme { get; private set; } 
    }
}