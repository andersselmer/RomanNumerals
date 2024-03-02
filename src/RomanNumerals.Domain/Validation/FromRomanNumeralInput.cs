namespace RomanNumerals.Domain.Validation
{
    internal static class FromRomanNumeralInput
    {
        internal static bool StringInputIsValidDumb(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            HashSet<char> validChars = ['I', 'V', 'X', 'L', 'C', 'D', 'M'];

            return s.All(c => validChars.Contains(c));
        }

        internal static bool CanRepeat(char c)
        {
            HashSet<char> validChars = ['I', 'V', 'X', 'L', 'C', 'D', 'M'];

            bool validChar = validChars.Contains(c);

            bool nonRepeatCharsContain = new HashSet<char> { 'V', 'L', 'D' }.Contains(c);

            return validChar && !nonRepeatCharsContain;
        }

        internal static bool CharInputIsValidDumb(IReadOnlyCollection<char> chars, char c)
        {
            return chars.Contains(c);
        }        
    }
}
