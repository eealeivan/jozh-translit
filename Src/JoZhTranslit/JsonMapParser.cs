using System;
using System.Collections.Generic;
using System.Text;

namespace JoZhTranslit
{
    internal static class JsonMapParser
    {
        /// <summary>
        /// Parses 'mapJson' parameter and returns a dictionary which is also reversed (values of
        /// original dictionary are used as keys, and keys as values). 
        /// </summary>
        /// <remarks>
        /// We do not use any JSON serializer becuase it's problematic to deserialize dictionaries
        /// in PORTABLE NET40, as there is no JavaScriptSerializer and DataContractJsonSerializerSettings.
        /// </remarks>
        public static ParseResult Parse(string mapJson)
        {
            var state = ParseState.LookingForOpenCurlyBracket;
            IDictionary<int, string> map = new Dictionary<int, string>();
            var keyGrapheme = new StringBuilder();
            var valueGraphemes = new List<StringBuilder>();
            int maxGraphemeLength = 0;

            foreach (char c in mapJson)
            {
                bool nonContextWhiteSpace =
                    Char.IsWhiteSpace(c) &&
                    state != ParseState.ReadingKeyGrapheme &&
                    state != ParseState.ReadingValueGrapheme;
                if (nonContextWhiteSpace)
                {
                    continue;
                }

                switch (state)
                {
                    case ParseState.LookingForOpenCurlyBracket:
                        if (c == '{')
                        {
                            state = ParseState.LookingForKeyGrapheme;
                        }
                        else { throw new MapJsonParseException("Map should begin with '{'."); }
                        break;

                    case ParseState.LookingForKeyGrapheme:
                        if (c == '"')
                        {
                            state = ParseState.ReadingKeyGrapheme;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.ReadingKeyGrapheme:
                        if (c == '"')
                        {
                            state = ParseState.LookingForColon;
                        }
                        else
                        {
                            keyGrapheme.Append(c);
                        }
                        break;

                    case ParseState.LookingForColon:
                        if (c == ':')
                        {
                            state = ParseState.LookingForOpenSquareBracket;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.LookingForOpenSquareBracket:
                        if (c == '[')
                        {
                            state = ParseState.LookingForValueGrapheme;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.LookingForValueGrapheme:
                        if (c == '"')
                        {
                            valueGraphemes.Add(new StringBuilder());
                            state = ParseState.ReadingValueGrapheme;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.ReadingValueGrapheme:
                        if (c == '"')
                        {
                            state = ParseState.LookingForCommaOrClosedSquareBracket;
                        }
                        else
                        {
                            valueGraphemes[valueGraphemes.Count - 1].Append(c);
                        }
                        break;

                    case ParseState.LookingForCommaOrClosedSquareBracket:
                        if (c == ',')
                        {
                            state = ParseState.LookingForValueGrapheme;
                        }
                        else if (c == ']')
                        {
                            foreach (var valueGrapheme in valueGraphemes)
                            {
                                map[new CharArray(valueGrapheme).GetMutableHashCode()] =
                                    keyGrapheme.ToString();
                                if (valueGrapheme.Length > maxGraphemeLength)
                                {
                                    maxGraphemeLength = valueGrapheme.Length;
                                }
                            }
                            keyGrapheme.Length = 0;
                            valueGraphemes.Clear();
                            state = ParseState.LookingForCommaOrClosedCurlyBracket;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.LookingForCommaOrClosedCurlyBracket:
                        if (c == ',')
                        {
                            state = ParseState.LookingForKeyGrapheme;
                        }
                        else if (c == '}')
                        {
                            state = ParseState.Ended;
                        }
                        else { throw new MapJsonParseException(string.Format("Invalid character '{0}'", c)); }
                        break;

                    case ParseState.Ended:
                        throw new MapJsonParseException(string.Format("Invalid character '{0}'", c));

                    default:
                        throw new ArgumentOutOfRangeException(string.Format("Invalid character '{0}'", c));
                }
            }

            return new ParseResult(map, maxGraphemeLength);
        }

        private enum ParseState
        {
            LookingForOpenCurlyBracket,
            LookingForKeyGrapheme,
            ReadingKeyGrapheme,
            LookingForColon,
            LookingForOpenSquareBracket,
            LookingForValueGrapheme,
            ReadingValueGrapheme,
            LookingForCommaOrClosedSquareBracket,
            LookingForCommaOrClosedCurlyBracket,
            Ended
        }

        public class ParseResult
        {
            public IDictionary<int, string> GraphemesMap { get; set; }
            public int MaxGraphemeLength { get; set; }

            public ParseResult(IDictionary<int, string> graphemesMap, int maxGraphemeLength)
            {
                GraphemesMap = graphemesMap;
                MaxGraphemeLength = maxGraphemeLength;
            }
        }
    }
}